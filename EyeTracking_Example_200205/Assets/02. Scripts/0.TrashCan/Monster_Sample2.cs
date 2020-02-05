using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Sample2 : MonoBehaviour
{
    private float time = 3.0f; 
    private float dieTime = 3.0f;
    private Renderer fRenderer = null;
    private Renderer ubRenderer = null;
    private Renderer dbRenderer = null;
    private Renderer mRenderer = null;
    private bool isDie = false;
    private bool isEyeon = false;
    // Start is called before the first frame update
    void Start()
    {
        fRenderer = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Renderer>();
        ubRenderer = transform.GetChild(0).GetChild(1).GetChild(0).GetChild(1).GetChild(0).GetComponent<Renderer>();
        dbRenderer = transform.GetChild(0).GetChild(1).GetChild(1).GetChild(1).GetChild(0).GetComponent<Renderer>();
        mRenderer = transform.GetChild(1).GetComponent<Renderer>();
        //HPBar 처음에는 보이지 않게 함
        transform.GetChild(3).gameObject.SetActive(false);
    }
    private void Update()
    {
        if (isDie)
        {
            destroyMonster();
        }
        if(!isEyeon)
        {
            recoverHPBar();
        }
    }

    public bool getIsDie()
    {
        return isDie;
    }

    public void setIsEyeon(bool state)
    {
        isEyeon = state;
    }

    public void createHPBar()
    {
        if (!isDie)
        {
            //공격 시간에 비례하여 HP 감소
            dieTime -= Time.deltaTime;
            transform.GetChild(3).gameObject.SetActive(true);
            transform.GetChild(3).transform.GetChild(1).localScale = new Vector3(1.0f * (dieTime / 3.0f), 1.0f, 1.0f);
            isEyeon = true;
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
        if (dieTime < 3.0f) dieTime += Time.deltaTime;
        transform.GetChild(3).transform.GetChild(1).localScale = new Vector3(1.0f * (dieTime / 3.0f), 1.0f, 1.0f);
        if (dieTime >= 3.0f) transform.GetChild(3).gameObject.SetActive(false);
    }
    public void destroyHPBar()
    {
        transform.GetChild(3).gameObject.SetActive(false);
        transform.GetChild(3).transform.GetChild(1).localScale = new Vector3(1.0f, 1.0f, 1.0f);
        dieTime = 3.0f;
    }
    public void destroyMonster()
    {
        dieTime -= Time.deltaTime;
        fRenderer.material.color = new Color(fRenderer.material.color.r, fRenderer.material.color.g, fRenderer.material.color.b, dieTime / 3.0f);
        ubRenderer.material.color = new Color(fRenderer.material.color.r, fRenderer.material.color.g, fRenderer.material.color.b, dieTime / 3.0f);
        dbRenderer.material.color = new Color(fRenderer.material.color.r, fRenderer.material.color.g, fRenderer.material.color.b, dieTime / 3.0f);
        mRenderer.material.color = new Color(fRenderer.material.color.r, fRenderer.material.color.g, fRenderer.material.color.b, dieTime / 3.0f);

        Destroy(this.transform.gameObject, 3.0f);
    }
}
