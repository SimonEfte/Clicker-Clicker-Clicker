using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Upgrades : MonoBehaviour, IDataPersistence
{
    public static bool isKnifePurchased, isBoulderPurchased, isSpikePurchased, isShurikenPurchased, isBouncyBallPurchased, isBoomerangPurchased, isSpearPurchased, isLaserPurchased, isBigBouncyBallPurchased, isArrowsPurchased, isSpikeFieldPurchased, isBallTurretPurchased;
    public static float cursorAOE;

    public static bool purchasedAutoClicker, purchasedClickAOE;
    public TextMeshProUGUI[] projectilePurchasePriceText, projectileUpgradePriceText;
    public TextMeshProUGUI[] projectileChanceText;
    public static float[] projectileChanceIncrement = new float[12];
    public static float[] projectileChance = new float[12];

    public static float projectilePriceIncrease, clickerPriceIncrease, activeAndPassiveIncrement;

    public static double activePrice, passivePrice;
    public static double activeIncrement, passiveIncrement;

    public static float critChance, critChanceIncrement, critIncrease, critIncreaseIncrement;
    public static float autoClickerDuration, autoClickerPerSecondDisplay;
    public static float AOEclicksIcrement;

    public static int timesPurchasedAOEclicks;

    public static float autoClickerIncrement;

    public static int firstTimePurchaseProjctile;

    public AudioManager audioManager;
    public OverlappingSounds soundsScript;
    public bool playSound;

    public GameObject exclShop;

    public static int demo12;
    public Achievements achScript;

    public static int timesUpgraded, timesUpgradedForSave;

    public static int currentClickscensionCoins;

    #region Start
    private void Start()
    {
        timesUpgradedForSave = 25;

        playSound = false;
        projectilePriceIncrease = 1.45f;

        if (Achievements.achSaves[35] == true) { projectilePriceIncrease -= 0.02f; }
        if (Achievements.achSaves[36] == true) { clickerPriceIncrease -= 0.02f; }
        if (Achievements.achSaves[37] == true) { clickerPriceIncrease -= 0.01f; }

        clickerPriceIncrease = 1.6f;
        if(Achievements.achSaves[11] == true) { clickerPriceIncrease -= 0.04f; }
        if (Achievements.achSaves[12] == true) { clickerPriceIncrease -= 0.03f; }

        activeAndPassiveIncrement = 1.3f;

        aoeCursor.transform.localScale = new Vector2(cursorAOE, cursorAOE);
        aoeCursor2.transform.localScale = new Vector2(cursorAOE, cursorAOE);
        aoeCursor3.transform.localScale = new Vector2(cursorAOE, cursorAOE);
        aoeCursor4.transform.localScale = new Vector2(cursorAOE, cursorAOE);
        aoeCursor5.transform.localScale = new Vector2(cursorAOE, cursorAOE);
        aoeCursor6.transform.localScale = new Vector2(cursorAOE, cursorAOE);

        int incrementDemo = 0;
        if(DemoScript.isDemo == true) { incrementDemo = 3; }
        else { incrementDemo = 12; }

        for (int i = 0; i < incrementDemo; i++)
        {
            projectilePurchasePriceText[i].text = ScaleNumbers.FormatPoints(projectilePrice[i]);
            projectileUpgradePriceText[i].text = ScaleNumbers.FormatPoints(projectileUpgradePrice[i]);
        }

        rotationSpeed = 45;

        if(projectilesPurchased > 0) { isKnifePurchased = true; }
        if (projectilesPurchased > 1) { isBoulderPurchased = true; }
        if (projectilesPurchased > 2) { isSpikePurchased = true; }
        if (projectilesPurchased > 3) { isShurikenPurchased = true; }
        if (projectilesPurchased > 4) { isBouncyBallPurchased = true; }
        if (projectilesPurchased > 5) { isBoomerangPurchased = true; }
        if (projectilesPurchased > 6) { isSpearPurchased = true; }
        if (projectilesPurchased > 7) { isLaserPurchased = true; }
        if (projectilesPurchased > 8) { isBigBouncyBallPurchased = true; }
        if (projectilesPurchased > 9) { isArrowsPurchased = true; }
        if (projectilesPurchased > 10) { isSpikeFieldPurchased = true; }
        if (projectilesPurchased > 11) { isBallTurretPurchased = true; }

        if(isBoulderPurchased == true) { StartCoroutine(DropBoulder()); }
        if(isSpikePurchased == true) { StabSpike(); }
        if (isBouncyBallPurchased == true) { SetBOuncyBallActive(); if (Achievements.achSaves[27] == true) { extraBouncyBall.SetActive(true); } }
        if (isLaserPurchased == true) { Laser(); }
        if(isSpikeFieldPurchased == true) { SpawnSpikeCircle(); }
        if(isBigBouncyBallPurchased == true) { SetBigBouncyBallActive(); if (Achievements.achSaves[31] == true) { extraBigBouncyBall.SetActive(true); } }
        if(purchasedAutoClicker == true) {  autoClickerParent.SetActive(true); StartCoroutine(AutoClickInAndout()); StartCoroutine(RotateAutoClicker()); }
        if (isBallTurretPurchased == true) { turretBall.SetActive(true); ShootBullet(); }

        for (int i = 0; i < 12; i++)
        {
            projectileUpgradePriceTotal[i] = projectileUpgradePrice[i];
        }

        for (int i = 0; i < 4; i++)
        {
            clickerUpgradePriceTotal[i] = clickerUpgradePrice[i];
        }

        activePriceTotal = activePrice;
        passivePriceTotal = passivePrice;


        SelectProjectileAMount(projectileMaxSelect);

        if(DemoScript.isDemo == false) { SelectClickerAmount(clickerMaxSelect); }


        for (int i = 0; i < projectilesPurchased; i++)
        { 
            SetProjectileIpUpgrades(i);
        }

        StartCoroutine(Wait());

        SelectAutoProjectile(projectileAutoNumber);
        SelectAutoClick(clickAutoNumber);
        SelectAutoActiveOrPassive(isActiveAuto);

        if(isAutoActivePassive == true) { autoActiveHighlight.SetActive(true); autoActiveCheckmark.SetActive(true); }

        for (int i = 0; i < firstTimePurchaseProjctile - 1; i++)
        {
            autoProjectileIcons[i].SetActive(true);
            autoProjectileLocks[i].SetActive(false);
        }

        for (int i = 0; i < firstTimePurchaseProjctile + 1; i++)
        {
            projectileAutoBTNS[i].interactable = true;
        }

        if(isAuto == true) { autoCheckmark.SetActive(true); autoProjectileHighlight.SetActive(true); }
        else { autoCheckmark.SetActive(false); }

        if(isAutoClick == true) { autoCheckmarkClick.SetActive(true); autoClickHighlight.SetActive(true); }
        else { autoCheckmarkClick.SetActive(false); }

        if(critChance > 100) { critChance = 100; }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        playSound = true;
    }
    #endregion

    #region Calculate 10X, 25X or max upgrade - PROJECTILE
    public double[] projectileMaxPrices, totalUpgradePriceProjectile;
    public double[] remainingPoints, total;
    public int[] maxUpgradeCount, count;

    public void CalculateProjectileUpgardes(int number)
    {
        int forLoopNumber = 0;
        if(DemoScript.isDemo == true) { forLoopNumber = 3; }
        if (DemoScript.isDemo == false) { forLoopNumber = 12; }
        
        for (int i = 0; i < forLoopNumber; i++)
        {
            maxUpgradeCount[i] = 0;
            count[i] = 0;

            remainingPoints[i] = MainCursorClick.totalClickPoints;

            totalUpgradePriceProjectile[i] = projectileUpgradePrice[i];
            projectileMaxPrices[i] = projectileUpgradePrice[i];

            if (number == 4)
            {
                while (remainingPoints[i] >= projectileMaxPrices[i])
                {
                    remainingPoints[i] -= projectileMaxPrices[i];
                    projectileMaxPrices[i] *= projectilePriceIncrease;
                    maxUpgradeCount[i]++;
                }

                projectileUpgradePriceTotal[i] = 0;

                while (count[i] < maxUpgradeCount[i])
                {
                    count[i] += 1;

                    projectileUpgradePriceTotal[i] += totalUpgradePriceProjectile[i];

                    totalUpgradePriceProjectile[i] *= projectilePriceIncrease;
                }

                if (maxUpgradeCount[i] >= 1) 
                {
                    projectileUpgradePriceText[i].text = $"{ScaleNumbers.FormatPoints(projectileUpgradePriceTotal[i])}<size=14>X{maxUpgradeCount[i]}";
                    projectileUpgradePriceText[i].color = Color.green;
                }
                else 
                { 
                    projectileUpgradePriceText[i].text = $"{ScaleNumbers.FormatPoints(projectileUpgradePrice[i])}<size=14>X{0}";
                    projectileUpgradePriceText[i].color = Color.red;
                }

                if (projectileChance[0] >= 100) { projectileUpgradePriceText[0].color = Color.red; projectileUpgradePriceText[0].text = LocalizationStrings.max; }
                if (projectileChance[1] >= 100) { projectileUpgradePriceText[1].color = Color.red; projectileUpgradePriceText[1].text = LocalizationStrings.max; }
                if (projectileChance[2] >= 100) { projectileUpgradePriceText[2].color = Color.red; projectileUpgradePriceText[2].text = LocalizationStrings.max; }
                if (projectileChance[3] >= 100) { projectileUpgradePriceText[3].color = Color.red; projectileUpgradePriceText[3].text = LocalizationStrings.max; }
                if (projectileChance[4] >= 40) { projectileUpgradePriceText[4].color = Color.red; projectileUpgradePriceText[4].text = LocalizationStrings.max; }
                if (projectileChance[5] >= 100) { projectileUpgradePriceText[5].color = Color.red; projectileUpgradePriceText[5].text = LocalizationStrings.max; }
                if (projectileChance[6] >= 100) { projectileUpgradePriceText[6].color = Color.red; projectileUpgradePriceText[6].text = LocalizationStrings.max; }
                if (projectileChance[7] >= 100) { projectileUpgradePriceText[7].color = Color.red; projectileUpgradePriceText[7].text = LocalizationStrings.max; }
                if (projectileChance[8] >= 40) { projectileUpgradePriceText[8].color = Color.red; projectileUpgradePriceText[8].text = LocalizationStrings.max; }
                if (projectileChance[9] >= 100) { projectileUpgradePriceText[9].color = Color.red; projectileUpgradePriceText[9].text = LocalizationStrings.max; }
                if (projectileChance[10] >= 100) { projectileUpgradePriceText[10].color = Color.red; projectileUpgradePriceText[10].text = LocalizationStrings.max; }
                if (projectileChance[11] >= 100) { projectileUpgradePriceText[11].color = Color.red; projectileUpgradePriceText[11].text = LocalizationStrings.max; }
            }
            else
            {
                int upgradeAmount = 0;
                int increment = 0;
                if(number == 2) { upgradeAmount = 10; }
                if (number == 3) { upgradeAmount = 25; }

                projectileUpgradePriceTotal[i] = 0;

                while (increment < upgradeAmount)
                {
                    projectileUpgradePriceTotal[i] += totalUpgradePriceProjectile[i];
                    totalUpgradePriceProjectile[i] *= projectilePriceIncrease;
                    increment += 1;
                }

                projectileUpgradePriceText[i].text = $"{ScaleNumbers.FormatPoints(projectileUpgradePriceTotal[i])}<size=14>X{upgradeAmount}";

                if(MainCursorClick.totalClickPoints >= projectileUpgradePriceTotal[i]) { projectileUpgradePriceText[i].color = Color.green; }
                else { projectileUpgradePriceText[i].color = Color.red; }

                if (projectileChance[0] >= 100) { projectileUpgradePriceText[0].color = Color.red; projectileUpgradePriceText[0].text = LocalizationStrings.max; }
                if (projectileChance[1] >= 100) { projectileUpgradePriceText[1].color = Color.red; projectileUpgradePriceText[1].text = LocalizationStrings.max; }
                if (projectileChance[2] >= 100) { projectileUpgradePriceText[2].color = Color.red; projectileUpgradePriceText[2].text = LocalizationStrings.max; }
                if (projectileChance[3] >= 100) { projectileUpgradePriceText[3].color = Color.red; projectileUpgradePriceText[3].text = LocalizationStrings.max; }
                if (projectileChance[4] >= 40) { projectileUpgradePriceText[4].color = Color.red; projectileUpgradePriceText[4].text = LocalizationStrings.max; }
                if (projectileChance[5] >= 100) { projectileUpgradePriceText[5].color = Color.red; projectileUpgradePriceText[5].text = LocalizationStrings.max; }
                if (projectileChance[6] >= 100) { projectileUpgradePriceText[6].color = Color.red; projectileUpgradePriceText[6].text = LocalizationStrings.max; }
                if (projectileChance[7] >= 100) { projectileUpgradePriceText[7].color = Color.red; projectileUpgradePriceText[7].text = LocalizationStrings.max; }
                if (projectileChance[8] >= 40) { projectileUpgradePriceText[8].color = Color.red; projectileUpgradePriceText[8].text = LocalizationStrings.max; }
                if (projectileChance[9] >= 100) { projectileUpgradePriceText[9].color = Color.red; projectileUpgradePriceText[9].text = LocalizationStrings.max; }
                if (projectileChance[10] >= 100) { projectileUpgradePriceText[10].color = Color.red; projectileUpgradePriceText[10].text = LocalizationStrings.max; }
                if (projectileChance[11] >= 100) { projectileUpgradePriceText[11].color = Color.red; projectileUpgradePriceText[11].text = LocalizationStrings.max; }
            }
        }
    }
    #endregion

    #region Calculate 10X, 25X or max upgrade - CLICK
    public double[] clickerMaxPrices, totalUpgradePriceclicker;
    public double[] remainingPointsclicker, totalclicker;
    public int[] maxUpgradeCountclicker, countclicker;

    public void CalculateClickerUpgardes(int number)
    {
        for (int i = 0; i < 4; i++)
        {
            maxUpgradeCountclicker[i] = 0;
            countclicker[i] = 0;

            remainingPointsclicker[i] = MainCursorClick.totalClickPoints;

            totalUpgradePriceclicker[i] = clickerUpgradePrice[i];
            clickerMaxPrices[i] = clickerUpgradePrice[i];

            if (number == 4)
            {
                while (remainingPointsclicker[i] >= clickerMaxPrices[i])
                {
                    remainingPointsclicker[i] -= clickerMaxPrices[i];
                    clickerMaxPrices[i] *= clickerPriceIncrease;
                    maxUpgradeCountclicker[i]++;
                }

                clickerUpgradePriceTotal[i] = 0;

                while (countclicker[i] < maxUpgradeCountclicker[i])
                {
                    countclicker[i] += 1;

                    clickerUpgradePriceTotal[i] += totalUpgradePriceclicker[i];

                    totalUpgradePriceclicker[i] *= clickerPriceIncrease;
                }


                if (critChance >= 100)
                {
                    critChancePriceText.color = Color.red;
                    critChancePriceText.text = LocalizationStrings.max;
                }
                else
                {
                    if (maxUpgradeCountclicker[0] >= 1)
                    {
                        critChancePriceText.text = $"{ScaleNumbers.FormatPoints(clickerUpgradePriceTotal[0])}<size=14>X{maxUpgradeCountclicker[0]}";
                        critChancePriceText.color = Color.green;
                    }
                    else
                    {
                        critChancePriceText.text = $"{ScaleNumbers.FormatPoints(clickerUpgradePrice[0])}<size=14>X{0}";
                        critChancePriceText.color = Color.red;
                    }
                }

                if (maxUpgradeCountclicker[1] >= 1)
                {
                    critIncreasePriceText.text = $"{ScaleNumbers.FormatPoints(clickerUpgradePriceTotal[1])}<size=14>X{maxUpgradeCountclicker[1]}";
                    critIncreasePriceText.color = Color.green;
                }
                else
                {
                    critIncreasePriceText.text = $"{ScaleNumbers.FormatPoints(clickerUpgradePrice[1])}<size=14>X{0}";
                    critIncreasePriceText.color = Color.red;
                }

                if (autoClickerDuration > 0.04f)
                {
                    if (maxUpgradeCountclicker[2] >= 1)
                    {
                        autoClickerPriceText.text = $"{ScaleNumbers.FormatPoints(clickerUpgradePriceTotal[2])}<size=14>X{maxUpgradeCountclicker[2]}";
                        autoClickerPriceText.color = Color.green;
                    }
                    else
                    {
                        autoClickerPriceText.text = $"{ScaleNumbers.FormatPoints(clickerUpgradePrice[2])}<size=14>X{0}";
                        autoClickerPriceText.color = Color.red;
                    }
                }
                else
                {
                    autoClickerPriceText.text = LocalizationStrings.max;
                    autoClickerPriceText.color = Color.red;
                }

                if (maxUpgradeCountclicker[3] >= 1)
                {
                    AOEpriceText.text = $"{ScaleNumbers.FormatPoints(clickerUpgradePriceTotal[3])}<size=14>X{maxUpgradeCountclicker[3]}";
                    AOEpriceText.color = Color.green;
                }
                else
                {
                    AOEpriceText.text = $"{ScaleNumbers.FormatPoints(clickerUpgradePrice[3])}<size=14>X{0}";
                    AOEpriceText.color = Color.red;
                }
            }
            else
            {
                int upgradeAmount = 0;
                int increment = 0;
                if (number == 2) { upgradeAmount = 10; }
                if (number == 3) { upgradeAmount = 25; }

                clickerUpgradePriceTotal[i] = 0;

                while (increment < upgradeAmount)
                {
                    clickerUpgradePriceTotal[i] += totalUpgradePriceclicker[i];
                    totalUpgradePriceclicker[i] *= clickerPriceIncrease;
                    increment += 1;
                }

                if (critChance >= 100)
                {
                    critChancePriceText.color = Color.red;
                    critChancePriceText.text = LocalizationStrings.max;
                }
                else
                {
                    critChancePriceText.text = $"{ScaleNumbers.FormatPoints(clickerUpgradePriceTotal[0])}<size=14>X{upgradeAmount}";
                    if (MainCursorClick.totalClickPoints >= clickerUpgradePriceTotal[0]) { critChancePriceText.color = Color.green; }
                    else { critChancePriceText.color = Color.red; }
                }

                critIncreasePriceText.text = $"{ScaleNumbers.FormatPoints(clickerUpgradePriceTotal[1])}<size=14>X{upgradeAmount}";
                AOEpriceText.text = $"{ScaleNumbers.FormatPoints(clickerUpgradePriceTotal[3])}<size=14>X{upgradeAmount}";

                if (MainCursorClick.totalClickPoints >= clickerUpgradePriceTotal[1]) { critIncreasePriceText.color = Color.green; }
                else { critIncreasePriceText.color = Color.red; }

                if (autoClickerDuration > 0.04f)
                {
                    if (MainCursorClick.totalClickPoints >= clickerUpgradePriceTotal[2]) { autoClickerPriceText.color = Color.green; }
                    else { autoClickerPriceText.color = Color.red; }
                    autoClickerPriceText.text = $"{ScaleNumbers.FormatPoints(clickerUpgradePriceTotal[2])}<size=14>X{upgradeAmount}";
                }
                else
                {
                    autoClickerPriceText.text = LocalizationStrings.max;
                    autoClickerPriceText.color = Color.red;
                }
                   
                if (MainCursorClick.totalClickPoints >= clickerUpgradePriceTotal[3]) { AOEpriceText.color = Color.green; }
                else { AOEpriceText.color = Color.red; }
            }
        }
    }
    #endregion

    #region Calculate 10X, 25X or max upgrade - ACTIVE
    public double activeMaxPrices, totalUpgradePriceActive;
    public double remainingPointsActive, totalActive;
    public int maxUpgradeCountActive, countActive;

    public void CalculateActiveUpgardes()
    {
        maxUpgradeCountActive = 0;
        countActive = 0;

        remainingPointsActive = MainCursorClick.totalClickPoints;

        totalUpgradePriceActive = activePrice;
        activeMaxPrices = activePrice;

        while (remainingPointsActive >= activeMaxPrices)
        {
            remainingPointsActive -= activeMaxPrices;
            activeMaxPrices *= activeAndPassiveIncrement;
            maxUpgradeCountActive++;
        }

        activePriceTotal = 0;

        while (countActive < maxUpgradeCountActive)
        {
            countActive += 1;

            activePriceTotal += totalUpgradePriceActive;

            totalUpgradePriceActive *= activeAndPassiveIncrement;
        }

        if (maxUpgradeCountActive >= 1)
        {
            activePriceText.text = $"{LocalizationStrings.price}{ScaleNumbers.FormatPoints(activePriceTotal)}<size=19>X{maxUpgradeCountActive}";
            activePriceText.color = Color.green;
        }
        else
        {
            activePriceText.text = $"{LocalizationStrings.price}{ScaleNumbers.FormatPoints(activePrice)}<size=19>X{0}";
            activePriceText.color = Color.red;
        }
    }
    #endregion

    #region Calculate 10X, 25X or max upgrade - PASSIVE
    public double passiveMaxPrices, totalUpgradePricePassive;
    public double remainingPointsPassive, totalPassive;
    public int maxUpgradeCountPassive, countPassive;

    public void CalculatePassiveUpgardes()
    {
        maxUpgradeCountPassive = 0;
        countPassive = 0;

        remainingPointsPassive = MainCursorClick.totalClickPoints;

        totalUpgradePricePassive = passivePrice;
        passiveMaxPrices = passivePrice;

        while (remainingPointsPassive >= passiveMaxPrices)
        {
            remainingPointsPassive -= passiveMaxPrices;
            passiveMaxPrices *= activeAndPassiveIncrement;
            maxUpgradeCountPassive++;
        }

        passivePriceTotal = 0;

        while (countPassive < maxUpgradeCountPassive)
        {
            countPassive += 1;

            passivePriceTotal += totalUpgradePricePassive;

            totalUpgradePricePassive *= activeAndPassiveIncrement;
        }

        if (maxUpgradeCountPassive >= 1)
        {
            passivePriceText.text = $"{LocalizationStrings.price}{ScaleNumbers.FormatPoints(passivePriceTotal)}<size=19>X{maxUpgradeCountPassive}";
            passivePriceText.color = Color.green;
        }
        else
        {
            passivePriceText.text = $"{LocalizationStrings.price}{ScaleNumbers.FormatPoints(passivePrice)}<size=19>X{0}";
            passivePriceText.color = Color.red;
        }
    }
    #endregion

    public static int projectileMaxSelect, clickerMaxSelect;

    #region Select projectile upgrade amount
    public GameObject selectedProjectile, x1upgrade, x10upgrade, x25upgrade, maxUpgrade;

    public void SelectProjectileAMount(int number)
    {
        if(playSound == true) { audioManager.Play("UI_Click1"); }

        is1XActiveProjectile = false;
        is10XActiveProjectile = false;
        is25XActiveProjectile = false;
        isMaxActiveProjectile = false;

        projectileMaxSelect = number;

        if (number == 1) { is1XActiveProjectile = true; selectedProjectile.transform.localPosition = x1upgrade.transform.localPosition; }
        if (number == 2) { is10XActiveProjectile = true; selectedProjectile.transform.localPosition = x10upgrade.transform.localPosition; }
        if (number == 3) { is25XActiveProjectile = true; selectedProjectile.transform.localPosition = x25upgrade.transform.localPosition; }
        if (number == 4) { isMaxActiveProjectile = true; selectedProjectile.transform.localPosition = maxUpgrade.transform.localPosition; }
    }
    #endregion

    #region Select clicker upgrade amount
    public GameObject selectedClicker, x1upgradeClicker, x10upgradeClicker, x25upgradeClicker, maxUpgradeClicker;
    public static bool is1XActiveClicker, is10XActiveClicker, is25XActiveClicker, isMaxActiveClicker;
    public void SelectClickerAmount(int number)
    {
        if (playSound == true) { audioManager.Play("UI_Click1"); }

        is1XActiveClicker = false;
        is10XActiveClicker = false;
        is25XActiveClicker = false;
        isMaxActiveClicker = false;

        clickerMaxSelect = number;

        if (number == 1) { is1XActiveClicker = true; selectedClicker.transform.localPosition = x1upgradeClicker.transform.localPosition; }
        if (number == 2) { is10XActiveClicker = true; selectedClicker.transform.localPosition = x10upgradeClicker.transform.localPosition; }
        if (number == 3) { is25XActiveClicker = true; selectedClicker.transform.localPosition = x25upgradeClicker.transform.localPosition; }
        if (number == 4) { isMaxActiveClicker = true; selectedClicker.transform.localPosition = maxUpgradeClicker.transform.localPosition; }
    }
    #endregion

    #region Update
    public TextMeshProUGUI activeAmountText, passiveAmountText, activePriceText, passivePriceText;
    public static bool is1XActiveProjectile, is10XActiveProjectile, is25XActiveProjectile, isMaxActiveProjectile;
    public bool isHoldingTab;
    public static bool exclSpawned;
    public bool pressedOff, pressedOffClick, pressedOffActive;

    private void Update()
    {
        if (SetAutoFrameOff.isHover == false)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if(isProjectileFrameOpen == true)
                {
                    autoProjectileFrame.SetActive(false);
                    isProjectileFrameOpen = false;
                    pressedOff = true;
                }
                else { pressedOff = false; }

                if (isClickFrameOpen == true)
                {
                    autoClickFrame.SetActive(false);
                    isClickFrameOpen = false;
                    pressedOffClick = true;
                }
                else { pressedOffClick = false; }

                if (isAutoActivePassiveOpen == true)
                {
                    autoActiveFrame.SetActive(false);
                    isAutoActivePassiveOpen = false;
                    pressedOffActive = true;
                }
                else { pressedOffActive = false; }
            }
        }

        #region Auto purchase
        if(isAutoActivePassive == true)
        {
            if(isActiveAuto == true)
            {
                if (MainCursorClick.totalClickPoints >= activePrice)
                {
                    UpgradeActive();
                }
            }
            else
            {
                if (MainCursorClick.totalClickPoints >= passivePrice)
                {
                    UpgradePAssive();
                }
            }
        }

        if (isAuto == true)
        {
            if(projectileAutoNumber == 0)
            {
                if (projectilesPurchased < 12)
                {
                    autoProjectileHighlight.transform.SetParent(projectileLocked[projectilesPurchased].gameObject.transform);
                    autoProjectileHighlight.transform.localPosition = new Vector2(0, 0);

                    if (MainCursorClick.totalClickPoints >= projectilePrice[projectilesPurchased])
                    {
                        PurchaseAndUnlockNewProjectile(projectilesPurchased);
                    }
                }
            }
            else if (projectileAutoNumber == 13)
            {
                autoProjectileHighlight.transform.SetParent(projectileFramePArent.gameObject.transform);
                if(projectilesPurchased == 0)
                {
                    autoProjectileHighlight.transform.localPosition = projectileUpgrades[0].transform.localPosition;
                }
                if(projectilesPurchased > 0)
                {
                    autoProjectileHighlight.transform.localPosition = projectileUpgrades[projectilesPurchased - 1].transform.localPosition;

                    if (MainCursorClick.totalClickPoints >= projectileUpgradePrice[projectilesPurchased - 1])
                    {
                        if((projectilesPurchased - 1) == 4 || (projectilesPurchased - 1) == 8)
                        {
                            if (projectileChance[projectilesPurchased - 1] >= 40)
                            {

                            }
                            else { UpgradeProjectile(projectilesPurchased - 1); }
                        }
                        else
                        {
                            if (projectileChance[projectilesPurchased - 1] >= 100)
                            {

                            }
                            else { UpgradeProjectile(projectilesPurchased - 1); }
                        }
                    }
                }
            }
            else
            {
                if ((projectileAutoNumber - 1) < projectilesPurchased)
                {
                    if (MainCursorClick.totalClickPoints >= projectileUpgradePrice[projectileAutoNumber - 1])
                    {
                        if ((projectileAutoNumber - 1) == 4 || (projectileAutoNumber - 1) == 8)
                        {
                            if (projectileChance[projectileAutoNumber - 1] >= 40)
                            {

                            }
                            else { UpgradeProjectile(projectileAutoNumber - 1); }
                        }
                        else
                        {
                            if (projectileChance[projectileAutoNumber - 1] >= 100)
                            {

                            }
                            else { UpgradeProjectile(projectileAutoNumber - 1); }
                        }
                    }
                }
            }
        }

        if(isAutoClick == true)
        {
            if (clickAutoNumber == 0)
            {
                if(critChance < 100)
                {
                    if (MainCursorClick.totalClickPoints >= clickerUpgradePrice[0])
                    {
                        UpgradeCrit(true);
                    }
                }
            }
            if (clickAutoNumber == 1)
            {
                if (MainCursorClick.totalClickPoints >= clickerUpgradePrice[1])
                {
                    UpgradeCrit(false);
                }
            }
            if (clickAutoNumber == 2)
            {
                if (autoClickerDuration > 0.04f)
                {
                    if (MainCursorClick.totalClickPoints >= clickerUpgradePrice[2])
                    {
                        UpgradeAutoClicker();
                    }
                }
            }
            if (clickAutoNumber == 3)
            {
                if (MainCursorClick.totalClickPoints >= clickerUpgradePrice[3])
                {
                    UpgradeAOE();
                }
            }
        }
        #endregion

        #region All active and passive

        if (Input.GetKey(KeyCode.Tab))
        {
            CalculateActiveUpgardes();
            CalculatePassiveUpgardes();
            isHoldingTab = true;
        }
        else
        {
            isHoldingTab = false;
            activePriceText.text = $"{LocalizationStrings.price}" + ScaleNumbers.FormatPoints(activePrice);
            passivePriceText.text = $"{LocalizationStrings.price}" + ScaleNumbers.FormatPoints(passivePrice);

            if (MainCursorClick.totalClickPoints >= activePrice)
            {
                activePriceText.color = Color.green;
            }
            else
            {
                activePriceText.color = Color.red;
            }

            if (MainCursorClick.totalClickPoints >= passivePrice)
            {
                passivePriceText.color = Color.green;
            }
            else
            {
                passivePriceText.color = Color.red;
            }
        }

        if(MainCursorClick.cursorClickPoint < 100) 
        {
            activeAmountText.text = $"{MainCursorClick.cursorClickPoint.ToString("F1")}{LocalizationStrings.cgc} (+{activeIncrement.ToString("F1")})";
        }
        else
        {
            activeAmountText.text = $"{ScaleNumbers.FormatPoints(MainCursorClick.cursorClickPoint)}{LocalizationStrings.cgc} (+{ScaleNumbers.FormatPoints(activeIncrement)})";
        }

        if (passiveIncrement < 100)
        {
            passiveAmountText.text = $"{MainCursorClick.totalPassivePoints.ToString("F1")}{LocalizationStrings.cgs} (+{passiveIncrement.ToString("F1")})";
        }
        else
        {
            passiveAmountText.text = $"{ScaleNumbers.FormatPoints(MainCursorClick.totalPassivePoints)}{LocalizationStrings.cgs} (+{ScaleNumbers.FormatPoints(passiveIncrement)})";
        }
        #endregion

        #region Check click upgrades text
        if (SettingsAndUI.isIntClickUpgradeFrame == true)
        {
            if (Input.GetKey(KeyCode.Tab))
            {
                CalculateClickerUpgardes(4);
                isHoldingTab = true;
            }
            else if (is10XActiveClicker == true || is25XActiveClicker == true || isMaxActiveClicker == true)
            {
                if (is10XActiveClicker == true) { CalculateClickerUpgardes(2); }
                if (is25XActiveClicker == true) { CalculateClickerUpgardes(3); }
                if (isMaxActiveClicker == true) { CalculateClickerUpgardes(4); }
                isHoldingTab = false;
            }
            else
            {
                isHoldingTab = false;
                critChancePriceText.text = ScaleNumbers.FormatPoints(clickerUpgradePrice[0]);
                critIncreasePriceText.text = ScaleNumbers.FormatPoints(clickerUpgradePrice[1]);
              
                AOEpriceText.text = ScaleNumbers.FormatPoints(clickerUpgradePrice[3]);

                if(critChance >= 100)
                {
                    critChancePriceText.color = Color.red;
                    critChancePriceText.text = LocalizationStrings.max;
                }
                else
                {
                    if (MainCursorClick.totalClickPoints >= clickerUpgradePrice[0]) { critChancePriceText.color = Color.green; }
                    else { critChancePriceText.color = Color.red; }
                }

                if (MainCursorClick.totalClickPoints >= clickerUpgradePrice[1]) { critIncreasePriceText.color = Color.green; }
                else { critIncreasePriceText.color = Color.red; }

                if (autoClickerDuration > 0.04f)
                {
                    if (MainCursorClick.totalClickPoints >= clickerUpgradePrice[2]) { autoClickerPriceText.color = Color.green; }
                    else { autoClickerPriceText.color = Color.red; }
                    autoClickerPriceText.text = ScaleNumbers.FormatPoints(clickerUpgradePrice[2]);
                }
                else
                {
                    autoClickerPriceText.text = LocalizationStrings.max;
                    autoClickerPriceText.color = Color.red;
                }

                if (MainCursorClick.totalClickPoints >= clickerUpgradePrice[3]) { AOEpriceText.color = Color.green; }
                else { AOEpriceText.color = Color.red; }
            }
        }
        #endregion

        #region Check projectile texts and set chance text

        if(SettingsAndUI.isInUpgradeFrame == true)
        {
            if (exclShop.activeInHierarchy)
            {
                exclShop.SetActive(false);
            }

            if (projectilesPurchased < 12)
            {
                if (MainCursorClick.totalClickPoints >= projectilePrice[projectilesPurchased] && exclSpawned == false)
                {
                    exclSpawned = true;
                }
            }

            int increment = 0;

            if(DemoScript.isDemo == true) { increment = 3; }
            else { increment = 12; }

            for (int i = 0; i < increment; i++)
            {
                if (MainCursorClick.totalClickPoints < projectilePrice[i]) { projectilePurchasePriceText[i].color = Color.red; }
                else { projectilePurchasePriceText[i].color = Color.green; }
            }

            #region Setting price text and colors
            if (Input.GetKey(KeyCode.Tab))
            {
                CalculateProjectileUpgardes(4);
                isHoldingTab = true;
            }
            else if (is10XActiveProjectile == true || is25XActiveProjectile == true || isMaxActiveProjectile == true) 
            { 
                if(is10XActiveProjectile == true) { CalculateProjectileUpgardes(2); }
                if (is25XActiveProjectile == true) { CalculateProjectileUpgardes(3); }
                if (isMaxActiveProjectile == true) { CalculateProjectileUpgardes(4); }
                isHoldingTab = false;
            }
            else
            {
                for (int i = 0; i < increment; i++)
                {
                    projectileUpgradePriceTotal[i] = projectileUpgradePrice[i];
                }
                isHoldingTab = false;

                for (int i = 0; i < increment; i++)
                {
                    if (MainCursorClick.totalClickPoints < projectileUpgradePriceTotal[i]) { projectileUpgradePriceText[i].color = Color.red; }
                    else { projectileUpgradePriceText[i].color = Color.green; }

                    projectileUpgradePriceText[i].text = ScaleNumbers.FormatPoints(projectileUpgradePriceTotal[i]);
                }

                if (projectileChance[0] >= 100) { projectileUpgradePriceText[0].color = Color.red; projectileUpgradePriceText[0].text = LocalizationStrings.max; }
                if (projectileChance[1] >= 100) { projectileUpgradePriceText[1].color = Color.red; projectileUpgradePriceText[1].text = LocalizationStrings.max; }
                if (projectileChance[2] >= 100) { projectileUpgradePriceText[2].color = Color.red; projectileUpgradePriceText[2].text = LocalizationStrings.max; }
                if (projectileChance[3] >= 100) { projectileUpgradePriceText[3].color = Color.red; projectileUpgradePriceText[3].text = LocalizationStrings.max; }
                if (projectileChance[4] >= 40) { projectileUpgradePriceText[4].color = Color.red; projectileUpgradePriceText[4].text = LocalizationStrings.max; }
                if (projectileChance[5] >= 100) { projectileUpgradePriceText[5].color = Color.red; projectileUpgradePriceText[5].text = LocalizationStrings.max; }
                if (projectileChance[6] >= 100) { projectileUpgradePriceText[6].color = Color.red; projectileUpgradePriceText[6].text = LocalizationStrings.max; }
                if (projectileChance[7] >= 100) { projectileUpgradePriceText[7].color = Color.red; projectileUpgradePriceText[7].text = LocalizationStrings.max; }
                if (projectileChance[8] >= 40) { projectileUpgradePriceText[8].color = Color.red; projectileUpgradePriceText[8].text = LocalizationStrings.max; }
                if (projectileChance[9] >= 100) { projectileUpgradePriceText[9].color = Color.red; projectileUpgradePriceText[9].text = LocalizationStrings.max; }
                if (projectileChance[10] >= 100) { projectileUpgradePriceText[10].color = Color.red; projectileUpgradePriceText[10].text = LocalizationStrings.max; }
                if (projectileChance[11] >= 100) { projectileUpgradePriceText[11].color = Color.red; projectileUpgradePriceText[11].text = LocalizationStrings.max; }
            }
            #endregion

            #region chance/c and S text
            projectileChanceText[0].text = $"<color=green>{projectileChance[0].ToString("F1")}% (+{(projectileChanceIncrement[0] * (1 + Prestige.projectileUpgradeIncrease)).ToString("F2")})<color=white> {LocalizationStrings.chanceC}";
            projectileChanceText[1].text = $"<color=green>{projectileChance[1].ToString("F1")}% (+{(projectileChanceIncrement[1] * (1 + Prestige.projectileUpgradeIncrease)).ToString("F2")})<color=white> {LocalizationStrings.chanceS}";
            projectileChanceText[2].text = $"<color=green>{projectileChance[2].ToString("F1")}% (+{(projectileChanceIncrement[2] * (1 + Prestige.projectileUpgradeIncrease)).ToString("F2")})<color=white> {LocalizationStrings.chanceS}";
            if(DemoScript.isDemo == false)
            {
                projectileChanceText[3].text = $"<color=green>{projectileChance[3].ToString("F1")}% (+{(projectileChanceIncrement[3] * (1 + Prestige.projectileUpgradeIncrease)).ToString("F2")})<color=white> {LocalizationStrings.chanceC}";
                projectileChanceText[4].text = $"<color=green>{projectileChance[4].ToString("F1")} (+{(projectileChanceIncrement[4] * (1 + Prestige.projectileUpgradeIncrease)).ToString("F2")})<color=white> {LocalizationStrings.speed}";
                projectileChanceText[5].text = $"<color=green>{projectileChance[5].ToString("F1")}% (+{(projectileChanceIncrement[5] * (1 + Prestige.projectileUpgradeIncrease)).ToString("F2")})<color=white> {LocalizationStrings.chanceC}";
                projectileChanceText[6].text = $"<color=green>{projectileChance[6].ToString("F1")}% (+{(projectileChanceIncrement[6] * (1 + Prestige.projectileUpgradeIncrease)).ToString("F2")})<color=white> {LocalizationStrings.chanceC}";
                projectileChanceText[7].text = $"<color=green>{projectileChance[7].ToString("F1")}% (+{(projectileChanceIncrement[7] * (1 + Prestige.projectileUpgradeIncrease)).ToString("F2")})<color=white> {LocalizationStrings.chanceS}";
                projectileChanceText[8].text = $"<color=green>{projectileChance[8].ToString("F1")} (+{(projectileChanceIncrement[8] * (1 + Prestige.projectileUpgradeIncrease)).ToString("F2")})<color=white> {LocalizationStrings.speed}";
                projectileChanceText[9].text = $"<color=green>{projectileChance[9].ToString("F1")}% (+{(projectileChanceIncrement[9] * (1 + Prestige.projectileUpgradeIncrease)).ToString("F2")})<color=white> {LocalizationStrings.chanceC}";
                projectileChanceText[10].text = $"<color=green>{projectileChance[10].ToString("F1")}% (+{(projectileChanceIncrement[10] * (1 + Prestige.projectileUpgradeIncrease)).ToString("F2")})<color=white> {LocalizationStrings.chanceS}";
                projectileChanceText[11].text = $"<color=green>{projectileChance[11].ToString("F1")}% (+{(projectileChanceIncrement[11] * (1 + Prestige.projectileUpgradeIncrease)).ToString("F2")})<color=white> {LocalizationStrings.chanceS}";
            }
            #endregion
        }
        else
        {
            if(projectilesPurchased < 12)
            {
                if (MainCursorClick.totalClickPoints >= projectilePrice[projectilesPurchased] && exclSpawned == false)
                {
                    exclSpawned = true;
                    exclShop.SetActive(true);
                    exclShop.GetComponent<Animation>().Play();
                }

                if(MainCursorClick.totalClickPoints < projectilePrice[projectilesPurchased])
                {
                    exclShop.SetActive(false);
                    exclSpawned = false;
                }
            }
        }
        #endregion
    }
    #endregion

    #region Set all text when open frame CLICK
    public void ClickUpgradeTexts()
    {
        AEOsizeText.text = $"<color=green>{cursorAOE.ToString("F3")} (+{(AOEclicksIcrement * (1 + Prestige.clickUpgradeIncrease)).ToString("F3")}) <color=white>{LocalizationStrings.size}";
        AOEpriceText.text = ScaleNumbers.FormatPoints(clickerUpgradePriceTotal[3]);

        critChanceText.text = $"<color=green>{critChance.ToString("F2")}% (+{(critChanceIncrement * (1 + Prestige.clickUpgradeIncrease)).ToString("F2")}%) <color=white>{LocalizationStrings.chance}";
        critChancePriceText.text = ScaleNumbers.FormatPoints(clickerUpgradePriceTotal[0]);

        critIncreaseText.text = $"<color=green>{(critIncrease * 100).ToString("F1")}% (+{((critIncreaseIncrement * 100) * (1 + Prestige.clickUpgradeIncrease)).ToString("F1")}%) <color=white>{LocalizationStrings.increase} ";
        critIncreasePriceText.text = ScaleNumbers.FormatPoints(clickerUpgradePriceTotal[1]);

        //autoClickerDuration

        if(autoClickerDuration == 0)
        { 
            autoClickerPerSecondText.text = $"<color=green>{(0).ToString("F3")} (+{(0.059 * (1 + Prestige.clickUpgradeIncrease)).ToString("F3")}) <color=white>{LocalizationStrings.cps}"; 
        }
        else
        {
            autoClickerPerSecondText.text = $"<color=green>{(1 / autoClickerDuration).ToString("F3")} (+{(0.059 * (1 + Prestige.clickUpgradeIncrease)).ToString("F3")}) <color=white>{LocalizationStrings.cps}";
        }

        if (autoClickerDuration > 0.04f)
        {
            autoClickerPriceText.text = LocalizationStrings.max;
            autoClickerPriceText.color = Color.red;
        }
    }
    #endregion

    #region Auto purchase
    public bool isAuto;
    public GameObject autoCheckmark;
    public GameObject autoProjectileFrame, selectedAutoProjectile;
    public bool isProjectileFrameOpen;

    public bool isAutoClick;
    public GameObject autoCheckmarkClick;
    public GameObject autoClickFrame, selectedAutoClick;
    public bool isClickFrameOpen;

    public void OpenAutoClickFrame()
    {
        if (playSound == true) { audioManager.Play("UI_Click1"); }

        if (isClickFrameOpen == false && pressedOffClick == false) { autoClickFrame.SetActive(true); isClickFrameOpen = true; }
        else if (isClickFrameOpen == true) { autoClickFrame.SetActive(false); isClickFrameOpen = false; }
        else if (pressedOffClick == true) { autoClickFrame.SetActive(false); isClickFrameOpen = false; }

        pressedOffClick = false;
    }

    public void OpenAutoPurchaseFrame()
    {
        if (playSound == true) { audioManager.Play("UI_Click1"); }

        if (isProjectileFrameOpen == false && pressedOff == false) { autoProjectileFrame.SetActive(true); isProjectileFrameOpen = true; }
        else if (isProjectileFrameOpen == true) { autoProjectileFrame.SetActive(false); isProjectileFrameOpen = false; }
        else if (pressedOff == true) { autoProjectileFrame.SetActive(false); isProjectileFrameOpen = false; }

        pressedOff = false;
    }

    //save this int
    public static int projectileAutoNumber;
    public Button[] projectileAutoBTNS;
    public GameObject[] autoProjectileIcons, autoProjectileLocks;
    public GameObject autoProjectileHighlight;
    public GameObject[] projectileUpgrades;
    public GameObject projectileFramePArent;

    public void SelectAutoProjectile(int auto)
    {
        if (playSound == true) { audioManager.Play("UI_Click1"); }
        selectedAutoProjectile.transform.SetParent(projectileAutoBTNS[auto].gameObject.transform);
        if(auto < 13 && auto > 0)
        {
            autoProjectileHighlight.transform.SetParent(projectileFramePArent.transform);
            autoProjectileHighlight.transform.localPosition = projectileUpgrades[auto - 1].transform.localPosition;
        }
   
        selectedAutoProjectile.transform.localPosition = new Vector2(0,2);
        selectedAutoProjectile.transform.localScale = new Vector2(0.781f, 0.781f);

        projectileAutoNumber = auto;
    }

    public static int clickAutoNumber;
    public GameObject[] clickAutoBTNS;
    public GameObject autoClickHighlight;
    public GameObject clickUpgrade1, clickUpgrade2, clickUpgrade3, clickUpgrade4;

    public void SelectAutoClick(int auto)
    {
        if (playSound == true) { audioManager.Play("UI_Click1"); }
        selectedAutoClick.transform.SetParent(clickAutoBTNS[auto].gameObject.transform);
        if (auto == 0) { autoClickHighlight.transform.SetParent(clickUpgrade1.gameObject.transform); }
        if (auto == 1) { autoClickHighlight.transform.SetParent(clickUpgrade2.gameObject.transform); }
        if (auto == 2) { autoClickHighlight.transform.SetParent(clickUpgrade3.gameObject.transform); }
        if (auto == 3) { autoClickHighlight.transform.SetParent(clickUpgrade4.gameObject.transform); }

        autoClickHighlight.transform.localPosition = new Vector2(0, 0);
        selectedAutoClick.transform.localPosition = new Vector2(0, 2);
        selectedAutoClick.transform.localScale = new Vector2(0.781f, 0.781f);

        clickAutoNumber = auto;
    }

    public void AutoPurchaseToggle(bool off)
    {
        if (playSound == true) { audioManager.Play("UI_Click1"); }
        if (off == true) { autoCheckmark.SetActive(false); isAuto = false; autoProjectileHighlight.SetActive(false); }
        if (off == false) { autoCheckmark.SetActive(true); isAuto = true; autoProjectileHighlight.SetActive(true); }
    }

    public void AutoClickToggle(bool off)
    {
        if (playSound == true) { audioManager.Play("UI_Click1"); }
        if (off == true) { autoCheckmarkClick.SetActive(false); isAutoClick = false; autoClickHighlight.SetActive(false); }
        if (off == false) { autoCheckmarkClick.SetActive(true); isAutoClick = true; autoClickHighlight.SetActive(true); }
    }
    #endregion

    #region Unlock new projectile
    public GameObject extraBouncyBall, extraBigBouncyBall;

    public GameObject[] projectilePriceLocked, projectileLockIcon, projectileLocked;
    public double[] projectilePrice, projectileUpgradePrice;
    public double[] projectileUpgradePriceTotal;
    public static int projectilesPurchased;
    public void PurchaseAndUnlockNewProjectile(int projectileNumber)
    {
        if(MainCursorClick.totalClickPoints >= projectilePrice[projectileNumber])
        {
            if(isAuto == false) 
            {
                audioManager.Play("ProjectilePurchase");
            }
            else
            {
                if (projectileAutoNumber > 0) { audioManager.Play("ProjectilePurchase"); }
            }
          
            projectilesPurchased += 1;
            if (projectilesPurchased > firstTimePurchaseProjctile) { firstTimePurchaseProjctile = projectilesPurchased; }

            MainCursorClick.totalClickPoints -= projectilePrice[projectileNumber];
            exclSpawned = false;

            SetProjectileIpUpgrades(projectileNumber);

            if (projectileNumber == 0) { isKnifePurchased = true; }
            else if (projectileNumber == 1) { isBoulderPurchased = true; BoulderChance(); }
            else if (projectileNumber == 2) { isSpikePurchased = true; StabSpike(); }
            else if (projectileNumber == 3) { isShurikenPurchased = true; }
            else if (projectileNumber == 4) { isBouncyBallPurchased = true; SetBOuncyBallActive(); if (Achievements.achSaves[27] == true) { extraBouncyBall.SetActive(true); } }
            else if (projectileNumber == 5) { isBoomerangPurchased = true; }
            else if (projectileNumber == 6) { isSpearPurchased = true; }
            else if (projectileNumber == 7) { isLaserPurchased = true; Laser(); }
            else if (projectileNumber == 8) { isBigBouncyBallPurchased = true;  SetBigBouncyBallActive(); if (Achievements.achSaves[31] == true) { extraBigBouncyBall.SetActive(true); } }
            else if (projectileNumber == 9) { isArrowsPurchased = true; }
            else if (projectileNumber == 10) { isSpikeFieldPurchased = true; SpawnSpikeCircle(); }
            else if (projectileNumber == 11) { isBallTurretPurchased = true; turretBall.SetActive(true); ShootBullet(); }

            projectileAutoBTNS[projectileNumber + 1].interactable = true;
            if(projectileNumber > 0)
            {
                autoProjectileIcons[projectileNumber - 1].SetActive(true);
                autoProjectileLocks[projectileNumber - 1].SetActive(false);
            }
           

            achScript.CheckAchievementsProgress(55);
        }
        else { CantAfford(); }
    }

    public void SetProjectileIpUpgrades(int projectileNumber)
    {
        if (projectileNumber < 11)
        {
            if (DemoScript.isDemo == true)
            {
                if (projectileNumber < 2)
                {
                    projectileLockIcon[projectileNumber].SetActive(false);
                    projectileLocked[projectileNumber + 1].GetComponent<Button>().interactable = true;
                    projectilePriceLocked[projectileNumber + 1].SetActive(true);
                }
                if (projectileNumber < 3)
                {
                    projectilePriceLocked[projectileNumber].SetActive(false);
                    projectileLocked[projectileNumber].SetActive(false);
                }
            }
            else
            {
                projectileLocked[projectileNumber].SetActive(false);
                projectileLocked[projectileNumber + 1].GetComponent<Button>().interactable = true;
                projectilePriceLocked[projectileNumber].SetActive(false);
                projectilePriceLocked[projectileNumber + 1].SetActive(true);
                projectileLockIcon[projectileNumber].SetActive(false);
            }
        }
        if (projectileNumber == 11)
        {
            projectileLocked[projectileNumber].SetActive(false);
        }
    }
    #endregion

    #region Upgrade projectile
    public static float ball1Speed, ball1SpeedIncrement, ball2Speed, ball2SpeedIncrement;

    public void UpgradeProjectile(int projectileNumber)
    {
        if(projectileNumber == 0 && projectileChance[0] >= 100) { CantAfford(); return;  }
        else if (projectileNumber == 1 && projectileChance[1] >= 100) { CantAfford(); return; }
        else if(projectileNumber == 2 && projectileChance[2] >= 100) { CantAfford(); return; }
        else if(projectileNumber == 3 && projectileChance[3] >= 100) {  CantAfford(); return; }
        else if(projectileNumber == 4 && projectileChance[4] >= 40) { CantAfford(); return; }
        else if(projectileNumber == 5 && projectileChance[5] >= 100) { CantAfford(); return; }
        else if(projectileNumber == 6 && projectileChance[6] >= 100) { CantAfford(); return; }
        else if(projectileNumber == 7 && projectileChance[7] >= 100) { CantAfford(); return; }
        else if(projectileNumber == 8 && projectileChance[8] >= 40) { CantAfford(); return; }
        else if (projectileNumber == 9 && projectileChance[9] >= 100) { CantAfford(); return; }
        else if (projectileNumber == 10 && projectileChance[10] >= 100) { CantAfford(); return; }
        else if (projectileNumber == 11 && projectileChance[11] >= 100) { CantAfford(); return; }

        bool isMaxor1X = false;
        bool is10or25x = false;
        int timesUpgrade = 1;
        if(isHoldingTab == true || isMaxActiveProjectile == true) { timesUpgrade = maxUpgradeCount[projectileNumber]; isMaxor1X = true; }
        else if (is10XActiveProjectile == true) { timesUpgrade = 10; is10or25x = true; }
        else if (is25XActiveProjectile == true) { timesUpgrade = 25; is10or25x = true; }
        else { isMaxor1X = true; }

        bool isAutoAndSelected = false;

        if (isAuto == true) 
        { 
            if(projectileAutoNumber - 1 == projectileNumber) { isMaxor1X = true; is10or25x = false; timesUpgrade = 1; isAutoAndSelected = true;  }
            if(projectileAutoNumber == 13 && projectilesPurchased - 1 == projectileNumber)
            {
                isMaxor1X = true; is10or25x = false; timesUpgrade = 1; isAutoAndSelected = true;
            }
        }

        if (isMaxor1X == true)
        {
            bool canAfford = true;

            if (MainCursorClick.totalClickPoints < projectileUpgradePrice[projectileNumber])
            {
                canAfford = false;
            }

            if (MainCursorClick.totalClickPoints >= projectileUpgradePrice[projectileNumber] && canAfford == true)
            {
                if(isAuto == true)
                {
                    if ((projectileAutoNumber - 1) == projectileNumber) { }
                    else if (projectileAutoNumber == 13 && (projectileNumber == projectilesPurchased - 1)) { }
                    else { SpawnPointer(); UpgradeSound(); }
                }
                else { SpawnPointer(); UpgradeSound(); }
               
                for (int i = 0; i < timesUpgrade; i++)
                {
                    UpgradeProjectileActually(projectileNumber);
                }
            }
            else
            { 
                if(isAutoAndSelected == false) { CantAfford();  }
            }
        }
        if(is10or25x == true)
        {
            if (MainCursorClick.totalClickPoints >= projectileUpgradePriceTotal[projectileNumber])
            {
                if (isAuto == true)
                {
                    if ((projectileAutoNumber - 1) == projectileNumber) { }
                    else if (projectileAutoNumber == 13 && (projectileNumber == projectilesPurchased - 1)) { }
                    else { SpawnPointer(); UpgradeSound(); }
                }
                else { SpawnPointer(); UpgradeSound(); }

                for (int i = 0; i < timesUpgrade; i++)
                {
                    UpgradeProjectileActually(projectileNumber);
                }
            }
            else { CantAfford(); }
        }
    }

    public void UpgradeProjectileActually(int projectileNumber)
    {
        MainCursorClick.totalClickPoints -= projectileUpgradePrice[projectileNumber];
        projectileUpgradePrice[projectileNumber] *= projectilePriceIncrease;

        projectileChance[projectileNumber] += projectileChanceIncrement[projectileNumber] * (1 + Prestige.projectileUpgradeIncrease);
        if(projectileChanceIncrement[projectileNumber] < 0.05f) { projectileChanceIncrement[projectileNumber] = 0.05f; }
        projectileChanceIncrement[projectileNumber] -= (projectileChanceIncrement[projectileNumber] * 0.015f);

        if(projectileNumber == 4 || projectileNumber == 8)
        {
            if(projectileChance[projectileNumber] > 40) 
            {
                projectileChance[projectileNumber] = 40;
                projectileChanceIncrement[projectileNumber] = 0;
            }
        }
        else
        {
            if (projectileChance[projectileNumber] > 100)
            {
                projectileChance[projectileNumber] = 100;
                projectileChanceIncrement[projectileNumber] = 0;
            }
        }
    }
    #endregion

    //Upgrading
    public double[] clickerUpgradePriceTotal;
    public double[] clickerUpgradePrice;

    public double activePriceTotal, passivePriceTotal;
    public static int timesActive, timesPassive;

    #region Upgrade Active and passive
    public void UpgradeActive()
    {
        int increment = 1;
        if (isHoldingTab == true) { increment = maxUpgradeCountActive; }
        if(isAutoActivePassive == true && isActiveAuto == true) { increment = 1; }

        if (MainCursorClick.totalClickPoints >= activePrice) 
        { 
            if(isAutoActivePassive == true && isActiveAuto == true)
            {
            }
            else { UpgradeSound(); SpawnPointer(); }
        }
        else 
        {
            CantAfford();
        }

        for (int i = 0; i < increment; i++)
        {
            if (MainCursorClick.totalClickPoints >= activePrice)
            {
                timesActive += 1;
                MainCursorClick.cursorClickPoint += activeIncrement;
                if(timesActive >= 38)
                {
                    if (MobileScript.isMobile == false) { activeIncrement *= 2.65f; }
                    else { activeIncrement *= 2.8f; }

                    timesActive = 0; 
                }
                else
                { 
                    if(MobileScript.isMobile == false) { activeIncrement *= 1.151f; }
                    else { activeIncrement *= 1.16f; }
                }
               
                MainCursorClick.totalClickPoints -= activePrice;
                activePrice *= activeAndPassiveIncrement;
            }
        }
    }

    public static int passiveUpgradeCount;
    public static bool firstTimeBoughtPassive;
    public FallingCurosrs fallCursorScript;

    public void UpgradePAssive()
    {
        int increment = 1;
        if(isHoldingTab == true) { increment = maxUpgradeCountPassive; }
        if (isAutoActivePassive == true && isActiveAuto == false) { increment = 1; }

        if (MainCursorClick.totalClickPoints >= passivePrice) 
        {
            if (isAutoActivePassive == true && isActiveAuto == false)
            {
            }
            else { UpgradeSound(); SpawnPointer(); }
        }
        else 
        {
            CantAfford();
        }

        for (int i = 0; i < increment; i++)
        {
            if (MainCursorClick.totalClickPoints >= passivePrice)
            {
                if (firstTimeBoughtPassive == false)
                {
                    firstTimeBoughtPassive = true;
                    fallCursorScript.FallCursorPassive();
                }

                passiveUpgradeCount += 1;
                MainCursorClick.totalPassivePoints += passiveIncrement;
                timesPassive += 1;
                if (timesPassive >= 38) 
                {
                    if (MobileScript.isMobile == false) { passiveIncrement *= 2.65f; }
                    else { passiveIncrement *= 2.8f; }
                    timesPassive = 0;
                }
                else 
                { 
                    if (MobileScript.isMobile == false) { passiveIncrement *= 1.151f; }
                    else { passiveIncrement *= 1.16f; }
                }
                MainCursorClick.totalClickPoints -= passivePrice;
                passivePrice *= activeAndPassiveIncrement;
            }
        }
    }

    public void SpawnPointer()
    {
        GameObject pointer = ObjectPool.instance.GetPointerFromPool();

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane; // Set this to the distance from the camera to the object.
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        pointer.transform.position = worldPosition;

        StartCoroutine(PointerAnim(pointer.GetComponent<SpriteRenderer>()));
    }

    IEnumerator PointerAnim(SpriteRenderer sprite)
    {
        if (sprite != null)
        {
            Color spriteColor = sprite.color;
            spriteColor.a = 1;
            sprite.color = spriteColor;
        }

        Vector3 originalPosition = sprite.gameObject.transform.position;
        originalPosition.z = 0f;

        Vector3 targetPosition = originalPosition + new Vector3(0, 2.3f, 0); 
        float duration = 0.6f;
        float elapsedTime = 0f;

        // Move upwards and after 0.45 seconds start fading out the sprite
        while (elapsedTime < duration)
        {
            // Move upwards
            sprite.gameObject.transform.position = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / duration);

            if (elapsedTime >= 0.45f)
            {
                // Fade out the sprite from 100% to 0% alpha in the last 0.15 seconds
                float fadeOutTime = (elapsedTime - 0.45f) / 0.15f;

                Color spriteColor = sprite.color;
                spriteColor.a = Mathf.Lerp(1, 0, fadeOutTime);
                sprite.color = spriteColor;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure final position and sprite alpha values are set correctly
        sprite.gameObject.transform.position = targetPosition;

        if (sprite != null)
        {
            Color spriteColor = sprite.color;
            spriteColor.a = 0;
            sprite.color = spriteColor;
        }

        ObjectPool.instance.ReturnPointerFromPool(sprite.gameObject);
    }
    #endregion

    #region Auto purchase active or passive
    public GameObject activeBtn, passiveBtn, activeFrameBtn, passiveFrameBtn, autoActiveFrame, autoActiveCheckmark;

    public bool isAutoActivePassiveOpen;
    public bool isAutoActivePassive;

    public GameObject autoActiveHighlight, autoActiveSelected;

    public void OpenAutoActiveFrame()
    {
        if (playSound == true) { audioManager.Play("UI_Click1"); }
        if (isAutoActivePassiveOpen == false && pressedOffActive == false) { autoActiveFrame.SetActive(true); isAutoActivePassiveOpen = true; }
        else if (isAutoActivePassiveOpen == true) { autoActiveFrame.SetActive(false); isAutoActivePassiveOpen = false; }
        else if (pressedOffActive == true) { autoActiveFrame.SetActive(false); isAutoActivePassiveOpen = false; }

        pressedOffActive = false;
    }

    public void TurnAutoActivePassiveOnOff(bool on)
    {
        if (playSound == true) { audioManager.Play("UI_Click1"); }
        if (on == true) { autoActiveCheckmark.SetActive(true); isAutoActivePassive = true; autoActiveHighlight.SetActive(true); }
        else { autoActiveCheckmark.SetActive(false); isAutoActivePassive = false; autoActiveHighlight.SetActive(false); }
    }

    public bool isActiveAuto;

    public void SelectAutoActiveOrPassive(bool active)
    {
        if (playSound == true) { audioManager.Play("UI_Click1"); }
        if (active == true) { autoActiveHighlight.transform.SetParent(activeBtn.transform); autoActiveSelected.transform.SetParent(activeFrameBtn.transform); isActiveAuto = true; }
        if (active == false) { autoActiveHighlight.transform.SetParent(passiveBtn.transform); autoActiveSelected.transform.SetParent(passiveFrameBtn.transform); isActiveAuto = false; }

        if(MobileScript.isMobile == true)
        {
            autoActiveHighlight.transform.localScale = new Vector2(9.23f, 9.23f);
        }

        autoActiveHighlight.transform.localPosition = new Vector2(0,0);
        autoActiveSelected.transform.localPosition = new Vector2(0, 0);
    }
    #endregion

    #region Upgrade AOE Clicks
    public GameObject aoeCursor, aoeCursor2, aoeCursor3, aoeCursor4, aoeCursor5, aoeCursor6;
    public static bool isAOEclicksPurchased;
    public TextMeshProUGUI AOEpriceText, AEOsizeText;

    public void UpgradeAOE()
    {
        bool isMaxor1X = false;
        bool is10or25x = false;
        int timesUpgrade = 1;
        if (isHoldingTab == true || isMaxActiveClicker == true) { timesUpgrade = maxUpgradeCountclicker[3]; isMaxor1X = true; }
        else if (is10XActiveClicker == true) { timesUpgrade = 10; is10or25x = true; }
        else if (is25XActiveClicker == true) { timesUpgrade = 25; is10or25x = true; }
        else { isMaxor1X = true; }

        bool isAutoAndSelected = false;
        if(isAutoClick == true && clickAutoNumber == 3) { isMaxor1X = true; is10or25x = false; timesUpgrade = 1; isAutoAndSelected = true; }

        if (isMaxor1X == true)
        {
            bool canAfford = true;

            if (MainCursorClick.totalClickPoints < clickerUpgradePrice[3])
            {
                canAfford = false;
            }

            if (MainCursorClick.totalClickPoints >= clickerUpgradePrice[3] && canAfford == true)
            {
                if(isAutoClick == true && clickAutoNumber == 3) { }
                else { SpawnPointer(); UpgradeSound(); }
              
                for (int i = 0; i < timesUpgrade; i++)
                {
                    AUOUpgrade();
                }
            }
            else
            {
                if (isAutoAndSelected == false) { CantAfford(); }
            }
        }
        if (is10or25x == true)
        {
            if (MainCursorClick.totalClickPoints >= clickerUpgradePriceTotal[3])
            {
                if (isAutoClick == true && clickAutoNumber == 3) { }
                else { SpawnPointer(); UpgradeSound(); }

                for (int i = 0; i < timesUpgrade; i++)
                {
                    AUOUpgrade();
                }
            }
            else { CantAfford(); }
        }
    }

    public void AUOUpgrade()
    {
        if (MainCursorClick.totalClickPoints >= clickerUpgradePrice[3])
        {
            MainCursorClick.totalClickPoints -= clickerUpgradePrice[3];

            clickerUpgradePrice[3] *= clickerPriceIncrease;

            if (isAOEclicksPurchased == false)
            {
                isAOEclicksPurchased = true;
            }

            timesPurchasedAOEclicks += 1;

            cursorAOE += AOEclicksIcrement * (1 + Prestige.clickUpgradeIncrease);
            if(MobileScript.isMobile == false) { AOEclicksIcrement = 0.025f / (1 + cursorAOE); }
            else { AOEclicksIcrement = 0.035f / (1 + cursorAOE); }
         
            achScript.CheckAchievementsProgress(40);

            ClickUpgradeTexts();

            aoeCursor.transform.localScale = new Vector2(cursorAOE, cursorAOE);
            aoeCursor2.transform.localScale = new Vector2(cursorAOE, cursorAOE);
            aoeCursor3.transform.localScale = new Vector2(cursorAOE, cursorAOE);
            aoeCursor4.transform.localScale = new Vector2(cursorAOE, cursorAOE);
            aoeCursor5.transform.localScale = new Vector2(cursorAOE, cursorAOE);
            aoeCursor6.transform.localScale = new Vector2(cursorAOE, cursorAOE);
        }
    }
    #endregion

    #region Upgrade Crit
    public TextMeshProUGUI critChanceText, critChancePriceText, critIncreaseText, critIncreasePriceText;

    public void UpgradeCrit(bool isCritChance)
    {
        if(isCritChance == true && critChance >= 100)
        {
            critChance = 100;
            CantAfford();
            return;
        }

        bool isMaxor1X = false;
        bool is10or25x = false;
        int timesUpgrade = 1;
        if (isHoldingTab == true || isMaxActiveClicker == true)
        {
            isMaxor1X = true; 
            if(isCritChance == true) { timesUpgrade = maxUpgradeCountclicker[0]; }
            else { timesUpgrade = maxUpgradeCountclicker[1]; }
        }
        else if (is10XActiveClicker == true) { timesUpgrade = 10; is10or25x = true; }
        else if (is25XActiveClicker == true) { timesUpgrade = 25; is10or25x = true; }
        else { isMaxor1X = true; }

        bool isAutoAndSelected = false;

        if (isAutoClick == true) 
        {
            if (clickAutoNumber == 0 && isCritChance == true) { isMaxor1X = true; is10or25x = false; timesUpgrade = 1; isAutoAndSelected = true; }
            if (clickAutoNumber == 1 && isCritChance == false) { isMaxor1X = true; is10or25x = false; timesUpgrade = 1; isAutoAndSelected = true; }
        }

        if (isMaxor1X == true)
        {
            bool canAfford = true;

            if (isCritChance == true)
            {
                if (MainCursorClick.totalClickPoints < clickerUpgradePrice[0])
                {
                    canAfford = false;
                }

                if (MainCursorClick.totalClickPoints >= clickerUpgradePrice[0] && canAfford == true)
                {
                    if (isAutoClick == true && clickAutoNumber == 0) { }
                    else { SpawnPointer(); UpgradeSound(); }

                    for (int i = 0; i < timesUpgrade; i++)
                    {
                        UpgradeCiritcal(isCritChance);
                    }
                }
                else
                {
                    if (isAutoAndSelected == false) { CantAfford(); }
                }
            }
            else
            {
                if (MainCursorClick.totalClickPoints < clickerUpgradePrice[1])
                {
                    canAfford = false;
                }

                if (MainCursorClick.totalClickPoints >= clickerUpgradePrice[1] && canAfford == true)
                {
                    if (isAutoClick == true && clickAutoNumber == 1) { }
                    else { SpawnPointer(); UpgradeSound(); }

                    for (int i = 0; i < timesUpgrade; i++)
                    {
                        UpgradeCiritcal(isCritChance);
                    }
                }
                else
                {
                    if (isAutoAndSelected == false) { CantAfford(); }
                }
            }
           
        }
        if (is10or25x == true)
        {
            if (isCritChance == true)
            {
                if (MainCursorClick.totalClickPoints >= clickerUpgradePriceTotal[0])
                {
                    if (isAutoClick == true && clickAutoNumber == 0) { }
                    else { SpawnPointer(); UpgradeSound(); }

                    for (int i = 0; i < timesUpgrade; i++)
                    {
                        UpgradeCiritcal(isCritChance);
                    }

                }
                else { CantAfford(); }
            }
            else
            {
                if (MainCursorClick.totalClickPoints >= clickerUpgradePriceTotal[1])
                {
                    if (isAutoClick == true && clickAutoNumber == 1) { }
                    else { SpawnPointer(); UpgradeSound(); }

                    for (int i = 0; i < timesUpgrade; i++)
                    {
                        UpgradeCiritcal(isCritChance);
                    }
                }
                else { CantAfford(); }
            }
        }
    }

    public void UpgradeCiritcal(bool isCritChance)
    {
        if (isCritChance == true)
        {
            if (MainCursorClick.totalClickPoints >= clickerUpgradePrice[0])
            {

                MainCursorClick.totalClickPoints -= clickerUpgradePrice[0];

                clickerUpgradePrice[0] *= clickerPriceIncrease;

                critChance += critChanceIncrement * (1 + Prestige.clickUpgradeIncrease);

                ClickUpgradeTexts();
            }
        }

        if (isCritChance == false)
        {
            if (MainCursorClick.totalClickPoints >= clickerUpgradePrice[1])
            {
                MainCursorClick.totalClickPoints -= clickerUpgradePrice[1];

                clickerUpgradePrice[1] *= clickerPriceIncrease;

                critIncrease += critIncreaseIncrement * (1 + Prestige.clickUpgradeIncrease);

                ClickUpgradeTexts();
            }
        }
    }
    #endregion

    #region Auto Clicker
    public static bool firstTimePurchaseAutoClicker;
    public TextMeshProUGUI autoClickerPriceText, autoClickerPerSecondText;

    public void UpgradeAutoClicker()
    {
        if (autoClickerDuration <= 0.04f)
        {
            CantAfford();
            return;
        }

        bool isMaxor1X = false;
        bool is10or25x = false;
        int timesUpgrade = 1;
        if (isHoldingTab == true || isMaxActiveClicker == true) { timesUpgrade = maxUpgradeCountclicker[2]; isMaxor1X = true; }
        else if (is10XActiveClicker == true) { timesUpgrade = 10; is10or25x = true; }
        else if (is25XActiveClicker == true) { timesUpgrade = 25; is10or25x = true; }
        else { isMaxor1X = true; }

        bool isAutoAndSelected = false;
        if (isAutoClick == true && clickAutoNumber == 2) { timesUpgrade = 1; isMaxor1X = true; is10or25x = false; isAutoAndSelected = true; }

        if (isMaxor1X == true)
        {
            bool canAfford = true;

            if (MainCursorClick.totalClickPoints < clickerUpgradePrice[2])
            {
                canAfford = false;
            }

            if (MainCursorClick.totalClickPoints >= clickerUpgradePrice[2] && canAfford == true)
            {
                if (isAutoClick == true && clickAutoNumber == 2) { }
                else { SpawnPointer(); UpgradeSound(); }

                for (int i = 0; i < timesUpgrade; i++)
                {
                    AutoUpgrade();
                }
            }
            else
            {
                if (isAutoAndSelected == false) { CantAfford(); }
            }
        }
        if (is10or25x == true)
        {
            if (MainCursorClick.totalClickPoints >= clickerUpgradePriceTotal[2])
            {
                if (isAutoClick == true && clickAutoNumber == 2) { }
                else { SpawnPointer(); UpgradeSound(); }

                for (int i = 0; i < timesUpgrade; i++)
                {
                    AutoUpgrade();
                }
            }
            else { CantAfford(); }
        }
    }

    public void AutoUpgrade()
    {
        if (MainCursorClick.totalClickPoints >= clickerUpgradePrice[2])
        {
            MainCursorClick.totalClickPoints -= clickerUpgradePrice[2];

            clickerUpgradePrice[2] *= clickerPriceIncrease;

            purchasedAutoClicker = true;

            if (firstTimePurchaseAutoClicker == false)
            {
                autoClickerParent.SetActive(true);
                firstTimePurchaseAutoClicker = true;

                StartCoroutine(AutoClickInAndout());
                StartCoroutine(RotateAutoClicker());
            }

            autoClickerIncrement += (0.1f * (1 + Prestige.clickUpgradeIncrease));

            autoClickerDuration = 1.7f / autoClickerIncrement;

            if (autoClickerDuration < 0.04f) 
            { 
                autoClickerDuration = 0.04f; 
            }

            ClickUpgradeTexts();
        }
    }

    public float clicksPerSecond = 1 / 3f;

    public GameObject autoClickerParent, autoClicker;
    public float rotationSpeed;

    public MainCursorClick mainClickScript;

    public Vector3 startPosition = new Vector3(-60, 0, 0);
    public Vector3 endPosition = new Vector3(-10, 0, 0);

    private IEnumerator RotateAutoClicker()
    {
        while (true)
        {
            autoClickerParent.transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);

            yield return null;
        }
    }

    private IEnumerator AutoClickInAndout()
    {
        yield return new WaitForSeconds(1f);

        while (true)
        {
            yield return StartCoroutine(MoveOverTime(startPosition, endPosition, (autoClickerDuration / 10)));

            if(firstTimePurchaseAutoClicker == true)
            {
                mainClickScript.ClickCursor();
                Stats.totalAutoClicks += 1;
                Stats.totalClicks += 1;
                achScript.CheckAchievementsProgress(10);
            }
          
            yield return StartCoroutine(MoveOverTime(endPosition, startPosition, (autoClickerDuration / 10)));

            yield return new WaitForSeconds(autoClickerDuration - (autoClickerDuration / 5f));
        }
    }

    private IEnumerator MoveOverTime(Vector3 start, Vector3 end, float time)
    {
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            autoClicker.transform.localPosition = Vector3.Lerp(start, end, elapsedTime / time);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.position = end;
    }
    #endregion

    //Projectile
    #region Shoot knife
    public void ShootKnife()
    {
        if (isKnifePurchased == true)
        {
            float random = Random.Range(0f, 99f);
            if (random < projectileChance[0] + bonanzaIncreaseKnife)
            {
                soundsScript.PlaySound(1);

                Stats.totalKnifes += 1;
                SetStatAncCheckAch();

                GameObject knife = ObjectPool.instance.GetKnifeFromPool();

                knife.transform.localPosition = new Vector2(0, 0);

                Vector2 randomDirection = Random.insideUnitCircle.normalized;

                float knifeSpeed = 0f;

                if (MobileScript.isMobile == false) { knifeSpeed = 12f; }
                else { knifeSpeed = 6f; }
               
                knife.GetComponent<Rigidbody2D>().linearVelocity = randomDirection * knifeSpeed;

                float angle = Mathf.Atan2(randomDirection.y, randomDirection.x) * Mathf.Rad2Deg;

                gameObject.transform.localScale = new Vector2(0.5f, 0.5f);
                knife.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                if(Achievements.achSaves[23] == true) { StartCoroutine(ScaleGameObjectUp(knife, 0.5f, 1.45f)); }
                StartCoroutine(SetKnifeBack(knife));
            }
        }
    }

    IEnumerator SetKnifeBack(GameObject knife)
    {
        yield return new WaitForSeconds(1.5f);
        ObjectPool.instance.ReturnKnifeFromPool(knife);
    }
    #endregion

    #region drop boulder
    IEnumerator DropBoulder()
    {
        yield return new WaitForSeconds(1 - GoldenFistMechanics.boulderTimeDecrease);
        BoulderChance();
    }

    public void BoulderChance()
    {
        if(isBoulderPurchased == true)
        {
            float random = Random.Range(0f, 99f);
            if (random < projectileChance[1] + GoldenFistMechanics.boulderPlussChance)
            {
                SetStatAncCheckAch();
                Stats.totalBoulders += 1;

                int randomBoulderPos = 0;
                if (MobileScript.isMobile == false) { randomBoulderPos = Random.Range(-870, 870); }
                else { randomBoulderPos = Random.Range(-515, 515); }
             
                GameObject boulder = ObjectPool.instance.GetBoulderFromPool();

                if (MobileScript.isMobile == false) { boulder.transform.localPosition = new Vector2(randomBoulderPos, 1000); }
                else { boulder.transform.localPosition = new Vector2(randomBoulderPos, 1200); }

                if (Achievements.achSaves[24] == true) 
                {
                    int randomSize = Random.Range(1,5);
                    if(randomSize == 3) { boulder.transform.localScale = new Vector2(80, 80); }
                    else
                    {
                        boulder.transform.localScale = new Vector2(40, 40);
                    }
                }

                StartCoroutine(SetBackBoulder(boulder));
            }
        }
        StartCoroutine(DropBoulder());
    }

    IEnumerator SetBackBoulder(GameObject boulder)
    {
        yield return new WaitForSeconds(0.5f);

        soundsScript.PlaySound(5);

        yield return new WaitForSeconds(2.5f);
        ObjectPool.instance.ReturnBoulderFromPool(boulder);
    }
    #endregion

    #region Spike stab
    public void StabSpike()
    {
        StartCoroutine(StabSpikeChance());
    }

    IEnumerator StabSpikeChance()
    {
        yield return new WaitForSeconds(1 - GoldenFistMechanics.spikeTimeDecrease);

        if (isSpikePurchased == true)
        {
            float random = Random.Range(0f, 99f);
            if (random < projectileChance[2] + GoldenFistMechanics.spikePlussChance)
            {
                soundsScript.PlaySound(6);
                SetStatAncCheckAch();
                Stats.totalSpikes += 1;

                GameObject spike = ObjectPool.instance.GetSpikeFromPool();
                spike.transform.localPosition = new Vector2(28, -67);

                int randomShootSpike = 0;

                if (Achievements.achSaves[25] == true)
                {
                    randomShootSpike = Random.Range(0,6);
                }

                if (randomShootSpike == 5)
                {
                    Vector2 randomDirection2 = Random.insideUnitCircle.normalized;

                    float knifeSpeed = 7f;
                    spike.GetComponent<Rigidbody2D>().linearVelocity = randomDirection2 * knifeSpeed;

                    float angle2 = Mathf.Atan2(randomDirection2.y, randomDirection2.x) * Mathf.Rad2Deg;

                    spike.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle2));
                }
                else
                {
                    int randomX = Random.Range(120, -47); int randomY = Random.Range(0, -185);

                    Vector2 randomDirection = Random.insideUnitCircle.normalized;
                    float angle = Mathf.Atan2(randomDirection.y, randomDirection.x) * Mathf.Rad2Deg;
                    spike.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

                    int randomPos = Random.Range(5, 5);

                    if (randomPos == 1)
                    {
                        spike.transform.localPosition = new Vector2(randomX, 0);
                        int randomZ = Random.Range(50, 140);
                        spike.transform.rotation = Quaternion.Euler(new Vector3(0, 0, randomZ));
                    }
                    if (randomPos == 2)
                    {
                        spike.transform.localPosition = new Vector2(randomX, -185);
                        int randomZ = Random.Range(-20, -150);
                        spike.transform.rotation = Quaternion.Euler(new Vector3(0, 0, randomZ));
                    }
                    if (randomPos == 3)
                    {
                        spike.transform.localPosition = new Vector2(-47, randomY);
                        int randomZ = Random.Range(-130, -240);
                        spike.transform.rotation = Quaternion.Euler(new Vector3(0, 0, randomZ));
                    }
                    if (randomPos == 4)
                    {
                        spike.transform.localPosition = new Vector2(120, randomY);
                        int randomZ = Random.Range(-55, 60);
                        spike.transform.rotation = Quaternion.Euler(new Vector3(0, 0, randomZ));
                    }
                }

                StartCoroutine(SetSpikeBack(spike));
            }
        }

        StabSpike();
    }

    IEnumerator SetSpikeBack(GameObject spike)
    {
        yield return new WaitForSeconds(2);
        ObjectPool.instance.ReturnSpikeFromPool(spike);
    }
    #endregion

    #region Shoot shuriken
    public void ShootShuriken()
    {
        if (isShurikenPurchased == true)
        {
            float random = Random.Range(0f, 99f);
            if (random < projectileChance[3] + bonanzaIncreaseShuriken)
            {
                soundsScript.PlaySound(1);
                SetStatAncCheckAch();
                Stats.totalShurikens += 3;

                GameObject shuriken1 = ObjectPool.instance.GetShurikenFromPool();
                GameObject shuriken2 = ObjectPool.instance.GetShurikenFromPool();
                GameObject shuriken3 = ObjectPool.instance.GetShurikenFromPool();

                shuriken1.transform.localPosition = new Vector2(0, 0);
                shuriken2.transform.localPosition = new Vector2(0, 0);
                shuriken3.transform.localPosition = new Vector2(0, 0);

                float knifeSpeed = 0f;

                if (MobileScript.isMobile == false) { knifeSpeed = 10f; }
                else { knifeSpeed = 6f; }

                Vector2 randomDirection1 = Random.insideUnitCircle.normalized;

                Vector2 middleDirection = Quaternion.Euler(0, 0, 0) * randomDirection1;
                Vector2 leftDirection = Quaternion.Euler(0, 0, -5) * randomDirection1;
                Vector2 rightDirection = Quaternion.Euler(0, 0, 5) * randomDirection1;

                shuriken1.GetComponent<Rigidbody2D>().linearVelocity = middleDirection.normalized * knifeSpeed;
                shuriken2.GetComponent<Rigidbody2D>().linearVelocity = leftDirection.normalized * knifeSpeed;
                shuriken3.GetComponent<Rigidbody2D>().linearVelocity = rightDirection.normalized * knifeSpeed;

                float angle = Mathf.Atan2(randomDirection1.y, randomDirection1.x) * Mathf.Rad2Deg;

                shuriken1.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                shuriken2.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                shuriken3.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

                gameObject.transform.localScale = new Vector2(0.15f, 0.15f);

                StartCoroutine(SetShurikenBack(shuriken1));
                StartCoroutine(SetShurikenBack(shuriken2));
                StartCoroutine(SetShurikenBack(shuriken3));

                if (Achievements.achSaves[26] == true) 
                { 
                    StartCoroutine(ScaleGameObjectUp(shuriken1, 0.15f, 0.45f));
                    StartCoroutine(ScaleGameObjectUp(shuriken2, 0.15f, 0.45f));
                    StartCoroutine(ScaleGameObjectUp(shuriken3, 0.15f, 0.45f));
                }
            }
        }
    }

    IEnumerator SetShurikenBack(GameObject shuriken)
    {
        yield return new WaitForSeconds(1.2f);
        ObjectPool.instance.ReturnShurikenFromPool(shuriken);
    }
    #endregion

    #region Set bouncy ball active
    public GameObject bouncyBall;

    public void SetBOuncyBallActive()
    {
        bouncyBall.SetActive(true);
    }
    #endregion

    #region Boomerang
    public GameObject boomerRangParent, boomerRangChild, boomerRangImage;
    public Coroutine rotateCoroutine;
    public int boomerangRotationSpeed;

    public void ShootBoomerang()
    {
        float random = Random.Range(0f, 99f);
        if (random < projectileChance[5] + bonanzaIncreaseBoomerang)
        {
            audioManager.Play("Boomerang");
            SetStatAncCheckAch();
            Stats.totalBoomerangs += 1;

            GameObject boomerang = ObjectPool.instance.GetBoomerangFromPool();
            boomerang.transform.localPosition = new Vector3(37, -65);
        }
    }
    #endregion

    #region Shoot spear
    public void ShootSpear()
    {
        if (isSpearPurchased == true)
        {
            float random = Random.Range(0f, 99f);
            if (random < projectileChance[6] + bonanzaIncreaseSpear)
            {
                soundsScript.PlaySound(1);
                SetStatAncCheckAch();
                Stats.totalSpear += 1;

                GameObject spear = ObjectPool.instance.GetSpearFromPool();

                spear.transform.localPosition = new Vector2(0, 0);

                Vector2 randomDirection = Random.insideUnitCircle.normalized;

                float spearSpeed = 0f;

                if (MobileScript.isMobile == false) { spearSpeed = 12f; }
                else { spearSpeed = 6f; }

                spear.GetComponent<Rigidbody2D>().linearVelocity = randomDirection * spearSpeed;

                float angle = Mathf.Atan2(randomDirection.y, randomDirection.x) * Mathf.Rad2Deg;

                spear.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

                if(Achievements.achSaves[29] == true) 
                {
                    int randomSize = Random.Range(0,10);
                    if(randomSize == 3) { spear.transform.localScale = new Vector2(1.2f, 1.2f); }
                    else { spear.transform.localScale = new Vector2(0.5f, 0.5f); }
                }
                else
                {
                    spear.transform.localScale = new Vector2(0.5f, 0.5f);
                }

                StartCoroutine(SetSpearBack(spear));
            }
        }
    }

    IEnumerator SetSpearBack(GameObject spear)
    {
        yield return new WaitForSeconds(2f);
        ObjectPool.instance.ReturnSpearFromPool(spear);
    }
    #endregion

    #region Lasers
    public void Laser()
    {
        StartCoroutine(CheckSpawnLaser());
    }

    IEnumerator CheckSpawnLaser()
    {
        yield return new WaitForSeconds(1f - GoldenFistMechanics.laserTimeDecrease);
        if (isLaserPurchased == true)
        {
            float random = Random.Range(0f, 99f);
            if (random < projectileChance[7] + GoldenFistMechanics.laserPlussChance)
            {
                soundsScript.PlaySound(4);
                SetStatAncCheckAch();
                Stats.totalLasers += 1;
                GameObject laser = ObjectPool.instance.GetLaserFromPool();

                int randomSpawnPosX = Random.Range(-300, 300);
                int randomSpawnPosY = Random.Range(200, -200);

                laser.transform.localPosition = new Vector2(randomSpawnPosX, randomSpawnPosY);
            }
        }
        Laser();
    }
    #endregion

    #region Set big bouncy ball active
    public GameObject bigBouncyBall;

    public void SetBigBouncyBallActive()
    {
        bigBouncyBall.SetActive(true);
    }
    #endregion

    #region Shoot arrows
    public void ShootArrows()
    {
        if (isArrowsPurchased == true)
        {
            float random = Random.Range(0f, 99f);
            if (random < projectileChance[9] + bonanzaIncreaseArrow)
            {
                soundsScript.PlaySound(7);
                int numberOfArrows = 10;
                if(Achievements.achSaves[32] == true) { numberOfArrows = 12; }

                float angleIncrement = 360f / numberOfArrows;

                SetStatAncCheckAch();
                Stats.totalArrows += numberOfArrows;

                for (int i = 0; i < numberOfArrows; i++)
                {
                    GameObject arrow = ObjectPool.instance.GetArrowFromPool();
                    Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
                    arrow.transform.position = new Vector2(0,0);

                    float angle = i * angleIncrement;
                    Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.up; // Convert angle to direction

                    if (MobileScript.isMobile == false) { rb.linearVelocity = direction * 11; }
                    else { rb.linearVelocity = direction * 6; }

                    // Set individual rotation for each arrow
                    float arrowRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    arrow.transform.rotation = Quaternion.Euler(0, 0, arrowRotation);

                    StartCoroutine(SetArrowBack(arrow));
                }
            }
        }
    }

    IEnumerator SetArrowBack(GameObject arrow)
    {
        yield return new WaitForSeconds(2f);
        ObjectPool.instance.ReturnArrowFromPool(arrow);
    }
    #endregion

    #region Spawn spike circle
    public void SpawnSpikeCircle()
    {
        StartCoroutine(AnotherCircleChance());
    }

    IEnumerator AnotherCircleChance()
    {
        yield return new WaitForSeconds(1 - GoldenFistMechanics.spikeCircleTimeDecrease);

        if (isSpikeFieldPurchased == true)
        {
            float random = Random.Range(0f, 99f);
            if (random < projectileChance[10] + GoldenFistMechanics.spikeCircleChance) { StartCoroutine(SpikeCircleChance()); }
        }
         
        SpawnSpikeCircle();
    }

    IEnumerator SpikeCircleChance()
    {
        soundsScript.PlaySound(8);
        SetStatAncCheckAch();
        Stats.totalSpikeballs += 1;

        GameObject spikeCircle = ObjectPool.instance.GetSpikeCircleFromPool();
        spikeCircle.transform.localScale = new Vector2(0, 0);

        int randomX = 0;
        int randomY = 0;

        if (MobileScript.isMobile == false) 
        {
            randomX = Random.Range(-730, 730);
            randomY = Random.Range(315, -315);
        }
        else
        {
            randomX = Random.Range(-425, 425);
            randomY = Random.Range(-700, 700);
        }

        spikeCircle.transform.localPosition = new Vector2(randomX, randomY);

        float scale = 0;
        float maxScale = 0.55f;
        if(Achievements.achSaves[33] == true) { maxScale = 0.75f; }

        while (scale < maxScale)
        {
            yield return new WaitForSeconds(0.01f);
            scale += 0.06f;
            spikeCircle.transform.localScale = new Vector2(scale, scale);
        }

        spikeCircle.transform.localScale = new Vector2(maxScale, maxScale);
        StartCoroutine(DeSpawnSpikeCircle(spikeCircle));
    }

    IEnumerator DeSpawnSpikeCircle(GameObject spikeCircle)
    {
        yield return new WaitForSeconds(7);

        float scale = 0.55f;
        if (Achievements.achSaves[33] == true) { scale = 0.75f; }

        while (scale > 0)
        {
            yield return new WaitForSeconds(0.01f);
            scale -= 0.07f;
            if (scale > 0) { spikeCircle.transform.localScale = new Vector2(scale, scale); }
        }

        spikeCircle.transform.localScale = new Vector2(0, 0);

        ObjectPool.instance.ReturnSpikeCircleFromPool(spikeCircle);
    }
    #endregion

    #region Shoot bullets
    public GameObject turretBall;

    public void ShootBullet()
    {
        StartCoroutine(ShootBulletChance());
    }

    IEnumerator ShootBulletChance()
    {
        yield return new WaitForSeconds(0.5f - GoldenFistMechanics.bulletTimeDecrease);

        if (isBallTurretPurchased == true)
        {
            float random = Random.Range(0f, 99f);
            if (random < projectileChance[11] + GoldenFistMechanics.bulletPlussChance)
            {
                audioManager.Play("Gun");
                BallShooter.shootBullets = true;
                BallShooter.shootBullets2 = true;
                BallShooter.shootBullets3 = true;
                BallShooter.shootBullets4 = true;

                SetStatAncCheckAch();
                Stats.totalBullets += 4;

                if(Achievements.achSaves[34] == true)
                {
                    int random3X = Random.Range(1,6);
                    if(random3X == 4) { StartCoroutine(Shot3X()); }
                }
            }
        }

        yield return new WaitForSeconds(0.5f - GoldenFistMechanics.bulletTimeDecrease);

        ShootBullet();
    }

    IEnumerator Shot3X()
    {
        yield return new WaitForSeconds(0.2f);
        audioManager.Play("Gun");
        BallShooter.shootBullets = true;
        BallShooter.shootBullets2 = true;
        BallShooter.shootBullets3 = true;
        BallShooter.shootBullets4 = true;
        yield return new WaitForSeconds(0.2f);
        audioManager.Play("Gun");
        BallShooter.shootBullets = true;
        BallShooter.shootBullets2 = true;
        BallShooter.shootBullets3 = true;
        BallShooter.shootBullets4 = true;
    }
    #endregion


    #region Shooting projectiles bonanza
    public static float bonanzaIncreaseKnife, bonanzaIncreaseShuriken, bonanzaIncreaseBoomerang, bonanzaIncreaseSpear, bonanzaIncreaseArrow;
    public Coroutine bonanzaCoroutine;

    public void StartShootingBonanza(int projectile)
    {
        if(GoldenFistMechanics.isBonanzaActive == true)
        {
            bonanzaCoroutine = StartCoroutine(WaitForProjectile(projectile));
        }
    }

    IEnumerator WaitForProjectile(int projectile)
    {
        if (GoldenFistMechanics.isBonanzaActive == true)
        {
            float random = 0;
            if (projectile == 3) { random = Random.Range(0.2f, 0.3f); if (Achievements.achSaves[18] == true) { random = Random.Range(0.18f, 0.26f); } }
            else if (projectile < 5) { random = Random.Range(0.05f, 0.08f); if (Achievements.achSaves[18] == true) { random = Random.Range(0.04f, 0.07f); } }
            else { random = Random.Range(0.1f, 0.2f); if (Achievements.achSaves[18] == true) { random = Random.Range(0.1f, 0.18f); } }

            yield return new WaitForSeconds(random);
            if (projectile == 1) { ShootKnife(); }
            if (projectile == 2) { ShootShuriken(); }
            if (projectile == 3) { ShootBoomerang(); }
            if (projectile == 4) { ShootSpear(); }
            if (projectile == 5) { ShootArrows(); }
            ShootAgain(projectile);
        }
    }

    public void ShootAgain(int projectile)
    {
        if (GoldenFistMechanics.isBonanzaActive == true)
        {
            bonanzaCoroutine = StartCoroutine(WaitForProjectile(projectile));
        }
    }

    public void StopBonanza()
    {
        bonanzaIncreaseKnife = 0;
        bonanzaIncreaseShuriken = 0;
        bonanzaIncreaseBoomerang = 0;
        bonanzaIncreaseSpear = 0;
        bonanzaIncreaseArrow = 0;
        
        if (bonanzaCoroutine != null) 
        {
            StopCoroutine(bonanzaCoroutine);
            bonanzaCoroutine = null;
        }
    }
    #endregion

    IEnumerator ScaleGameObjectUp(GameObject knife, float startSize, float endSize)
    {
        knife.transform.localScale = new Vector2(startSize, startSize);

        float duration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float scaleValue = Mathf.Lerp(0f, endSize, elapsedTime / duration);
            knife.transform.localScale = new Vector2(scaleValue, scaleValue);
            elapsedTime += Time.deltaTime;
            yield return null; 
        }

        knife.transform.localScale = new Vector2(endSize, endSize);
    }

    public void SetStatAncCheckAch()
    {
        Stats.totalProjectiles += 1;
        achScript.CheckAchievementsProgress(30);
    }

    public GameObject bouncyBallACH, bigBouncyBallACH;

    #region Reset
    public void ResetUpgrades()
    {
        int incrementDemo = 0;

        activePrice = 10;
        passivePrice = 10;
        activeIncrement = 0.25;
        passiveIncrement = 1.8f;

        passiveUpgradeCount = 0;

        cursorAOE = 0.1f;
        AOEclicksIcrement = 0.025f;

        aoeCursor.transform.localScale = new Vector2(cursorAOE, cursorAOE);
        aoeCursor2.transform.localScale = new Vector2(cursorAOE, cursorAOE);
        aoeCursor3.transform.localScale = new Vector2(cursorAOE, cursorAOE);
        aoeCursor4.transform.localScale = new Vector2(cursorAOE, cursorAOE);
        aoeCursor5.transform.localScale = new Vector2(cursorAOE, cursorAOE);
        aoeCursor6.transform.localScale = new Vector2(cursorAOE, cursorAOE);

        critChance = 0;
        critIncrease = 1;
        critChanceIncrement = 0.2f;
        critIncreaseIncrement = 0.10f;

        autoClickerDuration = 2.83f;
        autoClickerIncrement = 0.6f;

        //0,33
        //0,35
        projectilesPurchased = 0;
        purchasedAutoClicker = false;
        purchasedClickAOE = false;
        firstTimePurchaseAutoClicker = false;
        isAOEclicksPurchased = false;

        projectileChance[0] = 2.2f; //Knife
        projectileChance[1] = 14f; //Boulder
        projectileChance[2] = 45f; //Spike
        projectileChance[3] = 1.7f; //Shuriken
        projectileChance[4] = 7f; //Ball
        projectileChance[5] = 1.4f; //Boomerang
        projectileChance[6] = 2.2f; // Spear
        projectileChance[7] = 20f; //Laser
        projectileChance[8] = 5f; //Big ball
        projectileChance[9] = 1.5f; //Arrows
        projectileChance[10] = 25f; // Spike ball
        projectileChance[11] = 40f; //Ball turret

        projectileChanceIncrement[0] = 0.15f; //Knife
        projectileChanceIncrement[1] = 1.3f; //Boulder
        projectileChanceIncrement[2] = 2f; //Spike
        projectileChanceIncrement[3] = 0.13f; //Shurken
        projectileChanceIncrement[4] = 0.4f; //Ball
        projectileChanceIncrement[5] = 0.12f; //Boomerang
        projectileChanceIncrement[6] = 0.15f; //Spear
        projectileChanceIncrement[7] = 1.2f; //Laser
        projectileChanceIncrement[8] = 0.3f; //Big ball
        projectileChanceIncrement[9] = 0.2f; //Arrows
        projectileChanceIncrement[10] = 5f; //Spike ball
        projectileChanceIncrement[11] = 7f; //Ball turret

        clickerUpgradePrice[0] = 400;
        clickerUpgradePrice[1] = 700;
        clickerUpgradePrice[2] = 5000;
        clickerUpgradePrice[3] = 5000;
        
        if (DemoScript.isDemo == true) { incrementDemo = 3; }
        else { incrementDemo = 12; }

        projectilePrice[0] = 100;
        projectileUpgradePrice[0] = 100 * 0.75f;
        projectilePrice[1] = 25000;
        projectileUpgradePrice[1] = 25000 * 0.75f;
        projectilePrice[2] = 1000000; //40 more
        projectileUpgradePrice[2] = 1000000 * 0.75f;
        projectilePrice[3] = 50000000; //50 more
        projectileUpgradePrice[3] = 50000000 * 0.75f;
        projectilePrice[4] = 1200000000; //24 more
        projectileUpgradePrice[4] = 1000000000 * 0.6f;
        projectilePrice[5] = 100000000000; //125 more
        projectileUpgradePrice[5] = 100000000000 * 0.55f;
        projectilePrice[6] = 15000000000000; //300 more
        projectileUpgradePrice[6] = 15000000000000 * 0.55f;
        projectilePrice[7] = 800000000000000; //500 more
        projectileUpgradePrice[7] = 800000000000000 * 0.5f;
        projectilePrice[8] = 50000000000000000; //750 more
        projectileUpgradePrice[8] = 50000000000000000 * 0.45f;
        projectilePrice[9] = 1500000000000000000; //900 more
        projectileUpgradePrice[9] = 1500000000000000000 * 0.45f;
        projectilePrice[10] = 75000000000000000000f; //900 more
        projectileUpgradePrice[10] = 75000000000000000000f * 0.4f;
        projectilePrice[11] = 5000000000000000000000f; //900 more
        projectileUpgradePrice[11] = 5000000000000000000000f * 0.4f;

        for (int i = 0; i < incrementDemo; i++)
        {
            projectilePurchasePriceText[i].text = ScaleNumbers.FormatPoints(projectilePrice[i]);
            projectileUpgradePriceText[i].text = ScaleNumbers.FormatPoints(projectileUpgradePrice[i]);
        }

        isKnifePurchased = false;
        isBoulderPurchased = false;
        isSpikePurchased = false;
        isShurikenPurchased = false;
        isBouncyBallPurchased = false;
        isBoomerangPurchased = false;
        isSpearPurchased = false;
        isLaserPurchased = false;
        isBigBouncyBallPurchased = false;
        isArrowsPurchased = false;
        isSpikeFieldPurchased = false;
        isBallTurretPurchased = false;
     
        for (int i = 0; i < incrementDemo; i++)
        {
            projectileLocked[i].SetActive(true);
            projectileLocked[i].GetComponent<Button>().interactable = false;
            projectilePriceLocked[i].SetActive(false);
        }

        for (int i = 0; i < 11; i++)
        {
            projectileLockIcon[i].SetActive(true);
        }

        projectilePriceLocked[0].SetActive(true);
        projectileLocked[0].GetComponent<Button>().interactable = true;

        autoClickerParent.SetActive(false);

        bouncyBall.SetActive(false);
        bigBouncyBall.SetActive(false);
        turretBall.SetActive(false);

        bouncyBallACH.SetActive(false);
        bigBouncyBallACH.SetActive(false);

        for (int i = 0; i < projectileAutoBTNS.Length; i++)
        {
            projectileAutoBTNS[i].interactable = false;
        }

        projectileAutoBTNS[0].interactable = true;
        projectileAutoBTNS[13].interactable = true;

        for (int i = 0; i < firstTimePurchaseProjctile + 1; i++)
        {
            projectileAutoBTNS[i].interactable = true;
        }

        for (int i = 0; i < firstTimePurchaseProjctile - 1; i++)
        {
            autoProjectileIcons[i].SetActive(true);
            autoProjectileLocks[i].SetActive(false);
        }

        StopAllCoroutines();

        timesActive = 0;
        timesPassive = 0;
    }
    #endregion


    #region Sounds
    public void UpgradeSound()
    {
        audioManager.Play("Upgrade");

        timesUpgraded += 1;
        if (timesUpgraded >= timesUpgradedForSave)
        {
            timesUpgraded = 0;
            mainClickScript.SaveAgain();
        }
    }

    public void CantAfford()
    {
        audioManager.Play("Locked");
    }
    #endregion

    //Saves
    #region Load Data
    public void LoadData(GameData data)
    {
        isAutoActivePassive = data.isAutoActivePassive;
        isActiveAuto = data.isActiveAuto;

        currentClickscensionCoins = data.currentClickscensionCoins;

        projectileMaxSelect = data.projectileMaxSelect;
        clickerMaxSelect = data.clickerMaxSelect;

        projectileAutoNumber = data.projectileAutoNumber;
        clickAutoNumber = data.clickAutoNumber;
        isAuto = data.isAuto;
        isAutoClick = data.isAutoClick;

        passiveUpgradeCount = data.passiveUpgradeCount;

        projectilesPurchased = data.projectilesPurchased;
        firstTimePurchaseProjctile = data.firstTimePurchaseProjctile;
        cursorAOE = data.cursorAOE;
        purchasedAutoClicker = data.purchasedAutoClicker;
        purchasedClickAOE = data.purchasedClickAOE;
        for (int i = 0; i < projectileChanceIncrement.Length; i++)
        {
            projectileChanceIncrement[i] = data.projectileChanceIncrement[i];
        }
        for (int i = 0; i < projectileChance.Length; i++)
        {
            projectileChance[i] = data.projectileChance[i];
        }
        for (int i = 0; i < projectilePrice.Length; i++)
        {
            projectilePrice[i] = data.projectilePrice[i];
        }
        for (int i = 0; i < projectileUpgradePrice.Length; i++)
        {
            projectileUpgradePrice[i] = data.projectileUpgradePrice[i];
        }
        activePrice = data.activePrice;
        passivePrice = data.passivePrice;
        for (int i = 0; i < clickerUpgradePrice.Length; i++)
        {
            clickerUpgradePrice[i] = data.clickerUpgradePrice[i];
        }
        activeIncrement = data.activeIncrement;
        passiveIncrement = data.passiveIncrement;
        critChance = data.critChance;
        critChanceIncrement = data.critChanceIncrement;
        critIncrease = data.critIncrease;
        critIncreaseIncrement = data.critIncreaseIncrement;
        autoClickerDuration = data.autoClickerDuration;
        autoClickerPerSecondDisplay = data.autoClickerPerSecondDisplay;
        AOEclicksIcrement = data.AOEclicksIcrement;
        autoClickerIncrement = data.autoClickerIncrement;
        firstTimePurchaseAutoClicker = data.firstTimePurchaseAutoClicker;
        isAOEclicksPurchased = data.isAOEclicksPurchased;

        timesPassive = data.timesPassive;
        timesActive = data.timesActive;
    }
    #endregion

    #region Save Data
    public void SaveData(ref GameData data)
    {
        data.isAutoActivePassive = isAutoActivePassive;
        data.isActiveAuto = isActiveAuto;

        data.currentClickscensionCoins = currentClickscensionCoins;

        data.projectileMaxSelect = projectileMaxSelect;
        data.clickerMaxSelect = clickerMaxSelect;

        data.projectileAutoNumber = projectileAutoNumber;
        data.clickAutoNumber = clickAutoNumber;
        data.isAuto = isAuto;
        data.isAutoClick = isAutoClick;

        data.passiveUpgradeCount = passiveUpgradeCount;

        data.projectilesPurchased = projectilesPurchased;
        data.firstTimePurchaseProjctile = firstTimePurchaseProjctile;
        data.cursorAOE = cursorAOE;
        data.purchasedAutoClicker = purchasedAutoClicker;
        data.purchasedClickAOE = purchasedClickAOE;
        for (int i = 0; i < projectileChanceIncrement.Length; i++)
        {
            data.projectileChanceIncrement[i] = projectileChanceIncrement[i];
        }
        for (int i = 0; i < projectileChance.Length; i++)
        {
            data.projectileChance[i] = projectileChance[i];
        }
        for (int i = 0; i < projectilePrice.Length; i++)
        {
            data.projectilePrice[i] = projectilePrice[i];
        }
        for (int i = 0; i < projectileUpgradePrice.Length; i++)
        {
            data.projectileUpgradePrice[i] = projectileUpgradePrice[i];
        }
        data.activePrice = activePrice;
        data.passivePrice = passivePrice;
        for (int i = 0; i < clickerUpgradePrice.Length; i++)
        {
            data.clickerUpgradePrice[i] = clickerUpgradePrice[i];
        }
        data.activeIncrement = activeIncrement;
        data.passiveIncrement = passiveIncrement;
        data.critChance = critChance;
        data.critChanceIncrement = critChanceIncrement;
        data.critIncrease = critIncrease;
        data.critIncreaseIncrement = critIncreaseIncrement;
        data.autoClickerDuration = autoClickerDuration;
        data.autoClickerPerSecondDisplay = autoClickerPerSecondDisplay;
        data.AOEclicksIcrement = AOEclicksIcrement;
        data.autoClickerIncrement = autoClickerIncrement;
        data.firstTimePurchaseAutoClicker = firstTimePurchaseAutoClicker;
        data.isAOEclicksPurchased = isAOEclicksPurchased;

        data.timesPassive = timesPassive;
        data.timesActive = timesActive;
    }
    #endregion
}
