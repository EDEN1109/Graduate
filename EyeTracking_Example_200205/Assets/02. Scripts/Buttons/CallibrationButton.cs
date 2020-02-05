using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace ViveSR
{
    namespace anipal
    {
        namespace Eye
        {
            public class CallibrationButton : ButtonController
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
                    SRanipal_Eye_API.LaunchEyeCalibration(IntPtr.Zero);
                }
               
            }

        }
    }
}
