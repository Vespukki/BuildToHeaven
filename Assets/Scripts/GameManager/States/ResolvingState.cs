using BuildToHeaven.Cards;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace BuildToHeaven.GameManagement
{
    public class ResolvingState : GameState
    {
        Card cardPlayed;
        CardEffect effect = null;
        bool effectResult = false;

        public ResolvingState(GameManager _sm, Card card) : base(_sm)
        {
            cardPlayed = card;
        }

        public override void Enter()
        {
            base.Enter();

            CardEffect.OnEffectResolved += OnEffectResolve;
        }

        public override void Exit()
        {
            base.Exit();

            CardEffect.OnEffectResolved -= OnEffectResolve;
        }

        protected override async void Cycle()
        {
            base.Cycle();
            await WaitForEffectResolution();

            Debug.Log("effect resolved: " + effectResult.ToString());

            manager.ChangeState(new PlayingState(manager));
        }

        private void OnEffectResolve(bool result, CardEffect effect)
        {
            effectResult = result;
            this.effect = effect;
        }

        private async Task WaitForEffectResolution()
        {
            while(effect == null)
            {
                await Task.Yield();
            }
        }
    }
}
