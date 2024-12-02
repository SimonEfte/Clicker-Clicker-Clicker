using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GoldenFistMechanics : MonoBehaviour
{
    public static float bonanzaCountdownTime, goldBonusCountdownTime;
    public static int activeGoldBonusIncrease, passiveGoldBonusIncrease, fallingGoldBonusIncrease;
    public static int activeGoldBonusIncreaseORIGINAL, passiveGoldBonusIncreaseORIGINAL, fallingGoldBonusIncreaseORIGINAL;
    public AudioManager audioManager;
    public Achievements achScript;
    public int fistClickGoldIncrement;

    private void Awake()
    {
        isBonanzaActive = false;
        if(MobileScript.isMobile == false) { fistClickGoldIncrement = 1650; }
        else { fistClickGoldIncrement = 1300; }

        activeGoldBonusIncreaseORIGINAL = 3;
        passiveGoldBonusIncreaseORIGINAL = 3;
        fallingGoldBonusIncreaseORIGINAL = 2;

        bonanzaCountdownTime = 10;
        goldBonusCountdownTime = 20;
    }

    public void Start()
    {
        if (Achievements.achSaves[16] == true) { goldBonusCountdownTime += 2; bonanzaCountdownTime += 2; }
        if (Achievements.achSaves[17] == true) { goldBonusCountdownTime += 4; bonanzaCountdownTime += 4; }

        if (DemoScript.isTesting == false)
        {
            fistCoroutine = StartCoroutine(WaitForGoldenFist());
        }
    }

    public GameObject goldenFirst, fistGlow;

    public Coroutine fistCoroutine;

    IEnumerator WaitForGoldenFist()
    {
        yield return new WaitForSeconds(Prestige.fistTime);
        // yield return new WaitForSeconds(15);
        SpawnFist();
    }

    public void SpawnFist()
    {
        int randomX = 0;
        int randomY = 0;

        if (MobileScript.isMobile == false)
        {
            randomX = Random.Range(-550, 550);
            randomY = Random.Range(-280, 280);
        }
        else
        {
            randomX = Random.Range(-240, 240);
            randomY = Random.Range(-700, 700);
        }
       
        goldenFirst.transform.localPosition = new Vector2(randomX, randomY);
        fistGlow.transform.localPosition = new Vector2(randomX, randomY);
        goldenFirst.SetActive(true);
        fistGlow.SetActive(true);
        audioManager.Play("FistAppear");
        goldenFirst.GetComponent<Animation>().Play();
        fistGlow.GetComponent<Animation>().Play();
        if(fistCoroutine != null) { StopCoroutine(fistCoroutine); }
        fistCoroutine = null;
    }

    private void Update()
    {
        if(GoldenFist.hitJustGold == true) { GoldReward(); GoldenFist.hitJustGold = false; }
        if (GoldenFist.hitJustPrestigePoints == true) { PrestigePointsReward(); GoldenFist.hitJustPrestigePoints = false; }
        if (GoldenFist.hitActiveGold == true) { ActiveGoldBonus(); GoldenFist.hitActiveGold = false; }
        if (GoldenFist.hitPassiveGold == true) { PassiveGoldBonus(); GoldenFist.hitPassiveGold = false; }
        if (GoldenFist.hitFallingCursors == true) { FallingCursorBonus(); GoldenFist.hitFallingCursors = false; }
        if (GoldenFist.hitProjectileBonanza == true) { ProjectileBonanza(); GoldenFist.hitProjectileBonanza = false; }

        if(GoldenFist.clickerFist == true || GoldenFist.fistDespawned == true)
        {
            GoldenFist.clickerFist = false;
            GoldenFist.fistDespawned = false;
            fistGlow.SetActive(false);
            fistCoroutine = StartCoroutine(WaitForGoldenFist());
            achScript.CheckAchievementsProgress(20);
        }
    }

    public GameObject activeBonusIcon, passiveBonusIcon, fallingBonusIcon, bonanzaIcon;
    public Image activeFill, passiveFill, fallingFill, bonanzaFill;

    #region Gold reward
    public void GoldReward()
    {
        double fistReward = (MainCursorClick.cursorClickPoint * (1 + Prestige.activeGoldIncrease)) * fistClickGoldIncrement;

        MainCursorClick.totalClickPoints += fistReward;
        Stats.totalGold += fistReward;

        if (fistReward > (LevelUp.goldNeeded - LevelUp.currentPrestigeCoins))
        {
            StartCoroutine(OverlappingGold(fistReward));
        }
        else
        {
            LevelUp.currentPrestigeCoins += fistReward;
        }

        StartCoroutine(GoldenFistRewardText(1));
    }

    IEnumerator OverlappingGold(double fistReward)
    {
        double goldLeftWith = 0;
        double currentPrestigeCoins = LevelUp.currentPrestigeCoins;
        double goldNeededCurrent = LevelUp.goldNeeded;

        while (fistReward > (goldNeededCurrent - currentPrestigeCoins))
        {
            fistReward -= (goldNeededCurrent - currentPrestigeCoins);
            goldLeftWith = (goldNeededCurrent - currentPrestigeCoins);
            currentPrestigeCoins = 0;
            goldNeededCurrent *= 2;
            LevelUp.currentPrestigeCoins += goldLeftWith;
            yield return new WaitForSeconds(0.2f);
        }
        LevelUp.currentPrestigeCoins += fistReward;
    }
    #endregion

    #region prestige point reward
    public void PrestigePointsReward()
    {
        int plussClickscension = Mathf.FloorToInt(LevelUp.clickScensionCoins * (1 + Prestige.clickscensionCoinIncrease));

        if (plussClickscension < 3) 
        {
            Prestige.clickscensionCoinsGet += 1;
        }
        else
        {
            Prestige.clickscensionCoinsGet += Mathf.FloorToInt(plussClickscension / 3f);
        }

        achScript.CheckAchievementsProgress(50);
        StartCoroutine(GoldenFistRewardText(2));
    }
    #endregion

    #region passive gold reward
    public void PassiveGoldBonus()
    {
        StartCoroutine(GoldenFistRewardText(3));
        StartCoroutine(PassiveGoldCountdown());
    }

    IEnumerator PassiveGoldCountdown()
    {
        passiveBonusIcon.SetActive(true);
        passiveGoldBonusIncrease = passiveGoldBonusIncreaseORIGINAL - 1;

        passiveFill.fillAmount = 1;

        float timer = 0;

        while (timer < goldBonusCountdownTime)
        {
            // Add the time since the last frame
            timer += Time.deltaTime;

            // Update the fill amount
            passiveFill.fillAmount = 1 - (timer / goldBonusCountdownTime);

            yield return null; // Wait for the next frame
        }

        // Ensure it completes at exactly zero
        passiveFill.fillAmount = 0;

        passiveBonusIcon.SetActive(false);
        passiveGoldBonusIncrease = 0;
    }
    #endregion

    #region Active gold reward
    public void ActiveGoldBonus()
    {
        StartCoroutine(GoldenFistRewardText(4));
        StartCoroutine(ActiveGoldCountdown());
    }

    IEnumerator ActiveGoldCountdown()
    {
        activeBonusIcon.SetActive(true);
        activeGoldBonusIncrease = activeGoldBonusIncreaseORIGINAL - 1 ;

        activeFill.fillAmount = 1;

        float timer = 0;

        while (timer < goldBonusCountdownTime)
        {
            timer += Time.deltaTime;

            activeFill.fillAmount = 1 - timer / goldBonusCountdownTime;
            yield return null;
        }

        activeFill.fillAmount = 0;

        activeBonusIcon.SetActive(false);
        activeGoldBonusIncrease = 0;
    }
    #endregion

    #region falling cursor reward
    public void FallingCursorBonus()
    {
        StartCoroutine(GoldenFistRewardText(5));
        StartCoroutine(FallingGoldCountdown());
    }

    IEnumerator FallingGoldCountdown()
    {
        fallingBonusIcon.SetActive(true);
        fallingGoldBonusIncrease = fallingGoldBonusIncreaseORIGINAL -1;

        fallingFill.fillAmount = 1;

        float timer = 0;

        while (timer < goldBonusCountdownTime)
        {
            yield return new WaitForSeconds(0.05f);
            timer += 0.05f;
            fallingFill.fillAmount = 1 - timer / goldBonusCountdownTime;
        }

        fallingBonusIcon.SetActive(false);
        fallingGoldBonusIncrease = 0;
    }
    #endregion

    #region Bonanza reward
    public static int projectileBonanzaTrigger;
    public string pojectileBonanzaText;

    public static float knicePlussChance, boulderPlussChance, spikePlussChance, shurikenPlussChance, boomerangPlussChance, spearPlussChance, laserPlussChance, spikeCircleChance, arrowPlussChance, bulletPlussChance;

    public static float boulderTimeDecrease, spikeTimeDecrease, laserTimeDecrease, spikeCircleTimeDecrease, bulletTimeDecrease;

    public GameObject[] moreBalls;
    public GameObject[] moreBigBalls;

    public GameObject knifeIcon, boulderIcon, spikeIcon, sjurikenIcon, ballIcon, boomerangIcon, spearIcon, laserIcon, bigBallIcon, arrowIcon, spikeBallIcon, turretIcon;
    public Upgrades upgradeScript;
    public static bool isBonanzaActive, isKnifeBonanza, isShurikenBonanza, isSpearBonanza, isBoomerangBonanza, isArrowBonanza;

    public void SetIconsFalse()
    {
        knifeIcon.SetActive(false); boulderIcon.SetActive(false); spikeIcon.SetActive(false);
        sjurikenIcon.SetActive(false); ballIcon.SetActive(false); boomerangIcon.SetActive(false);
        spearIcon.SetActive(false); laserIcon.SetActive(false); bigBallIcon.SetActive(false);
        arrowIcon.SetActive(false); spikeBallIcon.SetActive(false); turretIcon.SetActive(false);
    }

    public void ProjectileBonanza()
    {
        SetIconsFalse();
        bonanzaIcon.SetActive(true);
        int random = Random.Range(1, Upgrades.projectilesPurchased +1);
        projectileBonanzaTrigger = 1;
        Stats.totalBonanzas += 1;

        if (random == 1) 
        {
            knifeIcon.SetActive(true);
            pojectileBonanzaText = LocalizationStrings.projectileName[0];
            isBonanzaActive = true;
            Upgrades.bonanzaIncreaseKnife = 100;
            upgradeScript.StartShootingBonanza(1);
            isKnifeBonanza = true;
        }
        if (random == 2) 
        {
            boulderIcon.SetActive(true);
            pojectileBonanzaText = LocalizationStrings.projectileName[1];
            boulderPlussChance = 40;
            boulderTimeDecrease = 0.85f;
            if(Achievements.achSaves[18] == true) { boulderTimeDecrease = 0.88f; }
        }
        if (random == 3)
        {
            spikeIcon.SetActive(true);
            pojectileBonanzaText = LocalizationStrings.projectileName[2];
            spikePlussChance = 75;
            spikeTimeDecrease = 0.90f;
            if (Achievements.achSaves[18] == true) { spikeTimeDecrease = 0.9f; }
        }
        if (random == 4)
        {
            sjurikenIcon.SetActive(true);
            pojectileBonanzaText = LocalizationStrings.projectileName[3];
            Upgrades.bonanzaIncreaseShuriken = 100;
            isShurikenBonanza = true;
            isBonanzaActive = true;
            upgradeScript.StartShootingBonanza(2);
        }
        if (random == 5) 
        {
            ballIcon.SetActive(true);
            pojectileBonanzaText = LocalizationStrings.projectileName[4];
            for (int i = 0; i < moreBalls.Length; i++)
            {
                moreBalls[i].SetActive(true);
                moreBalls[i].transform.localPosition = new Vector2(0,0);
            }
        }
        if (random == 6)
        {
            boomerangIcon.SetActive(true);
            pojectileBonanzaText = LocalizationStrings.projectileName[5];
            Upgrades.bonanzaIncreaseBoomerang = 100;
            isBonanzaActive = true;
            upgradeScript.StartShootingBonanza(3);
            isBoomerangBonanza = true;
        }
        if (random == 7)
        {
            spearIcon.SetActive(true);
            pojectileBonanzaText = LocalizationStrings.projectileName[6];
            Upgrades.bonanzaIncreaseSpear = 100;
            isBonanzaActive = true;
            upgradeScript.StartShootingBonanza(4);
            isSpearBonanza = true;
        }
        if (random == 8)
        {
            laserIcon.SetActive(true);
            pojectileBonanzaText = LocalizationStrings.projectileName[7];
            laserPlussChance = 45;
            laserTimeDecrease = 0.85f;
            if (Achievements.achSaves[18] == true) { laserPlussChance = 0.6f; laserTimeDecrease = 0.86f; }
        }
        if (random == 9)
        {
            bigBallIcon.SetActive(true);
            pojectileBonanzaText = LocalizationStrings.projectileName[8];
            for (int i = 0; i < moreBigBalls.Length; i++)
            {
                moreBigBalls[i].SetActive(true);
                moreBigBalls[i].transform.localPosition = new Vector2(0, 0);
            }
        }
        if (random == 10)
        {
            arrowIcon.SetActive(true);
            pojectileBonanzaText = LocalizationStrings.projectileName[9];
            Upgrades.bonanzaIncreaseArrow = 100;
            isBonanzaActive = true;
            upgradeScript.StartShootingBonanza(5);
            isArrowBonanza = true;
        }
        if (random == 11)
        {
            spikeBallIcon.SetActive(true);
            pojectileBonanzaText = LocalizationStrings.projectileName[10];
            spikeCircleChance = 65;
            spikeCircleTimeDecrease = 0.7f;

            if (Achievements.achSaves[18] == true) { spikeCircleChance = 0.75f; spikeCircleTimeDecrease = 0.8f; }
        }
        if (random == 12)
        {
            turretIcon.SetActive(true);
            pojectileBonanzaText = LocalizationStrings.projectileName[11];
            bulletPlussChance = 100;
            bulletTimeDecrease = 0.41f;

            if (Achievements.achSaves[18] == true) { bulletTimeDecrease = 0.4f;}
        }

        StartCoroutine(GoldenFistRewardText(6)); StartCoroutine(BonanzaCountdown());
    }

    IEnumerator BonanzaCountdown()
    {
        bonanzaFill.fillAmount = 1;

        float timer = 0;

        while (timer < bonanzaCountdownTime)
        {
            yield return new WaitForSeconds(0.05f);
            timer += 0.05f;
            bonanzaFill.fillAmount = 1 - timer/ bonanzaCountdownTime;
        }

        projectileBonanzaTrigger = 0;
        knicePlussChance = 0;
        boulderPlussChance = 0;
        boulderTimeDecrease = 0;
        spikePlussChance = 0;
        spikeTimeDecrease = 0f;
        shurikenPlussChance = 0;
        boomerangPlussChance = 0;
        spearPlussChance = 0;
        laserPlussChance = 0;
        laserTimeDecrease = 0f;
        arrowPlussChance = 0;
        spikeCircleChance = 0;
        spikeCircleTimeDecrease = 0f;
        bulletPlussChance = 0;
        bulletTimeDecrease = 0f;

        for (int i = 0; i < moreBalls.Length; i++)
        {
            moreBalls[i].SetActive(false);
        }

        for (int i = 0; i < moreBigBalls.Length; i++)
        {
            moreBigBalls[i].SetActive(false);
        }
        bonanzaIcon.SetActive(false);
        isBonanzaActive = false;
        isKnifeBonanza = false;
        isShurikenBonanza = false;
        isSpearBonanza = false;
        isBoomerangBonanza = false;
        isArrowBonanza = false;
        SettingsAndUI.spawnBonanza = false;
        upgradeScript.StopBonanza();
    }
    #endregion

    public TextMeshProUGUI firstText;
    IEnumerator GoldenFistRewardText(int reward)
    {
        if (reward == 1) { firstText.text = $"{LocalizationStrings.plussClickGoldFist}\n+{ScaleNumbers.FormatPoints((MainCursorClick.cursorClickPoint * (1 + Prestige.activeGoldIncrease)) * fistClickGoldIncrement)}"; firstText.color = Color.yellow; }
        if (reward == 2) 
        {
            int plussClickscension = Mathf.FloorToInt(LevelUp.clickScensionCoins * (1 + Prestige.clickscensionCoinIncrease));

            if (plussClickscension < 3)
            {
                firstText.text = $"{LocalizationStrings.clickscensionCoinsFist}!\n+{1}!";
            }
            else
            {
                firstText.text = $"{LocalizationStrings.clickscensionCoinsFist}!\n+{(Mathf.FloorToInt(plussClickscension / 2.5f).ToString("F0"))}!";
            }

            firstText.color = Color.magenta;
        }
        if (reward == 3) { firstText.text = $"{LocalizationStrings.passiveGoldFist}\n({goldBonusCountdownTime} {LocalizationStrings.secondsFist})"; firstText.color = Color.yellow; }
        if (reward == 4) { firstText.text = $"{LocalizationStrings.activeGoldFist}\n({goldBonusCountdownTime} {LocalizationStrings.secondsFist})"; firstText.color = Color.yellow; }
        if (reward == 5) { firstText.text = $"{LocalizationStrings.fallingCursorGoldFist}\n({goldBonusCountdownTime} {LocalizationStrings.secondsFist})"; firstText.color = Color.yellow; }
        if (reward == 6) { firstText.text = $"{LocalizationStrings.projectileBonanzaFist}\n {pojectileBonanzaText}! ({bonanzaCountdownTime} {LocalizationStrings.secondsFist})"; firstText.color = Color.yellow; }

        firstText.gameObject.SetActive(true);
        firstText.gameObject.transform.localScale = new Vector2(0, 0);
        firstText.gameObject.transform.localPosition = GoldenFist.fistPos;

        float reachScale = 1.3f;
        float duration = 0.3f; // Time to reach the scale (in seconds)
        float elapsedTime = 0f;

        // First scaling phase (0 -> reachScale)
        while (elapsedTime < duration)
        {
            float scaleValue = Mathf.Lerp(0f, reachScale, elapsedTime / duration);
            firstText.gameObject.transform.localScale = new Vector2(scaleValue, scaleValue);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }
        firstText.gameObject.transform.localScale = new Vector2(reachScale, reachScale);

        float reachScale2 = 1.6f;
        duration = 1.6f; // Adjust this duration as needed
        elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float scaleValue = Mathf.Lerp(reachScale, reachScale2, elapsedTime / duration);
            firstText.gameObject.transform.localScale = new Vector2(scaleValue, scaleValue);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }
        firstText.gameObject.transform.localScale = new Vector2(reachScale2, reachScale2); //

        float reachScale3 = 0f;
        duration = 0.3f; // Time to scale down
        elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float scaleValue = Mathf.Lerp(reachScale2, reachScale3, elapsedTime / duration);
            firstText.gameObject.transform.localScale = new Vector2(scaleValue, scaleValue);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }
        firstText.gameObject.transform.localScale = new Vector2(0, 0);
        firstText.gameObject.SetActive(false);
    }

    public void ResetFirst()
    {
        activeBonusIcon.SetActive(false);
        passiveBonusIcon.SetActive(false);
        fallingBonusIcon.SetActive(false);
        bonanzaIcon.SetActive(false);

        passiveGoldBonusIncrease = 0;
        activeGoldBonusIncrease = 0;
        fallingGoldBonusIncrease = 0;

        projectileBonanzaTrigger = 0;
        knicePlussChance = 0;
        boulderPlussChance = 0;
        boulderTimeDecrease = 0;
        spikePlussChance = 0;
        spikeTimeDecrease = 0f;
        shurikenPlussChance = 0;
        boomerangPlussChance = 0;
        spearPlussChance = 0;
        laserPlussChance = 0;
        laserTimeDecrease = 0f;
        arrowPlussChance = 0;
        spikeCircleChance = 0;
        spikeCircleTimeDecrease = 0f;
        bulletPlussChance = 0;
        bulletTimeDecrease = 0f;

        for (int i = 0; i < moreBalls.Length; i++)
        {
            moreBalls[i].SetActive(false);
        }

        for (int i = 0; i < moreBigBalls.Length; i++)
        {
            moreBigBalls[i].SetActive(false);
        }
        bonanzaIcon.SetActive(false);

        goldenFirst.SetActive(false);
        fistGlow.SetActive(false);

        if(fistCoroutine != null) { StopCoroutine(fistCoroutine); fistCoroutine = StartCoroutine(WaitForGoldenFist()); }
        else { fistCoroutine = StartCoroutine(WaitForGoldenFist()); }
    }

}
