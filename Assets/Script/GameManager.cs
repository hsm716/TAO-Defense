using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    public int game_time = 0;
    public float spawnTime;

    public Image stage_img;
    public Image stage_num_img;
    public Image stage_num_img2;
    public Sprite[] stage_sp = new Sprite[10];

    float alpha_value = 0f;
    public int stage = 1;
    public Text stage_txt;

    bool sShuffle = false;



    public Sprite[] select_icons = new Sprite[5];
    private string[] select_names = { "골드 수급 UP", "기폭 플레인", "썬더", "역병", "자동 수리" };
    private string[] select_discriptions = { "골드 수급량을\n증가 시킵니다.",
                                             "적 처치 시\n[10,20,50]%\n 확률로 폭발이\n일어난다.",
                                             "랜덤 위치에\n[100,200,500]\n데미지를 가하는\n낙뢰 발생.",
                                             "모든 적에게\n죽을 때까지\n지속적인 피해를\n[1,3,5]% 입히는\n역병이 일어남.",
                                             "필드 위\n플레이어의\n모든 Trap들을\n[1,2,3]수치만큼\n수리한다."};

    public Image[] select_img = new Image[3];
    public Text[] select_name = new Text[3];
    public Text[] select_discription = new Text[3];
    public GameObject levelUp_pannel;

    GameObject player;
    Player player_data;

    public GameObject stage_progress;
    public GameObject stage_Notice;

    public Image stage_progress_degree;
    public Text stage_progress_degree_txt;
    public Text player_level;

    public Text cur_Stage;

    public GameObject ballon_b;

    public GameObject fever_anim;

    public Text total_score;

    public bool isFever = false;


    public GameObject DeathPanel;

    int shield_enemy_count = 0;
    int dog_enemy_count = 0;
    void Start()
    {
        instance = this;
        player = GameObject.Find("Player");
        player_data = player.GetComponent<Player>();
        stage_Notice.GetComponent<Animator>().SetTrigger("doShow");
        StartCoroutine(Progress_Stage());
    }

    // Update is called once per frame
    void Update()
    {
        stage_txt.text = "" + stage;
        cur_Stage.text = "STAGE " + stage;

        total_score.text =""+ player_data.coin;
        stage_progress_degree.fillAmount = player_data.exp * 0.01f;
        stage_progress_degree_txt.text = player_data.exp + " / 100";
        player_level.text = player_data.level + "";

        if(player_data.coin <= -100 * player_data.level)
        {
            player_data.isPlaying = false;
            Time.timeScale = 0f;
            DeathPanel.SetActive(true);
        }

    }
    public void Stage_reward()
    {
        for (int i = 0; i < 6; i++)
        {
            Instantiate(ballon_b, ballon_b.transform.position, ballon_b.transform.rotation);
        }
        
    }

    IEnumerator Progress_Stage()
    {
        while (true)
        {
            game_time += 1;
            stage_progress.transform.localPosition += new Vector3(11.4f, 0, 0);
            if (game_time == 30)
            {
                isFever = true;
            }
            else if (game_time == 60)
            {
                isFever = false;

                game_time = 0;
                stage += 1;
                stage_progress.transform.localPosition = new Vector3(-314f, stage_progress.transform.localPosition.y, stage_progress.transform.localPosition.z);
                string stg = "" + stage;
                if (stage >= 10)
                {
                    stage_num_img2.gameObject.SetActive(true);
                    stage_num_img2.sprite = stage_sp[(int.Parse("" + stg[1]))];
                }
                stage_num_img.sprite = stage_sp[(int.Parse("" + stg[0]))];
                Stage_reward();
                
                stage_Notice.GetComponent<Animator>().SetTrigger("doShow");
            }
            fever_anim.SetActive(isFever);
            yield return new WaitForSeconds(1f);
        }
    }
    public void Select_(int index)
    {
        switch (select_name[index].text)
        {
            case "골드 수급 UP":
                player_data.digAmount += 5;
                break;
            case "기폭 플레인":
                player_data.deathBomb_level += 1;
                break;
            case "썬더":
                player_data.thunder_level += 1;
                break;
            case "역병":
                player_data.poison_level += 1;
                break;
            case "자동 수리":
                player_data.repair_level += 1;
                break;
        }
        Time.timeScale = 1f;
        player_data.isPlaying = true;
        levelUp_pannel.SetActive(false);
    }


    public IEnumerator Shuffle_()
    {
        sShuffle = true;
        yield return new WaitForSeconds(2f);
        if (sShuffle == true)
        {
            List<int> selected_state = new List<int>{ 0,1,2,3,4};
            int size = selected_state.Count-1;
            int count = 0;
            while (count < 3)
            {
                int r_idx = Random.Range(0, size);
                int rand_idx = selected_state[r_idx];
                //Debug.Log(rand_list[r_idx]);
                select_img[count].sprite = select_icons[rand_idx];
                select_name[count].text = select_names[rand_idx];
                select_discription[count].text = select_discriptions[rand_idx];

                selected_state[rand_idx] = selected_state[size];
                size -= 1;
                count++;
                /*if (selected_state[rand_idx] == false)
                {
                    selected_state[rand_idx] = true;
                    select_img[count].sprite = select_icons[rand_idx];
                    select_name[count].text = select_names[rand_idx];
                    select_discription[count].text = select_discriptions[rand_idx];
                    count++;
                }
                else
                {
                    continue;
                }*/
            }
            levelUp_pannel.SetActive(true);
            player_data.isPlaying = false;
            sShuffle = false;
            Time.timeScale = 0f;
        }
    }
}
