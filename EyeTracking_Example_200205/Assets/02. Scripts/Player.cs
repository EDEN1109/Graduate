using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float moveSpeed = 4f;
    private bool isGoal = false;
    private Monsters monster;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGoal && GameObject.FindWithTag("Monster") == null)
        {
            Work();
        }
        if(Input.GetMouseButtonDown(0))
        {
            monster = GameObject.FindWithTag("Monster").GetComponent<Monsters>();
            monster.IsEyeon = true;
        }
        if(Input.GetMouseButtonUp(0))
        {
            monster.IsEyeon = false;
        }
    }

    private void Work()
    {
        transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Goal")
        {
            GameObject.FindWithTag("Respawn").GetComponent<MonsterSpawner>().CanSpawn = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Goal")
        {
            isGoal = true;
            transform.GetChild(3).gameObject.SetActive(true);
            ScoreManager.instance.SetTotalScore();
            ScoreManager.instance.SaveScore();
        }
    }
}
