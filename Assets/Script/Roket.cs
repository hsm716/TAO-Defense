using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roket : MonoBehaviour
{
    [SerializeField]
    private GameObject Boom;
    
    void Start()
    {
        Destroy(this.gameObject, 5f);
    }

    void Update()
    {
        transform.position  += new Vector3(0.03f,0f,0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            GameObject boom_prefab = Instantiate(Boom, transform.position, transform.rotation);
            boom_prefab.GetComponent<TouchEffect_1>().atk = 180f;
            boom_prefab.GetComponent<TouchEffect_1>().isCharge = true;
            Destroy(this.gameObject,0.01f);
        }
    }
}
