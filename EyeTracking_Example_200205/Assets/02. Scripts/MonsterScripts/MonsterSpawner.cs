using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] monsterPrefabs = new GameObject[4];
    private bool isSpawned = false;
    private bool canSpawn = true; // 스폰해도 되는 상황인가, 목표지점 근접 등의 상황
    private float spawnTimer;

    public bool IsSpawned
    {
        get
        {
            return isSpawned;
        }

        set
        {
            isSpawned = value;
        }
    }

    public bool CanSpawn
    {
        get
        {
            return canSpawn;
        }

        set
        {
            canSpawn = value;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        spawnTimer = Random.Range(1, 6) / 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsSpawned && CanSpawn)
        {
            if(spawnTimer < 0f)
            {
                SpawnMonster();
                spawnTimer = Random.Range(1, 6) / 2.0f;
            }
            else
            {
                spawnTimer -= Time.deltaTime;
            }
        }
    }

    private void SpawnMonster()
    {
        int point = Random.Range(0, 4);
        int monster = Random.Range(0, 4);

        GameObject spawnedMonster = Instantiate(monsterPrefabs[monster], transform.GetChild(point).position, transform.GetChild(point).rotation);
        spawnedMonster.GetComponent<Monsters>().MonsterCode = monster * 2;
        IsSpawned = true;
    }
}
