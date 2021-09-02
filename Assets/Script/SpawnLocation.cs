using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLocation : MonoBehaviour
{
    public GameObject isObj;
    public SpriteRenderer sr;
    // Update is called once per frame
    void Update()
    {
        if (isObj ==true )
        {
            sr.color = new Color(255f, 255f, 255f, 0f);
        }
        else
        {
            sr.color = new Color(255f, 255f, 255f, 1f);
        }
    }
}
