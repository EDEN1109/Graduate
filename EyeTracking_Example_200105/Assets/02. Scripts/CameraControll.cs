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
            public class CameraControll : MonoBehaviour
            {
                private bool eye_callback_registered = false;
                private float MaxDistance = 1000.0f;
                private FocusInfo FocusInfo;
                private GameObject prev;
                private readonly GazeIndex[] GazePriority = new GazeIndex[] { GazeIndex.COMBINE, GazeIndex.LEFT, GazeIndex.RIGHT };
                private static EyeData eyeData = new EyeData();
                private float rotateSpeed = 5.0f;
                private float y = 0.0f;
                private float x = 0.0f;
                private Monster flower;
                private MovingEnemy sheep;
                private Bug bug;

                // Use this for initialization
                void Start()
                {
                    if (!SRanipal_Eye_Framework.Instance.EnableEye)
                    {
                        enabled = false;
                        return;
                    }
                }

                // Update is called once per frame
                void Update()
                {
                    // Limit Rotation
                    //HeadRotate();

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

                            if (FocusInfo.collider.tag == "Enemy")
                            {
                                Debug.Log("I can see Enemy");
                                GameObject e = GameObject.FindGameObjectWithTag("Enemy");
                                Destroy(e.gameObject);
                            }
                            if (FocusInfo.collider.tag == "Flower")
                            {

                                Debug.Log("Flower");
                                flower = FocusInfo.transform.GetComponent<Monster>();
                                flower.createHPBar();
                            }
                            else
                            {
                                if (flower != null) flower.setIsEyeon(false);
                            }
                            if (FocusInfo.collider.tag == "Bug")
                            {

                                Debug.Log("Bug");
                                bug = FocusInfo.transform.GetComponent<Bug>();
                                if(!bug.getisAttacked())
                                    bug.createHPBar();
                            }
                            else
                            {
                                if (bug != null) bug.setIsEyeon(false);
                            }
                            if (FocusInfo.collider.tag == "MEnemy")
                            {
                                sheep = FocusInfo.transform.GetComponent<MovingEnemy>();
                                sheep.createHPBar();
                            }
                            else
                            {
                                if (sheep != null) sheep.recoverHPBar();
                            }
                        }
                    }
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

                private void HeadRotate()
                {
                    float minX = -20f;
                    float maxX = 20f;
                    float minY = -60f;
                    float maxY = 60f;

                    Quaternion rot = UnityEngine.XR.InputTracking.GetLocalRotation(UnityEngine.XR.XRNode.Head);

                    //UnityEngine.XR.InputTracking.GetLocalRotation();
                    x = Mathf.Clamp(rot.x, minX, maxX);
                    y = Mathf.Clamp(rot.y, minY, maxY);
                    //y = Mathf.Clamp(rot.y + 90, minY + 90, maxY + 90);

                    this.transform.rotation = Quaternion.Euler(new Vector3(x*10, (y+45)*10, 0));
                }
            }
        }
    }
}
