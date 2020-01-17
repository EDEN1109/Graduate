using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour
{
    private const float moveLimit = 60f;
    private Animator animator;
    private float move = moveLimit;
    private bool flag = false;
    private float y;
    private bool isEyeon = false;
    private bool isAttacked = false;
    private int count = 0;
    //얼마나 연속으로 공격해야 죽는지
    private float dieTime = 3.0f;
    private bool isDie = false;
    void Start()
    {
        animator = transform.GetComponent<Animator>();
        y = transform.position.y;

        transform.position = new Vector3(transform.position.x, y + move / 100, transform.position.z);
        transform.GetChild(2).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
        {
            animator.ResetTrigger("isAttack");
            move = 1;
        }
        if (isDie)
        {
            destroyMonster();
        }
        if (!isEyeon)
        {
            recoverHPBar();
        }
        if (count == 3 && move == 0f)
        {
            animator.SetTrigger("isAttack");
            isAttacked = true;
        }
        else if(count < 3 && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            moveBug();
        }
        else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            moveBug();
        }
        if (move>(moveLimit + moveLimit/2))
        {
            isDie = true;
        }
    }

    public bool getisAttacked()
    {
        return isAttacked;
    }
    private void moveBug()
    {
        if(!isAttacked)
        {
            if (flag)
                move++;
            else
                move--;
        }
        else
        {
            move++;
        }
        if (move >= moveLimit)
        {
            flag = false;
            count++;
        }
        else if (move <= -moveLimit)
        {
            flag = true;
            count++;
        }
        transform.position = new Vector3(transform.position.x, y + move / 100, transform.position.z);
    }

    public void createHPBar()
    {
        if (!isDie)
        {
            //공격 시간에 비례하여 HP 감소
            dieTime -= Time.deltaTime;
            transform.GetChild(2).gameObject.SetActive(true);
            transform.GetChild(2).transform.GetChild(1).localScale = new Vector3(1.0f * (dieTime / 3.0f), 1.0f, 1.0f);
        }
        if (dieTime < 0)
        {
            //공격 시간 완료시 죽음
            transform.GetChild(1).gameObject.SetActive(false);
            isDie = true;
        }
    }

    //공격 중이 아닐 경우 HP 회복
    //전체 회복 후에는 다시 사라짐
    public void recoverHPBar()
    {
        if (dieTime <= 3.0f) dieTime += Time.deltaTime;
        transform.GetChild(2).transform.GetChild(1).localScale = new Vector3(1.0f * (dieTime / 3.0f), 1.0f, 1.0f);
        if (dieTime > 3.0f) transform.GetChild(2).gameObject.SetActive(false);
    }

    public void setIsEyeon(bool state)
    {
        isEyeon = state;
    }
    public void destroyMonster()
    {
        Destroy(this.transform.gameObject);
    }
}
