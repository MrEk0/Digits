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

    public int CurrentNumber { get; set; }

    private void Awake()
    {
        //digitText.text = CurrentNumber.ToString();
        thisRectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        digitText.text = CurrentNumber.ToString();
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

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
    }

    public void ChangeText()
    {
        CurrentNumber++;
        TileManager.Instance.SetMaxTileNumber(CurrentNumber);
        digitText.text = CurrentNumber.ToString();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        thisRectTransform.SetAsLastSibling();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.GetComponent<SquareDragging>() != null)
        {
            SquareDragging square = eventData.pointerDrag.GetComponent<SquareDragging>();

            if (CurrentNumber == square.GetNumber())
            {
                TileManager.Instance.RemoveTakenTilePosition(square.GetStartPosition());
                Destroy(square.gameObject);
                TileManager.Instance.ChangeScore();
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

    public Vector3 GetStartPosition()
    {
        return startPos;
    }

    public void BackToStartPosition()
    {
        transform.localPosition = startPos;
    }
}
