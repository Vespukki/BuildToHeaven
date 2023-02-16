using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CardObject : MonoBehaviour
{
    StateMachine stateMachine;

    [SerializeField] private Card card;
    [SerializeField] private Canvas canvas;

    HorizontalLayoutGroup group;

    Vector2 dragOffset;

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

        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform, pointerData.position, canvas.worldCamera, out position);

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
      

        if(!HoveringHand((PointerEventData)data))
        {
            Vector2 position = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
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
