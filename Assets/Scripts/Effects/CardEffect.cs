using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildToHeaven.GameManagement;
using System.Threading.Tasks;

namespace BuildToHeaven.Cards
{
    public abstract class CardEffect
    {
        public delegate void EffectResolveDelegate(bool result, CardEffect effect);
        public static event EffectResolveDelegate OnEffectResolved;

        public abstract Effect effect { get; }

        protected void InvokeOnEffectResolve(bool result, CardEffect effect)
        {
            OnEffectResolved?.Invoke(result, effect);
        }

        public virtual async Task Activate(Vector2 position)
        {
        }
    }

    public class Draw : CardEffect
    {
        public override Effect effect => Effect.Draw;

        public override async Task Activate(Vector2 position)
        {
            await base.Activate(position);

            GameManager.instance.Draw();
            InvokeOnEffectResolve(true, this);
        }
    }

}
