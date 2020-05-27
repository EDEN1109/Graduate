using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csTank : MonoBehaviour
{
    //총 쏘는 소리 클립
    public AudioClip sndExp;
    //총알 프리팹 받아옴
    public Transform bullet;
    //총알 프리팹의 위치 지정함
    Transform spPoint;
    //총알 발사 시 파워
    int power = 1800;

    //탱크 포탑 객체 받아옴
    public GameObject turret;
    //탱크 주포? 객체 받아옴
    public GameObject gun;
    //속도 초기화 크기가 클수록 이동 속도가 빨라짐
    int moveSpeed = 10;
    // 각도 움직이는 속도
    int rotSpeed = 120;

    void Start()
    {
        //spPoint라는 오브젝트를 찾아 transform을 가져옴
        spPoint = GameObject.Find("spPoint").transform;
    }

    void Update()
    {
        float amtMove = moveSpeed * Time.smoothDeltaTime;
        float amtRot = rotSpeed * Time.smoothDeltaTime;

        //물체 이동+회전
        //유니티 프로젝트 세팅에서 input 중 Vertical(축)을 가져옴
        float keyMove = Input.GetAxis("Vertical");
        //유니티 프로젝트 세팅에서 input 중 Horizontal(축)을 가져옴
        float KeyRot = Input.GetAxis("Horizontal");
        //유니티 프로젝트 세팅에서 input에서 Turret(축)을 추가한 뒤 가져오기
        float keyTurret = Input.GetAxis("Turret");
        //유니티 프로젝트 세팅에서 input 중 Mouse ScrollWheel(축)을 가져옴
        float keyGun = Input.GetAxis("Mouse ScrollWheel");

        //esc 누르면 프로그램 종료시키는 코드
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        //탱크 이동+회전
        transform.Translate(Vector3.forward * amtMove * keyMove);
        transform.Rotate(Vector3.up * amtRot * KeyRot);

        //탱크 주포와 포탑 이동+회전
        turret.transform.Rotate(Vector3.up * amtRot * keyTurret);
        gun.transform.Rotate(Vector3.right * keyGun * 4);

        //마우스 왼쪽 버튼을 누르면 총알이 나가는 함수 호출
        if (Input.GetButtonDown("Fire1"))
        {
            FireBullet();
        }
    }

    void FireBullet()
    {
        //총알 프리팹 생성 후 위치값과 회전값 가져옴
        Transform obj = Instantiate(bullet, spPoint.position, spPoint.rotation)
        as Transform;
        //총알 프리팹의 위치에서 power값만큼 앞으로 발사
        obj.GetComponent<Rigidbody>().AddForce(spPoint.forward * power);
        //총 쏘는 소리가 그 자리에서 재생됨
        AudioSource.PlayClipAtPoint(sndExp, transform.position);
    }
}
