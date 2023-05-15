using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildToHeaven.Cards;

namespace BuildToHeaven.Cards
{
    public class Hand : MonoBehaviour
    {
        public Canvas canvas;
        public List<CardObject> cards;

        private void Awake()
        {
            foreach(var card in transform.GetComponentsInChildren<CardObject>())
            {
                cards.Add(card);
            }
        }

      
    }
}