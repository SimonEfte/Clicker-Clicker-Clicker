using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class InfoTooltips : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject projectileTooltip, clickerTooltip, prestigeTooltip;
    bool isProjectile, isCliker, isPrestige, isHovering;
    public TextMeshProUGUI infoText;
    public LocalizationStrings locScript;

    public void Awake()
    {
        if(gameObject.name == "InfoProjectileHover") { isProjectile = true; }
        if (gameObject.name == "ClickUpgradeInfo") { isCliker = true; }
        if (gameObject.name == "PrestigeTooltipInfo") { isPrestige = true; }
    }

    private void Update()
    {
        if(isHovering == true)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.nearClipPlane; // Set this to the distance from the camera to the object.
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            if(MobileScript.isMobile == false)
            {
                if (isProjectile == true && isHovering == true) { projectileTooltip.transform.position = worldPosition; projectileTooltip.SetActive(true); }
                if (isCliker == true && isHovering == true) { clickerTooltip.transform.position = worldPosition; clickerTooltip.SetActive(true); }
                if (isPrestige == true && isHovering == true) { prestigeTooltip.transform.position = worldPosition; prestigeTooltip.SetActive(true); }
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;

        if (isPrestige)
        {
            if(Prestige.currentUpgradeSelected == 0) { infoText.text = LocalizationStrings.diamondExplain; }
            else if (Prestige.currentUpgradeSelected == 1) { infoText.text = LocalizationStrings.emeraldExplain; }
            else if (Prestige.currentUpgradeSelected == 2) { infoText.text = LocalizationStrings.rainbowExplain; }
            else if (Prestige.currentUpgradeSelected == 3)
            {
                locScript.SetText();
                infoText.text = LocalizationStrings.purpleExplain;
            }
            else if (Prestige.currentUpgradeSelected == 4) 
            {
                locScript.SetText();
                infoText.text = LocalizationStrings.tierExplain;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        if(MobileScript.isMobile == false)
        {
            projectileTooltip.SetActive(false);
            clickerTooltip.SetActive(false);
            prestigeTooltip.SetActive(false);
        }
    }
}
