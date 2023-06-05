using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuildToHeaven.Cards
{
    public class CardSlot : MonoBehaviour, IPlaceable
    {
        public bool ShowPreview => false;

        public CardObject held { get { return GetComponentInChildren<CardObject>(); } }


        public void Place(CardObject card)
        {
            if(held != null)
            {
                card.ReturnToLastSpot(); 
            }
            else
            {
                card.transform.position = transform.position;
                card.transform.SetParent(transform);
            }
            
        }
    }
}
