using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    private Transform[] points; //생성될 spawn point가 담긴 배열
    private GameObject[] enemys;
    public GameObject enemyPrefab;
    private const int minTime = 10;
    private const int maxTime = 30;
    private float attackTime = 0.5f;
    private int idx = -1;
    private int maxEnemy = 1;
    private string enemyTag;
    // Start is called before the first frame update
    void Start()
    {
        points = GetComponentsInChildren<Transform>();
        enemyTag = enemyPrefab.tag;
    }

    // Update is called once per frame
    void Update()
    {
        enemys = GameObject.FindGameObjectsWithTag(enemyTag);

        if(points.Length > 0)
        {
            Invoke("SpawnEnemy", attackTime);
            attackTime += (float)Random.Range(minTime, maxTime) / 10;
        }
    }

    //적 스폰 함수
    void SpawnEnemy()
    {
        int enemyCount = enemys.Length;
        if(enemyCount < maxEnemy)
        {
            bool isOk = true;  //중복되어 스폰되었을때(??)
            idx = Random.Range(1, points.Length);
                
            foreach (GameObject e in enemys)
            {
                if (enemys.Length < 1)
                {
                    break;
                }
                if (e.transform.position == points[idx].position)
                {
                    isOk = false;
                    break;
                }
            }

            if (isOk)
            {
                GameObject ch = Instantiate(enemyPrefab, points[idx].position, points[idx].rotation);
                ch.transform.parent = points[idx];
            }
        }
    }

    
}
