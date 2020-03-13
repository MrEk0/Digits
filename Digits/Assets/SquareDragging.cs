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
        Debug.Log(eventData.pointerDrag.gameObject);
        if (eventData.pointerDrag.GetComponent<SquareDragging>() != null)
        {
            Destroy(eventData.pointerDrag.gameObject);

            ChangeText();
        }
      
    }
}
