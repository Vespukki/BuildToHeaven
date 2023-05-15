using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuildToHeaven.Cards
{
    public class CardEffect : MonoBehaviour
    {
        public delegate void EffectResolveDelegate(bool result, CardEffect effect);
        public static event EffectResolveDelegate OnEffectResolved;

        protected void InvokeOnEffectResolve(bool result, CardEffect effect)
        {
            OnEffectResolved?.Invoke(result, effect);
        }

        public virtual void Activate(Vector2 position)
        {
        }
    }
}
