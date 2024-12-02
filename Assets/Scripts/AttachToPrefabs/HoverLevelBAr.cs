using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class HoverLevelBAr : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI goldNeededText, clickscensionCoinsGetText;
    public GameObject basicGoldText;
    public static bool isHover;

    private void Update()
    {
        if(isHover == true)
        {
            clickscensionCoinsGetText.text = $"+{(LevelUp.clickPointToGet).ToString("F0")}";

            goldNeededText.text = $"{ScaleNumbers.FormatPoints(LevelUp.currentPrestigeCoins)}/{ScaleNumbers.FormatPoints(LevelUp.goldNeeded)}";

             
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (MobileScript.isMobile == false)
        {
            basicGoldText.SetActive(false);
            goldNeededText.gameObject.SetActive(true); clickscensionCoinsGetText.gameObject.SetActive(true);
            isHover = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (MobileScript.isMobile == false)
        {
            isHover = false;
            basicGoldText.SetActive(true);
            goldNeededText.gameObject.SetActive(false);
            clickscensionCoinsGetText.gameObject.SetActive(false);
        }
    }
}
