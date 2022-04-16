using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;

    [SerializeField]
    private GameObject Enemy_dog;
    [SerializeField]
    private GameObject Enemy_shield;
    [SerializeField]
    private GameObject Enemy_knight5;
    [SerializeField]
    private GameObject Enemy_knight4;
    [SerializeField]
    private GameObject Enemy_knight3;
    [SerializeField]
    private GameObject Enemy_knight2;
    [SerializeField]
    private GameObject Enemy_knight;
    [SerializeField]
    private GameObject Enemy_person;


    public Transform[] SpawnPoints;

    public float spawnTime;
    public int stage = 1;
   
    int shield_enemy_count = 0;
    int dog_enemy_count = 0;
    void Awake()
    {
        instance = this;
        spawnTime = 2f;
        StartCoroutine(Spawn_Enemy());
    }
    private void Update()
    {
        stage = GameManager.instance.stage;
    }
    IEnumerator Spawn_Enemy()
    {
        while (true)
        {
            // 스테이지에 맞게 SpawnTime을 결정하고, 각각의 Enemy들을 생성한다.
            if (stage == 1)
            {
                Instantiate(Enemy_person, SpawnPoints[0].position, SpawnPoints[0].rotation);
            }
            else if (stage == 2)
            {
                spawnTime = 1.75f;
                Instantiate(Enemy_person, SpawnPoints[0].position, SpawnPoints[0].rotation);
            }
            else if (stage == 3)
            {
                spawnTime = 1.75f;
                Instantiate(Enemy_knight, SpawnPoints[0].position, SpawnPoints[0].rotation);
            }
            else if (stage == 4)
            {
                spawnTime = 1.5f;
                Instantiate(Enemy_knight, SpawnPoints[0].position, SpawnPoints[0].rotation);
            }
            else if (stage == 5)
            {
                spawnTime = 1.5f;
                Instantiate(Enemy_knight2, SpawnPoints[0].position, SpawnPoints[0].rotation);
            }
            else if (stage == 6)
            {
                spawnTime = 1.5f;
                Instantiate(Enemy_knight2, SpawnPoints[0].position, SpawnPoints[0].rotation);
            }
            else if (stage == 7)
            {
                spawnTime = 1f;
                Instantiate(Enemy_knight3, SpawnPoints[0].position, SpawnPoints[0].rotation);
            }
            else if (stage == 8)
            {
                spawnTime = 1f;
                Instantiate(Enemy_knight3, SpawnPoints[0].position, SpawnPoints[0].rotation);
            }
            else if (stage == 9)
            {
                spawnTime = 1f;
                Instantiate(Enemy_knight4, SpawnPoints[0].position, SpawnPoints[0].rotation);
            }
            else if (stage == 10)
            {
                spawnTime = 0.8f;
                Instantiate(Enemy_knight4, SpawnPoints[0].position, SpawnPoints[0].rotation);
            }
            else if (stage >= 10)
            {
                spawnTime = 0.8f;
                Instantiate(Enemy_knight5, SpawnPoints[0].position, SpawnPoints[0].rotation);
            }
            else if (stage >= 11)
            {
                spawnTime = 0.6f;
                Instantiate(Enemy_knight5, SpawnPoints[0].position, SpawnPoints[0].rotation);
            }

            shield_enemy_count += 1;
            dog_enemy_count += 1;
            // 특수 적들은 count를 통해 간격을 두어 생성한다.
            if (shield_enemy_count == 5)
            {
                Instantiate(Enemy_shield, SpawnPoints[0].position, SpawnPoints[0].rotation);
                shield_enemy_count = 0;
            }
            if (dog_enemy_count == 3)
            {
                Instantiate(Enemy_dog, SpawnPoints[0].position, SpawnPoints[0].rotation);
                dog_enemy_count = 0;
            }
            yield return new WaitForSeconds(spawnTime);
        }
    }

}
