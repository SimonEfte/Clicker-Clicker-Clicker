using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DemoScript : MonoBehaviour
{
    public static bool isDemo;
    public static bool isTesting;

    public void Awake()
    {
        SkinScript.isSkinsDLC = false;

        MobileScript.isMobile = true;

        MobileScript.isAppStoreOut = false;
        MobileScript.isGooglePlayOut = false;

        MobileScript.isThisAppStore = true;
        MobileScript.isThisGooglePlay = false;

        isDemo = false;
        isTesting = false;
        CheckDemoStuff();
    }

    public static bool isHoveringDemoStuff, isHoveringDemoStuff2;
    public static Vector2 demoTooltip1Pos, demoTooltip2Pos;
    public GameObject demoTooltip1, demoTooltip2;
    public GameObject moreLockIcons;
    public TextMeshProUGUI demoTooltipText, demoTooltipText2;

    public void Update()
    {
        Vector3 worldPosition = new Vector3(0, 0, 0);

        if (MobileScript.isMobile == false)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.nearClipPlane;
            worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        }

        if (isDemo == true)
        {
            if (isHoveringDemoStuff == true)
            {
                if (MobileScript.isMobile == false) { demoTooltip1.transform.position = worldPosition; demoTooltip1.SetActive(true); }
                if(DemoTooltip.demoLockedNumberStatic == 1) { demoTooltipText.text = LocalizationStrings.clickLocked; }
                if (DemoTooltip.demoLockedNumberStatic == 2) { demoTooltipText.text = LocalizationStrings.clickscensionLocked; }
                if (DemoTooltip.demoLockedNumberStatic == 3) { demoTooltipText.text = LocalizationStrings.achLocked; }
            }
            else if (isHoveringDemoStuff2 == true)
            {
                if (MobileScript.isMobile == false) { demoTooltip2.transform.position = worldPosition; demoTooltip2.SetActive(true); }
                if (DemoTooltip.demoLockedNumberStatic == 4) { demoTooltipText2.text = LocalizationStrings.projectileLocked; }
            }
            else
            {
                demoTooltip1.SetActive(false); demoTooltip2.SetActive(false);
            }
        }
    }

    public Button prestigeBtn, achBtn, clickUpgradesBtn;

    public void CheckDemoStuff()
    {
        if(isDemo == true)
        {
            moreLockIcons.SetActive(true);
            prestigeBtn.interactable = false; achBtn.interactable = false; clickUpgradesBtn.interactable = false;
        }
        else
        {
            moreLockIcons.SetActive(false);
        }
    }
}
