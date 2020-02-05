/*
 * 원근응시 몬스터에 관한 스크립트입니다.
 * MOD ONLY 원근
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspectiveMonster : Monsters
{
    private bool flag = true;
    private float timer = 0.0f;
    private float intervalTime = 1.5f;

    // Start is called before the first frame update
    override protected void Awake()
    {
        base.Awake();
        nowMod = MOD0;
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
    }

    protected override void Moving()
    {
        base.Moving();
        if (flag)
        {
            transform.position = new Vector3(transform.position.x, centerPosition.y - 1.5f, centerPosition.z - 8f);
            DarkScene();
        }
        else
        {
            transform.position = centerPosition;
            DarkScene();
        }
        
    }

    private void DarkScene()
    {
        timer += Time.deltaTime;
        if (timer > intervalTime)
        {
            timer = 0.0f;
            flag = !flag;
        }
    }

    override protected void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
