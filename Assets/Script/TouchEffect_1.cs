using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEffect_1 : MonoBehaviour
{
    GameObject player;
    Player player_data;
    public CircleCollider2D itsCol;
    public bool isCharge;
    public enum Type { normal,water,bubble,roket};
    public Type t;
    public float atk;
    void Awake()
    {
        player = GameObject.Find("Player");
        player_data = player.GetComponent<Player>();
        atk = player_data.atk;
        Destroy(this.gameObject, 0.8f);
    }
    private void Update()
    {
        if (isCharge == false)
        {
            itsCol.enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && isCharge == true)
        {
            itsCol.enabled = true;
            if (t == Type.normal)
            {
                collision.gameObject.GetComponent<Enemy>().StartCoroutine(collision.gameObject.GetComponent<Enemy>().OnDamage(atk * 2));
            }
            itsCol.enabled = false;
            if (t == Type.water)
            {
                collision.gameObject.GetComponent<Enemy>().StartCoroutine(collision.gameObject.GetComponent<Enemy>().OnDamage(atk * 1.5f));
                collision.gameObject.GetComponent<Enemy>().StartCoroutine(collision.gameObject.GetComponent<Enemy>().SpeedDown_slow());
            }
            if (t == Type.bubble)
            {
                collision.gameObject.GetComponent<Enemy>().StartCoroutine(collision.gameObject.GetComponent<Enemy>().OnDamage(atk * 0.7f));
            }
            if (t == Type.roket)
            {
                collision.gameObject.GetComponent<Enemy>().StartCoroutine(collision.gameObject.GetComponent<Enemy>().OnDamage(atk));
            }
        }
    }
}
