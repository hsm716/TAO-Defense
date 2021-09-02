using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap1 : MonoBehaviour
{
    public BoxCollider2D itsCol;
    public Animator anim;
    public float TrapAtk;
    public float catchTime;
    bool isActive=false;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")&&isActive==false)
        {
            isActive = true;
            itsCol.enabled = false;
            anim.SetTrigger("doCatch");
            Destroy(this.gameObject,2f);
        }
    }
}
