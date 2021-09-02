using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap2 : MonoBehaviour
{
    public BoxCollider2D itsCol;
    public Animator anim;
    public float TrapAtk;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            anim.SetTrigger("doActive");
            Destroy(this.gameObject, 10f);
        }
    }
}
