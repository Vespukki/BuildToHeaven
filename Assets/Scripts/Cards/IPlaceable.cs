using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuildToHeaven.Cards
{
    public interface IPlaceable
    {
        public void Place(CardObject card);
    }
}
