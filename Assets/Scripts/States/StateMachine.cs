using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuildToHeaven.States
{
    public class StateMachine : MonoBehaviour
    {
        public State currentState;
        public State previousState;

        public void ChangeState(State newState)
        {

            currentState?.Exit();
            previousState = currentState;
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
