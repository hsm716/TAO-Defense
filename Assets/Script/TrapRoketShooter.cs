using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TrapRoketShooter : MonoBehaviour
{
    [SerializeField]
    private GameObject roket_bulletPrfab;
    public float maxHP;
    public float curHP;
    public Image hpBack_img;
    public Image hp_img;
    public BoxCollider2D itsCol;
    public GameObject game_ui;
    public SpriteRenderer spr;
    int level = 0;

    public Animator anim;
    Player player_data;
    GameObject player;
    void Awake()
    {
        StartCoroutine(Shot_Roket());
        player = GameObject.Find("Player");
        player_data = player.GetComponent<Player>();

        curHP = 200;
        maxHP = 200;
    }

    // Update is called once per frame
    void Update()
    {
        hp_img.fillAmount = curHP / maxHP;
        if (curHP <= 0)
        {
            Destroy(this.gameObject);
        }
        if (curHP >= maxHP)
        {
            curHP = maxHP;
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
        if (player_data.coin >= 800)
        {
            level += 1;
            maxHP *= 2f;
            curHP = maxHP;
            player_data.coin -= 800;
            game_ui.SetActive(false);
        }
        if (player_data.repair_level >= 1)
        {
            curHP += 0.3f * player_data.repair_level;
        }
    }
    public void Destroy_()
    {
        Destroy(this.gameObject);
    }
    public void Fix()
    {
        if (player_data.coin >= 30)
        {
            curHP = maxHP;
            player_data.coin -= 30;
            game_ui.SetActive(false);
        }
    }
    IEnumerator Shot_Roket()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.5f);
            anim.SetTrigger("doShot");
            yield return new WaitForSeconds(0.65f);
            Instantiate(roket_bulletPrfab, transform.position+new Vector3(0.1f,0f,0f), transform.rotation);
        }
    }
}
