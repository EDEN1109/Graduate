using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : ButtonController
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
    }
    protected override void Response()
    {
        base.Response();
        main.SetActive(true);
        transform.parent.gameObject.SetActive(false);
    }
}
