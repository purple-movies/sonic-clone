using UnityEngine;
using System.Collections;
using com.draconianmarshmallows.boilerplate.controllers;

namespace DraconianMarshmallows.Controllers
{
    public class PlayerController : BasePlayerController
    {
        private const float JUMP_DISTANCE = 1f;
        private const float MAX_ANGULAR_VELOCITY = 500f;
        //[SerializeField] private Rigidbody2D body;
        [SerializeField] private Rigidbody2D wheel;
        //[SerializeField] private Transform wheel;
        //[SerializeField] private float spinSpeed = 2;

        private Vector2 jumpForce = new Vector2();
        private Vector3 endPoint = new Vector3();
        private Transform wheelTransform;
        
        protected override void Start()
        {
            base.Start();
            wheelTransform = wheel.transform;
        }

        private void Update()
        {
            endPoint = new Vector3(wheelTransform.position.x, wheelTransform.position.y, wheelTransform.position.z)
            {
                y = transform.position.y - JUMP_DISTANCE
            };
        }

        public void roll(float torque)
        {
            //wheel.Rotate(0, 0, - torque * spinSpeed);

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

        public void jump(float force)
        {
            Debug.Log("trying to jump ...");
            if (!Physics2D.Linecast(wheelTransform.position, endPoint)) return;

            Debug.Log("jumping !!!!");
            jumpForce.y = force;
            wheel.AddForce(jumpForce);
        }

        private void OnDrawGizmos()
        {
            if ( ! Application.isPlaying) return; // wheelTransform is only set when start is called while playing. 

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(wheelTransform.position, endPoint);
        }

        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            base.OnCollisionEnter2D(collision);
            Debug.Log("collistion : " + collision);
        }
    }
}
