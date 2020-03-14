using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FieldTile : MonoBehaviour, IDropHandler
{
    RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag!=null)
        {
            GameObject tile = eventData.pointerDrag;
            tile.GetComponent<RectTransform>().anchoredPosition = rectTransform.anchoredPosition;

            Vector3 tilePos= tile.GetComponent<SquareDragging>().GetStartPosition();

            if(tilePos!=transform.localPosition)
            {
                TileManager.Instance.RemoveTakenTilePosition(tilePos);
                TileManager.Instance.AddTilePosition(transform.localPosition);
            }
        }
    }
}
