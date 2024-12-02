using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //Main cursor click variables
    public double totalClickPoints, cursorClickPoint, totalPassivePoints;

    //Upgrades variables
    public int projectilesPurchased, firstTimePurchaseProjctile;
    public float cursorAOE;
    public bool purchasedAutoClicker, purchasedClickAOE;
    public float[] projectileChanceIncrement = new float[12];
    public float[] projectileChance = new float[12];
    public double[] projectilePrice = new double[12];
    public double[] projectileUpgradePrice = new double[12];

    public double activePrice, passivePrice;

    public double[] clickerUpgradePrice = new double[4];
    public double activeIncrement, passiveIncrement;

    public float critChance, critChanceIncrement, critIncrease, critIncreaseIncrement;
    public float autoClickerDuration, autoClickerPerSecondDisplay;
    public float AOEclicksIcrement;
    public float autoClickerIncrement;

    public bool firstTimePurchaseAutoClicker;
    public bool isAOEclicksPurchased;

    public int passiveUpgradeCount;

    public static double firstProjectilePrice;
    public int timesActive, timesPassive;

    public int projectileAutoNumber, clickAutoNumber;
    public bool isAuto, isAutoClick;
    public int currentClickscensionCoins;

    public bool isAutoActivePassive, isActiveAuto;

    //Stats
    public double totalGold, totalGoldActive, totalGoldPassive, totalGoldFalling, totalGoldCrit;
    public int totalClicks, totalCursorClicks, totalAutoClicks, totalFallingCursorClciks, totalCritClciks, totalGoldenFistClicks;
    public int totalClickscensionSpent, clickscensionUnlocked, clickscensionUpgraded;
    public int totalFallingCursors, totalDiamond, totalEmerald, totalRainbow, totalPurple;
    public int totalProjectiles, totalFallingHitByProjectile, totalKnifes, totalBoulders, totalSpikes, totalShurikens, totalBoomerangs, totalSpear, totalArrows, totalSpikeballs, totalBullets, totalLasers, ballBounced, bigBallBounced, totalBonanzas;
    public float totalClickscensionCoins;

    //Prestige
    public int[] prestigeUpgradeLevel = new int[12];
    public int[] prestigeUpgradePrice = new int[12];

    public float clickscensionCoins;
    public float clickscensionCoinsGet;

    public float diamondChance, emeraldChance, rainbowChance, purpleChance, fistTime, activeGoldIncrease, passiveGoldIncrease, projectileUpgradeIncrease, clickUpgradeIncrease;
    public int fallingCursorTier;
    public float minFallingCursorIncrease, maxFallingCursorIncrease;
    public float clickscensionCoinIncrease;
    public double startWithGoldAmount;

    public float diamondChanceIncrement, emeraldChanceIncrement, rainbowChanceIncrement, purpleChanceIncrement, fistTimeIncrement, activeGoldIncreaseIncrement, passiveGoldIncreaseIncrement, projectileUpgradeIncreaseIncrement, clickUpgradeIncreaseIncrement;
    public float minFallingCursorIncreaseIncrement, maxFallingCursorIncreaseIncrement;
    public float clickscensionCoinIncreaseIncrement;
    public double startWithGoldAmountIncrement;
    public bool[] unlockedPrestigeUpgrade = new bool[12];
    public int highestLevelUpgrade;
    public float clickScensionCoins;
    public int needForPrestige, timesPrestiged;

    //Level
    public int level, totalLevel;
    public double goldNeeded;
    public double currentPrestigeCoins;
    public double tierMultiplier = 1;
    public double baseValue = 200000;

    //Achievements
    public bool[] achSaves = new bool[55];
    public int totalAchCompleted;
    public bool hideCompleted;

    //Skins
    public int currentlySelected;
    public bool[] cursorUnlocked = new bool[11];

    public int projectileMaxSelect, clickerMaxSelect;

    public GameData()
    {
        isAutoActivePassive = false;
        isActiveAuto = true;
        currentClickscensionCoins = 0;

        projectileMaxSelect = 1;
        clickerMaxSelect = 1;

        firstProjectilePrice = 100;

        projectileAutoNumber = 0;
        clickAutoNumber = 0;
        isAuto = false;
        isAutoClick = false;

        #region Main cursor click saves
        totalClickPoints = 0;
        cursorClickPoint = 1;
        totalPassivePoints = 0;
        #endregion

        #region Upgrades saves
        activePrice = 10;
        passivePrice = 10;
        activeIncrement = 0.25;
        passiveIncrement = 1.8f;

        passiveUpgradeCount = 0;

        cursorAOE = 0.1f;
        AOEclicksIcrement = 0.025f;
        //0,353
        critChance = 0;
        critIncrease = 1;
        critChanceIncrement = 0.2f;
        critIncreaseIncrement = 0.10f;

        autoClickerDuration = 2.83f;
        autoClickerIncrement = 0.6f;

        projectilesPurchased = 0;
        firstTimePurchaseProjctile = 0;
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

        timesActive = 0;
        timesPassive = 0;
        #endregion

        #region Prestige
        clickscensionCoins = 0;
        clickscensionCoinsGet = 0;

        for (int i = 0; i < prestigeUpgradeLevel.Length; i++)
        {
            prestigeUpgradeLevel[i] = 0;
        }
      
        prestigeUpgradePrice[0] = 1;  //Diamond Falling cursors
        prestigeUpgradePrice[1] = 2;  //Emerald Falling cursors
        prestigeUpgradePrice[2] = 3;  //Rainbow Falling cursors
        prestigeUpgradePrice[3] = 4;  //Purple Falling cursors
        prestigeUpgradePrice[4] = 350; //Tier
        prestigeUpgradePrice[5] = 2; //Fist
        prestigeUpgradePrice[6] = 1; //Active gold
        prestigeUpgradePrice[7] = 1; //Passive gold
        prestigeUpgradePrice[8] = 1; //Start with gold
        prestigeUpgradePrice[9] = 1; //Clickscension coins
        prestigeUpgradePrice[10] = 2; //Better projectiles
        prestigeUpgradePrice[11] = 2; //Better click

        diamondChance = 0f;
        emeraldChance = 0f;
        rainbowChance = 0f;
        purpleChance = 0f;
        fistTime = 100f;
        activeGoldIncrease = 0f;
        passiveGoldIncrease = 0f;
        projectileUpgradeIncrease = 0f;
        clickUpgradeIncrease = 0f;
        minFallingCursorIncrease = 0f;
        maxFallingCursorIncrease = 0.75f;
        clickscensionCoinIncrease = 0f;
        fallingCursorTier = 1;
        startWithGoldAmount = 0;

        diamondChanceIncrement = 0.25f;
        emeraldChanceIncrement = 0.15f;
        rainbowChanceIncrement = 0.1f;
        purpleChanceIncrement = 0.035f;
        fistTimeIncrement = 1f;
        activeGoldIncreaseIncrement = 0.20f;
        passiveGoldIncreaseIncrement = 0.3f;
        projectileUpgradeIncreaseIncrement = 0.005f;
        clickUpgradeIncreaseIncrement = 0.005f;
        minFallingCursorIncreaseIncrement = 1f;
        maxFallingCursorIncreaseIncrement = 1f;
        clickscensionCoinIncreaseIncrement = 0.1f;
        startWithGoldAmountIncrement = 250;

        for (int i = 0; i < unlockedPrestigeUpgrade.Length; i++)
        {
            unlockedPrestigeUpgrade[i] = false;
        }

        highestLevelUpgrade = 0;
        clickScensionCoins = 1;
        needForPrestige = 15;
        timesPrestiged = 0;
        #endregion

        #region Stats
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
        #endregion

        #region Level up
        level = 0;
        totalLevel = 0;
        currentPrestigeCoins = 0;
        tierMultiplier = 1;
        baseValue = 200000;
        goldNeeded = baseValue;
        #endregion

        #region ACH
        for (int i = 0; i < achSaves.Length; i++)
        {
            achSaves[i] = false;
        }

        totalAchCompleted = 0;
        hideCompleted = true;
        #endregion

        #region Skins
        currentlySelected = 0;
        for (int i = 0; i < cursorUnlocked.Length; i++)
        {
            cursorUnlocked[i] = false;
        }
        #endregion
    }
}
