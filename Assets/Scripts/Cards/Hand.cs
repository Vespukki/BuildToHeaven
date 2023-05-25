using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildToHeaven.Cards;

namespace BuildToHeaven.Cards
{
    public class Hand : MonoBehaviour, IPlaceable
    {
        public Canvas canvas;
        public List<CardObject> cards;
        public Transform cardHolder;

        private void Awake()
        {
            foreach(var card in transform.GetComponentsInChildren<CardObject>())
            {
                cards.Add(card);
            }
        }

        public void Place(CardObject card)
        {
            card.ResetCardVisuals();
            card.transform.SetParent(card.canvas.transform);
            card.transform.SetParent(card.group.transform);
        }

    }
}