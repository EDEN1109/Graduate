using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MenuRay : MonoBehaviour
{
    public GameObject background;
    public GameObject main;
    public GameObject start;
    public GameObject settings;
    private float timer = 0f;
    private float timeInterval = 0f;
    private bool isRay = false;
    
    // Start is called before the first frame update
    void Start()
    {
        start.SetActive(false);
        settings.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isRay)
        {
            loadBackgorund();
        }
        else
        {
            initBackground();
        }
    }

    public void setIsRay(bool _isRay)
    {
        isRay = _isRay;
    }
    private void initBackground()
    {
        background.SetActive(false);
        background.transform.localScale = new Vector3(1.1f, 1.1f, 0);
        timer = 0.0f;
    }
    private void loadBackgorund()
    {
        //Debug.Log(gameObject.tag);
        background.SetActive(true);
        timeInterval = (timeInterval + Time.deltaTime) - timeInterval;
        timer += Time.deltaTime;
        if (timer > 2.0f) timer = 2.0f;
        if(background.transform.localScale.z < 1f)
        {
            background.transform.localScale += new Vector3(0, 0, timeInterval / 2.0f);
        }
        else
        {
            
        }
     
    }

}

