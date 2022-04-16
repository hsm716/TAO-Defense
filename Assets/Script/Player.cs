using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float atk;
    public float coin;
    public float digAmount;
    public bool isPlaying = true;
    public bool isGrab;
    
    public GameObject pannel_btn1;
    public GameObject pannel_btn2;

    public GameObject levelUp_pannel;
    public GameObject paused_pannel;

    public GameObject worldCanvas;


    [SerializeField]
    private Text CoinTxt;

    [SerializeField]
    private GameObject[] thunder_zones;


    public int thunder_level = 0;
    public int poison_level = 0;
    public int deathBomb_level = 0;
    public int repair_level = 0;

    public enum touchType { normal,water,bubble};
    public touchType tType;

    public GameObject touchEffect;
    [SerializeField]
    private GameObject touchEffect2;
    [SerializeField]
    private GameObject touchEffect3;

    [SerializeField]
    private GameObject thunderEffect;

    [SerializeField]
    private GameObject trap1_prefab;

    [SerializeField]
    private GameObject chargeEffect;

    private GameObject charge_obj;
    bool isCharge = false;
    public float chargeAmount;
    Vector2 touchPos;

    public int level;
    public float exp;

    public GameManager gameManager;


    private void Awake()
    {
        level = 1;
        exp = 0f;

        tType = touchType.normal;
        chargeAmount = 0f;


        Screen.SetResolution(1080,1920, true);
    }
    private void Start()
    {
        coin = 0;
        StartCoroutine(earnCoin());
        StartCoroutine(thunderAttack());
    }
    void Update()
    {
        CoinTxt.text = ""+(int)coin;
        if(exp >= 100f)  // 경험치가 모두 쌓이면, 레벨업
        {
            exp = 0f;
            level += 1;
            GameManager.instance.StartCoroutine("Shuffle_");
            // 게임 매니저에서 Shuffle_ 코루틴 함수 호출을 통해
            // 능력 선택 UI를 화면에 띄어줌.
        }
        if (Input.GetMouseButtonDown(0))
        {
            // 터치를 통해서 공격을 구현
            chargeAmount = 0f;
            isCharge = true;
            // 첫 터치에, charge 정보를 초기화

            touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // 터치한 위치 정보를 메인 카메라가 비추는 화면의 world좌표로 바꿔서 저장.
            RaycastHit2D hitInformation = Physics2D.Raycast(touchPos, Camera.main.transform.forward);
            // 카메라 앞 방향으로 touchPos위치에 Raycast를 쏴줌.
            if (hitInformation.collider.CompareTag("Enemy")|| hitInformation.collider.name=="UI_ZONE")
            {
                // 검출된 collider의 태그가 Enemy or UI_ZONE 일 경우에만 터치 적용.
                //Destroy(charge_obj);
                GameObject touchedObject = hitInformation.transform.gameObject;
                // 검출된 collider의 오브젝트 형식으로 저장.

                Enemy touchedEnemy = touchedObject.GetComponent<Enemy>();
                // 오브젝트를 Enemy 형식으로 다시 캐시해서 사용.

                // 각 공격 타입별로 생성 오브젝트를 달리함.
                if (tType == touchType.normal)
                {
                    Instantiate(touchEffect, touchPos, transform.rotation);
                    touchedEnemy.StartCoroutine(touchedEnemy.OnDamage(atk));
                }
                else if(tType == touchType.water)
                {
                    Instantiate(touchEffect2, touchPos, transform.rotation);
                    touchedEnemy.StartCoroutine(touchedEnemy.SpeedDown_slow());
                    touchedEnemy.StartCoroutine(touchedEnemy.OnDamage(atk*0.9f));
                    // 물타입 공격에는 슬로우를 적용.
                }
                else if(tType == touchType.bubble)
                {
                    Instantiate(touchEffect3, touchPos, transform.rotation);
                    StartCoroutine(doubleBubble(touchedObject));
                    touchedEnemy.StartCoroutine(touchedEnemy.OnDamage(atk*0.7f));
                }
            }


            // 설치한 방해물을 클릭하면, (업그레이드, 수리, 파괴) 항목을 
            // 선택할 수 있는 UI를 활성화 시켜 줌.
            if (hitInformation.collider.CompareTag("Trap Block"))
            {   
                hitInformation.collider.GetComponent<TrapBlock>().Active_ui();
            }
            else if(hitInformation.collider.CompareTag("Trap Snowman"))
            {
                hitInformation.collider.GetComponent<TrapSnowman>().Active_ui();
            }
            else if (hitInformation.collider.CompareTag("Trap RoketShooter"))
            {
                hitInformation.collider.GetComponent<TrapRoketShooter>().Active_ui();
            }

        }
        
        if (Input.GetMouseButton(0))
        { 
            // Charge 오브젝트가 생성되있지 않은 경우
            if (isCharge && charge_obj==false)
            {
                // Charge 량을 증가시키며, 지정한 량이 모일 때, ChargeEffect를 생성한다.
                chargeAmount += 1f;
                if (chargeAmount >= 20f)
                {
                    charge_obj = Instantiate(chargeEffect, touchPos, transform.rotation);
                    charge_obj.transform.parent = worldCanvas.transform;
                    charge_obj.transform.localScale = new Vector3(2f, 2f, 1f);
                }
                
            }
            else if(isCharge=true && charge_obj)
            {
                //Charge 오브젝트가 생성되어 있는 경우
                //마찬가지로 Charge량을 증가시키며, 최대 100까지 쌓인다.
                chargeAmount += 1f;
                if (chargeAmount > 100)
                {
                    chargeAmount = 100;
                }
                charge_obj.GetComponent<Image>().fillAmount = chargeAmount / 100f;
                touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                // Charge 오브젝트가 현재 터치 중인 곳에 위치하도록 함.
                charge_obj.transform.position = new Vector3(touchPos.x, touchPos.y, -1f);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            // isCharge 상태를 False로 만들고, ChargeEffect를 제거.
            isCharge = false;
            Destroy(charge_obj);

            // Charge 공격이 발동되는 최소량
            if (chargeAmount >= 50)
            {
                GameObject chargeAttack=null;
                
                // 각 타입별 Charge 공격 방식 채택.
                if (tType == touchType.normal)
                {
                    chargeAttack = Instantiate(touchEffect, touchPos, transform.rotation);
                    chargeAttack.transform.localScale = new Vector3(chargeAmount / 200, chargeAmount / 200, chargeAmount / 200);
                    chargeAttack.GetComponent<TouchEffect_1>().isCharge = true;
                }
                else if (tType == touchType.water)
                {
                    chargeAttack = Instantiate(touchEffect2, touchPos, transform.rotation);
                    chargeAttack.transform.localScale = new Vector3(chargeAmount / 200, chargeAmount / 200, chargeAmount / 200);
                    chargeAttack.GetComponent<TouchEffect_1>().isCharge = true;
                }
                else if (tType == touchType.bubble)
                {
                    int bubbleCount = (int)chargeAmount / 10;
                    StartCoroutine(ChargeBubble( chargeAttack, bubbleCount));
                }
            

            }
        }
    }
    public ParticleSystem effect_GetCoin;
    public ParticleSystem effect_LostCoin;
    public void GetCoin(int amount)
    {
        //coin += amount;
        effect_GetCoin.Play();
        StartCoroutine(ChangeDegree(amount, 1, 0));
    }
    public void LostCoin(int amount)
    {
        //coin -= amount;
        effect_LostCoin.Play();
        StartCoroutine(ChangeDegree(amount, -1,0));
    }

    public void GetExp(int amount)
    {
        //coin += amount;
        StartCoroutine(ChangeDegree(amount, 1, 1));
    }

    IEnumerator ChangeDegree(int amount,int plus_minus,int type)
    {
     

        while (amount >= 0)
        {
            if (type == 0)
                coin += plus_minus;
            else if (type == 1)
                exp += plus_minus;
            amount--;
            yield return new WaitForSeconds(0.005f);
        }

    }

    IEnumerator ChargeBubble(GameObject chargeAttack,int bubbleCount)
    {
        for (int i = 0; i < bubbleCount; i++)
        {
            Vector2 randomVec = new Vector2(Random.Range(-2f, 2f) + touchPos.x, Random.Range(-2f, 2f) + touchPos.y);
            chargeAttack = Instantiate(touchEffect3, touchPos + randomVec, transform.rotation);
            chargeAttack.GetComponent<TouchEffect_1>().isCharge = true;

            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator earnCoin()
    {
        while (true)
        {
            coin += digAmount;
            yield return new WaitForSeconds(1f);
        }
    }
    IEnumerator thunderAttack()
    {
        while (true)
        {
            if (thunder_level >= 1)
            {
                Instantiate(thunderEffect, thunder_zones[0].transform.position, transform.rotation);
            }
            if (thunder_level >= 3)
            {
                Instantiate(thunderEffect, thunder_zones[1].transform.position, transform.rotation);
            }
            if (thunder_level >= 5)
            {
                Instantiate(thunderEffect, thunder_zones[2].transform.position, transform.rotation);
            }
            yield return new WaitForSeconds(2f);
        }
    }
    IEnumerator doubleBubble(GameObject target_obj)
    {
        yield return new WaitForSeconds(0.2f);
        Instantiate(touchEffect3, new Vector2(touchPos.x+0.06f, touchPos.y + 0.06f), transform.rotation);
        target_obj.GetComponent<Enemy>().StartCoroutine(target_obj.GetComponent<Enemy>().OnDamage(atk));

    }
    public void ShowBtn_1()
    {
        pannel_btn1.SetActive(true);
        pannel_btn2.SetActive(false);
    }
    public void ShowBtn_2()
    {
        pannel_btn2.SetActive(true);
        pannel_btn1.SetActive(false);
    }
    public void PowerUp()
    {
        if (coin >= 150)
        {
            LostCoin(150);
            //coin -= 150;
            atk += 5;
        }
    }
    public void ClickSetting()
    {
        paused_pannel.SetActive(true);
        Time.timeScale = 0f;
        isPlaying = false;
    }
    public void Resume()
    {
        paused_pannel.SetActive(false);
        Time.timeScale = 1f;
        isPlaying = true;
    }
    public void Restart()
    {
        SceneManager.LoadScene("Sample Scene");
    }
    public void Quit_()
    {
        Application.Quit();
    }
    public Image UI_StyleBack_img;
    public Sprite UI_Normal_sp;
    public Sprite UI_Water_sp;
    public Sprite UI_Bubble_sp;
    public void selectBubble()
    {
        tType = touchType.bubble;
        UI_StyleBack_img.sprite = UI_Bubble_sp;
        
    }
    public void selectWater()
    {
        tType = touchType.water;
        UI_StyleBack_img.sprite = UI_Water_sp;
    }
    public void selectNormal()
    {
        tType = touchType.normal;
        UI_StyleBack_img.sprite = UI_Normal_sp;
    }

}
