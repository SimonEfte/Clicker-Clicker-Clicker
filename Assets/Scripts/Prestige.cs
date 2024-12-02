using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Prestige : MonoBehaviour, IDataPersistence
{
    public static float clickscensionCoins;
    public static float clickscensionCoinsGet;
    public static bool isInPrestige;
    public AudioManager audioManager;
    public Achievements achScript;

    public static int diamondIncrease, emeraldIncrease, rainbowTotalCursors, rainbowMinIncrease, rainbowMaxIncrease;

    //Prestige upgradable variables

    public static int[] prestigeUpgradeLevel = new int[12];
    public static int[] prestigeUpgradePrice = new int[12];

    public static int  totalRainbowBonus;
    public static int minRanbowIncrease, maxRandomIncrease;

    public static int needForPrestige, timesPrestiged;

    private void Awake()
    {
        minRanbowIncrease = 2;
        maxRandomIncrease = 7;

        rainbowTotalCursors = 15;
        rainbowMinIncrease = 2;
        rainbowMaxIncrease = 6;

        diamondIncrease = 2;
        emeraldIncrease = 4;
    }

    public void Start()
    {
        for (int i = 0; i < unlockedPrestigeUpgrade.Length; i++)
        {
            if(unlockedPrestigeUpgrade[i] == true) { clickscensionUpgradeIcon[i].color = new Color32(255, 255, 255, 255); }
        }
    }

    #region Update
    public TextMeshProUGUI totalClickscentionCoinsText, clickscensionCoinGetText;
    private void Update()
    {
        if (SettingsAndUI.isInPrestigeFrame == true)
        {
            clickscensionCoinGetText.text = "+" + clickscensionCoinsGet.ToString("F0");
            totalClickscentionCoinsText.text = "" +clickscensionCoins.ToString("F0");

            clickscensionCoinGetText.color = Color.magenta;
            totalClickscentionCoinsText.color = Color.magenta;

            if(currentUpgradeSelected == 5)
            {
                if (fistTime <= 27)
                {
                    priceText.text = LocalizationStrings.max;
                    priceText.color = Color.red;
                }
                else
                {
                    if (clickscensionCoins < prestigeUpgradePrice[currentUpgradeSelected]) { priceText.color = Color.red; }
                    else { priceText.color = Color.green; }
                }
            }
            else
            {
                if (clickscensionCoins < prestigeUpgradePrice[currentUpgradeSelected]) { priceText.color = Color.red; }
                else { priceText.color = Color.green; }
            }

            upgradeLevelText.text = LocalizationStrings.levelText + prestigeUpgradeLevel[currentUpgradeSelected].ToString();

            if (clickscensionCoinsGet >= needForPrestige)
            {
                needForPrestigeText.color = Color.green;
            }
            else
            {
                needForPrestigeText.color = Color.red;
            }

            needForPrestigeText.text = $"({LocalizationStrings.need}{needForPrestige})";
        }
    }
    #endregion

    #region Prestige for real
    public DataPersistenceManager dataPersistanceScript;
    public GameObject chooseToAscendFrame, ascendBlack;
    public TextMeshProUGUI needForPrestigeText;

    public void OpenChooseToAscend()
    {
        if(clickscensionCoinsGet >= needForPrestige)
        {
            audioManager.Play("UI_Click1");
            chooseToAscendFrame.SetActive(true);
            ascendBlack.SetActive(true);
        }
        else
        {
            audioManager.Play("Locked");
        }
    }

    public void CloseChooseToAscend()
    {
        audioManager.Play("UI_Click1");
        chooseToAscendFrame.SetActive(false);
        ascendBlack.SetActive(false);
    }

    public void PrestigeForReal()
    {
        timesPrestiged += 1;
        if (timesPrestiged > 1) 
        {
            float newNeeded = (float)needForPrestige * 1.192f;

            needForPrestige = Mathf.FloorToInt(newNeeded);
        }
        StartCoroutine(PrestigeCircle());
    }

    public Image resetCirlce;

    IEnumerator PrestigeCircle()
    {
        audioManager.Play("Transition1");

        resetCirlce.fillAmount = 0;
        resetCirlce.gameObject.SetActive(true);

        float duration = 0.5f; // Total duration of the transition
        float elapsedTime = 0f;

        // Fill the circle over time
        while (elapsedTime < duration)
        {
            resetCirlce.fillAmount = Mathf.Lerp(0f, 1f, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }
        resetCirlce.fillAmount = 1f;

        yield return new WaitForSeconds(0.5f);
        CloseChooseToAscend();
        PrestigeResetStuff();
        audioManager.Play("Transition1");

        clickscensionCoins += clickscensionCoinsGet;
        Stats.totalClickscensionCoins += clickscensionCoinsGet;
        clickscensionCoinsGet = 0;
        MainCursorClick.totalClickPoints = startWithGoldAmount;
      
        // Reset elapsed time for the reverse transition
        elapsedTime = 0f;

        // Unfill the circle over time
        while (elapsedTime < duration)
        {
            resetCirlce.fillAmount = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }
        resetCirlce.fillAmount = 0f;

        resetCirlce.gameObject.SetActive(false);
        SaveGame();
        MainCursorClick.currentGold = 1000;
    }

    public MainCursorClick mainScript;
    public Upgrades upgradeScript;
    public SettingsAndUI settingsScipt;
    public LevelUp levelScript;
    public GoldenFistMechanics fistScript;

    public void PrestigeResetStuff()
    {
        fistScript.ResetFirst();
        levelScript.ResetLevelUp();
        settingsScipt.SetProjectilesInactive();
        mainScript.ResetMain();
        upgradeScript.ResetUpgrades();
    }

    public void SaveGame()
    {
        dataPersistanceScript.SaveGame();
    }
    #endregion

    #region Select upgrade
    public GameObject selectedFrame;
    public GameObject[] prestigeUpgrade;
    public GameObject[] prestigeTextParent;
    public GameObject selectAUpgradeBar, levelUpBtn;
    public TextMeshProUGUI upgradeLevelText;

    public static int currentUpgradeSelected;
    public GameObject prestigeTooltipIcon;

    public void SelectPrestigeUpgrade(int upgrade)
    {
        audioManager.Play("UI_Click1");
        levelUpBtn.SetActive(true);
        upgradeLevelText.gameObject.SetActive(true);
        selectAUpgradeBar.SetActive(false);

        currentUpgradeSelected = upgrade;
        priceText.text = prestigeUpgradePrice[currentUpgradeSelected].ToString();
        upgradeLevelText.text = LocalizationStrings.levelText + prestigeUpgradeLevel[currentUpgradeSelected].ToString();

        for (int i = 0; i < prestigeTextParent.Length; i++)
        {
            prestigeTextParent[i].SetActive(false);
        }

        prestigeTextParent[upgrade].SetActive(true);

        selectedFrame.SetActive(true);
        selectedFrame.transform.SetParent(prestigeUpgrade[upgrade].transform);
        selectedFrame.transform.localPosition = new Vector2(0,0.8f);

        if(upgrade < 5) { prestigeTooltipIcon.SetActive(true); }
        else { prestigeTooltipIcon.SetActive(false); }

        DisplayUpgradeText(currentUpgradeSelected);
    }
    #endregion

    #region Upgrade prestige upgrade
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI diamondChanceText, emeraldChanceText, rainbowChanceText, purpleChanceText, tierText, fistTimeText, activeGoldText, passiveGoldText, startWithGoldText, clickscensionCoinIncreaseText, betterPRojectileText, betterClickText;
    public static float diamondChance, emeraldChance, rainbowChance, purpleChance, fistTime, activeGoldIncrease, passiveGoldIncrease, projectileUpgradeIncrease, clickUpgradeIncrease;
    public static int fallingCursorTier;
    public static float minFallingCursorIncrease, maxFallingCursorIncrease;
    public static float clickscensionCoinIncrease;
    public static double startWithGoldAmount;

    public static float diamondChanceIncrement, emeraldChanceIncrement, rainbowChanceIncrement, purpleChanceIncrement, fistTimeIncrement, activeGoldIncreaseIncrement, passiveGoldIncreaseIncrement, projectileUpgradeIncreaseIncrement, clickUpgradeIncreaseIncrement;
    public static float minFallingCursorIncreaseIncrement, maxFallingCursorIncreaseIncrement;
    public static float clickscensionCoinIncreaseIncrement;
    public static double startWithGoldAmountIncrement;

    public static bool[] unlockedPrestigeUpgrade = new bool[12];
    public Image[] clickscensionUpgradeIcon;
    public static int highestLevelUpgrade;

    public void UpgradePrestigeUpgrade()
    {
        if (clickscensionCoins >= prestigeUpgradePrice[currentUpgradeSelected])
        {
            audioManager.Play("Upgrade");
            Stats.clickscensionUpgraded += 1;
           
            if (unlockedPrestigeUpgrade[currentUpgradeSelected] == false) 
            {
                unlockedPrestigeUpgrade[currentUpgradeSelected] = true;
                clickscensionUpgradeIcon[currentUpgradeSelected].color = new Color32(255, 255, 255, 255);
                Stats.clickscensionUnlocked += 1;
            }

            if (currentUpgradeSelected == 5 && fistTime <= 27)
            {
            }
            else
            {
                prestigeUpgradeLevel[currentUpgradeSelected] += 1;
                clickscensionCoins -= prestigeUpgradePrice[currentUpgradeSelected];
                Stats.totalClickscensionSpent += prestigeUpgradePrice[currentUpgradeSelected];
            }
               
            if(currentUpgradeSelected == 0) { prestigeUpgradePrice[currentUpgradeSelected] += 2; }
            if (currentUpgradeSelected == 1) { prestigeUpgradePrice[currentUpgradeSelected] += 3; }
            if (currentUpgradeSelected == 2) { prestigeUpgradePrice[currentUpgradeSelected] += 4; }
            if (currentUpgradeSelected == 3) { prestigeUpgradePrice[currentUpgradeSelected] += 6; }
            if (currentUpgradeSelected == 4) 
            { 
                if(fallingCursorTier == 1) { prestigeUpgradePrice[currentUpgradeSelected] += 1250; }
                else if (fallingCursorTier == 2) { prestigeUpgradePrice[currentUpgradeSelected] += 7500; }
                else if (fallingCursorTier == 3) { prestigeUpgradePrice[currentUpgradeSelected] += 45000; }
                else if (fallingCursorTier == 4) { prestigeUpgradePrice[currentUpgradeSelected] += 100000; }
            }
            if (currentUpgradeSelected == 5) { prestigeUpgradePrice[currentUpgradeSelected] += 3; }
            if (currentUpgradeSelected == 6) { prestigeUpgradePrice[currentUpgradeSelected] += 2; }
            if (currentUpgradeSelected == 7) { prestigeUpgradePrice[currentUpgradeSelected] += 2; }
            if (currentUpgradeSelected == 8) { prestigeUpgradePrice[currentUpgradeSelected] += 1; }
            if (currentUpgradeSelected == 9) { prestigeUpgradePrice[currentUpgradeSelected] += 2; }
            if (currentUpgradeSelected == 10) { prestigeUpgradePrice[currentUpgradeSelected] += 3; }
            if (currentUpgradeSelected == 11) { prestigeUpgradePrice[currentUpgradeSelected] += 3; }

            priceText.text = prestigeUpgradePrice[currentUpgradeSelected].ToString();

         
            if (prestigeUpgradeLevel[currentUpgradeSelected] > highestLevelUpgrade) { highestLevelUpgrade = prestigeUpgradeLevel[currentUpgradeSelected]; }
            upgradeLevelText.text = LocalizationStrings.levelText + prestigeUpgradeLevel[currentUpgradeSelected].ToString();

            if(currentUpgradeSelected == 0)
            { 
                diamondChance += diamondChanceIncrement;
                diamondChanceIncrement *= 0.99f;
            }
            else if (currentUpgradeSelected == 1) 
            { 
                emeraldChance += emeraldChanceIncrement;
                emeraldChanceIncrement *= 0.993f;
            }
            else if (currentUpgradeSelected == 2) 
            {
                rainbowChance += rainbowChanceIncrement;
                rainbowChanceIncrement *= 0.992f;
            }
            else if (currentUpgradeSelected == 3) 
            { 
                purpleChance += purpleChanceIncrement;
                purpleChanceIncrement *= 0.994f;
            }
            else if (currentUpgradeSelected == 4) 
            { 
                fallingCursorTier += 1;
                minFallingCursorIncrease += minFallingCursorIncreaseIncrement;
                maxFallingCursorIncrease += maxFallingCursorIncreaseIncrement;
            }
            else if (currentUpgradeSelected == 5) 
            {
                if (fistTime <= 27)
                {
                    audioManager.Play("Locked");
                }
                else
                {
                    fistTime -= fistTimeIncrement;
                    if (fistTime < 90) { fistTimeIncrement = 0.9f; }
                    if (fistTime < 85) { fistTimeIncrement = 0.8f; }
                    if (fistTime < 80) { fistTimeIncrement = 0.7f; }
                    if (fistTime < 75) { fistTimeIncrement = 0.6f; }
                    if (fistTime < 70) { fistTimeIncrement = 0.5f; }
                    if (fistTime < 65) { fistTimeIncrement = 0.4f; }
                    if (fistTime < 60) { fistTimeIncrement = 0.3f; }
                    if (fistTime < 50) { fistTimeIncrement = 0.2f; }
                    if (fistTime < 40) { fistTimeIncrement = 0.1f; }
                    if (fistTime <= 27) { fistTimeIncrement = 0f; fistTime = 27; }
                }
            }
            else if (currentUpgradeSelected == 6)
            {
                activeGoldIncrease += activeGoldIncreaseIncrement;
            }
            else if (currentUpgradeSelected == 7) 
            { 
                passiveGoldIncrease += passiveGoldIncreaseIncrement;
            }

            else if (currentUpgradeSelected == 8)
            { 
                startWithGoldAmount += startWithGoldAmountIncrement; 
            }
            else if (currentUpgradeSelected == 9) 
            { 
                clickscensionCoinIncrease += clickscensionCoinIncreaseIncrement;
                //clickscensionCoinIncreaseIncrement += 0.01f;
            }
            else if (currentUpgradeSelected == 10) { projectileUpgradeIncrease += projectileUpgradeIncreaseIncrement; }
            else if (currentUpgradeSelected == 11) { clickUpgradeIncrease += clickUpgradeIncreaseIncrement; }

            DisplayUpgradeText(currentUpgradeSelected);
            achScript.CheckAchievementsProgress(50);
        }
        else
        {
            audioManager.Play("Locked");
        }
    }
    #endregion

    #region Display price and upgrade amount texts
    public void DisplayUpgradeText(int upgrade)
    {
        if(upgrade == 0) { diamondChanceText.text = $"<color=green>{diamondChance.ToString("F3")}% +({diamondChanceIncrement.ToString("F3")}%)"; }
        else if (upgrade == 1) { emeraldChanceText.text = $"<color=green>{emeraldChance.ToString("F3")}% +({emeraldChanceIncrement.ToString("F3")}%)"; }
        else if (upgrade == 2) { rainbowChanceText.text = $"<color=green>{rainbowChance.ToString("F3")}% +({rainbowChanceIncrement.ToString("F3")}%)"; }
        else if (upgrade == 3) { purpleChanceText.text = $"<color=green>{purpleChance.ToString("F3")}% +({purpleChanceIncrement.ToString("F3")}%)"; }
        else if (upgrade == 4) { tierText.text = $"<color=green>{LocalizationStrings.tier}{fallingCursorTier.ToString("F0")} +(1)"; }
        else if (upgrade == 5) { fistTimeText.text = $"<color=green>{fistTime.ToString("F0")} {LocalizationStrings.secondsPrestige} -({fistTimeIncrement.ToString("F1")})"; }
        else if (upgrade == 6) { activeGoldText.text = $"<color=green>{(activeGoldIncrease * 100).ToString("F0")}% +({(activeGoldIncreaseIncrement *100).ToString("F0")}%)"; }
        else if (upgrade == 7) { passiveGoldText.text = $"<color=green>{(passiveGoldIncrease * 100).ToString("F0")}% +({(passiveGoldIncreaseIncrement * 100).ToString("F0")}%)"; }
        else if (upgrade == 8) { startWithGoldText.text = $"<color=green>{startWithGoldAmount.ToString("F0")} +({startWithGoldAmountIncrement.ToString("F0")})"; }
        else if (upgrade == 9) { clickscensionCoinIncreaseText.text = $"<color=green>{(clickscensionCoinIncrease * 100).ToString("F0")}% +({(clickscensionCoinIncreaseIncrement * 100).ToString("F0")}%)"; }
        else if (upgrade == 10) { betterPRojectileText.text = $"<color=green>{(projectileUpgradeIncrease * 100).ToString("F2")}% +({(projectileUpgradeIncreaseIncrement * 100).ToString("F2")}%)"; }
        else if (upgrade == 11) { betterClickText.text = $"<color=green>{(clickUpgradeIncrease * 100).ToString("F2")}% +({(clickUpgradeIncreaseIncrement * 100).ToString("F2")}%)"; }
    }
    #endregion

    #region Load Data
    public void LoadData(GameData data)
    {
        highestLevelUpgrade = data.highestLevelUpgrade;

        clickscensionCoins = data.clickscensionCoins;
        clickscensionCoinsGet = data.clickscensionCoinsGet;

        for (int i = 0; i < prestigeUpgradeLevel.Length; i++)
        {
            prestigeUpgradeLevel[i] = data.prestigeUpgradeLevel[i];
            prestigeUpgradePrice[i] = data.prestigeUpgradePrice[i];
        }

        for (int i = 0; i < unlockedPrestigeUpgrade.Length; i++)
        {
            unlockedPrestigeUpgrade[i] = data.unlockedPrestigeUpgrade[i];
        }

        diamondChance = data.diamondChance;
        emeraldChance = data.emeraldChance;
        rainbowChance = data.rainbowChance;
        purpleChance = data.purpleChance;
        fistTime = data.fistTime;
        activeGoldIncrease = data.activeGoldIncrease;
        passiveGoldIncrease = data.passiveGoldIncrease;
        projectileUpgradeIncrease = data.projectileUpgradeIncrease;
        clickUpgradeIncrease = data.clickUpgradeIncrease;
        fallingCursorTier = data.fallingCursorTier;
        minFallingCursorIncrease = data.minFallingCursorIncrease;
        maxFallingCursorIncrease = data.maxFallingCursorIncrease;
        clickscensionCoinIncrease = data.clickscensionCoinIncrease;
        startWithGoldAmount = data.startWithGoldAmount;

        diamondChanceIncrement = data.diamondChanceIncrement;
        emeraldChanceIncrement = data.emeraldChanceIncrement;
        rainbowChanceIncrement = data.rainbowChanceIncrement;
        purpleChanceIncrement = data.purpleChanceIncrement;
        fistTimeIncrement = data.fistTimeIncrement;
        activeGoldIncreaseIncrement = data.activeGoldIncreaseIncrement;
        passiveGoldIncreaseIncrement = data.passiveGoldIncreaseIncrement;
        projectileUpgradeIncreaseIncrement = data.projectileUpgradeIncreaseIncrement;
        clickUpgradeIncreaseIncrement = data.clickUpgradeIncreaseIncrement;
        minFallingCursorIncreaseIncrement = data.minFallingCursorIncreaseIncrement;
        maxFallingCursorIncreaseIncrement = data.maxFallingCursorIncreaseIncrement;
        clickscensionCoinIncreaseIncrement = data.clickscensionCoinIncreaseIncrement;
        startWithGoldAmountIncrement = data.startWithGoldAmountIncrement;

        needForPrestige = data.needForPrestige;
        timesPrestiged = data.timesPrestiged;
    }
    #endregion

    #region Save Data
    public void SaveData(ref GameData data)
    {
        data.highestLevelUpgrade = highestLevelUpgrade;

        data.clickscensionCoins = clickscensionCoins;
        data.clickscensionCoinsGet = clickscensionCoinsGet;

        for (int i = 0; i < prestigeUpgradeLevel.Length; i++)
        {
            data.prestigeUpgradeLevel[i] = prestigeUpgradeLevel[i];
            data.prestigeUpgradePrice[i] = prestigeUpgradePrice[i];
        }

        for (int i = 0; i < unlockedPrestigeUpgrade.Length; i++)
        {
            data.unlockedPrestigeUpgrade[i] = unlockedPrestigeUpgrade[i];
        }

        data.diamondChance = diamondChance;
        data.emeraldChance = emeraldChance;
        data.rainbowChance = rainbowChance;
        data.purpleChance = purpleChance;
        data.fistTime = fistTime;
        data.activeGoldIncrease = activeGoldIncrease;
        data.passiveGoldIncrease = passiveGoldIncrease;
        data.projectileUpgradeIncrease = projectileUpgradeIncrease;
        data.clickUpgradeIncrease = clickUpgradeIncrease;
        data.fallingCursorTier = fallingCursorTier;
        data.minFallingCursorIncrease = minFallingCursorIncrease;
        data.maxFallingCursorIncrease = maxFallingCursorIncrease;
        data.clickscensionCoinIncrease = clickscensionCoinIncrease;
        data.startWithGoldAmount = startWithGoldAmount;

        data.diamondChanceIncrement = diamondChanceIncrement;
        data.emeraldChanceIncrement = emeraldChanceIncrement;
        data.rainbowChanceIncrement = rainbowChanceIncrement;
        data.purpleChanceIncrement = purpleChanceIncrement;
        data.fistTimeIncrement = fistTimeIncrement;
        data.activeGoldIncreaseIncrement = activeGoldIncreaseIncrement;
        data.passiveGoldIncreaseIncrement = passiveGoldIncreaseIncrement;
        data.projectileUpgradeIncreaseIncrement = projectileUpgradeIncreaseIncrement;
        data.clickUpgradeIncreaseIncrement = clickUpgradeIncreaseIncrement;
        data.minFallingCursorIncreaseIncrement = minFallingCursorIncreaseIncrement;
        data.maxFallingCursorIncreaseIncrement = maxFallingCursorIncreaseIncrement;
        data.clickscensionCoinIncreaseIncrement = clickscensionCoinIncreaseIncrement;
        data.startWithGoldAmountIncrement = startWithGoldAmountIncrement;

        data.needForPrestige = needForPrestige;
        data.timesPrestiged = timesPrestiged;
    }
    #endregion

    #region Reset prestige
    public void ResetPrestige()
    {
        clickscensionCoins = 0;
        clickscensionCoinsGet = 0;

        for (int i = 0; i < prestigeUpgradeLevel.Length; i++)
        {
            prestigeUpgradeLevel[i] = 0;
            clickscensionUpgradeIcon[i].color = new Color32(166, 166, 166, 194);
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
        needForPrestige = 15;
        timesPrestiged = 0;
    }
    #endregion
}
