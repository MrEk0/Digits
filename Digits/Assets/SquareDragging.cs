using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class SquareDragging : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IPointerDownHandler, IDropHandler
{
    [SerializeField] TextMeshProUGUI digitText;

    Vector3 startPos;
    RectTransform thisRectTransform;
    CanvasGroup canvasGroup;
    Canvas canvas;
    int currentNumber;

    private void Awake()
    {
        currentNumber = Convert.ToInt32(digitText.text);
        thisRectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        startPos = transform.localPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        thisRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void ChangeText()
    {
        currentNumber++;
        digitText.text = currentNumber.ToString();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        thisRectTransform.SetAsLastSibling();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.GetComponent<SquareDragging>() != null)
        {
            SquareDragging square = eventData.pointerDrag.GetComponent<SquareDragging>();

            if (currentNumber == square.GetNumber())
            {
                Destroy(square.gameObject);

                ChangeText();
            }
            else
            {
                square.BackToStartPosition();
            }
        }    
    }

    public int GetNumber()
    {
        return Convert.ToInt32(digitText.text);
    }

    public void BackToStartPosition()
    {
        transform.localPosition = startPos;
    }
}
