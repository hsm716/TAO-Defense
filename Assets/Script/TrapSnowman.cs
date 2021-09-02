using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrapSnowman : MonoBehaviour
{
    public GameObject snowball;
    public BoxCollider2D itsCol;
    public float curHP;
    public float maxHP;
    public bool isDeath;
    public Image hp_img;
    public Image hpBack_img;
    public Animator anim;
    public GameObject game_ui;
    Player player_data;
    GameObject player;
    int level = 0;
    void Start()
    {
        player = GameObject.Find("Player");
        player_data = player.GetComponent<Player>();
        isDeath = false;
        curHP = 50f;
        maxHP = 50f;
    }

    // Update is called once per frame
    void Update()
    {
        hp_img.fillAmount = curHP / maxHP;
        if (curHP <= 0)
        {
            hpBack_img.enabled = false;
            itsCol.enabled = false;
            isDeath = true;
            anim.SetTrigger("doDie");
            Destroy(this.gameObject,0.6f);
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
        if (curHP >= maxHP)
        {
            curHP = maxHP;
        }
        if (player_data.repair_level >= 1)
        {
            curHP += 0.3f * player_data.repair_level;
        }
    }
    public IEnumerator Attack()
    {

        while (true)
        {
            if (isDeath)
            {
                break;
            }
            anim.SetTrigger("doAttack");
            yield return new WaitForSeconds(0.4f);
            GameObject sb= Instantiate(snowball, transform.position + new Vector3(0f, 0f, -2f), transform.rotation);
            switch (level)
            {
                case 0:
                    sb.GetComponent<snowBall>().atk = 200;
                    break;
                case 1:
                    sb.GetComponent<snowBall>().atk = 300;
                    break;
                case 2:
                    sb.GetComponent<snowBall>().atk = 400;
                    break;
                case 3:
                    sb.GetComponent<snowBall>().atk = 500;
                    break;
                case 4:
                    sb.GetComponent<snowBall>().atk = 600;
                    break;
            }

            yield return new WaitForSeconds(2.6f);
        }
    }

    public void Active_ui()
    {
        if (game_ui.activeSelf == false)
        {
            game_ui.SetActive(true);
        }
        else
        {
            game_ui.SetActive(false);
        }
    }
    public void Upgrade()
    {
        if (player_data.coin >= 700)
        {
            level += 1;
            maxHP *= 1.25f;
            curHP = maxHP;
            player_data.coin -= 700;
            game_ui.SetActive(false);
        }
    }
    public void Destroy_()
    {
        Destroy(this.gameObject);
    }
    public void Fix()
    {
        if (player_data.coin >= 50)
        {
            curHP = maxHP;
            player_data.coin -= 50;
            game_ui.SetActive(false);
        }
    }


}


