//========= Copyright 2018, HTC Corporation. All rights reserved. ===========
using UnityEngine;

namespace ViveSR.anipal.Eye
{
    public class Enemy : MonoBehaviour
    {
        private void Start()
        {
            Focus(Vector3.zero);
        }

        public void Focus(Vector3 focusPoint)
        {
            float maxDist = 0.42f * transform.localScale.x;
            float dist = Vector3.Distance(focusPoint, transform.position);

            Vector3 axis = (focusPoint - transform.position) / Vector3.Distance(focusPoint, transform.position);
            float ang = SignedAngle(transform.right, axis, transform.forward);
            if (ang < 0) ang += 360f;
        }

        public float SignedAngle(Vector3 v1, Vector3 v2, Vector3 v_forward)
        {
            float dotP = Vector3.Dot(v1, v2);
            float unsignedAngle = Mathf.Acos(dotP) * (180 / 3.14159f);

            float sign = Vector3.Dot(v_forward, Vector3.Cross(v1, v2));
            float signedAngle = unsignedAngle * (sign > 0f ? 1f : -1f) + 180f;
            return signedAngle;
        }
    }
}
