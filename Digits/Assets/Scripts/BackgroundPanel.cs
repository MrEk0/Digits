using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackgroundPanel : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag!=null)
        {
            eventData.pointerDrag.GetComponent<TileDragging>().BackToStartPosition();
        }
    }
}
