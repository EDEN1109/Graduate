using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ViveSR
{
    namespace anipal
    {
        namespace Eye
        {
            public class UIRaycast : RootRaycast
            {
                private ButtonController start;
                private ButtonController stastics;
                private ButtonController settings;
                private ButtonController quit;
                private ButtonController back;
                private ButtonController callibration;
                private ButtonController tutorial;

                override protected void Awake()
                {
                    base.Awake();
                }

                override protected void Update()
                {
                    base.Update();
                }

                protected override void CallWhenSee(FocusInfo FocusInfo)
                {
                    base.CallWhenSee(FocusInfo);

                    if (FocusInfo.collider.tag == "Start")
                    {
                        Debug.Log("Start Ray");
                        start = FocusInfo.transform.GetComponent<StartButton>();
                        start.IsRay = true;
                    }
                    else
                    {
                        if (start != null) start.IsRay = false;
                    }
                    if (FocusInfo.collider.tag == "Stastics")
                    {
                        Debug.Log("Stastics Ray");
                        stastics = FocusInfo.transform.GetComponent<StasticsButton>();
                        stastics.IsRay = true;
                    }
                    else
                    {
                        if (stastics != null) stastics.IsRay = false;
                    }
                    if (FocusInfo.collider.tag == "Settings")
                    {
                        Debug.Log("Settings Ray");
                        settings = FocusInfo.transform.GetComponent<SettingsButton>();
                        settings.IsRay = true;
                    }
                    else
                    {
                        if (settings != null) settings.IsRay = false;
                    }
                    if (FocusInfo.collider.tag == "Quit")
                    {
                        Debug.Log("Quit Ray");
                        quit = FocusInfo.transform.GetComponent<QuitButton>();
                        quit.IsRay = true;
                    }
                    else
                    {
                        if (quit != null) quit.IsRay = false;
                    }
                    if (FocusInfo.collider.tag == "Back")
                    {
                        back = FocusInfo.transform.GetComponent<BackButton>();
                        back.IsRay = true;
                    }
                    else
                    {
                        if (back != null) back.IsRay = false;
                    }
                    if (FocusInfo.collider.tag == "Callibration")
                    {
                        callibration = FocusInfo.transform.GetComponent<CallibrationButton>();
                        callibration.IsRay = true;
                    }
                    else
                    {
                        if (callibration != null) callibration.IsRay = false;
                    }
                    if (FocusInfo.collider.tag == "Tutorial")
                    {
                        tutorial = FocusInfo.transform.GetComponent<TutorialButton>();
                        tutorial.IsRay = true;
                    }
                    else
                    {
                        if (tutorial != null) tutorial.IsRay = false;
                    }
                }
            }
        }

    }
}
