using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrapBlock : MonoBehaviour
{
    public float maxHP;
    public float curHP;
    public Image hpBack_img;
    public Image hp_img;
    public BoxCollider2D itsCol;
    public GameObject game_ui;
    public SpriteRenderer spr;
    public Sprite level2;
    public Sprite level3;
    int level = 0;
    Player player_data;
    GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
        player_data = player.GetComponent<Player>();

        curHP = 300;
        maxHP = 300;
    }

    // Update is called once per frame
    void Update()
    {
        hp_img.fillAmount = curHP / maxHP;
        if (curHP <= 0)
        {
            Destroy(this.gameObject);
        }
        if(curHP >= maxHP)
        {
            curHP = maxHP;
        }
        switch (level)
        {
            case 1:
                spr.sprite = level2;
                break;
            case 2:
                spr.sprite = level3;
                break;
        }
        if (player_data.repair_level >= 1)
        {
            curHP += 0.3f * player_data.repair_level;
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
        if (player_data.coin >= 500)
        {
            level += 1;
            maxHP *= 2f;
            curHP = maxHP;
            player_data.coin -= 500;
            game_ui.SetActive(false);
        }
    }
    public void Destroy_()
    {
        Destroy(this.gameObject);
    }
    public void Fix() {
        if (player_data.coin >= 30)
        {
            curHP = maxHP;
            player_data.coin -= 30;
            game_ui.SetActive(false);
        }
    }
    public IEnumerator damaged_effect()
    {
        transform.localScale = new Vector3(0.8f,0.8f,1.0f);
        yield return new WaitForSeconds(0.1f);
        transform.localScale = new Vector3(0.77f, 0.74f, 1.0f);
    }
}
