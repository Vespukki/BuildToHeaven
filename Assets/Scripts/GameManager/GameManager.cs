using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildToHeaven.Cards;
using BuildToHeaven.States;
namespace BuildToHeaven.GameManagement
{
    public class GameManager : StateMachine
    {
        public static GameManager instance;
        public Hand hand;
        public Deck deck;

        public CameraController cam;

        [SerializeField] CardObject cardObject;

        public List<Block> placedBlocks = new();
        public GameObject platform;

        private void OnEnable()
        {
            Block.OnFailedResolution += OnFailedResolution;
        }
        private void OnDisable()
        {
            Block.OnFailedResolution -= OnFailedResolution;
        }


        private void Awake()
        {
            if(instance != null)
            {
                Destroy(this);
            }
            instance = this;
            ChangeState(new PlayingState(this));
        }

        protected override void Update()
        {
            base.Update();

            Debug.Log(currentState.GetType());
        }

        void OnFailedResolution(Block block)
        {
            Debug.Log("THE GAME ENDED ITS OVER YOU LOST GO HOME");
        }

        public void Draw()
        {
            Card newCard = deck.Draw();
            if (newCard == null)
            {
                return;
            }
            cardObject.gameObject.SetActive(false);
            GameObject newCardGO = Instantiate(cardObject.gameObject, hand.transform);
            cardObject.gameObject.SetActive(true);


            CardObject newCardObj = newCardGO.GetComponent<CardObject>();
            newCardObj.card = newCard;
            newCardGO.SetActive(true);

            hand.cards.Add(newCardObj);
        }
    }
}
