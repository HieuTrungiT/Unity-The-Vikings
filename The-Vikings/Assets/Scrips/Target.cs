using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scrips
{
    public class Target : MonoBehaviour
    {
        [SerializeField] private Transform arm_ThrowingForce;
        [SerializeField] private Transform arm_ThrowAngle;

        private float _mousePressedX;
        private float _mousePressedY;
        private Vector2 _posMouse;

        private void Start()
        {
        }

        void Update()
        {
            OnTargetToch();
        }

        private void OnTargetToch()
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    _mousePressedX = Input.mousePosition.x;
                    _mousePressedY = Input.mousePosition.y;
                    // arm_ThrowingForce.localRotation = Quaternion.Euler(0f, 0f, -120f);
                }

                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    print("Touch has Ended");
                }

                float detalX = Input.mousePosition.x - _mousePressedX + 120f;
                float detalY = Input.mousePosition.y - _mousePressedY;
                _posMouse = new Vector2(detalX, detalY);
                arm_ThrowAngle.position = new Vector2(_posMouse.y * 0.01f, _posMouse.y * 0.01f);

                arm_ThrowingForce.localRotation = Quaternion.Euler(0f, 0f, -Math.Clamp(_posMouse.x, 0f, 120f));
            }
        }
    }
}