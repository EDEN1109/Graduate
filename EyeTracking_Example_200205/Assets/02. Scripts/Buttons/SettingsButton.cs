using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButton : ButtonController
{
    [SerializeField]
    private GameObject setting;

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
        main.SetActive(false);
        setting.SetActive(true);
    }
}
