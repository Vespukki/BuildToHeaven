using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuildToHeaven.Cards
{
    public interface IPlaceable
    {
        public void Place(CardObject card);

        public bool ShowPreview { get; } //should the card preview be shown when the card is hovered over this?
    }
}
