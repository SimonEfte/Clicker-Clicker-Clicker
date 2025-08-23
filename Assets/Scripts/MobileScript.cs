using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Advertisements;
using UnityEngine.Purchasing;
using System;

public class MobileScript : MonoBehaviour
{
    public static bool isMobile, isGooglePlayOut, isAppStoreOut;
    public static bool isThisGooglePlay, isThisAppStore;

    public GameObject closeProjectileBTN, closeClickUpgradeBTN, closeClickscensionBTN, closeStatsBTN, closeACHBtn, closeSettingsBTN, closeSkinsBTN;
    public GameObject close1, close2, close3, close4, close5, close6, close7;

    public static bool isAdsRemoved;

    public AudioManager audioManager;

    public GameObject autoActiveBTN;

    public static MobileScript Instance { get; private set; }

    public int adWaitTime;

    #region Awake
    private void Awake()
    {
        if (isMobile == true)
        {
            adWaitTime = 30;

            Application.targetFrameRate = 60;

            SetMobileUI();
            SetProjectuleFrame();
            SetClickUpgradeFrame();
            SetClickscensionFrame();
            SetStatsFrame();
            SetAchievementFrame();
            SetSettingsFrame();
            SetSknisFrame();

            closeProjectileBTN.SetActive(true); closeClickUpgradeBTN.SetActive(true); closeClickscensionBTN.SetActive(true); closeStatsBTN.SetActive(true);
            closeACHBtn.SetActive(true); closeSettingsBTN.SetActive(true); closeSkinsBTN.SetActive(true);

            close1.SetActive(false); close2.SetActive(false); close3.SetActive(false); close4.SetActive(false);
            close5.SetActive(false); close6.SetActive(false); close7.SetActive(false);

            closeProjectileBTN.transform.localPosition = new Vector2(-20.4f, -86f);
            closeProjectileBTN.transform.localScale = new Vector2(0.6f, 0.6f);

            closeClickUpgradeBTN.transform.localPosition = new Vector2(-20.4f, -86f);
            closeClickUpgradeBTN.transform.localScale = new Vector2(0.6f, 0.6f);

            closeClickscensionBTN.transform.localPosition = new Vector2(0f, -87.6f);
            closeClickscensionBTN.transform.localScale = new Vector2(0.6f, 0.6f);

            closeACHBtn.transform.localPosition = new Vector2(-235f, -803f);
            closeACHBtn.transform.localScale = new Vector2(6.1f, 6.1f);

            closeStatsBTN.transform.localPosition = new Vector2(0, -845f);
            closeStatsBTN.transform.localScale = new Vector2(5.1f, 5.1f);

            closeSettingsBTN.transform.localPosition = new Vector2(0, -1119);
            closeSettingsBTN.transform.localScale = new Vector2(6.6f, 6.6f);

            closeSkinsBTN.transform.localPosition = new Vector2(0, -554);
            closeSkinsBTN.transform.localScale = new Vector2(9f, 9f);

            if(MobileScript.isThisAppStore == false)
            {
                StartCoroutine(SetAdBtn());
            }
        }
        else
        {
            SetAnchor(projectileBtn.GetComponent<RectTransform>(), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f));
            SetAnchor(clickUpgradeBtn.GetComponent<RectTransform>(), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f));
            SetAnchor(prestigeBtn.GetComponent<RectTransform>(), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f));
            SetAnchor(statsBtn.GetComponent<RectTransform>(), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f));
            SetAnchor(achBtn.GetComponent<RectTransform>(), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f));
            SetAnchor(autoActiveBTN.GetComponent<RectTransform>(), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f));

            autoActiveBTN.transform.localScale = new Vector2(0.75f, 0.75f);
            autoActiveBTN.transform.localPosition = new Vector2(870, -387);
            projectileBtn.transform.localPosition = new Vector2(870, 286);
            clickUpgradeBtn.transform.localPosition = new Vector2(870, 146);
            prestigeBtn.transform.localPosition = new Vector2(870, 6);
            statsBtn.transform.localPosition = new Vector2(870, -132);
            achBtn.transform.localPosition = new Vector2(870, -272);
        }
    }
    #endregion

    

    #region all AD stuff
    public GameObject cgAdBtn, clickscensionAdBtn, adFrame, plussGoldText, plussClickscensionText, claimRewardFrame, claimGoldText, claimClickscensionText;
    public int adChosen;

    public static bool isGoldReward;
    public bool isGameLoaded;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            ScreenCapture.CaptureScreenshot("screenshot-" + DateTime.Now.ToString("yyyy-mm-dd-hh-sss") + ".png", 1);
        }

        if (isRewarded == true)
        {
            isRewarded = false;

            if(isGameLoaded == true)
            {
                if (isGoldReward == true) { claimGoldText.SetActive(true); claimClickscensionText.SetActive(false); }
                else { claimClickscensionText.SetActive(true); claimGoldText.SetActive(false); }
                claimRewardFrame.SetActive(true);
            }
        }
    }

    bool firstTime;

    IEnumerator SetAdBtn()
    {
        yield return new WaitForSeconds(adWaitTime);
        isGameLoaded = true;
        //yield return new WaitForSeconds(5);

        isGoldReward = false;

        if (firstTime == false) { firstTime = true; adChosen = 1; adWaitTime = 170; }
        else 
        {
            if(adChosen == 1) { adChosen = 2; }
            else { adChosen = 1; }
        }

        if (adChosen == 1)
        {
            cgAdBtn.SetActive(true); plussGoldText.SetActive(true); plussClickscensionText.SetActive(false); isGoldReward = true;
            claimGoldText.SetActive(true); claimClickscensionText.SetActive(false);
        }
        else 
        { 
            clickscensionAdBtn.SetActive(true); plussClickscensionText.SetActive(true); plussGoldText.SetActive(false); isGoldReward = false;
            claimClickscensionText.SetActive(true); claimGoldText.SetActive(false);
        }
    }

    public void ClaimReward()
    {
        audioManager.Play("UI_Click1");

        if (isGoldReward == true) 
        { 
            MainCursorClick.totalClickPoints += goldRewardAmount;
            Stats.totalGold += goldRewardAmount;
            LevelUp.currentPrestigeCoins += goldRewardAmount;
        }
        else 
        {
            Prestige.clickscensionCoinsGet += Mathf.FloorToInt(Upgrades.currentClickscensionCoins);
        }

        claimRewardFrame.SetActive(false);

        StartCoroutine(SetAdBtn());
    }

    public void OpenAdFrame()
    {
        audioManager.Play("UI_Click1");

        cgAdBtn.SetActive(false);
        clickscensionAdBtn.SetActive(false);

        if (isGoldReward == true)
        {
            goldRewardAmount = 0;

            int goldtimes = 1100;
            int clickscensionTimes = 155;

            if(Prestige.fallingCursorTier == 2)
            {
                goldtimes = 1200;
                clickscensionTimes = 165;
            }
            if (Prestige.fallingCursorTier > 2)
            {
                goldtimes = 1400;
                clickscensionTimes = 180;
            }

            goldRewardAmount += (MainCursorClick.cursorClickPoint * (1 + Prestige.activeGoldIncrease)) * goldtimes;
            goldRewardAmount += (MainCursorClick.totalPassivePoints * (1 + Prestige.passiveGoldIncrease)) * clickscensionTimes;

            //Debug.Log((MainCursorClick.cursorClickPoint * (1 + Prestige.activeGoldIncrease)) * goldtimes);
            //Debug.Log((MainCursorClick.totalPassivePoints * (1 + Prestige.passiveGoldIncrease)) * clickscensionTimes);

            plussGoldText.GetComponent<TextMeshProUGUI>().text = $"+{ScaleNumbers.FormatPoints(goldRewardAmount)}";
            claimGoldText.GetComponent<TextMeshProUGUI>().text = $"+{ScaleNumbers.FormatPoints(goldRewardAmount)}";
        }
        else
        {
            clickscensionRewardAmount = Mathf.FloorToInt(LevelUp.clickScensionCoins * (1 + Prestige.clickscensionCoinIncrease));

            if(Mathf.FloorToInt(LevelUp.clickScensionCoins * (1 + Prestige.clickscensionCoinIncrease)) > Upgrades.currentClickscensionCoins)
            {
                Upgrades.currentClickscensionCoins = Mathf.FloorToInt(LevelUp.clickScensionCoins * (1 + Prestige.clickscensionCoinIncrease));
            }

            claimClickscensionText.GetComponent<TextMeshProUGUI>().text = $"+{Upgrades.currentClickscensionCoins}";
            plussClickscensionText.GetComponent<TextMeshProUGUI>().text = $"+{Upgrades.currentClickscensionCoins}";
        }

        if (isAdsRemoved == false)
        {
            adFrame.SetActive(true);
        }
        else
        {
            claimRewardFrame.SetActive(true);
        }

        if(InAppPurchase.isAdsRemovedPlayerprefs == 1)
        {
            adFrame.SetActive(false);
            claimRewardFrame.SetActive(true);
        }
    }

    public void CloseAdFrame()
    {
        audioManager.Play("UI_Click1");

        adFrame.SetActive(false);
        cgAdBtn.SetActive(false);
        clickscensionAdBtn.SetActive(false);

        StartCoroutine(SetAdBtn());
    }

    public static bool isRewarded = false;
    public static int isRewardedPlayerpref;

    public double goldRewardAmount;
    public int clickscensionRewardAmount;

    public void WatchAd()
    {
        if(isAdsRemoved == false) { AdsManager.Instance.rewardedAds.ShowRewardedAd(); }
        else
        {
            isRewarded = true;
        }

        adFrame.SetActive(false);
        cgAdBtn.SetActive(false);
        clickscensionAdBtn.SetActive(false);
    }
    #endregion

    #region UI Stuff
    public GameObject topRightInfo;
    public GameObject patternBackground, frame, frameMobile;

    public GameObject activeBtn, passiveBtn, projectileBtn, clickUpgradeBtn, prestigeBtn, statsBtn, achBtn, mainCursor, topTotalGold, offlineProgression;
    public GameObject lineColliders, linesCollidersMobile, settingsTopLeft, skinsTopLeft, clickscensionPlussText;

    public GameObject highlightPRojectile, highlightClick, hightlightActive, activeAutoFrame;

    public void SetMobileUI()
    {
        activeAutoFrame.transform.localScale = new Vector2(2.7f, 2.7f);
        activeAutoFrame.transform.localPosition = new Vector2(-73f, 1134f);

        highlightPRojectile.transform.localScale = new Vector2(0.89f, 0.89f);
        hightlightActive.transform.localScale = new Vector2(9.23f, 9.23f);

        settingsTopLeft.transform.localScale = new Vector2(1.2f, 1.2f);
        skinsTopLeft.transform.localScale = new Vector2(1.2f, 1.2f);

        lineColliders.SetActive(false);
        linesCollidersMobile.SetActive(true);

        offlineProgression.transform.localScale = new Vector2(1.3f,1.3f);

        topRightInfo.SetActive(false);
        frame.SetActive(false);
        frameMobile.SetActive(true);
        patternBackground.transform.localScale = new Vector2(24.5f, 24.5f);

        mainCursor.transform.localPosition = new Vector2(-30f, 52f);
        mainCursor.transform.localScale = new Vector2(7.6f, 7.6f);

        activeBtn.transform.localPosition = new Vector2(-230f, -515f);
        activeBtn.transform.localScale = new Vector2(1.20f, 1.20f);

        passiveBtn.transform.localPosition = new Vector2(230f, -515f);
        passiveBtn.transform.localScale = new Vector2(1.25f, 1.25f);
        passiveBtn.transform.localScale = new Vector2(1.25f, 1.25f);

        topTotalGold.transform.localPosition = new Vector2(0, 585);
        topTotalGold.transform.localScale = new Vector2(1.20f, 1.20f);

        clickscensionPlussText.transform.localPosition = new Vector2(-30, 620);

        //projectileBtn.transform.localPosition = new Vector2(-340, -1330);
        //clickUpgradeBtn.transform.localPosition = new Vector2(-170, -875);
        //prestigeBtn.transform.localPosition = new Vector2(0, -875);
        //statsBtn.transform.localPosition = new Vector2(170, -875);
        //achBtn.transform.localPosition = new Vector2(340, -875);

        projectileBtn.transform.localScale = new Vector2(1.32f, 1.32f);
        clickUpgradeBtn.transform.localScale = new Vector2(1.32f, 1.32f);
        prestigeBtn.transform.localScale = new Vector2(1.32f, 1.32f);
        statsBtn.transform.localScale = new Vector2(1.32f, 1.32f);
        achBtn.transform.localScale = new Vector2(1.32f, 1.32f);
    }
    #endregion

    #region Set projectile frame
    public GameObject projectileFrame, projectileFrame2, projectileShopText, projectileGoldBar, autoPurchaseProjectile, autoPurchaseBarProjectile, x1projectile, x10projectile, x25projectile, maxProjectile, projectileInfo;
    public GameObject[] projectileUpgrades, lockedProjectileUpgrades;

    public void SetProjectuleFrame()
    {
        projectileInfo.transform.localPosition = new Vector3(0f, 66f, 0f);
        projectileInfo.transform.localScale = new Vector3(0.184f, 0.184f, 0.184f);

        x1projectile.transform.localPosition = new Vector3(-34f, -73.5f, 0f);
        x10projectile.transform.localPosition = new Vector3(-12f, -73.5f, 0f);
        x25projectile.transform.localPosition = new Vector3(12f, -73.5f, 0f);
        maxProjectile.transform.localPosition = new Vector3(34f, -73.5f, 0f);

        projectileFrame.transform.localScale = new Vector3(8.5f, 8.5f, 8.5f);

        projectileFrame.GetComponent<RectTransform>().sizeDelta = new Vector2(93, 190);
        projectileFrame2.GetComponent<RectTransform>().sizeDelta = new Vector2(93.5f, 191.3f);

        projectileShopText.transform.localPosition = new Vector3(0f, 85.3f, 0f);
        projectileShopText.transform.localScale = new Vector3(0.51f, 0.51f, 0.51f);

        for (int i = 0; i < projectileUpgrades.Length; i++)
        {
            projectileUpgrades[i].transform.localScale = new Vector2(0.97f, 0.97f);
            lockedProjectileUpgrades[i].transform.localScale = new Vector2(0.97f, 0.97f);
        }

        projectileUpgrades[0].transform.localPosition = new Vector2(-22f, -40.7f);
        lockedProjectileUpgrades[0].transform.localPosition = new Vector2(-22f, -40.7f);
        projectileUpgrades[1].transform.localPosition = new Vector2(22f, -40.7f);
        lockedProjectileUpgrades[1].transform.localPosition = new Vector2(22f, -40.7f);
        projectileUpgrades[2].transform.localPosition = new Vector2(22f, -23.7f);
        lockedProjectileUpgrades[2].transform.localPosition = new Vector2(22f, -23.7f);
        projectileUpgrades[3].transform.localPosition = new Vector2(-22f, -23.7f);
        lockedProjectileUpgrades[3].transform.localPosition = new Vector2(-22f, -23.7f);
        projectileUpgrades[4].transform.localPosition = new Vector2(-22f, -6.7f);
        lockedProjectileUpgrades[4].transform.localPosition = new Vector2(-22f, -6.7f);
        projectileUpgrades[5].transform.localPosition = new Vector2(22f, -6.7f);
        lockedProjectileUpgrades[5].transform.localPosition = new Vector2(22f, -6.7f);
        projectileUpgrades[6].transform.localPosition = new Vector2(22f, 10.3f);
        lockedProjectileUpgrades[6].transform.localPosition = new Vector2(22f, 10.3f);
        projectileUpgrades[7].transform.localPosition = new Vector2(-22f, 10.3f);
        lockedProjectileUpgrades[7].transform.localPosition = new Vector2(-22f, 10.3f);
        projectileUpgrades[8].transform.localPosition = new Vector2(-22f, 27.3f);
        lockedProjectileUpgrades[8].transform.localPosition = new Vector2(-22f, 27.3f);
        projectileUpgrades[9].transform.localPosition = new Vector2(22f, 27.3f);
        lockedProjectileUpgrades[9].transform.localPosition = new Vector2(22f, 27.3f);
        projectileUpgrades[10].transform.localPosition = new Vector2(22f, 44.3f);
        lockedProjectileUpgrades[10].transform.localPosition = new Vector2(22f, 44.3f);
        projectileUpgrades[11].transform.localPosition = new Vector2(-22f, 44.3f);
        lockedProjectileUpgrades[11].transform.localPosition = new Vector2(-22f, 44.3f);

        autoPurchaseProjectile.transform.localPosition = new Vector2(8f, -86.5f);
        autoPurchaseProjectile.transform.localScale = new Vector2(0.109f, 0.109f);

        autoPurchaseBarProjectile.transform.localPosition = new Vector2(0f, 110f);
        autoPurchaseBarProjectile.transform.localScale = new Vector2(2.34f, 2.34f);

        projectileGoldBar.transform.localPosition = new Vector2(0f, -59f);
        projectileGoldBar.transform.localScale = new Vector2(0.123f, 0.123f);
    }
    #endregion

    #region Set click upgrades frame
    public GameObject clickUpgradesText, clickUpgradesInfo, clickAOE, autoClicker, critIncrease, critChance, clickGoldBar, click1X, click10X, click25X, clickMax, clickAutoPurchase, clickAutoPurchaseBAR, clickUpgradesFrame, clickUpgradesFrame2;

    public void SetClickUpgradeFrame()
    {
        clickUpgradesFrame.GetComponent<RectTransform>().sizeDelta = new Vector2(93f, 190);
        clickUpgradesFrame2.GetComponent<RectTransform>().sizeDelta = new Vector2(93.5f, 191.2f);

        clickAutoPurchase.transform.localPosition = new Vector2(8.5f, -86f);
        clickAutoPurchase.transform.localScale = new Vector2(0.12f, 0.12f);

        clickAutoPurchaseBAR.transform.localPosition = new Vector2(-0, -37f);
        clickAutoPurchaseBAR.transform.localScale = new Vector2(0.26f, 0.26f);

        clickUpgradesText.transform.localPosition = new Vector2(0, 86f);
        clickUpgradesText.transform.localScale = new Vector2(0.59f, 0.59f);

        clickUpgradesInfo.transform.localPosition = new Vector2(0, 67);
        clickUpgradesInfo.transform.localScale = new Vector2(0.17f, 0.17f);

        clickAOE.transform.localPosition = new Vector2(0, 44);
        clickAOE.transform.localScale = new Vector2(1.6f, 1.6f);

        autoClicker.transform.localPosition = new Vector2(0, 16.7f);
        autoClicker.transform.localScale = new Vector2(1.6f, 1.6f);

        critIncrease.transform.localPosition = new Vector2(0, -10.3f);
        critIncrease.transform.localScale = new Vector2(1.6f, 1.6f);

        critChance.transform.localPosition = new Vector2(0, -37.3f);
        critChance.transform.localScale = new Vector2(1.6f, 1.6f);

        clickGoldBar.transform.localPosition = new Vector2(0, -59.6f);
        clickGoldBar.transform.localScale = new Vector2(0.1f, 0.1f);

        click1X.transform.localPosition = new Vector2(-33, -73.2f);
        click1X.transform.localScale = new Vector2(0.72f, 0.72f);

        click10X.transform.localPosition = new Vector2(-11, -73.2f);
        click10X.transform.localScale = new Vector2(0.72f, 0.72f);

        click25X.transform.localPosition = new Vector2(11, -73.2f);
        click25X.transform.localScale = new Vector2(0.72f, 0.72f);

        clickMax.transform.localPosition = new Vector2(33, -73.2f);
        clickMax.transform.localScale = new Vector2(0.72f, 0.72f);
    }
    #endregion

    #region Set clickscension frame
    public GameObject clickscensionFrame1, clickscensionFrame2;
    public GameObject clickScensionText, clickscensionCoisnDisplay, upgradeDisplayFrame, ascendButton, clickscensionInfo;
    public GameObject[] clickscensionUpgrades;

    public void SetClickscensionFrame()
    {
        clickscensionFrame1.GetComponent<RectTransform>().sizeDelta = new Vector2(93, 190);
        clickscensionFrame2.GetComponent<RectTransform>().sizeDelta = new Vector2(93.5f, 191);

        clickScensionText.transform.localPosition = new Vector2(0, 84);
        clickScensionText.transform.localScale = new Vector2(0.48f, 0.48f);

        clickscensionCoisnDisplay.transform.localPosition = new Vector2(0, -42.6f);
        clickscensionCoisnDisplay.transform.localScale = new Vector2(0.1f, 0.1f);

        upgradeDisplayFrame.transform.localPosition = new Vector2(0, -55.5f);
        upgradeDisplayFrame.transform.localScale = new Vector2(0.84f, 0.84f);

        ascendButton.transform.localPosition = new Vector2(0, -71);
        ascendButton.transform.localScale = new Vector2(0.066f, 0.066f);

        clickscensionInfo.transform.localPosition = new Vector2(33, -80);
        clickscensionInfo.transform.localScale = new Vector2(0.155f, 0.155f);

        float xpos, ypos;
        xpos = -28;
        ypos = 64.6f;
        int increment = 0;

        for (int i = 0; i < clickscensionUpgrades.Length; i++)
        {
            clickscensionUpgrades[i].transform.localScale = new Vector2(1.65f, 1.65f);

            if(i == 0 || i == 3 || i == 6 || i == 9) { xpos = -28f; }
            if (i == 1 || i == 4 || i == 7 || i == 10) { xpos = 0f; }
            if (i == 2 || i == 5 || i == 8 || i == 11) { xpos = 28f; }

            clickscensionUpgrades[i].transform.localPosition = new Vector2(xpos, ypos);

            increment += 1;
            if(increment == 3) { increment = 0; ypos -= 29.2f; }
        }
    }
    #endregion

    #region Set stats  frame
    public GameObject statsFrame, brownFrame, frameOutline1, frameOutline2, statsBar,  itemHolder, scrollview, scrollContent, scrollBar, statsText; 

    public void SetStatsFrame()
    {
        statsText.transform.localPosition = new Vector2(0f, 802f);
        statsText.transform.localScale = new Vector2(1.86f, 1.86f);

        statsFrame.transform.localScale = new Vector2(0.87f, 0.87f);
        statsFrame.GetComponent<RectTransform>().sizeDelta = new Vector2(915, 1847);

        brownFrame.GetComponent<RectTransform>().sizeDelta = new Vector2(1014, 1876);
        brownFrame.transform.localPosition = new Vector2(0f, -38f);
        brownFrame.transform.localScale = new Vector2(0.78f, 0.78f);

        frameOutline1.GetComponent<RectTransform>().sizeDelta = new Vector2(915, 1780);
        frameOutline1.transform.localScale = new Vector2(1.06f, 1.06f);

        statsBar.GetComponent<RectTransform>().sizeDelta = new Vector2(1050, 1903);
        statsBar.transform.localScale = new Vector2(0.787f, 0.787f);
        statsBar.transform.localPosition = new Vector2(0, -43);

        itemHolder.GetComponent<RectTransform>().sizeDelta = new Vector2(1000, 1818);
        itemHolder.transform.localPosition = new Vector2(0, -62);

        scrollview.GetComponent<RectTransform>().sizeDelta = new Vector2(1000, 1855);
        scrollview.transform.localPosition = new Vector2(0, 66.8f);

        scrollContent.GetComponent<RectTransform>().sizeDelta = new Vector2(970, 2818);
        scrollContent.transform.localPosition = new Vector2(15.4f, -481);

        scrollBar.GetComponent<RectTransform>().sizeDelta = new Vector2(37.8f, 1861);
        scrollBar.transform.localPosition = new Vector2(-483f, 5.16f);
    }
    #endregion

    #region Set ach  frame
    public GameObject achFrame, achOutlineFrame, achText, achCountText, achBar, itemsHolderAch, scrollViewAch, scrollBarAch, toggleCompleted;

    public void SetAchievementFrame()
    {
        achFrame.GetComponent<RectTransform>().sizeDelta = new Vector2(923, 1850);
        achFrame.transform.localScale = new Vector2(0.88f, 0.88f);

        achOutlineFrame.GetComponent<RectTransform>().sizeDelta = new Vector2(915, 1770);
        achOutlineFrame.transform.localScale = new Vector2(1.065f, 1.065f);

        achText.transform.localPosition = new Vector2(0, 823);
        achText.transform.localScale = new Vector2(3.4f, 3.4f);

        achCountText.transform.localPosition = new Vector2(0, 718);
        achCountText.transform.localScale = new Vector2(3.4f, 3.4f);

        achBar.GetComponent<RectTransform>().sizeDelta = new Vector2(1049, 1675);
        achBar.transform.localPosition = new Vector2(0, -55.8f);
        achBar.transform.localScale = new Vector2(0.78f, 0.78f);

        itemsHolderAch.GetComponent<RectTransform>().sizeDelta = new Vector2(1000, 1628);
        itemsHolderAch.transform.localPosition = new Vector2(0, 3f);
        itemsHolderAch.transform.localScale = new Vector2(1f, 1f);

        scrollViewAch.GetComponent<RectTransform>().sizeDelta = new Vector2(1000, 1626);
        scrollViewAch.transform.localPosition = new Vector2(0, 0.5f);
        scrollViewAch.transform.localScale = new Vector2(1f, 1f);

        scrollBarAch.transform.localPosition = new Vector2(-483, 3);
        scrollBarAch.GetComponent<RectTransform>().sizeDelta = new Vector2(37.8f, 1628);

        toggleCompleted.transform.localPosition = new Vector2(23, -803);
        toggleCompleted.transform.localScale = new Vector2(0.97f, 0.97f);
    }
    #endregion

    #region Set settings frame
    public GameObject resDropdown, fullscreen, languages, steamBtn, exitBtn, settings, settingsFrame, audioSliders, particleEffect, textPopUpSliders, saveBtn, resetBtn, discordBtn, line1, line2, gameSavedText, resetGameFrame;

    public GameObject moreGamesGooglePlayBtn, moreGamesAppStoreBtn, checkOutMoreGamesText;

    public GameObject english, polish, french, spanish, german, japanese, korean, chinese, italian, russian, portugese;


    public void SetSettingsFrame()
    {
        english.transform.localPosition = new Vector2(0, 600);
        polish.transform.localPosition = new Vector2(-295, -320);
        french.transform.localPosition = new Vector2(-295, 307);
        spanish.transform.localPosition = new Vector2(0, 307);
        german.transform.localPosition = new Vector2(295, 307);
        japanese.transform.localPosition = new Vector2(0, -320);
        korean.transform.localPosition = new Vector2(0, -628);
        chinese.transform.localPosition = new Vector2(-295, -5f);
        italian.transform.localPosition = new Vector2(0,-5);
        russian.transform.localPosition = new Vector2(295, -5);
        portugese.transform.localPosition = new Vector2(295, -320);

        settings.transform.localPosition = new Vector2(0, 750);
        settings.transform.localScale = new Vector2(1.33f, 1.33f);

        settingsFrame.GetComponent<RectTransform>().sizeDelta = new Vector2(793, 1350);
        settingsFrame.transform.localPosition = new Vector2(0, -540);
        settingsFrame.transform.localScale = new Vector2(0.78f, 0.78f);

        audioSliders.transform.localPosition = new Vector2(287, 324);
        audioSliders.transform.localScale = new Vector2(1.07f, 1.07f);

        textPopUpSliders.transform.localPosition = new Vector2(-293, 38);
        textPopUpSliders.transform.localScale = new Vector2(1.07f, 1.07f);

        particleEffect.transform.localPosition = new Vector2(-126, -49);
        particleEffect.transform.localScale = new Vector2(0.65f, 0.65f);

        saveBtn.transform.localPosition = new Vector2(0, -580);
        saveBtn.transform.localScale = new Vector2(1.34f, 1.34f);

        resetBtn.transform.localPosition = new Vector2(293, -595);
        resetBtn.transform.localScale = new Vector2(0.7f, 0.7f);

        discordBtn.transform.localPosition = new Vector2(0, -425);
        discordBtn.transform.localScale = new Vector2(1.1f, 1.1f);

        line1.transform.localPosition = new Vector2(0, -252);
        line1.transform.localScale = new Vector2(0.6f, 0.6f);

        line2.transform.localPosition = new Vector2(0, -481);
        line2.transform.localScale = new Vector2(0.6f, 0.6f);

        resetGameFrame.transform.localPosition = new Vector2(10, 7);
        resetGameFrame.transform.localScale = new Vector2(0.63f, 0.63f);

        languages.transform.localPosition = new Vector2(-352, -30.7f);
        languages.transform.localScale = new Vector2(1, 1);

        resDropdown.SetActive(false);
        fullscreen.SetActive(false);
        steamBtn.SetActive(false);
        exitBtn.SetActive(false);

        gameSavedText.transform.localPosition = new Vector2(0, -920);

        if (isThisGooglePlay == true)
        {
            moreGamesGooglePlayBtn.SetActive(true); checkOutMoreGamesText.SetActive(true);
            moreGamesGooglePlayBtn.transform.localPosition = new Vector2(0,-275);
            moreGamesGooglePlayBtn.transform.localScale = new Vector2(3.68f, 3.68f);

            checkOutMoreGamesText.transform.localPosition = new Vector2(0, -163);
            checkOutMoreGamesText.transform.localScale = new Vector2(1.06f, 1.06f);
        }
        if(isThisAppStore == true)
        {
            moreGamesAppStoreBtn.SetActive(true); checkOutMoreGamesText.SetActive(true);
            moreGamesAppStoreBtn.transform.localPosition = new Vector2(0, -275);
            moreGamesAppStoreBtn.transform.localScale = new Vector2(3.68f, 3.68f);

            checkOutMoreGamesText.transform.localPosition = new Vector2(0, -163);
            checkOutMoreGamesText.transform.localScale = new Vector2(1.06f, 1.06f);
        }
    }
    #endregion

    #region Set skins frame
    public GameObject skinFrame1, skinFrame2;

    public void SetSknisFrame()
    {
        skinFrame1.transform.localPosition = new Vector2(0,0);
        skinFrame2.transform.localPosition = new Vector2(0, 0);

        skinFrame1.transform.localScale = new Vector2(0.905f, 0.905f);
        skinFrame2.transform.localScale = new Vector2(0.905f, 0.905f);
    }
    #endregion

    #region Set anchor
    public void SetAnchor(RectTransform rectTransform, Vector2 anchorMin, Vector2 anchorMax, Vector2 pivot)
    {
        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax;
        rectTransform.pivot = pivot;

        // Adjust the anchored position as needed (optional)
        rectTransform.anchoredPosition = Vector2.zero;
    }
    #endregion

    public GameObject projectileTooltip, clickTooltip, prestigeTooltip, closePorjectileTooltip, closeClickTooltip, closeClickscensionToolip, tooltipDark;

    public void OpenTooltip(int tooltip)
    {
        if(isMobile == true)
        {
            tooltipDark.SetActive(true);

            if (tooltip == 1) 
            { 
                projectileTooltip.SetActive(true); closePorjectileTooltip.SetActive(true);
                projectileTooltip.transform.localPosition = new Vector2(-432, 507);
                projectileTooltip.transform.localScale = new Vector2(1.84f, 1.84f);
            }
            if (tooltip == 2) 
            { 
                clickTooltip.SetActive(true); closeClickTooltip.SetActive(true);
                clickTooltip.transform.localPosition = new Vector2(-432, 507);
                clickTooltip.transform.localScale = new Vector2(1.84f, 1.84f);
            }
            if (tooltip == 3) 
            {
                prestigeTooltip.SetActive(true); closeClickscensionToolip.SetActive(true);
                prestigeTooltip.transform.localPosition = new Vector2(0, -507);
                prestigeTooltip.transform.localScale = new Vector2(1.48f, 1.48f);
            }
        }
    }

    public void CloseTooltop()
    {
        tooltipDark.SetActive(false);
        projectileTooltip.SetActive(false); closePorjectileTooltip.SetActive(false);
        clickTooltip.SetActive(false); closeClickTooltip.SetActive(false);
        prestigeTooltip.SetActive(false); closeClickscensionToolip.SetActive(false);
    }

    public void MoreGamesGOOGLEPLAYbtn()
    {
        Application.OpenURL("https://play.google.com/store/apps/developer?id=EagleEye+Games+Norway&hl=en");
    }

    public void MoreGamesAPPSTORE()
    {
        Application.OpenURL("https://apps.apple.com/us/developer/simon-eftest%C3%B8l/id1782530055");
    }
}
