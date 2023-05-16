using BuildToHeaven.Cards;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

namespace BuildToHeaven.GameManagement
{
    public class PlayingState : GameState
    {
        public PlayingState(GameManager _sm) : base(_sm)
        {
        }

        Card cardPlayed = null;

        public override void Enter()
        {
            base.Enter();

            CardObject.OnCardUsed += CardPlayed;
            Cycle();

        }

        public override void Exit()
        {
            base.Exit();

            CardObject.OnCardUsed -= CardPlayed;
        }

        protected override async void Cycle()
        {
            await WaitForCardPlayed(); //past here cardPlayed is populated
            Debug.Log("card played");
            manager.ChangeState(new ResolvingState(manager, cardPlayed));
        }

        async private Task WaitForCardPlayed()
        {
            while(cardPlayed == null)
            {
                await Task.Yield();
            }
        }

        private void CardPlayed(Card card)
        {
            cardPlayed = card;
            //manager.Draw();
        }
    }
}
