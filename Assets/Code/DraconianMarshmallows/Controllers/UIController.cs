using UnityEngine;
using System.Collections;
using com.draconianmarshmallows.boilerplate.controllers;
using com.draconianmarshmallows.boilerplate.managers;
using com.draconianmarshmallows.boilerplate;

namespace DraconianMarshmallows.Controllers
{
    public class UIController : BaseUIController, IUpdateable
    {
        private const string HORIZONTAL = "Horizontal";
        private const string JUMP       = "Jump";
        private const float ROLL_TORQUE = 5f;
        private const float JUMP_FORCE  = 500f;

        private PlayerController playerController;

        private float horizontalAxis;

        protected override void Start()
        {
            base.Start();
            UpdateManager.Instance.AddUpdateable(this);
        }

        public override void OnLevelStarted(BaseLevelController levelController)
        {
            playerController = BasePlayerController.Instance as PlayerController;
        }

        public void OnUpdate()
        {
            horizontalAxis = Input.GetAxis(HORIZONTAL);

            if (horizontalAxis < 0)
            {
                playerController.roll(ROLL_TORQUE);
            }
            else if (horizontalAxis > 0)
            {
                playerController.roll(-ROLL_TORQUE);
            }

            if (Input.GetButtonDown(JUMP))
                playerController.jump(JUMP_FORCE);
        }
    }
}
