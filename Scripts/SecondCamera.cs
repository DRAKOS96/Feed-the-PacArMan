using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class SecondCamera : MonoBehaviour
{
        public Camera sourceCamera;

        private Camera _myCamera;

        private void OnEnable()
        {
            _myCamera = GetComponent<Camera>();
        }

        private void OnPreRender()
        {
            if (_myCamera == null)
            {
                return;
            }
            if (sourceCamera == null)
            {
                return;
            }
            _myCamera.projectionMatrix = sourceCamera.projectionMatrix;

        }
}

