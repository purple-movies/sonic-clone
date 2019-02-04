using com.draconianmarshmallows.boilerplate.controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DraconianMarshmallows.Controllers
{
    public class MasterController : BaseMasterController
    {
        protected override void Start()
        {
            base.Start();
            StartGame();
        }

        public override void OnLevelCompleted(bool levelWon)
        {
            throw new System.NotImplementedException();
        }
    }
}
