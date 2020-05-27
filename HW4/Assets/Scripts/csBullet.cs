using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csBullet : MonoBehaviour
{
    // 폭발음을 저장하기 위한 오디오클립 변수이다.
    public AudioClip sndExp;
    // 바닥 게임 오브젝트를 저장할 GameObject 변수이다.
    private GameObject plane;
    // 폭발 파티클을 저장할 변수이다.
    public Transform explode;

    // Start is called before the first frame update
    void Start()
    {
        // 바닥을 찾아 할당한다.
        plane = GameObject.Find("Plane");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider coll)
    {
        // 폭발 파티클을 총알의 충돌위치에 스폰하여 재생한다.
        Instantiate(explode, transform.position, Quaternion.identity);
        // 총알의 충돌위치에 폭발음을 재생한다.
        AudioSource.PlayClipAtPoint(sndExp, transform.position);

        if(coll.tag == "Gound")
        {

        }
        // OnTrigger된 오브젝트가 땅이 아니라면 실행되는 조건문이다.
        else
        {
            // 총알에 맞은 오브젝트를 제거한다.
            Destroy(coll.gameObject);
        }
        // 총알을 제거한다.
        Destroy(gameObject);
    }
}
