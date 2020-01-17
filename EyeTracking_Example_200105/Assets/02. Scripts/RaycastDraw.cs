using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RaycastDraw : MonoBehaviour
{
    private RaycastHit hit;
    private float timer = 3.0f;
    public float alpha = 3.0f;
    private float h = 0.0f;
    private float v;
    private float moveSpeed = 5.0f;
    private float rotateSpeed = 5.0f;
    private float y = 0.0f;
    private float x = 0.0f;
    private float maxDistance = 110f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        HeadRotate();

        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
        {
            Debug.DrawRay(transform.position, transform.forward * maxDistance, Color.red, 0.3f);
            if(hit.transform.gameObject.tag == "StartBtn")
            {
                Debug.Log("Back");
                //SceneManager.LoadScene("battle");
            }
            else if(hit.transform.gameObject.tag == "Enemy")
            {
                Debug.Log("front");
            }
            else if(hit.transform.gameObject.tag == "HelpBtn")
            {
                //help scene
                Debug.Log("Back");
            }
            else if(hit.transform.gameObject.tag == "CalibrationBtn")
            {
                //calibration
            }
        }
    }

    private void HeadRotate()
    {
        float minX = -20f;
        float maxX = 20f;

        y += Input.GetAxis("MouseX") * rotateSpeed * Time.deltaTime;
        x += Input.GetAxis("MouseY") * rotateSpeed * Time.deltaTime;
        x= Mathf.Clamp(x, minX, maxX);
        transform.rotation = Quaternion.Euler(new Vector3(-x, y, 0));
       // transform.rotation = Quaternion.Euler(new Vector3(0, y, 0));
    }

}
