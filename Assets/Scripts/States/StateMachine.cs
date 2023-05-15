using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuildToHeaven.States
{
    public class StateMachine : MonoBehaviour
    {
        protected State currentState;
        protected State previousState;

        public void ChangeState(State newState)
        {
            currentState?.Exit();
            currentState = newState;
            newState?.Enter();
        }

        protected virtual void FixedUpdate()
        {
            currentState.FixedUpdate();

        }

        protected virtual void Update()
        {
            currentState.Update();

        }
    }
}
