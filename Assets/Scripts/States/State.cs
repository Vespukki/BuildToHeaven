using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuildToHeaven.States
{
    public class State
    {
        protected StateMachine sm;
        public State(StateMachine _sm)
        {
            sm = _sm;
        }

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
    }
}
