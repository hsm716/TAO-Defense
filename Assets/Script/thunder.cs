using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thunder : MonoBehaviour
{
    public BoxCollider2D itsCol;
    public float atk;

    GameObject player;
    Player player_data;
    private void Awake()
    {
        player = GameObject.Find("Player");
        player_data = player.GetComponent<Player>();
        Destroy(this.gameObject, 1f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (player_data.thunder_level >= 1)
            {
                atk = 125* player_data.thunder_level;
            }
            collision.gameObject.GetComponent<Enemy>().StartCoroutine(collision.gameObject.GetComponent<Enemy>().OnDamage(atk));
            itsCol.enabled = false;
        }
    }
}
