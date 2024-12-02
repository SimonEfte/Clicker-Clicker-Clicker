using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SetHoverCursor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Texture2D handIcon;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(MobileScript.isMobile == false) { Cursor.SetCursor(handIcon, Vector2.zero, CursorMode.Auto); }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (MobileScript.isMobile == false) { Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); }
    }
}
