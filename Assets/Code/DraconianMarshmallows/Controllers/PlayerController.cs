﻿using UnityEngine;
using System.Collections;
using com.draconianmarshmallows.boilerplate.controllers;
using System;

namespace DraconianMarshmallows.Controllers
{
    public class PlayerController : BasePlayerController
    {
        private const float JUMP_DISTANCE = 1f;
        private const float MAX_ANGULAR_VELOCITY = 500f;

        public bool Rolling { get { return wheel.angularVelocity > 0; } }

        [SerializeField] private Rigidbody2D wheel;
        [SerializeField] private Renderer head;
        [SerializeField] private GameObject sheild;

        private Vector2 jumpForce = new Vector2();
        private Vector3 endPoint = new Vector3();
        private Transform headTransform;
        private Transform wheelTransform;
        private bool coiled; 
        
        protected override void Start()
        {
            base.Start();
            headTransform = head.transform;
            wheelTransform = wheel.transform;
        }

        // TODO:: implement update-manager. 
        private void Update()
        {
            endPoint = new Vector3(wheelTransform.position.x, wheelTransform.position.y, wheelTransform.position.z)
            {
                y = transform.position.y - JUMP_DISTANCE
            };

            //Debug.Log("sheild position : " + sheild.transform.position);

            sheild.transform.position = (headTransform.position + wheel.transform.position) / 2;

            // NOTE:: THE PLAYER OBJECT IS ONLY A CONTAINER .....
            //sheild.transform.position = transform.position;
            //Debug.Log(sheild.transform.position + " = " + transform.position);
        }

        public override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
            Debug.Log("on trigger enter 2d fired on parent delegate.");
        }

        public void roll(float torque)
        {
            //wheel.Rotate(0, 0, - torque * spinSpeed);

            if ( ! coiled) return;

            if (Mathf.Abs(wheel.angularVelocity) > MAX_ANGULAR_VELOCITY)
            {
                wheel.angularVelocity = wheel.angularVelocity > 0 ?
                    MAX_ANGULAR_VELOCITY : -MAX_ANGULAR_VELOCITY;
            }
            else
            {
                wheel.AddTorque(torque);
            }
        }

        public void Coil()
        {
            Debug.Log("Coiled...");
            coiled = true;
            head.enabled = false;
            sheild.SetActive(false);
        }

        public void UnCoil()
        {
            Debug.Log("Un-coiled...");
            coiled = false;
            head.enabled = true;
            sheild.SetActive(true);
            wheel.angularVelocity = 0;
        }

        public void jump(float force)
        {
            Debug.Log("trying to jump ...");
            if (!Physics2D.Linecast(wheelTransform.position, endPoint)) return;

            Debug.Log("jumping !!!!");
            jumpForce.y = force;
            wheel.AddForce(jumpForce);
        }

        //private void OnDrawGizmos()
        //{
        //    if ( ! Application.isPlaying) return; // wheelTransform is only set when start is called while playing. 

        //    Gizmos.color = Color.blue;
        //    Gizmos.DrawLine(wheelTransform.position, endPoint);
        //}

        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            base.OnCollisionEnter2D(collision);
            Debug.Log("collistion : " + collision);
        }
    }
}
