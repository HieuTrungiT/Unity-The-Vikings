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
        [SerializeField] private GameObject parentAxe;
        [SerializeField] private GameObject pf_Axe;
        

        private float _mousePressedX;
        private float _mousePressedY;
        private float _detalForce;
        private Vector2 _posMouse;

    

        void Update()
        {
            OnTargetTouch();
        }

        private void OnTargetTouch()
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    _mousePressedX = Input.mousePosition.x;
                    _mousePressedY = Input.mousePosition.y;
                }

                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    pf_Axe.GetComponent<Axe>().SetThrowAxe(100f - ((_detalForce - 30) / (120 - 30)) * 100f);
                }

                float detalX = Input.mousePosition.x - _mousePressedX + 120f;
                float detalY = Input.mousePosition.y - _mousePressedY;
                _posMouse = new Vector2(detalX, detalY);
                
                ThrowAngleHuman();
                ThrowingForceHuman();
            }
        }

        private void ThrowAngleHuman()
        {
            arm_ThrowAngle.localPosition = new Vector2(_posMouse.y == 0 ? 0f : _posMouse.y * 0.01f , _posMouse.y == 0 ? 0f : _posMouse.y * 0.01f);
        }
        private void ThrowingForceHuman()
        {
            _detalForce = Math.Clamp(_posMouse.x, 30f, 120f);
            arm_ThrowingForce.localRotation = Quaternion.Euler(0f, 0f, -_detalForce);

        }
    }
}