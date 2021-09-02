using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public enum Type {person,knight,knight_blue, knight_red, knight_purple, knight_brown, shield,dog};
    public enum State {Fine,Catched,Dead,Attack};
    public State state;
    public Type type;
    public float maxHP;
    public float curHP;
    public float spd;
    bool isEarn;
    public bool isCatched;
    public bool inTrap2;
    public bool isSlow;
    bool isDamage;
    float damageDelay;
    public Image hpBack_img;
    public Image hp_img;
    private Transform EnemyTransform;
    public CapsuleCollider2D hisCol1;
    public CapsuleCollider2D hisCol2;
    public Animator anim;
    public Rigidbody2D rgbd;
    Player player_data;

    public GameManager gameManager;

    float exp_;

    float atk;
    float setCoin;
    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        float anim_run_spd = Random.Range(0.9f,1.1f);
        anim.SetFloat("anim_RunSpeed", anim_run_spd);
        state = State.Fine;
        isEarn = false;
        isCatched = false;
        damageDelay = 0f;
        EnemyTransform = GetComponent<Transform>();
        GameObject player_obj = GameObject.Find("Player");
        player_data = player_obj.GetComponent<Player>();
        switch (type)
        {
            case Type.person:
                curHP = 150f;
                maxHP = 150f;
                atk = 20f;
                setCoin = 10f;
                exp_ = 4f;
                break;
            case Type.knight:
                curHP = 300f;
                maxHP = 300f;
                atk = 40f;
                setCoin = 20f;
                exp_ = 3f;
                break;
            case Type.knight_blue:
                curHP = 500f;
                maxHP = 500f;
                atk = 60f;
                setCoin = 30f;
                exp_ = 2f;
                break;
            case Type.knight_red:
                curHP = 800f;
                maxHP = 800f;
                atk = 80f;
                setCoin = 40f;
                exp_ = 1f;
                break;
            case Type.knight_purple:
                curHP = 1100f;
                maxHP = 1100f;
                atk = 110f;
                setCoin = 60f;
                exp_ = 1f;
                break;
            case Type.knight_brown:
                curHP = 1500f;
                maxHP = 1500f;
                atk = 200f;
                setCoin = 100f;
                exp_ = 1f;
                break;
            case Type.shield:
                curHP = 300f;
                maxHP = 300f;
                atk = 0f;
                setCoin = 50f;
                exp_ = 14f;
                break;
            case Type.dog:
                curHP = 50f;
                maxHP = 50f;
                atk = 0f;
                setCoin = 5f;
                exp_ = 3f;
                StartCoroutine(jumpTheDog());
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player_data.isPlaying == true)
        {
            if (curHP < 0)
            {
                StartCoroutine(OnDamage(0));
                curHP = 0f;

            }
            if (player_data.poison_level >= 1)
            {
                curHP -= maxHP * 0.0008f* player_data.poison_level;
            }

            damageDelay += Time.deltaTime;
            hp_img.fillAmount = curHP / maxHP;
            if (curHP <= 0)
            {
                state = State.Dead;
            }
            if (EnemyTransform.position.x <= -3.25) // 화면 밖을 벗어나는 좌표
            {
                if (EnemyTransform.position.y >= -3f && EnemyTransform.position.y <= -1f)
                {
                    EnemyTransform.position = new Vector3(3.39f, 1.07f, 1f); // 2층 시작지점 좌표
                }
                else if (EnemyTransform.position.y >= 0.9f && EnemyTransform.position.y <= 1.5f)
                    EnemyTransform.position = new Vector3(3.39f, 4.06f, 1f); // 3층 시작지점 좌표

            }
            if (inTrap2)
            {
                curHP -= 1f;
            }

            if (state == State.Fine)
            {
                anim.SetBool("isFine", true);
                hisCol1.enabled = true;
                // hisCol2.enabled = true;
                rgbd.isKinematic = false;
                Move();
            }
            if (state == State.Catched)
            {
                anim.SetBool("isCatched", true);
                rgbd.isKinematic = true;
                //  hisCol2.enabled = false;
            }
            if (state == State.Dead)
            {
                hpBack_img.enabled = false;
                rgbd.isKinematic = true;
                hisCol1.enabled = false;
                //   hisCol2.enabled = false;
                anim.SetTrigger("doDie");
                Destroy(this.gameObject, 2);
            }
            if (state == State.Attack)
            {
                anim.SetBool("isFine", false);
            }
        }
    }
    private void Move()
    {
        // Fever 타임이 아닐 경우, 정상 속도로 이동.
        if (gameManager.isFever==false) 
        {
            EnemyTransform.position -= new Vector3(spd, 0f, 0f);
        }// Fever 타임시 빨리 이동.
        else
        {
            EnemyTransform.position -= new Vector3(spd*1.3f, 0f, 0f);
        }
       
    }
    public IEnumerator freeCatched(float catchTime)
    {
        yield return new WaitForSeconds(catchTime);
        state = State.Fine;
        anim.SetBool("isCatched", false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trap1"))
        {
            state = State.Catched;
            Trap1 trap1 = collision.gameObject.GetComponent<Trap1>();
            StartCoroutine(OnDamage(trap1.TrapAtk));
            StartCoroutine(freeCatched(trap1.catchTime));
        }
        
        if(collision.CompareTag("Trap Block")|| collision.CompareTag("Trap Snowman") || collision.CompareTag("Trap RoketShooter"))
        {
            state = State.Attack;
            GameObject targetedObj = collision.gameObject;
            StartCoroutine(Attack(targetedObj));
        }
        if (collision.CompareTag("Trap2"))
        {
            inTrap2 = true;
        }

        if (collision.CompareTag("DeadZone"))
        {
            player_data.coin -= 100 * GameManager.instance.stage;
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Trap Block")|| collision.CompareTag("Trap Snowman") || collision.CompareTag("Trap RoketShooter"))
        {
            state = State.Fine;
        }
        if (collision.CompareTag("Trap2"))
        {
            inTrap2 = false;
        }
    }
    public IEnumerator OnDamage(float dmg)
    {
        // 플레이어 능력 중, 기폭플레인이 발동될 확률
        int death_bomb_percent=Random.Range(0,100);
        isDamage = true;
        yield return new WaitForSeconds(0.00f);
        if (isDamage)
        {
            // dmg 만큼의 피해를 입는다.
            curHP -= dmg;
            if (isEarn==false&&curHP <= 0)
            {
                // 체력이 0이되면, 플레이어의 골드와 경험치를 올려준다.
                player_data.coin += setCoin;
                player_data.exp += exp_;
                
                isEarn = true;

                // 플레이어 능력 중, 기폭플레인 처리 내용
                if (player_data.deathBomb_level >= 1)
                {
                    // 플레이어가 해당 능력을 업그레이드 할 수록, 확률이 높아진다.
                    if (10 <= death_bomb_percent && death_bomb_percent <= (20 + 10 * player_data.deathBomb_level))
                    {
                        // 적이 쓰러진 위치에 폭발 이펙트를 하나 생성해서 주변에 피해를 입힌다.
                        GameObject deathBomb=Instantiate(player_data.touchEffect, transform.position, transform.rotation);
                        deathBomb.transform.localScale *= 2.5f;
                        deathBomb.GetComponent<TouchEffect_1>().atk = 200 * player_data.deathBomb_level;
                        deathBomb.GetComponent<TouchEffect_1>().isCharge = true;
                    }
                }
            }


        }
        isDamage = false;

    }
    IEnumerator Attack(GameObject targetObj)
    {
        while (true)
        {
            // 적의 상태가 공격 상태가 아니라면, 공격을 중지함
            if (state == State.Fine|| state == State.Dead)
            {
                break;
            }
            anim.SetTrigger("doAttack");
            yield return new WaitForSeconds(0.6f);
            if (state == State.Fine || state == State.Dead)
            {
                break;
            }

            // 공격 대상의 체력을 감소시켜 데미지를 줌.
            if (targetObj.CompareTag("Trap Block"))
            {
                TrapBlock blockObj = targetObj.GetComponent<TrapBlock>();
                blockObj.curHP -= atk;
                blockObj.StartCoroutine(blockObj.damaged_effect());
            }
            if (targetObj.CompareTag("Trap Snowman"))
            {
                targetObj.GetComponent<TrapSnowman>().curHP -= atk;
            }
            if (targetObj.CompareTag("Trap RoketShooter"))
            {
                targetObj.GetComponent<TrapRoketShooter>().curHP -= atk;
            }
            yield return new WaitForSeconds(1.4f);

        }
    }
    public IEnumerator SpeedDown_slow()
    {
        if (isSlow == false)
        {
            spd -= 0.001f;
            isSlow = true;
            yield return new WaitForSeconds(2f);
            spd += 0.001f;
            isSlow = false;
        }
    }

    public IEnumerator jumpTheDog()
    {
        while (true)
        {
            anim.SetTrigger("doJump");
            yield return new WaitForSeconds(1.5f);
        }
    }
}
