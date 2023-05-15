using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuildToHeaven.Cards
{
    public class Deck : MonoBehaviour
    {
        public List<Card> cards;

        public Card Draw()
        {
            if(cards.Count > 0)
            {
                Card target = cards[0];
                cards.Remove(target);
                return target;
            }
            else
            {
                Debug.Log("deck empty");
                return null;
            }
        }
    }
}