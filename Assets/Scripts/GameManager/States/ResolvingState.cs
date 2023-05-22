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
        bool cardResolved = false;


        public ResolvingState(GameManager _sm, Card card) : base(_sm)
        {
            cardPlayed = card;
        }

        public override void Enter()
        {
            base.Enter();

            Card.OnCardResolved += OnCardResolved;
        }

        public override void Exit()
        {
            base.Exit();

            Card.OnCardResolved -= OnCardResolved;
        }

        protected override async void Cycle()
        {
            base.Cycle();
            await WaitForCardResolution();
            //await WaitForBlocksToSettle();
            manager.MoveBars();
            manager.ChangeState(new PlayingState(manager));
        }

        private async Task WaitForBlocksToSettle()
        {
            while(manager.placedBlocks.TrueForAll(block => block.resolved != BlockResolution.success))
            {
                await Task.Yield();
            }
        }

        private void OnCardResolved(Card card)
        {
            Debug.Log( "attempted resolved");
            if (card == cardPlayed)
            {
                cardResolved = true;
            }
            else
            {
                Debug.LogError("Card resolved was not card played");
            }
        }

        private async Task WaitForCardResolution()
        {
            while(!cardResolved)
            {
                await Task.Yield();
            }
        }
    }
}
