using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace BuildToHeaven.Cards
{
    [CreateAssetMenu]
    public class Card : ScriptableObject
    {
        public delegate void CardResolveDelegate(Card card);
        public static event CardResolveDelegate OnCardResolved;

        [SerializeField] List<Effect> _effects;
        public List<Effect> effects => _effects;

        [SerializeField] CardEffect _effect;
        public CardEffect Effect => _effect;

        [SerializeField] string _cardName;
        public string CardName => _cardName;

        [SerializeField] Sprite _previewSprite;
        public Sprite PreviewSprite => _previewSprite;


        Dictionary<Effect, Type> effectsToTypes = new();

        private void OnEnable()
        {
            Debug.Log(this.name + " initialized");
            var abilityTypes = Assembly.GetAssembly(typeof(CardEffect)).GetTypes().Where(type => type.IsClass && !type.IsAbstract && type.IsSubclassOf(typeof(CardEffect)));


            foreach(var type in abilityTypes)
            {
                CardEffect tempEffect = Activator.CreateInstance(type) as CardEffect;
                effectsToTypes.Add(tempEffect.effect, type);
            }
        }

        public CardEffect GetCardEffect(Effect effect)
        {

            if (effectsToTypes.ContainsKey(effect))
            {
                Type type = effectsToTypes[effect];
                CardEffect tempEffect = Activator.CreateInstance(type) as CardEffect;

                return tempEffect;
            }

            return null;
        }

        public virtual async void Use(Vector2 position)
        {
            foreach(var effect in effects)
            {
                await GetCardEffect(effect)?.Activate(position);
            }

            while(OnCardResolved == null)
            {
                await Task.Yield();
            }
            OnCardResolved.Invoke(this);
        }
    }

    public enum Effect
    {
        Draw
    }


}