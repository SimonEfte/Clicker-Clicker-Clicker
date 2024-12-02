using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DemoTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int demoLockedNumber;
    public static int demoLockedNumberStatic;
    public bool hoveringInfo;
    public GameObject infoTooltip;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(gameObject.name == "InfoHover") { hoveringInfo = true; }
        else
        {
            //Demo hover
            demoLockedNumberStatic = demoLockedNumber;
            if (demoLockedNumberStatic < 4) { DemoScript.isHoveringDemoStuff = true; }
            else { DemoScript.isHoveringDemoStuff2 = true; }
        }
    }

    private void Update()
    {
        if(hoveringInfo == true)
        {
            if(MobileScript.isMobile == false)
            {
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = Camera.main.nearClipPlane; // Set this to the distance from the camera to the object.
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                infoTooltip.transform.position = worldPosition;
                infoTooltip.SetActive(true);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(hoveringInfo == true)
        {
            infoTooltip.SetActive(false);
        }
        hoveringInfo = false;
        DemoScript.isHoveringDemoStuff2 = false; DemoScript.isHoveringDemoStuff = false;
    }
}
