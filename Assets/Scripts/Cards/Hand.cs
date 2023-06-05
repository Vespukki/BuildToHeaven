using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildToHeaven.Cards;
using UnityEngine.UI;

namespace BuildToHeaven.Cards
{
    public class Hand : MonoBehaviour, IPlaceable
    {
        public Canvas canvas;
        public List<CardObject> cards;
        public Transform cardHolder;
        [SerializeField] private VerticalLayoutGroup group;

        [HideInInspector] public bool ShowPreview => false;

        private void Awake()
        {
            foreach(var card in transform.GetComponentsInChildren<CardObject>())
            {
                cards.Add(card);
            }
        }

        public void Place(CardObject card)
        {
            card.ShowCard();
            card.transform.SetParent(canvas.transform);
            card.transform.SetParent(group.transform);
        }

    }
}