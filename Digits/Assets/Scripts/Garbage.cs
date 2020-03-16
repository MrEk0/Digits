using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Garbage : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag!=null)
        {
            GameObject tile = eventData.pointerDrag;
            Vector3 tileStartPos = tile.GetComponent<TileDragging>().GetStartPosition();
            TileManager.Instance.AddAvailableTilePosition(tileStartPos);
            Destroy(tile);
        }
    }
}
