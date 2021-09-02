using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowBall : MonoBehaviour
{
    bool isActive = false;
    public CircleCollider2D itsCol;
    public Animator anim;
    public float atk;
    // Update is called once per frame
    void Update()
    {
        if (isActive == false)
        {
            transform.position += new Vector3(0.03f, 0f, 0f);
        }
        if(transform.position.x >= 5f)
        {
            Destroy(this.gameObject);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().StartCoroutine(collision.gameObject.GetComponent<Enemy>().OnDamage(atk));
            isActive = true;
            itsCol.enabled = false;
            transform.localScale = new Vector3(1f, 1f, 1f);
            anim.SetTrigger("doActive");
            Destroy(this.gameObject,0.5f);
        }
    }
}
