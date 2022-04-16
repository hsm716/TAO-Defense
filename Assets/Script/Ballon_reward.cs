using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballon_reward : MonoBehaviour
{
    public bool isPang = false;
    bool dropItem=false;
    private Animator anim;
    public CircleCollider2D cc2;

    public GameObject xp;
    public GameObject coin;
    GameObject reward_1;

    GameObject player;
    Player player_data;
    int random_item_num;
    GameObject canvasObj;
    void Start()
    {
        canvasObj = GameObject.Find("Canvas-Overay");
        transform.parent = canvasObj.transform;
        transform.localScale = new Vector3(2.5f, 2.5f, 1f);

        float Random_posX = Random.Range(-500f, 500f);
        float Random_posY = Random.Range(-700f, -800f);
        transform.localPosition = new Vector3(Random_posX, Random_posY, 1f);



        player = GameObject.Find("Player");
        player_data = player.GetComponent<Player>();
        random_item_num = Random.Range(0, 2);
        anim = GetComponent<Animator>();
        Destroy(this.gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPang == true)
        {
            cc2.enabled = false;
            anim.SetTrigger("doPang");
            if (dropItem == false)
            {
                dropItem = true;
                if (random_item_num == 0)
                {
                    reward_1 = Instantiate(xp, transform.localPosition, Quaternion.identity);
                    reward_1.transform.parent = this.transform;
                    reward_1.transform.position = this.transform.position;
                    reward_1.transform.localScale = new Vector3(0.3f,0.3f,1f);
                    player_data.GetExp(10);
                    //player_data.exp += 10;
                    Destroy(reward_1, 1.2f);
                }
                else
                {
                    reward_1 = Instantiate(coin, transform.localPosition, Quaternion.identity);
                    reward_1.transform.parent = this.transform;
                    reward_1.transform.position = this.transform.position;
                    reward_1.transform.localScale = new Vector3(0.3f, 0.3f, 1f);
                    player_data.GetCoin(100);
                    //player_data.coin += 100;
                    Destroy(reward_1, 1.2f);
                }
            }
        }
        else
        {
            transform.position += new Vector3(0f, 2.5f, 0f)*Time.deltaTime;
        }
        reward_1.transform.position += new Vector3(0f, 0.7f, 0f)*Time.deltaTime;
    }

    public void Pang()
    {
        isPang = true;
    }
}
