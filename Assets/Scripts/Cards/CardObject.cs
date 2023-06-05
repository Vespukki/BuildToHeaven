using BuildToHeaven.GameManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using BuildToHeaven.Effects;
using BuildToHeaven.GameManagement.States;

namespace BuildToHeaven.Cards
{
    public class CardObject : MonoBehaviour
    {
        public Card card;
        [HideInInspector] public Canvas canvas;
        private Image image;
        [SerializeField] private TextMeshProUGUI cardName;

        public delegate void CardDelegate(Card card);
        public static event CardDelegate OnCardUsed;

        [HideInInspector] public VerticalLayoutGroup group;

        Vector2 dragOffset;

        private bool CanPlayCards { get { return GameManager.instance.stateMachine.currentState.canPlayCards; } }

       
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

        public void StartDragging(PointerEventData data)
        {
            group = GetComponentInParent<VerticalLayoutGroup>();
            dragOffset = data.position - (Vector2)transform.position;
            transform.SetParent(canvas.transform);
        }

        public void DragStartHandler(BaseEventData data)
        {
            StartDragging((PointerEventData)data);
        }

        public void ShowCard()
        {
            CardPreview.Instance.gameObject.SetActive(false);
            GetComponent<Image>().enabled = true;

            foreach (var text in GetComponentsInChildren<TextMeshProUGUI>(true))
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
            }
        }

        public void Drag(PointerEventData data)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform, data.position, canvas.worldCamera, out Vector2 position);

            transform.position = canvas.transform.TransformPoint(position - dragOffset);

            IPlaceable placeable = GetHoveredPlaceable(data);

            if (placeable != null && !placeable.ShowPreview)
            {
                ShowCard();
            }
            else
            {
                CardPreview.Instance.gameObject.SetActive(true);
                CardPreview.Instance.GetComponent<SpriteRenderer>().sprite = card.PreviewSprite;
                GetComponent<Image>().enabled = false;

                foreach (var text in GetComponentsInChildren<TextMeshProUGUI>(true))
                {
                    text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
                }
            }
        }

        public void DragHandler(BaseEventData data)
        {
            Drag((PointerEventData)data);

        }

        private IPlaceable GetHoveredPlaceable(PointerEventData pointerData)
        {
            List<RaycastResult> hits = new();
            EventSystem.current.RaycastAll(pointerData, hits);

            foreach(RaycastResult hit in hits)
            {
                if(hit.gameObject.TryGetComponent(out IPlaceable iPlaceable))
                {
                    return iPlaceable;
                }
            }

            return null;
        }

        public void PlaceCard(PointerEventData data) //placing is putting it somewhere, playing is activating it
        {
            CardPreview.Instance.gameObject.SetActive(false);
            Vector2 position = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            bool canPlay = (card.CanPlayAnywhere || (GameManager.instance.GetHighestBlockHeight() < Block.GetLowestPoint(CardPreview.Instance.spriter).y));

            IPlaceable placeable = GetHoveredPlaceable((PointerEventData)data);
            if (placeable != null) //place the card
            {
                Debug.Log(placeable.ToString());
                placeable.Place(this);
                
            }
            else if(canPlay && CanPlayCards) //play the card
            {
                OnCardUsed?.Invoke(card);
                GameManager.instance.hand.cards.Remove(this);
                card.Use(position);
                Destroy(gameObject);
            }
            else //return it to its last spot
            {
                ReturnToLastSpot();
            }
        }

        public void ReturnToLastSpot()
        {
            ShowCard();
            transform.SetParent(canvas.transform);
            transform.SetParent(group.transform);
        }

        public void DragEndHander(BaseEventData data)
        {
            PlaceCard((PointerEventData)data);
        }
    }
}