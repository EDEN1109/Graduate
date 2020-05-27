using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csTank: MonoBehaviour
{
    // 발사음을 저장하기위한 오디오 클립 변수이다.
    public AudioClip sndExp;
    // 총알 Prefab을 저장할 변수이다.
    public Transform bullet;
    // 총알의 발사 위치를 정하는 변수이다.
    private Transform spPoint;
    // 총알의 발사 속도를 정의하는 변수이다.
    private int power = 1800;

    // 각각 터렛과 총구 부분의 게임 오브젝트를 적용시키기 위한 변수이다.
    public GameObject turret;
    public GameObject gun;
    // 움직이는 속도와 회전 속도를 정의하는 변수이다.
    private int moveSpeed = 10;
    private int rotSpeed = 120;

    //private int speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        // 발사 포인트를 찾아 할당한다
        spPoint = GameObject.Find("spPoint").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Time.deltaTime과 Time.smoothDeltaTime은 한 프레임과 다음 프레임 사이의 시간이라는 점이 같지만
        // Time.smoothDeltaTime은 그 값이 좀 더 균등하게 나온다. 따라서 오브젝트가 순간이동 하는 것 처럼 워프현상이 일어나는 것을 방지할 수 있다.
        // 따라서 눈에 보이는 시각적인 부분에서는 Time.smoothDeltaTime의 사용이 권장된다.
        float amtMove = moveSpeed * Time.smoothDeltaTime;
        float amtRot = rotSpeed * Time.smoothDeltaTime;

        // 각각 Project Settings에 설정된 Input에 따라 값을 받아온다.
        // 움직임은 W, S, 회전은 A, D키를 통해 받아올 수 있다.
        float keyMove = Input.GetAxis("Vertical");
        float keyRot = Input.GetAxis("Horizontal");

        // 회전을 위한 값을 Project Settings에 설정된 Input에 따라 받아온다.
        // 터렛의 회전은 Q, E, 총구의 회전은 마우스 휠을 통해 받아올 수 있다.
        float keyTurret = Input.GetAxis("Turret");
        float keyGun = Input.GetAxis("Mouse ScrollWheel");

        // 입력받은 값에 따라 움직이거나 회전한다.
        // Cube를 움직이거나 회전하는 내용이다. Positive 값일때 전진 혹은 y축 +방향으로 회전한다.
        transform.Translate(Vector3.forward * amtMove * keyMove);
        transform.Rotate(Vector3.up * amtRot * keyRot);

        // 입력받은 값에 따라 회전한다.
        // Positive 값일때 Sphere은 y축 +방향으로 회전하며, Cylinder은 x축 +방향으로 회전한다.
        // gun의 회전에서 곱해준 4는 회전 속도를 의미한다.
        turret.transform.Rotate(Vector3.up * amtRot * keyTurret);
        gun.transform.Rotate(Vector3.right * keyGun * 4);

        // Project Settings에 Fire1으로 설정된 Input이 발생하면 작동하는 if문이다.
        if (Input.GetButtonDown("Fire1"))
        {
            // 함수를 실행한다.
            FireBullet();
        }

        // ESC키를 누르면 작동하는 if문이다.
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            // 게임을 종료한다.
            Application.Quit();
        }
    }

    private void FireBullet()
    {
        // 총알을 spPoint로 지정된 위치 및 각도로 스폰한뒤 위치 정보를 obj에 대입한다.
        Transform obj = Instantiate(bullet, spPoint.position, spPoint.rotation) as Transform;
        // 총알을 지정한 방향과 power로 날아가도록 AddForce해준다.
        obj.GetComponent<Rigidbody>().AddForce(spPoint.forward * power);
        // 발사음을 포구위치에서 재생한다.
        AudioSource.PlayClipAtPoint(sndExp, transform.position);
    }
}
