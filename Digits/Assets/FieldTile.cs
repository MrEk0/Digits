using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FieldTile : MonoBehaviour, IDropHandler
{
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag!=null)
        {
            GameObject tile = eventData.pointerDrag;

            if (!TileManager.Instance.IsTileAvailable(transform.localPosition))
            {
                tile.GetComponent<TileDragging>().BackToStartPosition();
                return;
            }

            tile.GetComponent<RectTransform>().anchoredPosition = rectTransform.anchoredPosition;

            Vector3 tileStartPos= tile.GetComponent<TileDragging>().GetStartPosition();

            if (tileStartPos != transform.localPosition)
            {
                TileManager.Instance.AddAvailableTilePosition(tileStartPos);
                TileManager.Instance.RemoveAvailableTilePosition(transform.localPosition);
            }
        }
    }
}
