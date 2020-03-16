using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class TileDragging : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IPointerDownHandler, IDropHandler
{
    [SerializeField] TextMeshProUGUI digitText;

    private Vector3 startPos;
    private RectTransform thisRectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas;

    public int CurrentNumber { get; set; }

    private void Awake()
    {
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
        if (eventData.pointerDrag.GetComponent<TileDragging>() != null)
        {
            TileDragging tile = eventData.pointerDrag.GetComponent<TileDragging>();
           
            if (CurrentNumber == tile.CurrentNumber)
            {
                TileManager.Instance.AddAvailableTilePosition(tile.GetStartPosition());
                Destroy(tile.gameObject);
                ScoreSystem.Instance.ChangeScoreAfterSumming(CurrentNumber);
                ChangeText();
            }
            else
            {
                tile.BackToStartPosition();
            }
        }
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
