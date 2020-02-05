using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using System.Runtime.InteropServices;
using UnityEngine.Assertions;

namespace ViveSR
{
    namespace anipal
    {
        namespace Eye
        {
            public class RootRaycast : MonoBehaviour
            {
                private bool eye_callback_registered = false;
                private float MaxDistance = 1000.0f;
                private FocusInfo FocusInfo;
                private readonly GazeIndex[] GazePriority = new GazeIndex[] { GazeIndex.COMBINE, GazeIndex.LEFT, GazeIndex.RIGHT };
                private static EyeData eyeData = new EyeData();
                private bool isBlink; // 눈을 감았는가
                virtual protected void Awake()
                {
                    if (!SRanipal_Eye_Framework.Instance.EnableEye)
                    {
                        enabled = false;
                        return;
                    }
                }

                // Update is called once per frame
                virtual protected void Update()
                {
                    // Position Tracking Disable
                    UnityEngine.XR.InputTracking.disablePositionalTracking = true;

                    // Eye Tracking
                    if (SRanipal_Eye_Framework.Status != SRanipal_Eye_Framework.FrameworkStatus.WORKING &&
                SRanipal_Eye_Framework.Status != SRanipal_Eye_Framework.FrameworkStatus.NOT_SUPPORT) return;

                    if (SRanipal_Eye_Framework.Instance.EnableEyeDataCallback == true && eye_callback_registered == false)
                    {
                        SRanipal_Eye.WrapperRegisterEyeDataCallback(Marshal.GetFunctionPointerForDelegate((SRanipal_Eye.CallbackBasic)EyeCallback));
                        eye_callback_registered = true;
                    }
                    else if (SRanipal_Eye_Framework.Instance.EnableEyeDataCallback == false && eye_callback_registered == true)
                    {
                        SRanipal_Eye.WrapperUnRegisterEyeDataCallback(Marshal.GetFunctionPointerForDelegate((SRanipal_Eye.CallbackBasic)EyeCallback));
                        eye_callback_registered = false;
                    }

                    foreach (GazeIndex index in GazePriority)
                    {
                        Ray GazeRay;
                        bool eye_focus;

                        if (eye_callback_registered)
                            eye_focus = SRanipal_Eye.Focus(index, out GazeRay, out FocusInfo, 0, MaxDistance, ~0, eyeData);
                        else
                            eye_focus = SRanipal_Eye.Focus(index, out GazeRay, out FocusInfo, 0, MaxDistance, ~0);

                        // Coding When See Something
                        if (eye_focus)
                        {

                            Vector3 point = new Vector3(FocusInfo.transform.position.x, FocusInfo.transform.position.y, 0);

                            CallWhenSee(FocusInfo);
                        }
                    }
                }

                virtual protected void CallWhenSee(FocusInfo FocusInfo)
                {
                    Debug.Log("Now I See " + FocusInfo.collider.gameObject.name);
                }

                private void Release()
                {
                    if (eye_callback_registered == true)
                    {
                        SRanipal_Eye.WrapperUnRegisterEyeDataCallback(Marshal.GetFunctionPointerForDelegate((SRanipal_Eye.CallbackBasic)EyeCallback));
                        eye_callback_registered = false;
                    }
                }
                private static void EyeCallback(ref EyeData eye_data)
                {
                    eyeData = eye_data;
                }
            }
        }
    }
}
