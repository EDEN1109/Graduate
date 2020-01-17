////========= Copyright 2018, HTC Corporation. All rights reserved. ===========
//using System.Collections.Generic;
//using System.Runtime.InteropServices;
//using UnityEngine;
//using UnityEngine.Assertions;

//namespace ViveSR
//{
//    namespace anipal
//    {
//        namespace Eye
//        {
//            public class Sphere : MonoBehaviour
//            {
//                public bool NeededToGetData = true;
//                private GameObject[] EyeAnchors;
//                private const int NUM_OF_EYES = 2;
//                private static EyeData eyeData = new EyeData();
//                private bool eye_callback_registered = false;
//                private void Start()
//                {
//                    if (!SRanipal_Eye_Framework.Instance.EnableEye)
//                    {
//                        enabled = false;
//                        return;
//                    }
//                }

//                private void Update()
//                {
//                    if (SRanipal_Eye_Framework.Status != SRanipal_Eye_Framework.FrameworkStatus.WORKING &&
//                        SRanipal_Eye_Framework.Status != SRanipal_Eye_Framework.FrameworkStatus.NOT_SUPPORT) return;

//                    if (NeededToGetData)
//                    {
//                        if (SRanipal_Eye_Framework.Instance.EnableEyeDataCallback == true && eye_callback_registered == false)
//                        {
//                            SRanipal_Eye.WrapperRegisterEyeDataCallback(Marshal.GetFunctionPointerForDelegate((SRanipal_Eye.CallbackBasic)EyeCallback));
//                            eye_callback_registered = true;
//                        }
//                        else if (SRanipal_Eye_Framework.Instance.EnableEyeDataCallback == false && eye_callback_registered == true)
//                        {
//                            SRanipal_Eye.WrapperUnRegisterEyeDataCallback(Marshal.GetFunctionPointerForDelegate((SRanipal_Eye.CallbackBasic)EyeCallback));
//                            eye_callback_registered = false;
//                        }
//                        else if (SRanipal_Eye_Framework.Instance.EnableEyeDataCallback == false)
//                            SRanipal_Eye_API.GetEyeData(ref eyeData);

//                        bool isLeftEyeActive = false;
//                        bool isRightEyeAcitve = false;
//                        if (SRanipal_Eye_Framework.Status == SRanipal_Eye_Framework.FrameworkStatus.WORKING)
//                        {
//                            isLeftEyeActive = eyeData.verbose_data.left.GetValidity(SingleEyeDataValidity.SINGLE_EYE_DATA_GAZE_ORIGIN_VALIDITY);
//                            isRightEyeAcitve = eyeData.verbose_data.right.GetValidity(SingleEyeDataValidity.SINGLE_EYE_DATA_GAZE_ORIGIN_VALIDITY);
//                        }
//                        else if (SRanipal_Eye_Framework.Status == SRanipal_Eye_Framework.FrameworkStatus.NOT_SUPPORT)
//                        {
//                            isLeftEyeActive = true;
//                            isRightEyeAcitve = true;
//                        }

//                        Vector3 GazeOriginCombinedLocal, GazeDirectionCombinedLocal = Vector3.zero;
//                        if (eye_callback_registered == true)
//                        {
//                            if (SRanipal_Eye.GetGazeRay(GazeIndex.COMBINE, out GazeOriginCombinedLocal, out GazeDirectionCombinedLocal, eyeData)) { }
//                            else if (SRanipal_Eye.GetGazeRay(GazeIndex.LEFT, out GazeOriginCombinedLocal, out GazeDirectionCombinedLocal, eyeData)) { }
//                            else if (SRanipal_Eye.GetGazeRay(GazeIndex.RIGHT, out GazeOriginCombinedLocal, out GazeDirectionCombinedLocal, eyeData)) { }
//                        }
//                        else
//                        {
//                            if (SRanipal_Eye.GetGazeRay(GazeIndex.COMBINE, out GazeOriginCombinedLocal, out GazeDirectionCombinedLocal)) { }
//                            else if (SRanipal_Eye.GetGazeRay(GazeIndex.LEFT, out GazeOriginCombinedLocal, out GazeDirectionCombinedLocal)) { }
//                            else if (SRanipal_Eye.GetGazeRay(GazeIndex.RIGHT, out GazeOriginCombinedLocal, out GazeDirectionCombinedLocal)) { }

//                        }
//                        UpdateGazeRay(GazeDirectionCombinedLocal);
//                    }
//                }
//                private void Release()
//                {
//                    if (eye_callback_registered == true)
//                    {
//                        SRanipal_Eye.WrapperUnRegisterEyeDataCallback(Marshal.GetFunctionPointerForDelegate((SRanipal_Eye.CallbackBasic)EyeCallback));
//                        eye_callback_registered = false;
//                    }
//                }
//                private void OnDestroy()
//                {
//                    DestroyEyeAnchors();
//                }
//                public void UpdateGazeRay(Vector3 gazeDirectionCombinedLocal)
//                {
//                    for (int i = 0; i < this.Length; ++i)
//                    {
//                        Vector3 target = EyeAnchors[i].transform.TransformPoint(gazeDirectionCombinedLocal);
//                        EyesModels[i].LookAt(target);
//                    }
//                }
//                private void CreateEyeAnchors()
//                {
//                    EyeAnchors = new GameObject[NUM_OF_EYES];
//                    for (int i = 0; i < NUM_OF_EYES; ++i)
//                    {
//                        EyeAnchors[i] = new GameObject();
//                        EyeAnchors[i].name = "EyeAnchor_" + i;
//                        EyeAnchors[i].transform.SetParent(gameObject.transform);
//                        EyeAnchors[i].transform.localPosition = EyesModels[i].localPosition;
//                        EyeAnchors[i].transform.localRotation = EyesModels[i].localRotation;
//                        EyeAnchors[i].transform.localScale = EyesModels[i].localScale;
//                    }
//                }
//                private void DestroyEyeAnchors()
//                {
//                    if (EyeAnchors != null)
//                    {
//                        foreach (var obj in EyeAnchors)
//                            if (obj != null) Destroy(obj);
//                    }
//                }
//                private static void EyeCallback(ref EyeData eye_data)
//                {
//                    eyeData = eye_data;
//                }
//            }
//        }
//    }
//}