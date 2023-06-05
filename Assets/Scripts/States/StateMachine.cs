using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuildToHeaven.States
{
    public class StateMachine<T> where T : State
    {
        public T currentState;
        public T previousState;

        public void ChangeState(T newState)
        {

            currentState?.Exit();
            previousState = currentState;
            currentState = newState;
            newState?.Enter();
        }
    }
}
