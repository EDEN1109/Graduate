using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Sample : MonoBehaviour
{
    private Transform cameraCenter;
    private float time = 2.0f; 
    private bool isDie = false;
    private bool isCenter = false;
    // Start is called before the first frame update
    void Start()
    {
        cameraCenter = GameObject.FindGameObjectWithTag("Finish").GetComponent<Transform>();
    }
    private void Update()
    {
        if(!isCenter)
        {
            goToCenter();
        }
        else if (isDie)
        {
            destroyMonster();
        }
        else
        {
            if(Input.GetKey(KeyCode.Space))
            {
                time -= Time.deltaTime;
                if(transform.GetChild(0).GetComponent<ParticleController>().IsPlay == false)
                {
                    transform.GetChild(0).GetComponent<ParticleController>().StartPlay();
                }
            }
            else
            {
                if(transform.GetChild(0).GetComponent<ParticleController>().IsPlay == true)
                {
                    transform.GetChild(0).GetComponent<ParticleController>().StopPlay();
                }
            }
            if(time<0f)
            {
                isDie = true;
            }
        }
    }
    
    private void destroyMonster()
    {
        GameObject.FindGameObjectWithTag("Respawn").GetComponent<MonsterSpawner>().IsSpawned = false;
        Destroy(this.gameObject);
    }

    private void goToCenter()
    {
        transform.position = Vector3.MoveTowards(transform.position, cameraCenter.position, 0.1f);
        if (transform.position == cameraCenter.position)
            isCenter = true;
    }
}
