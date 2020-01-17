using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//체력바가 서서히 차오르게 하기!
public class MovingEnemy : MonoBehaviour
{
    public float movePower = 0.2f; //움직이는 속도
    public Transform player; //추적 타겟

    //몬스터의 렌더러
    public Renderer eRenderer;
    public Renderer tRenderer;
    public Renderer bRenderer;
    public Renderer lRenderer;

    //얼마나 연속으로 공격해야 죽는지
    private float dieTime = 3.0f;

    //유니티 자체 추적 사용
    private NavMeshAgent nav;

    //공격 당하는 중인지, 죽었는지
    private bool isAttack = false;
    private bool isDie = false;

    private void Awake()
    {
        nav= GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        //HPBar 처음에는 보이지 않게 함
        transform.GetChild(3).gameObject.SetActive(true);
    }
    private void Update()
    {
        ///몬스터가 플레이어를 추적하도록 목적지를 플레이어의 위치로 변경
        nav.SetDestination(player.position);
        if(isDie)
        {
            destroyMonster();
        }
        
    }

    public bool getIsDie()
    {
        return isDie;
    }

    public void setIsAttack(bool isAttack)
    {
        this.isAttack = isAttack;
    }


    public void createHPBar()
    {
        if (!isDie)
        {
            //공격 시간에 비례하여 HP 감소
            dieTime -= Time.deltaTime;
            transform.GetChild(3).gameObject.SetActive(true);
            transform.GetChild(3).transform.GetChild(1).localScale = new Vector3(1.0f * (dieTime / 2.0f), 1.0f, 1.0f);
        }
        if (dieTime < 0)
        {
            //공격 시간 완료시 죽음
            transform.GetChild(3).gameObject.SetActive(false);
            isDie = true;
        }
    }

    //공격 중이 아닐 경우 HP 회복
    //전체 회복 후에는 다시 사라짐
    public void recoverHPBar()
    {
        if(dieTime <= 2.0f) dieTime += Time.deltaTime;
        transform.GetChild(3).transform.GetChild(1).localScale = new Vector3(1.0f * (dieTime / 2.0f), 1.0f, 1.0f);
        if(dieTime > 2.0f) transform.GetChild(3).gameObject.SetActive(false);
    }


    public void destroyMonster()
    {
        nav.isStopped = true;
        dieTime -= Time.deltaTime;
        eRenderer.material.color = new Color(eRenderer.material.color.r, eRenderer.material.color.g, eRenderer.material.color.b, dieTime / 3.0f);
        tRenderer.material.color = new Color(tRenderer.material.color.r, tRenderer.material.color.g, tRenderer.material.color.b, dieTime / 3.0f);
        bRenderer.material.color = new Color(bRenderer.material.color.r, bRenderer.material.color.g, bRenderer.material.color.b, dieTime / 3.0f);
        lRenderer.material.color = new Color(lRenderer.material.color.r, lRenderer.material.color.g, lRenderer.material.color.b, dieTime / 3.0f);
        Destroy(this.transform.gameObject, 3.0f);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Debug.Log("Player Trigger");
            if(nav.isStopped == false) transform.Translate(new Vector3(0.0f, 0.0f, -1.0f));
        }
    }
}
