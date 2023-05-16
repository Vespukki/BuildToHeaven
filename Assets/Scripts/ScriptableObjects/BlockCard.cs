using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildToHeaven.Cards;

namespace BuildToHeaven
{
    public class BlockCard : Card
    {
        [SerializeField] Block _block;
        public Block HeldBlock => _block;

        public override void Use(Vector2 position)
        {
            base.Use(position);

        }
    }
}
