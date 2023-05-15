using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace BuildToHeaven.Cards
{
    [CreateAssetMenu]
    public class Card : ScriptableObject
    {
        [SerializeField] CardEffect _effect;
        public CardEffect Effect => _effect;

        [SerializeField] string _cardName;
        public string CardName => _cardName;


        public virtual void Use(Vector2 position)
        {
            Effect.Activate(position);
        }

        public Sprite PreviewSprite
        {
            get { return Effect.GetComponent<SpriteRenderer>().sprite; }
        }
    }

}