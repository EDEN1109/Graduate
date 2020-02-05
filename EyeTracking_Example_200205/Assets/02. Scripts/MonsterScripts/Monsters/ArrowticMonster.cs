/*
 * 상하좌우 몬스터에 관한 스크립트입니다.
 * MOD0 상하
 * MOD1 좌우
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowticMonster : Monsters
{
    private float speed = 3f; // 움직임 속도

    // Start is called before the first frame update
    override protected void Awake()
    {
        base.Awake();
        // 움직일 범위 초기화
        MoveRange = 4f;

        if (nowMod == MOD0)
        {
            RequiredTime = 5.5f;
        }
        else
        {
            RequiredTime = 5.0f;
        }
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
    }

    protected override void Moving()
    {
        base.Moving();

        if (nowMod == MOD0)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if (centerPosition.y - transform.position.y >= MoveRange || centerPosition.y - transform.position.y <= -MoveRange) { speed *= -1; }
        if (centerPosition.x - transform.position.x >= MoveRange || centerPosition.x - transform.position.x <= -MoveRange) { speed *= -1; }
    }

    override protected void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
