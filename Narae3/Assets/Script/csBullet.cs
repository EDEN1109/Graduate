using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csBullet : MonoBehaviour
{
    //폭발하는 소리 클립
    public AudioClip sndExp;
    //바닥 객체 가져옴
    GameObject plane;
    //총알과 닿았을 때 일어나는 폭발 파티클
    public Transform explode;

    void Start()
    {
        //Plane이라는 이름의 객체를 가져옴
        plane = GameObject.Find("Plane");
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider coll)
    {
        //폭발 이펙트 위치값과 회전값 가져옴
        Instantiate(explode, transform.position, Quaternion.identity);
        //벽과 총알이 닿으면 폭발하는 소리를 부딪힌 곳에서 재생시킴
        AudioSource.PlayClipAtPoint(sndExp, transform.position);

        //총알과 닿은 객체의 태그가 바닥이면
        if (coll.tag == "바닥")
        {
        }
        //아니라면
        else
        {
            //총알과 부딪힌 객체 파괴
            Destroy(coll.gameObject);
        }
        //총알 프리팹 파괴
        Destroy(gameObject);

    }
}
