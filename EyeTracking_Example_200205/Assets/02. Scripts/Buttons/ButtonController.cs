using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField]
    protected GameObject background;
    [SerializeField]
    protected GameObject main;
    protected float timer = 0f;
    private float timeInterval = 0f;
    private bool isRay = false;

    public bool IsRay
    {
        get
        {
            return isRay;
        }

        set
        {
            isRay = value;
        }
    }

    virtual protected void Awake()
    {

    }
    // Update is called once per frame
    virtual protected void Update()
    {
        if (IsRay)
        {
            LoadBackgorund();
        }
        else
        {
            InitBackground();
        }
    }
    private void InitBackground()
    {
        if (gameObject.tag == "Back") Debug.Log("init");
        background.SetActive(false);
        background.transform.localScale = new Vector3(1.1f, 1.1f, 0);
        timer = 0.0f;
    }
    private void LoadBackgorund()
    {
        background.SetActive(true);
        timeInterval = (timeInterval + Time.deltaTime) - timeInterval;
        timer += Time.deltaTime;
        if (timer > 2.0f)
        {
            timer = 2.0f;
        }
        if (background.transform.localScale.z < 1f)
        {
            background.transform.localScale += new Vector3(0, 0, timeInterval / 2.0f);
        }
        else
        {
            InitBackground();
            Response();
        }
    }

    virtual protected void Response() {
        Debug.Log(this.name + " response"); 
    }
}
