using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class LevelUp : MonoBehaviour, IDataPersistence
{
    public static int level, totalLevel;
    public static double goldNeeded;
    public Image purpleLine;
    public static float clickScensionCoins;
    public static double currentPrestigeCoins;
    public double tierMultiplier = 1;
    public double baseValue = 200000;
    public Achievements achScript;

    private void Update()
    {
        if (currentPrestigeCoins >= goldNeeded)
        {
            currentPrestigeCoins = 0;

            GiveClickscensionCoins(clickPointToGet);

            level++;
            totalLevel++;

            if (level >= 5)
            {
                tierMultiplier *= 1000;
                level = 0;

                clickScensionCoins *= 1.75f;
            }
            else
            {
                clickScensionCoins += 1;
            }

            goldNeeded = baseValue * tierMultiplier * (level + 1);
        }

        integerPart = Mathf.FloorToInt(clickScensionCoins * (1 + Prestige.clickscensionCoinIncrease));
        float decimalPart = clickScensionCoins * (1 + Prestige.clickscensionCoinIncrease) - integerPart;

        if (decimalPart >= 0.5f)
        {
            clickPointToGet = integerPart + 1;
        }
        else
        {
            clickPointToGet = integerPart;
        }

        purpleLine.fillAmount = (float)(currentPrestigeCoins / goldNeeded);
    }
    public static int integerPart, clickPointToGet;

    public void GiveClickscensionCoins(float clickPoints)
    {
        integerPart = Mathf.FloorToInt(clickPoints);
        float decimalPart = clickPoints - integerPart;

        if (decimalPart >= 0.5f)
        {
            Prestige.clickscensionCoinsGet += (clickPointToGet);
            StartCoroutine(ClickscensionCoinAnim(clickPointToGet));
        }
        else
        {
            Prestige.clickscensionCoinsGet += (clickPointToGet);
            StartCoroutine(ClickscensionCoinAnim(clickPointToGet));
        }

        achScript.CheckAchievementsProgress(50);
    }

    public AudioManager audioManager;
    public TextMeshProUGUI plussPrestigeCoinsText;

    IEnumerator ClickscensionCoinAnim(float clickPoints)
    {
        audioManager.Play("LevelUp");
        plussPrestigeCoinsText.text = $"+{clickPoints.ToString("F0")}";
        plussPrestigeCoinsText.gameObject.SetActive(true);
        plussPrestigeCoinsText.gameObject.GetComponent<Animation>().Play();
        yield return new WaitForSeconds(2);
        plussPrestigeCoinsText.gameObject.SetActive(false);
    }

    public void ResetLevelUp()
    {
        clickScensionCoins = 1;
        level = 0;
        totalLevel = 0;
        tierMultiplier = 1;
        goldNeeded = 200000;
        currentPrestigeCoins = 0;
        purpleLine.fillAmount = (float)(currentPrestigeCoins / goldNeeded);
    }

    bool isSet;
    public GameObject basicGoldText, goldNeededText, clickscensionCoinsGetText;

    public void MobileSetLevelup()
    {
        if(isSet == false) 
        {
            basicGoldText.SetActive(false);
            goldNeededText.gameObject.SetActive(true); clickscensionCoinsGetText.gameObject.SetActive(true);
            isSet = true; HoverLevelBAr.isHover = true; 
        }
        else
        {
            basicGoldText.SetActive(true);
            goldNeededText.gameObject.SetActive(false); clickscensionCoinsGetText.gameObject.SetActive(false);
            isSet = false; HoverLevelBAr.isHover = false; 
        }
    }

    #region Load Data
    public void LoadData(GameData data)
    {
        goldNeeded = data.goldNeeded;
        level = data.level;
        totalLevel = data.totalLevel;
        currentPrestigeCoins = data.currentPrestigeCoins;
        tierMultiplier = data.tierMultiplier;
        baseValue = data.baseValue;
        clickScensionCoins = data.clickScensionCoins;
    }
    #endregion

    #region Save Data
    public void SaveData(ref GameData data)
    {
        data.goldNeeded = goldNeeded;
        data.level = level;
        data.totalLevel = totalLevel;
        data.currentPrestigeCoins = currentPrestigeCoins;
        data.tierMultiplier = tierMultiplier;
        data.baseValue = baseValue;
        data.clickScensionCoins = clickScensionCoins;
    }
    #endregion
}
