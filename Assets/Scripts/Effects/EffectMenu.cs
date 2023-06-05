using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildToHeaven.Cards;
using System.Linq;

namespace BuildToHeaven.Effects
{
    public abstract class EffectMenu : MonoBehaviour
    {
        public List<CardSlot> slots = new();

        protected virtual void Effect()
        {

        }

        protected virtual void FixedUpdate()
        {
            if(slots.TrueForAll(slot => slot.held != null))
            {
                Effect();
                Destroy(gameObject);
            }
        }
    }
}
