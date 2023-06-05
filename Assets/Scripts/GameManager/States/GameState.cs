using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildToHeaven.States;
using BuildToHeaven.Cards;
using UnityEngine.EventSystems;

namespace BuildToHeaven.GameManagement.States
{
    public abstract class GameState : State
    {
        protected GameManager manager;
        public bool canPlayCards = true;
        public GameState(GameManager manager)
        {
            this.manager = manager;
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
