using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildToHeaven.Cards;
using BuildToHeaven.States;
using System.Linq;
using System;
using BuildToHeaven.Effects;

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

        public LoseBar loseBar;
        public Transform placementBar;
        public float lossOffset;

        public float LossHeight { get { return cam.currentHeight + lossOffset; } }

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

        private void Start()
        {
            MoveBars();
        }

        public void MoveBars()
        {
            cam.MoveCamera();


            if (loseBar.transform.position.y != LossHeight)
            {
                loseBar.transform.position = new(platform.transform.position.x, LossHeight, 0);
            }
            placementBar.transform.position = new(platform.transform.position.x, GetHighestBlockHeight());
        }

        public float GetHighestBlockHeight()
        {
            float max = Block.GetHighestPoint(platform.GetComponent<Collider2D>()).y;
            foreach (var block in placedBlocks)
            {
                float highPoint = Block.GetHighestPoint(block.coll).y;

                if (highPoint > max)
                {
                    max = highPoint;
                }
            }
            return max;
        }

        void OnFailedResolution(Block block)
        {
            Debug.Log("THE GAME ENDED ITS OVER YOU LOST GO HOME");
            Time.timeScale = 0;
        }

        public void AddBlock(Block block)
        {
            placedBlocks.Add(block);
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


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;

            //Gizmos.DrawLine(new(-100, GetHighestBlockHeight()), new(100, GetHighestBlockHeight()));
        }
    }
}
