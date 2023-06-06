using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

namespace Scrips
{
    public class Target : MonoBehaviour
    {
        [SerializeField] private Transform arm_ThrowingForce;
        [SerializeField] private Transform arm_ThrowAngle;
        [SerializeField] private GameObject pf_Axe;
        [SerializeField] private GameObject axeContainer;

        [Header("Animation")]
        [SerializeField] private Animator animator;

        GameObject _axeClone;


        private float _mousePressedX;
        private float _mousePressedY;
        private float _detalForce;
        private Vector2 _posMouse;


        private void Start()
        {
            _axeClone = Instantiate(pf_Axe, axeContainer.transform);
            _axeClone.SetActive(false);
        }

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
                    // Set angle to throw
                    axeContainer.transform.position = arm_ThrowingForce.transform.position;
                    axeContainer.transform.rotation = arm_ThrowingForce.transform.rotation;

                    _axeClone.GetComponent<Axe>().SetThrowAxe(100f - ((_detalForce - 30) / (120 - 30)) * 100f);
                    _axeClone.SetActive(true);
                    animator.SetTrigger("IsThrown 2");
                    GenerateNewAxe();
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
            arm_ThrowAngle.localPosition = new Vector2(_posMouse.y == 0 ? 0f : _posMouse.y * 0.01f, _posMouse.y == 0 ? 0f : _posMouse.y * 0.01f);
        }
        private void ThrowingForceHuman()
        {
            _detalForce = Math.Clamp(_posMouse.x, 30f, 120f);
            arm_ThrowingForce.localRotation = Quaternion.Euler(0f, 0f, -_detalForce);
        }

        //IEnumerator GenerateNewAxe()
        //{
        //    yield return new WaitForSeconds(0.5f);
        //    //_axeClone = Instantiate(pf_Axe, arm_ThrowingForce.transform);
        //    //arm_ThrowingForce.SetParent(_axeClone.transform);
        //    _axeClone = AxeObjectPooling.Instance.GetPooledObject();
        //    if (_axeClone != null)
        //    {
        //        _axeClone.transform.position = arm_ThrowingForce.transform.position;
        //        _axeClone.SetActive(true);
        //    }
        //}
        private void GenerateNewAxe()
        {
            _axeClone = Instantiate(pf_Axe, axeContainer.transform);
            _axeClone.SetActive(false);
            //_axeClone = AxeObjectPooling.Instance.GetPooledObject();
            //if (_axePool != null)
            //{
            //    _axePool.transform.position = arm_ThrowingForce.transform.position;
            //    _axePool.SetActive(true);
            //}
        }
    }
}