using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

namespace Camera
{
    public class CameraController : MonoBehaviour
    {
        public GameObject target;

        public float smoothValue = 2f;
        public Vector3 offset = new Vector3(0f, 0f, 0f);

        private void Awake()
        {
            if(target == null)
            {
                target = GameObject.Find("Player");
                
                if(target == null)
                {
                    Debug.LogError("Camera has a null Target");
                }
            }
        }


        private void LateUpdate()
        {
            Vector3 targetPosition = target.transform.position;
            targetPosition.z = transform.position.z;

            transform.position = Vector3.Lerp(transform.position, targetPosition - offset, smoothValue * Time.deltaTime);
        }
    }
}