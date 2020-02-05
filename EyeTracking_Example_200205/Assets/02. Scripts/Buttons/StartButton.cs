﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : ButtonController
{
    [SerializeField]
    private GameObject start;

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
        start.SetActive(true);
    }
}