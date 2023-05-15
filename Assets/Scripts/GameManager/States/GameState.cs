using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildToHeaven.States;

namespace BuildToHeaven.GameManagement
{
    public class GameState : State
    {
        protected GameManager manager;

        public GameState(GameManager _sm) : base(_sm)
        {
            manager = _sm;
        }

        public override void Enter()
        {
            base.Enter();

            Cycle();
        }

        protected virtual void Cycle()
        {
        }
    }
}
