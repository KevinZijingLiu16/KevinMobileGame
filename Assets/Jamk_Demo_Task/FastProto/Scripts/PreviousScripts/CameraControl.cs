using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AE0672
{
    public class CameraControl : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset = new Vector3(0f, 2f, -5f);
        public float smoothness = 5.0f;
        public float sensitivityX = 2f; // Sensitivity for looking left/right
        public float sensitivityY = 2f; // Sensitivity for looking up/down
        public float minYAngle = -60f; // Minimum angle to look down
        public float maxYAngle = 60f; // Maximum angle to look up

        private Vector3 previousMousePos;
        private float currentRotationX = 0f; // Track current rotation around the X-axis

        // Threshold for mouse movement along x and y axes to allow camera rotation
        public float thresholdX = 10f;
        public float thresholdY = 10f;

        void LateUpdate()
        {
            if (target != null)
            {
                transform.position = Vector3.Lerp(transform.position, target.position, smoothness * Time.deltaTime);

                transform.GetChild(0).transform.localPosition = offset;

                
                Vector2 mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

                
                if (Mathf.Abs(mouseMovement.x) > thresholdX)
                {
                    
                    float mouseX = mouseMovement.x * sensitivityX;
                    transform.Rotate(0, mouseX, 0);
                }

               
                if (Mathf.Abs(mouseMovement.y) > thresholdY)
                {
                    
                    float mouseY = mouseMovement.y * sensitivityY;
                    currentRotationX -= mouseY; 

                   
                    currentRotationX = Mathf.Clamp(currentRotationX, minYAngle, maxYAngle);

                   
                    transform.rotation = Quaternion.Euler(currentRotationX, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
                }
            }
        }
    }
}
