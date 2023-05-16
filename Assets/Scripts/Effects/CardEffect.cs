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

        public abstract Task Activate(Vector2 position, Card card);
    }

    public class Draw : CardEffect
    {
        public override Effect effect => Effect.Draw;

        public override async Task Activate(Vector2 position, Card card)
        {
            GameManager.instance.Draw();
            await Task.Delay(150);
            InvokeOnEffectResolve(true, this);
        }
    }

    public class Place : CardEffect
    {
        public override Effect effect => Effect.Place;

        public override async Task Activate(Vector2 position, Card card)
        {
            Block block = MonoBehaviour.Instantiate(card.Block.gameObject, position, Quaternion.identity).GetComponent<Block>();

            GameManager.instance.placedBlocks.Add(block);

            await WaitForBlockResolution(block);

            InvokeOnEffectResolve(true, this);
        }

        public async Task WaitForBlockResolution(Block block)
        {
            while (block.resolved == BlockResolution.resolving)
            {
                await Task.Yield();
            }
        }
    }

}
