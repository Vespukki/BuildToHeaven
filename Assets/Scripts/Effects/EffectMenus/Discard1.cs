using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuildToHeaven.Effects.EffectMenus
{
    public class Discard1 : EffectMenu
    {
        protected override void Effect()
        {
            base.Effect();
            
            foreach(var slot in slots)
            {
                Destroy(slot.held?.gameObject);
            }
        }
    }
}
