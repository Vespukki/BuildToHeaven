using BuildToHeaven.GameManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace BuildToHeaven.Cards
{
    public class CardObject : MonoBehaviour
    {
        public Card card;
        private Canvas canvas;
        private Image image;
        [SerializeField] private TextMeshProUGUI cardName;

        public delegate void CardDelegate(Card card);
        public static event CardDelegate OnCardUsed;

        VerticalLayoutGroup group;

        Vector2 dragOffset;


       
        private void Awake()
        {
            Initialize(card, GetComponentInParent<Hand>());
            image = GetComponent<Image>();
        }

        public void Initialize(Card card, Hand hand)
        {
            this.card = card;
            canvas = hand.canvas;
            cardName.SetText(card.name);
        }

        private void Update()
        {
            image.raycastTarget = (GameManager.instance.currentState is PlayingState);
        }

        public void DragStartHandler(BaseEventData data)
        {
            group = GetComponentInParent<VerticalLayoutGroup>();

            PointerEventData pointerData = (PointerEventData)data;
             
            dragOffset = pointerData.position - (Vector2)transform.position;

            transform.SetParent(canvas.transform);
        }

        void ResetCardVisuals()
        {
            CardPreview.Instance.gameObject.SetActive(false);
            GetComponent<Image>().enabled = true;

            foreach (var text in GetComponentsInChildren<TextMeshProUGUI>(true))
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
            }
        }

        public void DragHandler(BaseEventData data)
        {
            PointerEventData pointerData = (PointerEventData)data;

            RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform, pointerData.position, canvas.worldCamera, out Vector2 position);

            transform.position = canvas.transform.TransformPoint(position - dragOffset);


            if (!HoveringHand(pointerData))
            {
                CardPreview.Instance.gameObject.SetActive(true);
                CardPreview.Instance.GetComponent<SpriteRenderer>().sprite = card.PreviewSprite;
                GetComponent<Image>().enabled = false;

                foreach (var text in GetComponentsInChildren<TextMeshProUGUI>(true))
                {
                    text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
                }
            }
            else
            {
                ResetCardVisuals();
                
            }

            

        }

        bool HoveringHand(PointerEventData pointerData)
        {
            List<RaycastResult> hits = new();
            EventSystem.current.RaycastAll(pointerData, hits);

            bool onHand = false;

            foreach (RaycastResult hit in hits)
            {
                if (hit.gameObject.CompareTag("Hand"))
                {
                    onHand = true;
                }
            }

            return onHand;
        }

        public void DragEndHander(BaseEventData data)
        {
            CardPreview.Instance.gameObject.SetActive(false);
            Vector2 position = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());


            if ((!HoveringHand((PointerEventData)data))) //card played;
            {
                if(card.CanPlaceAnywhere || (GameManager.instance.GetHighestBlockHeight() < Block.GetLowestPoint(CardPreview.Instance.spriter).y))
                {
                    OnCardUsed?.Invoke(card);
                    GameManager.instance.hand.cards.Remove(this);
                    card.Use(position);
                    Destroy(gameObject);
                }
            }
            else
            {
                //resets position in hand
                ResetCardVisuals();
                transform.SetParent(canvas.transform);
                transform.SetParent(group.transform);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;

            if(CardPreview.Instance != null)
            {
                Gizmos.DrawLine(new(-100f, Block.GetLowestPoint(CardPreview.Instance.spriter).y), new(100f, Block.GetLowestPoint(CardPreview.Instance.spriter).y));
            }
        }
    }
}