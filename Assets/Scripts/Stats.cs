using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stats : MonoBehaviour, IDataPersistence
{
    public static double totalGold, totalGoldActive, totalGoldPassive, totalGoldFalling, totalGoldCrit;
    public static int totalClicks, totalCursorClicks, totalAutoClicks, totalFallingCursorClciks, totalCritClciks, totalGoldenFistClicks;
    public static int  totalClickscensionSpent, clickscensionUnlocked, clickscensionUpgraded;
    public static int totalFallingCursors, totalDiamond, totalEmerald, totalRainbow, totalPurple;
    public static int totalProjectiles, totalFallingHitByProjectile, totalKnifes, totalBoulders, totalSpikes, totalShurikens, totalBoomerangs, totalSpear, totalArrows, totalSpikeballs, totalBullets, totalLasers, totalBonanzas, ballBounced, bigBallBounced;

    public static float totalClickscensionCoins;

    public TextMeshProUGUI totalGoldText, totalGoldActiveText, totalGoldPassiveText, totalGoldFallingText, totalGoldCritText;
    public TextMeshProUGUI totalClicksText, totalCursorClicksText, totalAutoClicksText, totalFallingCursorClciksText, totalCritClciksText, totalGoldenFistClicksText;
    public TextMeshProUGUI totalClickscensionCoinsText, totalClickscensionSpentText, clickscensionUnlockedText, clickscensionUpgradedText;
    public TextMeshProUGUI totalFallingCursorsText, totalDiamondText, totalEmeraldText, totalRainbowText, totalPurpleText;
    public TextMeshProUGUI totalProjectilesText, totalFallingHitByProjectileText, totalKnifesText, totalBouldersText, totalSpikesText, totalShurikensText, totalBoomerangsText, totalSpearText, totalArrowsText, totalSpikeballsText, totalBulletsText, totalLasersText, totalBallbouncesText, totalBigBallBouncesText, totalProjectielBonanzasText;

    public TextMeshProUGUI clickscensionAcquiredText, clickscensionSpentText, clickScensionUnlocked, clickscensionPurchased, fallingDiamondText, fallingEmeraldText, fallingRainbowText, fallingPurpleText, knivesText, boulderText, spikesText, shurikenText, boomerangsText, spearsText, laserText, arrowText, spikeBallText, bulletText, totalBallbouncesTextLeft, totalBigBallBouncesTextLeft, totalProjectielBonanzasTextLeft;

    public void Update()
    {
        if (SettingsAndUI.isInStats == true)
        {
            totalGoldText.text = ScaleNumbers.FormatPoints(totalGold);
            totalGoldActiveText.text = ScaleNumbers.FormatPoints(totalGoldActive);
            totalGoldPassiveText.text = ScaleNumbers.FormatPoints(totalGoldPassive);
            totalGoldFallingText.text = ScaleNumbers.FormatPoints(totalGoldFalling);
            totalGoldCritText.text = ScaleNumbers.FormatPoints(totalGoldCrit);

            // Click-related stats
            totalClicksText.text = totalClicks.ToString();
            totalCursorClicksText.text = totalCursorClicks.ToString();
            totalAutoClicksText.text = totalAutoClicks.ToString();

            totalFallingCursorClciksText.text = totalFallingCursorClciks.ToString();
            totalCritClciksText.text = totalCritClciks.ToString();
            totalGoldenFistClicksText.text = totalGoldenFistClicks.ToString();

            // Clickscension-related stats
            // Falling cursors and gem-related stats
            if (DemoScript.isDemo == true)
            {
                totalClickscensionCoinsText.text = "???";
                totalClickscensionSpentText.text = "???";
                clickscensionUnlockedText.text = "???";
                clickscensionUpgradedText.text = "???";

                clickscensionAcquiredText.text = "??????????";
                clickscensionSpentText.text = "??????????";
                clickScensionUnlocked.text = "??????????";
                clickscensionPurchased.text = "??????????";
            }
            else
            {
                totalClickscensionCoinsText.text = totalClickscensionCoins.ToString("F0");
                totalClickscensionSpentText.text = totalClickscensionSpent.ToString("F0");
                clickscensionUnlockedText.text = clickscensionUnlocked.ToString() +"/12";
                clickscensionUpgradedText.text = clickscensionUpgraded.ToString();

                clickscensionAcquiredText.text = LocalizationStrings.totalClickscensionCoins;
                clickscensionSpentText.text = LocalizationStrings.totalCLickscensionSpent;
                clickScensionUnlocked.text = LocalizationStrings.totalClickscensionUnlocked;
                clickscensionPurchased.text = LocalizationStrings.totalClickscensionPurchased;
            }

            totalFallingCursorsText.text = totalFallingCursors.ToString();

            if (DemoScript.isDemo == true)
            {
                totalDiamondText.text = "???";
                totalEmeraldText.text = "???";
                totalRainbowText.text = "???";
                totalPurpleText.text = "???";

                fallingDiamondText.text = "??????????";
                fallingEmeraldText.text = "??????????";
                fallingRainbowText.text = "??????????";
                fallingPurpleText.text = "??????????";
            }
            else
            {
                totalDiamondText.text = totalDiamond.ToString();
                totalEmeraldText.text = totalEmerald.ToString();
                totalRainbowText.text = totalRainbow.ToString();
                totalPurpleText.text = totalPurple.ToString();

                fallingDiamondText.text = LocalizationStrings.totalDiamond;
                fallingEmeraldText.text = LocalizationStrings.totalEmerald;
                fallingRainbowText.text = LocalizationStrings.totalRainbow;
                fallingPurpleText.text = LocalizationStrings.totalPurple;
            }

            totalFallingHitByProjectileText.text = totalFallingHitByProjectile.ToString();
            totalProjectilesText.text = totalProjectiles.ToString();
            totalProjectielBonanzasText.text = totalBonanzas.ToString();


            if (Upgrades.firstTimePurchaseProjctile > 0) { totalKnifesText.text = totalKnifes.ToString(); }
            if (Upgrades.firstTimePurchaseProjctile > 1) { totalBouldersText.text = totalBoulders.ToString(); }
            if (Upgrades.firstTimePurchaseProjctile > 2) { totalSpikesText.text = totalSpikes.ToString(); }
            if (Upgrades.firstTimePurchaseProjctile > 3) { totalShurikensText.text = totalShurikens.ToString(); }
            if (Upgrades.firstTimePurchaseProjctile > 4) { totalBallbouncesText.text = ballBounced.ToString(); }
            if (Upgrades.firstTimePurchaseProjctile > 5) { totalBoomerangsText.text = totalBoomerangs.ToString(); }
            if (Upgrades.firstTimePurchaseProjctile > 6) { totalSpearText.text = totalSpear.ToString(); }
            if (Upgrades.firstTimePurchaseProjctile > 7) { totalLasersText.text = totalLasers.ToString(); }
            if (Upgrades.firstTimePurchaseProjctile > 7) { totalBigBallBouncesText.text = bigBallBounced.ToString(); }
            if (Upgrades.firstTimePurchaseProjctile > 9) { totalArrowsText.text = totalArrows.ToString(); }
            if (Upgrades.firstTimePurchaseProjctile > 10) { totalSpikeballsText.text = totalSpikeballs.ToString(); }
            if (Upgrades.firstTimePurchaseProjctile > 11) { totalBulletsText.text = totalBullets.ToString(); }

            if (Upgrades.firstTimePurchaseProjctile < 1) { totalKnifesText.text = "???"; }
            if (Upgrades.firstTimePurchaseProjctile < 2) { totalBouldersText.text = "???"; }
            if (Upgrades.firstTimePurchaseProjctile < 3) { totalSpikesText.text = "???"; }
            if (Upgrades.firstTimePurchaseProjctile < 4) { totalShurikensText.text = "???"; }
            if (Upgrades.firstTimePurchaseProjctile < 5) { totalBallbouncesText.text = "???"; }
            if (Upgrades.firstTimePurchaseProjctile < 6) { totalBoomerangsText.text = "???"; }
            if (Upgrades.firstTimePurchaseProjctile < 7) { totalSpearText.text = "???"; }
            if (Upgrades.firstTimePurchaseProjctile < 8) { totalLasersText.text = "???"; }
            if (Upgrades.firstTimePurchaseProjctile < 9) { totalBigBallBouncesText.text = "???"; }
            if (Upgrades.firstTimePurchaseProjctile < 10) { totalArrowsText.text = "???"; }
            if (Upgrades.firstTimePurchaseProjctile < 11) { totalSpikeballsText.text = "???"; }
            if (Upgrades.firstTimePurchaseProjctile < 12) { totalBulletsText.text = "???"; }

            //Left text
            if (Upgrades.firstTimePurchaseProjctile > 0) { knivesText.text = LocalizationStrings.totalKnifes; }
            if (Upgrades.firstTimePurchaseProjctile > 1) { boulderText.text = LocalizationStrings.totalBouldes; }
            if (Upgrades.firstTimePurchaseProjctile > 2) { spikesText.text = LocalizationStrings.totalSpikes; }
            if (Upgrades.firstTimePurchaseProjctile > 3) { shurikenText.text = LocalizationStrings.totslShurikens; }
            if (Upgrades.firstTimePurchaseProjctile > 4) { totalBallbouncesTextLeft.text = LocalizationStrings.totalBounce; }
            if (Upgrades.firstTimePurchaseProjctile > 5) { boomerangsText.text = LocalizationStrings.totalBoomerangs; }
            if (Upgrades.firstTimePurchaseProjctile > 6) { spearsText.text = LocalizationStrings.totalSpears; }
            if (Upgrades.firstTimePurchaseProjctile > 7) { laserText.text = LocalizationStrings.totalLasers; }
            if (Upgrades.firstTimePurchaseProjctile > 8) { totalBigBallBouncesTextLeft.text = LocalizationStrings.totalBigBounce; }
            if (Upgrades.firstTimePurchaseProjctile > 9) { arrowText.text = LocalizationStrings.totalArrows; }
            if (Upgrades.firstTimePurchaseProjctile > 10) { spikeBallText.text = LocalizationStrings.totalSpikeBalls; }
            if (Upgrades.firstTimePurchaseProjctile > 11) { bulletText.text = LocalizationStrings.totalBullets; }

            if (Upgrades.firstTimePurchaseProjctile < 1) { knivesText.text = "??????????"; }
            if (Upgrades.firstTimePurchaseProjctile < 2) { boulderText.text = "??????????"; }
            if (Upgrades.firstTimePurchaseProjctile < 3) { spikesText.text = "??????????"; }
            if (Upgrades.firstTimePurchaseProjctile < 4) { shurikenText.text = "??????????"; }
            if (Upgrades.firstTimePurchaseProjctile < 5) { totalBallbouncesTextLeft.text = "??????????"; }
            if (Upgrades.firstTimePurchaseProjctile < 6) { boomerangsText.text = "??????????"; }
            if (Upgrades.firstTimePurchaseProjctile < 7) { spearsText.text = "??????????"; }
            if (Upgrades.firstTimePurchaseProjctile < 8) { laserText.text = "??????????"; }
            if (Upgrades.firstTimePurchaseProjctile < 9) { totalBigBallBouncesTextLeft.text = "??????????"; }
            if (Upgrades.firstTimePurchaseProjctile < 10) { arrowText.text = "??????????"; }
            if (Upgrades.firstTimePurchaseProjctile < 11) { spikeBallText.text = "??????????"; }
            if (Upgrades.firstTimePurchaseProjctile < 12) { bulletText.text = "??????????"; }
        }
    }

    public void ResetStats()
    {
        totalGold = 0;
        totalGoldActive = 0;
        totalGoldPassive = 0;
        totalGoldFalling = 0;
        totalGoldCrit = 0;

        totalClicks = 0;
        totalCursorClicks = 0;
        totalAutoClicks = 0;
        totalFallingCursorClciks = 0;
        totalCritClciks = 0;
        totalGoldenFistClicks = 0;

        totalClickscensionCoins = 0;
        totalClickscensionSpent = 0;
        clickscensionUnlocked = 0;
        clickscensionUpgraded = 0;

        totalFallingCursors = 0;
        totalDiamond = 0;
        totalEmerald = 0;
        totalRainbow = 0;
        totalPurple = 0;

        totalProjectiles = 0;
        totalFallingHitByProjectile = 0;
        totalKnifes = 0;
        totalBoulders = 0;
        totalSpikes = 0;
        totalShurikens = 0;
        totalBoomerangs = 0;
        totalSpear = 0;
        totalArrows = 0;
        totalSpikeballs = 0;
        totalBullets = 0;
        totalLasers = 0;

        ballBounced = 0;
        bigBallBounced = 0;
        totalBonanzas = 0;
    }

    #region Load Data
    public void LoadData(GameData data)
    {
        // Gold-related data
        totalGold = data.totalGold;
        totalGoldActive = data.totalGoldActive;
        totalGoldPassive = data.totalGoldPassive;
        totalGoldFalling = data.totalGoldFalling;
        totalGoldCrit = data.totalGoldCrit;

        // Click-related data
        totalClicks = data.totalClicks;
        totalCursorClicks = data.totalCursorClicks;
        totalAutoClicks = data.totalAutoClicks;
        totalFallingCursorClciks = data.totalFallingCursorClciks;
        totalCritClciks = data.totalCritClciks;
        totalGoldenFistClicks = data.totalGoldenFistClicks;

        // Clickscension-related data
        totalClickscensionCoins = data.totalClickscensionCoins;
        totalClickscensionSpent = data.totalClickscensionSpent;
        clickscensionUnlocked = data.clickscensionUnlocked;
        clickscensionUpgraded = data.clickscensionUpgraded;

        // Currency-related data
        totalFallingCursors = data.totalFallingCursors;
        totalDiamond = data.totalDiamond;
        totalEmerald = data.totalEmerald;
        totalRainbow = data.totalRainbow;
        totalPurple = data.totalPurple;

        // Projectile-related data
        totalProjectiles = data.totalProjectiles;
        totalFallingHitByProjectile = data.totalFallingHitByProjectile;
        totalKnifes = data.totalKnifes;
        totalBoulders = data.totalBoulders;
        totalSpikes = data.totalSpikes;
        totalShurikens = data.totalShurikens;
        totalBoomerangs = data.totalBoomerangs;
        totalSpear = data.totalSpear;
        totalArrows = data.totalArrows;
        totalSpikeballs = data.totalSpikeballs;
        totalBullets = data.totalBullets;
        totalLasers = data.totalLasers;

        ballBounced = data.ballBounced;
        bigBallBounced = data.bigBallBounced;
        totalBonanzas = data.totalBonanzas;
    }
    #endregion

    #region Save Data
    public void SaveData(ref GameData data)
    {
        // Gold-related data
        data.totalGold = totalGold;
        data.totalGoldActive = totalGoldActive;
        data.totalGoldPassive = totalGoldPassive;
        data.totalGoldFalling = totalGoldFalling;
        data.totalGoldCrit = totalGoldCrit;

        // Click-related data
        data.totalClicks = totalClicks;
        data.totalCursorClicks = totalCursorClicks;
        data.totalAutoClicks = totalAutoClicks;
        data.totalFallingCursorClciks = totalFallingCursorClciks;
        data.totalCritClciks = totalCritClciks;
        data.totalGoldenFistClicks = totalGoldenFistClicks;

        // Clickscension-related data
        data.totalClickscensionCoins = totalClickscensionCoins;
        data.totalClickscensionSpent = totalClickscensionSpent;
        data.clickscensionUnlocked = clickscensionUnlocked;
        data.clickscensionUpgraded = clickscensionUpgraded;

        // Currency-related data
        data.totalFallingCursors = totalFallingCursors;
        data.totalDiamond = totalDiamond;
        data.totalEmerald = totalEmerald;
        data.totalRainbow = totalRainbow;
        data.totalPurple = totalPurple;

        // Projectile-related data
        data.totalProjectiles = totalProjectiles;
        data.totalFallingHitByProjectile = totalFallingHitByProjectile;
        data.totalKnifes = totalKnifes;
        data.totalBoulders = totalBoulders;
        data.totalSpikes = totalSpikes;
        data.totalShurikens = totalShurikens;
        data.totalBoomerangs = totalBoomerangs;
        data.totalSpear = totalSpear;
        data.totalArrows = totalArrows;
        data.totalSpikeballs = totalSpikeballs;
        data.totalBullets = totalBullets;
        data.totalLasers = totalLasers;

        data.ballBounced = ballBounced;
        data.bigBallBounced = bigBallBounced;
        data.totalBonanzas = totalBonanzas;
    }
    #endregion
}
