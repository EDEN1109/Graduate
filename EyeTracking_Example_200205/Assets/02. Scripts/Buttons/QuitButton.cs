using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : ButtonController
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
        Application.Quit();
    }
}
