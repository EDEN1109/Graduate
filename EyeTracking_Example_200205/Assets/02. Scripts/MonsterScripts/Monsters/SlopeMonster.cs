/*
 * 사면 몬스터에 관한 스크립트입니다.
 * MOD0 좌상우하
 * MOD1 우상좌하 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopeMonster : Monsters
{
    private float wSpeed = 5f;
    private float hSpeed = 2f;

    // Start is called before the first frame update
    override protected void Awake()
    {
        base.Awake();
        // 움직일 범위 초기화
        MoveRange = 5.0f;
        RequiredTime = 4.0f;
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
    }

    protected override void Moving()
    {
        base.Moving();
        //좌상우하
        if (nowMod == MOD0)
        {
            transform.Translate(Vector3.left * wSpeed * Time.deltaTime);
            transform.Translate(Vector3.down * hSpeed * Time.deltaTime);
        }
        //우상좌하
        else
        {
            transform.Translate(Vector3.right * wSpeed * Time.deltaTime);
            transform.Translate(Vector3.down * hSpeed * Time.deltaTime);
        }

        if (centerPosition.x - transform.position.x >= MoveRange || centerPosition.x - transform.position.x <= -MoveRange)
        {
            wSpeed *= -1;
            hSpeed *= -1;
        }
    }

    override protected void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
