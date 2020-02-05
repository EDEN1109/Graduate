using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    private bool isPlay = false;
    private ParticleSystem ps;

    public bool IsPlay
    {
        get
        {
            return isPlay;
        }

        set
        {
            isPlay = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ps = transform.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartPlay()
    {
        isPlay = true;
        ps.Play(true);
    }

    public void StopPlay()
    {
        isPlay = false;
        ps.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }
}
