/*
 * 원/8자 몬스터에 관한 스크립트입니다.
 * MOD0 원
 * MOD1 8자
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclicMonster : Monsters
{
    private float speed = 2.0f;
    private float angle = 0.0f;
    private float moveX = 2.0f;
    private float deltaX;
    private bool flag = true;
    override protected void Awake()
    {
        base.Awake();
        // 움직일 범위 초기화
        MoveRange = 2.5f;

        if(nowMod == MOD0)
        {
            RequiredTime = 4.0f;
        }
        else
        {
            RequiredTime = 7.0f;
        }

        if (nowMod == MOD0)
        {
            angle = 0.0f;
        }
        else
        {
            angle = 180.0f;
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
            if (transform.position.x >= centerPosition.x + MoveRange + moveX)
            {
                flag = false;
            }
            else if (flag)
            {
                IsEyeon = false;
                transform.Translate(Vector3.right * speed * 2f * Time.deltaTime);
            }
            if (!flag)
            {
                transform.position = new Vector3((MoveRange + moveX) * Mathf.Cos(angle * Mathf.Deg2Rad) + centerPosition.x, MoveRange * Mathf.Sin(angle * Mathf.Deg2Rad) + centerPosition.y, centerPosition.z);
                angle = (angle + speed) % 360;
            }
        }
        else
        {
            transform.position = new Vector3(MoveRange * Mathf.Cos(angle * Mathf.Deg2Rad) + centerPosition.x + MoveRange + deltaX, MoveRange * Mathf.Sin(angle * Mathf.Deg2Rad) + centerPosition.y, centerPosition.z);
            if (flag) angle += 2f;
            else angle -= 2f;

            if (angle >= 540f)
            {
                deltaX = -2f * MoveRange;
                angle = 0f;
                flag = !flag;
            }
            else if (angle <= -360f)
            {
                deltaX = 0.0f;
                angle = 180.0f;
                flag = !flag;
            }

        }
    }


    override protected void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
