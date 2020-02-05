using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monsters : MonoBehaviour
{
    [SerializeField]
    private GameObject HPBar;
    private Transform cameraCenter;
    private Transform gonePoint;
    private int monsterCode;
    private int centerPass = 0;
    private float centerMoveSpeed = 5.0f;
    private float moveRange = 5.0f; // 움직일 범위
    private float eyeOnTime = 0f; // 현재까지 봐진 시간
    private float recoveredTime = 0f; // 현재 체력
    private float requiredTime = 8.0f; // 공격을 지속해야하는 시간, 즉 체력
    private bool isCenter = false;
    private bool isRecovered = false;
    private bool isEyeon = false;
    protected Vector3 centerPosition;
    protected const char MOD0 = '0';
    protected const char MOD1 = '1';
    protected char nowMod;

    public bool IsEyeon
    {
        get
        {
            return isEyeon;
        }

        set
        {
            isEyeon = value;
        }
    }

    protected float RequiredTime
    {
        get
        {
            return requiredTime;
        }

        set
        {
            requiredTime = value;
        }
    }

    protected float MoveRange
    {
        get
        {
            return moveRange;
        }

        set
        {
            moveRange = value;
        }
    }

    public int MonsterCode
    {
        get
        {
            return monsterCode;
        }

        set
        {
            monsterCode = value;
        }
    }

    // Start is called before the first frame update
    virtual protected void Awake()
    {
        cameraCenter = GameObject.FindWithTag("Finish").GetComponent<Transform>();
        gonePoint = GameObject.FindWithTag("Gone").GetComponent<Transform>();
        if (Random.Range(0, 2) == 0) { nowMod = MOD0; }
        else { nowMod = MOD1; }
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        if (!isCenter) { GoToCenter(); }
        else if(centerPass >= 4) 
        {
            StopParticle();
            RunAway();
        }
        else { Moving(); }

        if (isRecovered) { DestroyMonster(); }
        else
        {
            if (IsEyeon)
            {
                // 파티클 재생
                if(isCenter && centerPass < 4)
                {
                    PlayParticle();
                    RecoverMonster();
                }
            }
            else
            {
                // 파티클 정지
                StopParticle();
                LostHealthMonster();
            }
        }
    }

    // 파티클 재생
    private void PlayParticle()
    {
        if (transform.GetChild(0).GetComponent<ParticleController>().IsPlay == false)
        {
            transform.GetChild(0).GetComponent<ParticleController>().StartPlay();
        }
    }

    // 파티클 재생 정지
    private void StopParticle()
    {
        if (transform.GetChild(0).GetComponent<ParticleController>().IsPlay == true)
        {
            transform.GetChild(0).GetComponent<ParticleController>().StopPlay();
        }
    }

    // 몬스터가 체력을 잃음
    private void LostHealthMonster()
    {
        if (recoveredTime > 0f)
        {
            HPBar.SetActive(true);
            recoveredTime -= Time.deltaTime;
            HPBar.transform.GetChild(1).localScale = new Vector3(1.0f * (recoveredTime / RequiredTime), 1.0f, 1.0f);
        }
        if (recoveredTime <= 0f)
        {
            recoveredTime = 0f;
            HPBar.transform.GetChild(1).localScale = new Vector3(0f, 1.0f, 1.0f);
            HPBar.SetActive(false);
        }
    }

    // 몬스터를 회복시켜줌
    private void RecoverMonster()
    {        
        if (recoveredTime < RequiredTime)
        {
            HPBar.SetActive(true);
            eyeOnTime += Time.deltaTime;
            recoveredTime += Time.deltaTime;
            HPBar.transform.GetChild(1).localScale = new Vector3(1.0f * (recoveredTime / RequiredTime), 1.0f, 1.0f);
        }
        if (recoveredTime >= RequiredTime)
        {
            isRecovered = true;
            recoveredTime = RequiredTime;
            HPBar.transform.GetChild(1).localScale = new Vector3(1.0f, 1.0f, 1.0f);
            HPBar.SetActive(false);
        }
    }

    private void DestroyMonster()
    {
        // 몬스터 스폰 가능 상태로 변경
        GameObject.FindWithTag("Respawn").GetComponent<MonsterSpawner>().IsSpawned = false;
        // 모드 별로 점수를 따로 계산함
        ScoreManager.instance.GetScore(monsterCode + (int)char.GetNumericValue(nowMod), eyeOnTime);
        // 죽는 애니메이션 재생 후 Destroy
        Destroy(this.gameObject);
    }

    // 몬스터가 화면 중앙으로 이동
    private void GoToCenter()
    {
        transform.position = Vector3.MoveTowards(transform.position, cameraCenter.position, Time.deltaTime * centerMoveSpeed);
        if (transform.position == cameraCenter.position)
        {
            isCenter = true;
            centerPosition = gameObject.transform.position;
        }
    }

    // 몬스터가 도망감
    private void RunAway()
    {
        transform.position = Vector3.MoveTowards(transform.position, gonePoint.position, Time.deltaTime * centerMoveSpeed);
        if (transform.position == gonePoint.position)
        {
            GameObject.FindWithTag("Respawn").GetComponent<MonsterSpawner>().IsSpawned = false;
            Destroy(this.gameObject);
        }
    }

    // 몬스터별 움직임 구현
    virtual protected void Moving()
    {

    }

    virtual protected void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Finish")
        {
            centerPass++;
        }
    }
}
