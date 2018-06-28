using System;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

namespace Assets.Scripts.Cameras
{
    public class IsometricCameraController : MonoBehaviour
    {
        public Camera mainCamera;
        public MouseButton panMouseButton = MouseButton.LeftMouse;

        public float minOrthographicSize = 2.0f;
        public float maxOrthographicSize = 30.0f;
        public float zoomStep = 0.65f;
        
        public float panSpeed = 180.0f;
        public bool invertPanning = false;

        private Vector3 lastMousePosition;
        
        
        void Update ()
        {
            Zoom();
            Pan();
        }

        void Zoom()
        {
            var mouseWheelDelta = Input.GetAxis("Mouse ScrollWheel");

            if (Math.Abs(mouseWheelDelta) > 0.00001f) // forward
            {
                mainCamera.orthographicSize -= Mathf.Sign(mouseWheelDelta) * zoomStep;
                if (mainCamera.orthographicSize < minOrthographicSize)
                {
                    mainCamera.orthographicSize = minOrthographicSize;
                }

                if (mainCamera.orthographicSize > maxOrthographicSize)
                {
                    mainCamera.orthographicSize = maxOrthographicSize;
                }
            }
        }

        void Pan()
        {
            if (Input.GetMouseButton((int)panMouseButton))
            {
                float xAxis = Input.GetAxis("Mouse X");
                float yAxis = Input.GetAxis("Mouse Y");

                if (invertPanning)
                {
                    xAxis *= -1;
                    yAxis *= -1;
                }

                Vector3 translation = new Vector3(-xAxis, -yAxis, 0) * (1.0f / maxOrthographicSize) * mainCamera.orthographicSize;
                mainCamera.transform.Translate(translation * panSpeed * Time.deltaTime);
            }

        }
    }
}
