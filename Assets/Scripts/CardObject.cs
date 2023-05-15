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
        [SerializeField] private TextMeshProUGUI cardName;

        public delegate void CardDelegate(Card card);
        public static event CardDelegate OnCardUsed;

        HorizontalLayoutGroup group;

        Vector2 dragOffset;

        private void Awake()
        {
            Initialize(card, GetComponentInParent<Hand>());
        }

        public void Initialize(Card card, Hand hand)
        {
            this.card = card;
            canvas = hand.canvas;
            cardName.SetText(card.name);
        }

        public void DragStartHandler(BaseEventData data)
        {
            group = GetComponentInParent<HorizontalLayoutGroup>();

            PointerEventData pointerData = (PointerEventData)data;

            dragOffset = pointerData.position - (Vector2)transform.position;
        }

        public void DragHandler(BaseEventData data)
        {
            PointerEventData pointerData = (PointerEventData)data;

       

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
                CardPreview.Instance.gameObject.SetActive(false);
                GetComponent<Image>().enabled = true;

                foreach (var text in GetComponentsInChildren<TextMeshProUGUI>(true))
                {
                    text.color =new Color(text.color.r, text.color.g, text.color.b, 1);
                }
            }

            RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform, pointerData.position, canvas.worldCamera, out Vector2 position); 

            transform.position = canvas.transform.TransformPoint(position - dragOffset);

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

            if (!HoveringHand((PointerEventData)data)) //card played;
            {
                Vector2 position = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                Debug.Log("Card used");
                OnCardUsed?.Invoke(card);
                GameManager.instance.hand.cards.Remove(this);
                card.Use(position);
                Destroy(gameObject);

            }
            else
            {
                //resets position in hand
                transform.SetParent(canvas.transform);
                transform.SetParent(group.transform);
            }
        }
    }
}