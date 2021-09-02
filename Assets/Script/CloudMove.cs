using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    [SerializeField]
    private GameObject cloud1;
    [SerializeField]
    private GameObject cloud2;

    GameObject player;
    Player player_data;

    private void Awake()
    {
        player = GameObject.Find("Player");
        player_data = player.GetComponent<Player>();
                 
    }
    // Update is called once per frame
    void Update()
    {
        if (player_data.isPlaying)
        {
            cloud1.transform.position -= new Vector3(0.004f, 0f, 0f);
            cloud2.transform.position -= new Vector3(0.004f, 0f, 0f);
            if (cloud1.transform.position.x <= -7.4f)
                cloud1.transform.position = new Vector3(cloud2.transform.position.x + 7.7f, cloud2.transform.position.y,2f);
        }
        
    }
}
