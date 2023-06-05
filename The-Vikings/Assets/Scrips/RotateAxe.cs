using System;
using UnityEngine;

namespace Scrips
{
    public class RotateAxe : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 700f;
        private float _percentagForce;
        private bool _isRotate = false;
     

        public void SetRotate(float percentagForce,bool value)
        {
            _percentagForce = percentagForce;
            _isRotate = value;
        }

        private void FixedUpdate()
        {
            if (_isRotate)
            {
                Rotate();
            }
        }

        private void Rotate()
        {
            transform.Rotate(new Vector3(0f,0f,  Math.Clamp((_percentagForce / 100) * rotationSpeed,50f,rotationSpeed))* Time.deltaTime);
        }
     

        private void OnCollisionEnter2D(Collision2D other)
        {
            GetComponent<Rigidbody2D>().gravityScale = 1;
           Destroy(gameObject.GetComponentInParent<Axe>()); 
        }
            
    }
}