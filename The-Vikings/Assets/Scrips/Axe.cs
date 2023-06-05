using System;
using UnityEngine;


namespace Scrips
{
    public class Axe : MonoBehaviour
    {
        [SerializeField] private float startSpeed = 5f;
        [SerializeField] private float gravity = 5f;
        private Vector3 _startPosition;
        private Vector3 _vector_v_x;
        private Vector3 _vector_v_y;
        private float _t;
        private float _v_x;
        private float _v_y;
        private float _alpha;
        private float _percentagForce;
        private bool isThrowAxe = false;

        public void SetThrowAxe(float percentageForce)
        {
            _percentagForce = percentageForce;
            if (!isThrowAxe)
            {
                isThrowAxe = true;
                startSpeed = Math.Clamp((percentageForce / 100) * 13,3f,13f);
                _startPosition = transform.position;
                _vector_v_x = new Vector3(transform.forward.x, 0, transform.forward.z);
                _vector_v_y = new Vector3(0f, transform.forward.y, 0f);
                _alpha = Vector3.Angle(_vector_v_x, transform.forward);
                _v_x = startSpeed * Mathf.Cos(_alpha * Mathf.Deg2Rad);
            }
        }

        private void FixedUpdate()
        {
            if (isThrowAxe)
            {
                FlyAxe();
                // FlyDirectionAxe();
                GetComponentInChildren<RotateAxe>().SetRotate(_percentagForce ,true);
                _t = _t + Time.fixedDeltaTime;
            }
        }

        private void FlyAxe()
        {
            var x = _v_x * _t;
            var y = _startPosition.y + startSpeed * Mathf.Sin(_alpha * Mathf.Deg2Rad) * _t - 0.5f * gravity * _t * _t;
            var z = _v_x * _t;
            transform.position = new Vector3(_startPosition.x + x, y, _startPosition.z + z);
        }
        

        private void FlyDirectionAxe()
        {
            _v_y = startSpeed * Mathf.Sin(_alpha * Mathf.Deg2Rad) - gravity * _t;
            var vector_v_0_at_t = _vector_v_x * _v_x + _vector_v_y * _v_y;
            transform.forward = vector_v_0_at_t;
        }
    }
}