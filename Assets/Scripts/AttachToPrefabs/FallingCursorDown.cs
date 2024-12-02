using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FallingCursorDown : MonoBehaviour
{
    public Transform transformCursor;
    public Rigidbody2D rigidbody2d;
    public GameObject overLappingObject;
    public OverlappingSounds overlapping;

    public GameObject achObject;
    public Achievements achScript;

    public Transform goldCursor, emeraldCursor, diamondCursor, rainbowCursor, purpleCursor;

    int cursorClick;

    public bool hitPurple;
    public static int hitCursors, totalHitCursors;

    private void Awake()
    {
        goldCursor = transform.Find("GoldCursor");
        diamondCursor = transform.Find("DiamondCursor");
        emeraldCursor = transform.Find("EmeraldCursor");
        rainbowCursor = transform.Find("RainbowCursor");
        purpleCursor = transform.Find("PurpleCursor");

        transformCursor = GetComponent<Transform>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        overLappingObject = GameObject.Find("OverlappingSounds");
        overlapping = overLappingObject.GetComponent<OverlappingSounds>();

        achObject = GameObject.Find("AchievementsScript");
        achScript = achObject.GetComponent<Achievements>();
    }

    public float fallingCursorIncrease;
    public bool isRainbow;
    public void OnEnable()
    {
        clickOnce = false;
        cursorClick = 0;

        float random = Random.Range(22,-22);
        gameObject.transform.rotation = Quaternion.Euler(0, 0, random);

        float randomScale = Random.Range(9, 18);
        gameObject.transform.localScale = new Vector2(randomScale, randomScale);

        float random2 = Random.Range(0.01f, 0.1f);
        rigidbody2d.gravityScale = random2;

        fallingCursorIncrease = 0;
        fallingCursorIncrease += 1;

        float topChance = 99;
        float randomCursor = 0;
        bool hitCursor = false;
        bool hitDiamond = false;
        bool hitEmerald = false;
        isRainbow = false;

        SetCursorsFalse();

        if (Prestige.unlockedPrestigeUpgrade[0] == true) 
        {
            randomCursor = Random.Range(0, topChance);
            if (randomCursor < Prestige.diamondChance) { diamondCursor.gameObject.SetActive(true);hitCursor = true; hitDiamond = true; Stats.totalDiamond += 1; }
            else { topChance -= Prestige.diamondChance; }
        }
        if (Prestige.unlockedPrestigeUpgrade[1] == true && hitCursor == false)
        {
            randomCursor = Random.Range(0, topChance);
            if (randomCursor < Prestige.emeraldChance) { emeraldCursor.gameObject.SetActive(true); hitCursor = true; hitEmerald = true; Stats.totalEmerald += 1; }
            else { topChance -= Prestige.emeraldChance; }
        }
        if (Prestige.unlockedPrestigeUpgrade[2] == true && hitCursor == false)
        {
            randomCursor = Random.Range(0, topChance);
            if (randomCursor < Prestige.rainbowChance) { rainbowCursor.gameObject.SetActive(true); hitCursor = true; isRainbow = true; Stats.totalRainbow += 1; }
            else { topChance -= Prestige.rainbowChance; }
        }
        if (Prestige.unlockedPrestigeUpgrade[3] == true && hitCursor == false)
        {
            randomCursor = Random.Range(0, topChance);
            if (randomCursor < Prestige.purpleChance) { purpleCursor.gameObject.SetActive(true); hitCursor = true; hitPurple = true; Stats.totalPurple += 1; }
        }

        achScript.CheckAchievementsProgress(20);
        if (hitCursor == false) { goldCursor.gameObject.SetActive(true); }

        float randomIncrease = 0f;

        randomIncrease += Random.Range(Prestige.minFallingCursorIncrease, Prestige.maxFallingCursorIncrease);
        if(hitDiamond == true) { randomIncrease += Prestige.diamondIncrease; }
        else if (hitEmerald) { randomIncrease += Prestige.emeraldIncrease; }

        fallingCursorIncrease += randomIncrease;
    }

    public void SetCursorsFalse()
    {
        hitPurple = false;
        goldCursor.gameObject.SetActive(false);
        diamondCursor.gameObject.SetActive(false);
        emeraldCursor.gameObject.SetActive(false);
        rainbowCursor.gameObject.SetActive(false);
        purpleCursor.gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            ObjectPool.instance.ReturnFallingCursorFromPool(gameObject);
            FallingCurosrs.totalFallingCursorsOnScreen -= 1;
        }
    }

    bool clickOnce;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 9 || collision.gameObject.layer == 12 || collision.gameObject.layer == 13 || collision.gameObject.layer == 14 || collision.gameObject.layer == 15 || collision.gameObject.layer == 16)
        {
            if(clickOnce == true) { return; }

            if(hitPurple == true) { cursorClick += 500; }
            else { cursorClick += 1; }

            if(cursorClick >= Prestige.fallingCursorTier)
            {
                clickOnce = true;

                hitCursors += 1;

                if(hitCursors >= totalHitCursors)
                {
                    hitCursors = 0;

                    MainCursorClick.isAlotOfCursors = true;
                  
                }

                Prestige.totalRainbowBonus -= 1;
                if(Prestige.totalRainbowBonus < 0) { Prestige.totalRainbowBonus = 0; }
                FallingCurosrs.totalFallingCursorsOnScreen -= 1;

                if(hitPurple == false)
                {
                    if (isRainbow == true)
                    {
                        Prestige.totalRainbowBonus += Prestige.rainbowTotalCursors;
                    }

                    if (Prestige.totalRainbowBonus > 0)
                    {
                        int randomRainbow = Random.Range(Prestige.minRanbowIncrease, Prestige.maxRandomIncrease);
                        fallingCursorIncrease += randomRainbow;
                    }

                    float randomText = Random.Range(0f, 1f);
                    if (randomText < SettingsAndUI.fallingChance)
                    {
                        TextMeshProUGUI pointText = ObjectPool.instance.GetFallingTextPopUpFromPool();
                        pointText.transform.position = gameObject.transform.position;
                        float randomTexTSize = Random.Range(0.25f, 0.4f);
                        pointText.transform.localScale = new Vector2(randomTexTSize, randomTexTSize);

                        pointText.color = Color.yellow;
                        double totalPoints = ((MainCursorClick.cursorClickPoint * (1 + Prestige.activeGoldIncrease)) * fallingCursorIncrease) * (1 + GoldenFistMechanics.fallingGoldBonusIncrease);

                        if (totalPoints < 100) { pointText.text = "+" + totalPoints.ToString("F1"); }
                        else { pointText.text = "+" + ScaleNumbers.FormatPoints(totalPoints); }

                        float preferredWidth = pointText.GetPreferredValues().x;
                        pointText.rectTransform.sizeDelta = new Vector2(preferredWidth, pointText.rectTransform.sizeDelta.y);

                        MainCursorClick.totalClickPoints += totalPoints;
                        LevelUp.currentPrestigeCoins += totalPoints;
                        Stats.totalGold += totalPoints;
                        Stats.totalGoldFalling += totalPoints;
                    }
                    else
                    {
                        double totalPoints = ((MainCursorClick.cursorClickPoint * (1 + Prestige.activeGoldIncrease)) * fallingCursorIncrease) * (1 + GoldenFistMechanics.fallingGoldBonusIncrease);
                        MainCursorClick.totalClickPoints += totalPoints;
                        LevelUp.currentPrestigeCoins += totalPoints;
                        Stats.totalGold += totalPoints;
                        Stats.totalGoldFalling += totalPoints;
                    }
                }
                else
                {
                    TextMeshProUGUI prestigePoint = ObjectPool.instance.GetFallingTextPopUpFromPool();
                    prestigePoint.transform.position = gameObject.transform.position;
                    float randomTexTSize = Random.Range(0.51f, 0.55f);
                    prestigePoint.transform.localScale = new Vector2(randomTexTSize, randomTexTSize);

                    prestigePoint.color = Color.magenta;
                    prestigePoint.text = "+" + (1 * (1 + Prestige.clickscensionCoinIncrease)).ToString("F0");

                    int integerPart = Mathf.FloorToInt(1 * (1 + Prestige.clickscensionCoinIncrease));
                    float decimalPart = (1 * (1 + Prestige.clickscensionCoinIncrease)) - integerPart;

                    if (decimalPart >= 0.5f)
                    {
                        // Add the decimal part to make it an even number
                        Prestige.clickscensionCoinsGet += integerPart + 1;
                    }
                    else
                    {
                        // Ignore the decimal part and just add the integer part
                        Prestige.clickscensionCoinsGet += integerPart;
                    }

                    achScript.CheckAchievementsProgress(50);

                    float preferredWidth = prestigePoint.GetPreferredValues().x;
                    prestigePoint.rectTransform.sizeDelta = new Vector2(preferredWidth, prestigePoint.rectTransform.sizeDelta.y);
                }

                cursorClick = 0;

                if(SettingsAndUI.particleOn == 0)
                {
                    if(hitPurple == true) { GetParticle(2); }
                    else { GetParticle(1); }
               
                }

                ObjectPool.instance.ReturnFallingCursorFromPool(gameObject);
            }
        }

        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 12 || collision.gameObject.layer == 13 || collision.gameObject.layer == 14 || collision.gameObject.layer == 15 || collision.gameObject.layer == 16)
        {
            Stats.totalFallingCursorClciks += 1;
            achScript.CheckAchievementsProgress(20);
        }
        else if (collision.gameObject.layer == 9)
        {
            Stats.totalFallingHitByProjectile += 1;
            achScript.CheckAchievementsProgress(20);
        }
    }

    public static Vector2 fallingPos;

    public void GetParticle(int cursorType)
    {
        GameObject particle = ObjectPool.instance.GetParticleFromPool();
        particle.transform.localPosition = gameObject.transform.localPosition;
        if (cursorType == 1) { particle.transform.localScale = new Vector2(0.3f, 0.3f); }
        else if (cursorType == 2) { particle.transform.localScale = new Vector2(0.35f, 0.35f); }
    }
}
