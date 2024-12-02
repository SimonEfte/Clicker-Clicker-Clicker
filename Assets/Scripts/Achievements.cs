using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Achievements : MonoBehaviour, IDataPersistence
{
    public AudioManager audioManager;
    public GameObject[] claimButton;
    public GameObject[] achFrame;
    public GameObject[] achCheckmark;
    public static bool[] achSaves = new bool[55];
    public bool[] achCompleted = new bool[55];
    public static int totalAchCompleted;
    public TextMeshProUGUI achCompletedTexT;
    public bool hideCompleted;
    public int totalAch;
    public SkinScript skinsScript;
    public Animation exclAnim;

    public RectTransform scrollContent;
    public float originalHeightSize, minusHeightSize;

    private void Update()
    {
        if(SettingsAndUI.isInAchievements == true)
        {
            exclAnim.gameObject.SetActive(false);
        }
    }

    private void Awake()
    {
        originalHeightSize = 7866.36f;
        minusHeightSize = 143.2f;
        totalAch = 55;
    }

    private void Start()
    {
        CheckAchievementsProgress(1);
        CheckAchievementsProgress(10);
        CheckAchievementsProgress(20);
        CheckAchievementsProgress(30);
        CheckAchievementsProgress(40);
        CheckAchievementsProgress(50);
        CheckAchievementsProgress(55);

        if (Upgrades.projectilesPurchased > 5 && achSaves[27] == true) { extraBouncyBall.SetActive(true); }
        if (Upgrades.projectilesPurchased > 8 && achSaves[31] == true) { extraBigBouncyBall.SetActive(true); }

        for (int i = 0; i < achSaves.Length; i++)
        {
            if(achSaves[i] == true)
            {
                achCheckmark[i].SetActive(true);
                claimButton[i].SetActive(false);
                achFrame[i].SetActive(false);
            }
        }

        HideCompleted(false);
        achCompletedTexT.text = totalAchCompleted + "/" + totalAch;

        if(achSaves[0] == true) { TriggerSteamAchTest("ach_gold1"); }
        if (achSaves[1] == true) { TriggerSteamAchTest("ach_gold2"); }
        if (achSaves[2] == true) { TriggerSteamAchTest("ach_gold3"); }
        if (achSaves[3] == true) { TriggerSteamAchTest("ach_gold4"); }
        if (achSaves[4] == true) { TriggerSteamAchTest("ach_gold5"); }
        if (achSaves[5] == true) { TriggerSteamAchTest("ach_gold6"); }
        if (achSaves[6] == true) { TriggerSteamAchTest("ach_click1"); }
        if (achSaves[7] == true) { TriggerSteamAchTest("ach_Click2"); }
        if (achSaves[8] == true) { TriggerSteamAchTest("ach_Click3"); }
        if (achSaves[9] == true) { TriggerSteamAchTest("ach_mainCursorClick1"); }
        if (achSaves[10] == true) { TriggerSteamAchTest("ach_mainCursorClick2"); }
        if (achSaves[11] == true) { TriggerSteamAchTest("ach_autoClicks1"); }
        if (achSaves[12] == true) { TriggerSteamAchTest("ach_autoClicks2"); }
        if (achSaves[13] == true) { TriggerSteamAchTest("ach_fallingCursors1"); }
        if (achSaves[14] == true) { TriggerSteamAchTest("ach_fallingCursors2"); }
        if (achSaves[15] == true) { TriggerSteamAchTest("ach_fallingCursors3"); }
        if (achSaves[16] == true) { TriggerSteamAchTest("ach_goldenFist1"); }
        if (achSaves[17] == true) { TriggerSteamAchTest("ach_goldenFist2"); }
        if (achSaves[18] == true) { TriggerSteamAchTest("ach_boonanza"); }
        if (achSaves[19] == true) { TriggerSteamAchTest("ach_diamondCursor"); }
        if (achSaves[20] == true) { TriggerSteamAchTest("ach_emeraldCursor"); }
        if (achSaves[21] == true) { TriggerSteamAchTest("ach_rainbowCursor"); }
        if (achSaves[22] == true) { TriggerSteamAchTest("ach_purpleCursor"); }
        if (achSaves[23] == true) { TriggerSteamAchTest("ach_knife"); }
        if (achSaves[24] == true) { TriggerSteamAchTest("ach_boulder"); }
        if (achSaves[25] == true) { TriggerSteamAchTest("ach_spikes"); }
        if (achSaves[26] == true) { TriggerSteamAchTest("ach_shurikens"); }
        if (achSaves[27] == true) { TriggerSteamAchTest("ach_bounceBall"); }
        if (achSaves[28] == true) { TriggerSteamAchTest("ach_boomerangs"); }
        if (achSaves[29] == true) { TriggerSteamAchTest("ach_spears"); }
        if (achSaves[30] == true) { TriggerSteamAchTest("ach_lasers"); }
        if (achSaves[31] == true) { TriggerSteamAchTest("ach_bigBall"); }
        if (achSaves[32] == true) { TriggerSteamAchTest("ach_arrows"); }
        if (achSaves[33] == true) { TriggerSteamAchTest("ach_spikeBall"); }
        if (achSaves[34] == true) { TriggerSteamAchTest("ach_bullets"); }
        if (achSaves[35] == true) { TriggerSteamAchTest("ach_projectiles1"); }
        if (achSaves[36] == true) { TriggerSteamAchTest("ach_projectiles2"); }
        if (achSaves[37] == true) { TriggerSteamAchTest("ach_projectiles3"); }
        if (achSaves[38] == true) { TriggerSteamAchTest("ach_crit1"); }
        if (achSaves[39] == true) { TriggerSteamAchTest("ach_crit2"); }
        if (achSaves[40] == true) { TriggerSteamAchTest("ach_AOE1"); }
        if (achSaves[41] == true) { TriggerSteamAchTest("ach_AEO2"); }
        if (achSaves[42] == true) { TriggerSteamAchTest("ach_clickscensionCoins1"); }
        if (achSaves[43] == true) { TriggerSteamAchTest("ach_clickscensionCoins2"); }
        if (achSaves[44] == true) { TriggerSteamAchTest("ach_clickscensionCoins3"); }
        if (achSaves[45] == true) { TriggerSteamAchTest("ach_clickscensionLevel1"); }
        if (achSaves[46] == true) { TriggerSteamAchTest("ach_clickscensionLevel2"); }
        if (achSaves[47] == true) { TriggerSteamAchTest("ach_clickscensionUpgrades1"); }
        if (achSaves[48] == true) { TriggerSteamAchTest("ach_clickscensionUpgrades2"); }
        if (achSaves[49] == true) { TriggerSteamAchTest("ach_clickscensionUpgrade3"); }
        if (achSaves[50] == true) { TriggerSteamAchTest("ach_allClickscension"); }
        if (achSaves[51] == true) { TriggerSteamAchTest("ach_purchaseSpike"); }
        if (achSaves[52] == true) { TriggerSteamAchTest("ach_purchaseLaser"); }
        if (achSaves[53] == true) { TriggerSteamAchTest("ach_purchaseSpikeBall"); }
        if (achSaves[54] == true) { TriggerSteamAchTest("ach_purchaseTurret"); }
    }

    public GameObject extraBouncyBall, extraBigBouncyBall;

    #region Cliam ach button
    public void ClaimAch(int achNumber)
    {
        audioManager.Play("Claim");
        achFrame[achNumber].transform.SetSiblingIndex(achNumber);

        //Gold
        if (achNumber == 0) { Prestige.activeGoldIncrease += 0.02f; Prestige.passiveGoldIncrease += 0.02f; TriggerACH("ach_gold1", achNumber); SetText(); }
        else if (achNumber == 1) { Prestige.activeGoldIncrease += 0.2f; Prestige.passiveGoldIncrease += 0.2f; TriggerACH("ach_gold2", achNumber); SetText(); }
        else if (achNumber == 2) { Prestige.activeGoldIncrease += 4f; Prestige.passiveGoldIncrease += 4f; TriggerACH("ach_gold3", achNumber); SetText(); }
        else if (achNumber == 3) { Prestige.activeGoldIncrease += 10f; Prestige.passiveGoldIncrease += 10f; TriggerACH("ach_gold4", achNumber); SetText(); }
        else if (achNumber == 4) { Prestige.activeGoldIncrease += 75f; Prestige.passiveGoldIncrease += 75f; TriggerACH("ach_gold5", achNumber); SetText(); }
        else if (achNumber == 5) { Prestige.activeGoldIncrease += 250f; Prestige.passiveGoldIncrease += 250f; TriggerACH("ach_gold6", achNumber); SetText(); }

        //Click
        else if (achNumber == 6) { skinsScript.UnlockSkin(0); TriggerACH("ach_click1", achNumber); }
        else if (achNumber == 7) { skinsScript.UnlockSkin(5); TriggerACH("ach_Click2", achNumber); }
        else if (achNumber == 8) { skinsScript.UnlockSkin(6); TriggerACH("ach_Click3", achNumber); }
        
        //Main cursor click     
        else if (achNumber == 9) { Prestige.activeGoldIncrease += 0.1f; TriggerACH("ach_mainCursorClick1", achNumber); SetText(); }
        else if (achNumber == 10) { Prestige.activeGoldIncrease += 10f; TriggerACH("ach_mainCursorClick2", achNumber); SetText(); }

        //Auto clicks
        else if (achNumber == 11) { Upgrades.clickerPriceIncrease -= 0.04f; TriggerACH("ach_autoClicks1", achNumber); }
        else if (achNumber == 12) { Upgrades.clickerPriceIncrease -= 0.03f; TriggerACH("ach_autoClicks2", achNumber); }

        //Falling cursors 
        else if (achNumber == 13) { Prestige.clickscensionCoinsGet += 3; TriggerACH("ach_fallingCursors1", achNumber); }
        else if (achNumber == 14) { Prestige.clickscensionCoinsGet += 75; TriggerACH("ach_fallingCursors2", achNumber); }
        else if (achNumber == 15) { Prestige.clickscensionCoinsGet += 20000; TriggerACH("ach_fallingCursors3", achNumber); }
        //473
        //837 - 1 Hour
        //Fist
        else if (achNumber == 16) { TriggerACH("ach_goldenFist1", achNumber); GoldenFistMechanics.goldBonusCountdownTime += 2; GoldenFistMechanics.bonanzaCountdownTime += 2; }
        else if (achNumber == 17) { TriggerACH("ach_goldenFist2", achNumber); GoldenFistMechanics.goldBonusCountdownTime += 4; GoldenFistMechanics.bonanzaCountdownTime += 4; }
        else if (achNumber == 18) {  TriggerACH("ach_boonanza", achNumber); }

        //Falling prestige cursors
        else if (achNumber == 19) { TriggerACH("ach_diamondCursor", achNumber); skinsScript.UnlockSkin(1); }
        else if (achNumber == 20) { TriggerACH("ach_emeraldCursor", achNumber); skinsScript.UnlockSkin(2); }
        else if (achNumber == 21) { TriggerACH("ach_rainbowCursor", achNumber); skinsScript.UnlockSkin(3); }
        else if (achNumber == 22) { TriggerACH("ach_purpleCursor", achNumber); skinsScript.UnlockSkin(4); }

        //Projectile spawns
        else if (achNumber == 23) { TriggerACH("ach_knife", achNumber); } //Knife bonus
        else if (achNumber == 24) { TriggerACH("ach_boulder", achNumber); } //Boulder bonus
        else if (achNumber == 25) { TriggerACH("ach_spikes", achNumber); } //Spike bonus
        else if (achNumber == 26) { TriggerACH("ach_shurikens", achNumber); } //Shuriken bonus
        else if (achNumber == 27) { TriggerACH("ach_bounceBall", achNumber); 
            if(Upgrades.projectilesPurchased > 5) { extraBouncyBall.SetActive(true); }
        } //Ball bonus
        else if (achNumber == 28) { TriggerACH("ach_boomerangs", achNumber); } //Boomerang bonus
        else if (achNumber == 29) { TriggerACH("ach_spears", achNumber); } //Spear bonus
        else if (achNumber == 30) { TriggerACH("ach_lasers", achNumber); } //Laser bonus
        else if (achNumber == 31) { TriggerACH("ach_bigBall", achNumber);
            if (Upgrades.projectilesPurchased > 8) { extraBigBouncyBall.SetActive(true); }
        } //Big ball bonus
        else if (achNumber == 32) { TriggerACH("ach_arrows", achNumber); } //Arrow bonus
        else if (achNumber == 33) { TriggerACH("ach_spikeBall", achNumber); } //Spike balls bonus
        else if (achNumber == 34) { TriggerACH("ach_bullets", achNumber); } //Bullets bonus

        //Total projectile spawn
        else if (achNumber == 35) { TriggerACH("ach_projectiles1", achNumber); Upgrades.projectilePriceIncrease -= 0.02f; }
        else if (achNumber == 36) { TriggerACH("ach_projectiles2", achNumber); Upgrades.projectilePriceIncrease -= 0.02f; }
        else if (achNumber == 37) { TriggerACH("ach_projectiles3", achNumber); Upgrades.projectilePriceIncrease -= 0.02f; }

        //Crits
        else if (achNumber == 38) { TriggerACH("ach_crit1", achNumber); Prestige.activeGoldIncrease += 0.14f; SetText(); }
        else if (achNumber == 39) { TriggerACH("ach_crit2", achNumber); Prestige.activeGoldIncrease += 1.15f; SetText(); }

        //AOE
        else if (achNumber == 40) { TriggerACH("ach_AOE1", achNumber); Prestige.activeGoldIncrease += 0.3f; SetText(); }
        else if (achNumber == 41) { TriggerACH("ach_AEO2", achNumber); Prestige.activeGoldIncrease += 3f; SetText(); }

        //Clickscenson coins get
        else if (achNumber == 42) { TriggerACH("ach_clickscensionCoins1", achNumber); Prestige.clickscensionCoinIncrease += 0.04f; }
        else if (achNumber == 43) { TriggerACH("ach_clickscensionCoins2", achNumber); Prestige.clickscensionCoinIncrease += 0.2f; }
        else if (achNumber == 44) { TriggerACH("ach_clickscensionCoins3", achNumber); Prestige.clickscensionCoinIncrease += 1f; }

        //Level up 1 clickscension ugpgrade
        else if (achNumber == 45) { TriggerACH("ach_clickscensionLevel1", achNumber); Prestige.clickscensionCoinsGet += 30f; }
        else if (achNumber == 46) { TriggerACH("ach_clickscensionLevel2", achNumber); Prestige.clickscensionCoinsGet += 1500f; }

        //Level up clickscension upgrades
        else if (achNumber == 47) { TriggerACH("ach_clickscensionUpgrades1", achNumber); Prestige.clickscensionCoinsGet += 25f; }
        else if (achNumber == 48) { TriggerACH("ach_clickscensionUpgrades2", achNumber); Prestige.clickscensionCoinsGet += 500f; }
        else if (achNumber == 49) { TriggerACH("ach_clickscensionUpgrade3", achNumber); Prestige.clickscensionCoinsGet += 10000f; }

        //All prestige upgrades
        else if (achNumber == 50) { TriggerACH("ach_allClickscension", achNumber); Prestige.clickscensionCoinsGet += 65; }

        else if (achNumber == 51) { TriggerACH("ach_purchaseSpike", achNumber); skinsScript.UnlockSkin(7); }
        else if (achNumber == 52) { TriggerACH("ach_purchaseLaser", achNumber); skinsScript.UnlockSkin(8); }
        else if (achNumber == 53) { TriggerACH("ach_purchaseSpikeBall", achNumber); skinsScript.UnlockSkin(9); }
        else if (achNumber == 54) { TriggerACH("ach_purchaseTurret", achNumber); skinsScript.UnlockSkin(10); }
    }
    #endregion

    #region Check ACH progress
    public void CheckAchievementsProgress(int achCheck)
    {
        for (int i = 0; i < achCompleted.Length; i++)
        {
            if (achCompleted[i] == true && achSaves[i] == false)
            {
                achFrame[i].transform.SetSiblingIndex(0);
            }
        }

        if(achCheck < 7)
        {
            //Gold ach
            if(!achCompleted[0] && !achSaves[0]) { if (Stats.totalGold >= 1000000) { StartExclAnim(); achCompleted[0] = true; claimButton[0].SetActive(true);  } }
            if (!achCompleted[1] && !achSaves[1]) { if (Stats.totalGold >= 1000000000) { StartExclAnim(); achCompleted[1] = true; claimButton[1].SetActive(true); } }
            if (!achCompleted[2] && !achSaves[2]) { if (Stats.totalGold >= 1000000000000) { StartExclAnim(); achCompleted[2] = true; claimButton[2].SetActive(true); } }
            if (!achCompleted[3] && !achSaves[3]) { if (Stats.totalGold >= 1000000000000000) { StartExclAnim(); achCompleted[3] = true; claimButton[3].SetActive(true); } } //Sept
            if (!achCompleted[4] && !achSaves[4]) { if (Stats.totalGold >= 1000000000000000000) { StartExclAnim(); achCompleted[4] = true; claimButton[4].SetActive(true); } }
            if (!achCompleted[5] && !achSaves[5]) { if (Stats.totalGold >= 1000000000000000000000f) { StartExclAnim(); achCompleted[5] = true; claimButton[5].SetActive(true); } }
        }
        else if (achCheck < 14)
        {
            //Click ach (SKINS)
            if (!achCompleted[6] && !achSaves[6]) { if (Stats.totalClicks >= 500) { StartExclAnim(); achCompleted[6] = true; claimButton[6].SetActive(true); } }
            if (!achCompleted[7] && !achSaves[7]) { if (Stats.totalClicks >= 50000) { StartExclAnim(); achCompleted[7] = true; claimButton[7].SetActive(true); } }
            if (!achCompleted[8] && !achSaves[8]) { if (Stats.totalClicks >= 500000) { StartExclAnim(); achCompleted[8] = true; claimButton[8].SetActive(true); } }

            //Main cursor click
            if (!achCompleted[9] && !achSaves[9]) { if (Stats.totalCursorClicks >= 10000) { StartExclAnim(); achCompleted[9] = true; claimButton[9].SetActive(true); } }
            if (!achCompleted[10] && !achSaves[10]) { if (Stats.totalCursorClicks >= 750000) { StartExclAnim(); achCompleted[10] = true; claimButton[10].SetActive(true); } }

            //Auto clicks
            if (!achCompleted[11] && !achSaves[11]) { if (Stats.totalAutoClicks >= 1000) { StartExclAnim(); achCompleted[11] = true; claimButton[11].SetActive(true); } }
            if (!achCompleted[12] && !achSaves[12]) { if (Stats.totalAutoClicks >= 25000) { StartExclAnim(); achCompleted[12] = true; claimButton[12].SetActive(true); } }
        }
        else if (achCheck < 24)
        {
            //Falling cursors break
            if (!achCompleted[13] && !achSaves[13]) { if ((Stats.totalFallingCursorClciks + Stats.totalFallingHitByProjectile) >= 1000) { StartExclAnim(); achCompleted[13] = true; claimButton[13].SetActive(true); } }
            if (!achCompleted[14] && !achSaves[14]) { if ((Stats.totalFallingCursorClciks + Stats.totalFallingHitByProjectile) >= 75000) { StartExclAnim(); achCompleted[14] = true; claimButton[14].SetActive(true); } }
            if (!achCompleted[15] && !achSaves[15]) { if ((Stats.totalFallingCursorClciks + Stats.totalFallingHitByProjectile) >= 1500000) { StartExclAnim(); achCompleted[15] = true; claimButton[15].SetActive(true); } }

            //Fist
            if (!achCompleted[16] && !achSaves[16]) { if (Stats.totalGoldenFistClicks >= 5) { StartExclAnim(); achCompleted[16] = true; claimButton[16].SetActive(true); } }
            if (!achCompleted[17] && !achSaves[17]) { if (Stats.totalGoldenFistClicks >= 30) { StartExclAnim(); achCompleted[17] = true; claimButton[17].SetActive(true); } }
            if (!achCompleted[18] && !achSaves[18]) { if (Stats.totalBonanzas >= 5) { StartExclAnim(); achCompleted[18] = true; claimButton[18].SetActive(true); } }

            //Falling clickscension cursors (SKINS)
            if (!achCompleted[19] && !achSaves[19]) { if (Stats.totalDiamond >= 400) { StartExclAnim(); achCompleted[19] = true; claimButton[19].SetActive(true); } }
            if (!achCompleted[20] && !achSaves[20]) { if (Stats.totalEmerald >= 300) { StartExclAnim(); achCompleted[20] = true; claimButton[20].SetActive(true); } }
            if (!achCompleted[21] && !achSaves[21]) { if (Stats.totalRainbow >= 123) { StartExclAnim(); achCompleted[21] = true; claimButton[21].SetActive(true); } }
            if (!achCompleted[22] && !achSaves[22]) { if (Stats.totalPurple >= 69) { StartExclAnim(); achCompleted[22] = true; claimButton[22].SetActive(true); } }
        }
        else if (achCheck < 39)
        {
            //Spawn knifes
            if (!achCompleted[23] && !achSaves[23]) { if (Stats.totalKnifes >= 5000) { StartExclAnim(); achCompleted[23] = true; claimButton[23].SetActive(true); } }
            if (!achCompleted[24] && !achSaves[24]) { if (Stats.totalBoulders >= 1200) { StartExclAnim(); achCompleted[24] = true; claimButton[24].SetActive(true); } }
            if (!achCompleted[25] && !achSaves[25]) { if (Stats.totalSpikes >= 2500) { StartExclAnim(); achCompleted[25] = true; claimButton[25].SetActive(true); } }
            if (!achCompleted[26] && !achSaves[26]) { if (Stats.totalShurikens >= 6000) { StartExclAnim(); achCompleted[26] = true; claimButton[26].SetActive(true); } }
            if (!achCompleted[27] && !achSaves[27]) { if (Stats.ballBounced >= 10000) { StartExclAnim(); achCompleted[27] = true; claimButton[27].SetActive(true); } }
            if (!achCompleted[28] && !achSaves[28]) { if (Stats.totalBoomerangs >= 2500) { StartExclAnim(); achCompleted[28] = true; claimButton[28].SetActive(true); } }
            if (!achCompleted[29] && !achSaves[29]) { if (Stats.totalSpear >= 4000) { StartExclAnim(); achCompleted[29] = true; claimButton[29].SetActive(true); } }
            if (!achCompleted[30] && !achSaves[30]) { if (Stats.totalLasers >= 2700) { StartExclAnim(); achCompleted[30] = true; claimButton[30].SetActive(true); } }
            if (!achCompleted[31] && !achSaves[31]) { if (Stats.bigBallBounced >= 4000) { StartExclAnim(); achCompleted[31] = true; claimButton[31].SetActive(true); } }
            if (!achCompleted[32] && !achSaves[32]) { if (Stats.totalArrows >= 10000) { StartExclAnim(); achCompleted[32] = true; claimButton[32].SetActive(true); } }
            if (!achCompleted[33] && !achSaves[33]) { if (Stats.totalSpikeballs >= 2000) { StartExclAnim(); achCompleted[33] = true; claimButton[33].SetActive(true); } }
            if (!achCompleted[34] && !achSaves[34]) { if (Stats.totalBullets >= 5000) { StartExclAnim(); achCompleted[34] = true; claimButton[34].SetActive(true); } }

            //Spawn projectiles
            if (!achCompleted[35] && !achSaves[35]) { if (Stats.totalProjectiles >= 5000) { StartExclAnim(); achCompleted[35] = true; claimButton[35].SetActive(true); } }
            if (!achCompleted[36] && !achSaves[36]) { if (Stats.totalProjectiles >= 50000) { StartExclAnim(); achCompleted[36] = true; claimButton[36].SetActive(true); } }
            if (!achCompleted[37] && !achSaves[37]) { if (Stats.totalProjectiles >= 200000) { StartExclAnim(); achCompleted[37] = true; claimButton[37].SetActive(true); } }
        }
        else if (achCheck < 43)
        {
            //Cit
            if (!achCompleted[38] && !achSaves[38]) { if (Stats.totalCritClciks >= 1000) { StartExclAnim(); achCompleted[38] = true; claimButton[38].SetActive(true); } }
            if (!achCompleted[39] && !achSaves[39]) { if (Stats.totalCritClciks >= 50000) { StartExclAnim(); achCompleted[39] = true; claimButton[39].SetActive(true); } }

            //AOE
            if (!achCompleted[40] && !achSaves[40]) { if (Upgrades.cursorAOE >= 0.5f) { StartExclAnim(); achCompleted[40] = true; claimButton[40].SetActive(true); } }
            if (!achCompleted[41] && !achSaves[41]) { if (Upgrades.cursorAOE >= 1f) { StartExclAnim(); achCompleted[41] = true; claimButton[41].SetActive(true); } }
        }
        else if (achCheck < 52)
        {
            //Clickscension coins // stopped here
            if (!achCompleted[42] && !achSaves[42]) { if (Stats.totalClickscensionCoins >= 25) { StartExclAnim(); achCompleted[42] = true; claimButton[42].SetActive(true); } }
            if (!achCompleted[43] && !achSaves[43]) { if (Stats.totalClickscensionCoins >= 500) { StartExclAnim(); achCompleted[43] = true; claimButton[43].SetActive(true); } }
            if (!achCompleted[44] && !achSaves[44]) { if (Stats.totalClickscensionCoins >= 5000) { StartExclAnim(); achCompleted[44] = true; claimButton[44].SetActive(true); } }

            //Level up 1 clickscension upgrade
            if (!achCompleted[45] && !achSaves[45]) { if (Prestige.highestLevelUpgrade >= 20) { StartExclAnim(); achCompleted[45] = true; claimButton[45].SetActive(true); } }
            if (!achCompleted[46] && !achSaves[46]) { if (Prestige.highestLevelUpgrade >= 70) { StartExclAnim(); achCompleted[46] = true; claimButton[46].SetActive(true); } }

            //Level up total clickscension upgrades
            if (!achCompleted[47] && !achSaves[47]) { if (Stats.clickscensionUpgraded >= 50) { StartExclAnim(); achCompleted[47] = true; claimButton[47].SetActive(true); } }
            if (!achCompleted[48] && !achSaves[48]) { if (Stats.clickscensionUpgraded >= 200) { StartExclAnim(); achCompleted[48] = true; claimButton[48].SetActive(true); } }
            if (!achCompleted[49] && !achSaves[49]) { if (Stats.clickscensionUpgraded >= 1000) { StartExclAnim(); achCompleted[49] = true; claimButton[49].SetActive(true); } }

            if (!achCompleted[50] && !achSaves[50]) { if (Stats.clickscensionUnlocked >= 12) { StartExclAnim(); achCompleted[50] = true; claimButton[50].SetActive(true); } }
        }
        else if (achCheck < 56)
        {
            //Purchased projectile (SKINS)
            if (!achCompleted[51] && !achSaves[51]) { if (Upgrades.projectilesPurchased > 2) { StartExclAnim(); achCompleted[51] = true; claimButton[51].SetActive(true); } }
            if (!achCompleted[52] && !achSaves[52]) { if (Upgrades.projectilesPurchased > 7) { StartExclAnim(); achCompleted[52] = true; claimButton[52].SetActive(true); } }
            if (!achCompleted[53] && !achSaves[53]) { if (Upgrades.projectilesPurchased > 10) { StartExclAnim(); achCompleted[53] = true; claimButton[53].SetActive(true); } }
            if (!achCompleted[54] && !achSaves[54]) { if (Upgrades.projectilesPurchased > 11) { StartExclAnim(); achCompleted[54] = true; claimButton[54].SetActive(true); } }
        }
    }
    #endregion

    public void StartExclAnim()
    {
        exclAnim.gameObject.SetActive(true);
        exclAnim.Play();
    }

    public void TriggerSteamAchTest(string achNAME)
    {
        var ach = new Steamworks.Data.Achievement(achNAME);
        if (ach.State == false)
        {
            ach.Trigger();
        }
    }

    #region Trigger ach
    public void TriggerACH(string achNAME, int achNumber)
    {
        if (SteamIntgr.noSteamInt == true) { return; }

        if (DemoScript.isDemo == false)
        {
            var ach = new Steamworks.Data.Achievement(achNAME);
            if (ach.State == false)
            {
            ach.Trigger();
            }

            if (hideCompleted == true)
            {
                achFrame[achNumber].SetActive(false);
            }

            claimButton[achNumber].SetActive(false);
            achCheckmark[achNumber].SetActive(true);
            achSaves[achNumber] = true;
            
            totalAchCompleted += 1;
            achCompletedTexT.text = totalAchCompleted + "/" + totalAch;

            if(hideCompleted == true)
            {
                scrollContent.sizeDelta = new Vector2(969.8f, originalHeightSize - (minusHeightSize * totalAchCompleted));
            }
        }
    }
    #endregion

    #region Hide completed
    public GameObject checkMarkHide;

    public void HideCompleted(bool isButton)
    {
        if (isButton == true)
        {
            audioManager.Play("Claim");

            if (hideCompleted == true) { hideCompleted = false; }
            else
            {
                hideCompleted = true;
            }
        }

        if(hideCompleted == true)
        {
            checkMarkHide.SetActive(true);
            for (int i = 0; i < totalAch; i++)
            {
                if (achSaves[i] == true)
                {
                    achFrame[i].SetActive(false);
                }
            }

            scrollContent.sizeDelta = new Vector2(969.8f, originalHeightSize - (minusHeightSize * totalAchCompleted));
        }
        else
        {
            checkMarkHide.SetActive(false);
            for (int i = 0; i < totalAch; i++)
            {
                achFrame[i].SetActive(true);
            }
            scrollContent.sizeDelta = new Vector2(969.8f, originalHeightSize);
        }
    }
    #endregion

    public Prestige prestigeScript;
    public void SetText()
    {
        prestigeScript.DisplayUpgradeText(Prestige.currentUpgradeSelected);
    }

    public void ResetAch()
    {
        for (int i = 0; i < achSaves.Length; i++)
        {
            achSaves[i] = false;
            achCompleted[i] = false;
        }

        totalAchCompleted = 0;
        hideCompleted = true;

        for (int i = 0; i < claimButton.Length; i++)
        {
            claimButton[i].SetActive(false);
        }

        for (int i = 0; i < achFrame.Length; i++)
        {
            achFrame[i].SetActive(true);
        }

        for (int i = 0; i < achCheckmark.Length; i++)
        {
            achCheckmark[i].SetActive(false);
        }

        achCompletedTexT.text = "0/" + totalAch;

        scrollContent.sizeDelta = new Vector2(969.8f, originalHeightSize);
        exclAnim.gameObject.SetActive(false);
    }

    #region Load Data
    public void LoadData(GameData data)
    {
        for (int i = 0; i < achSaves.Length; i++)
        {
            achSaves[i] = data.achSaves[i];
        }

        totalAchCompleted = data.totalAchCompleted;
        hideCompleted = data.hideCompleted;
    }
    #endregion

    #region Save Data
    public void SaveData(ref GameData data)
    {
        for (int i = 0; i < achSaves.Length; i++)
        {
            data.achSaves[i] = achSaves[i];
        }

        data.totalAchCompleted = totalAchCompleted;
        data.hideCompleted = hideCompleted;
    }
    #endregion


    #region Clear ACH
    public void AchClear(string achNAME)
    {
        //var ach = new Steamworks.Data.Achievement(achNAME);
        //ach.Clear();
    }

    public void ClearACH()
    {
        AchClear("ach_gold1");
        AchClear("ach_gold2");
        AchClear("ach_gold3");
        AchClear("ach_gold4");
        AchClear("ach_gold5");
        AchClear("ach_gold6");
        AchClear("ach_click1");
        AchClear("ach_Click2");
        AchClear("ach_Click3");
        AchClear("ach_mainCursorClick1");
        AchClear("ach_mainCursorClick2");
        AchClear("ach_autoClicks1");
        AchClear("ach_autoClicks2");
        AchClear("ach_fallingCursors1");
        AchClear("ach_fallingCursors2");
        AchClear("ach_fallingCursors3");
        AchClear("ach_goldenFist1");
        AchClear("ach_goldenFist2");
        AchClear("ach_boonanza");
        AchClear("ach_diamondCursor");
        AchClear("ach_emeraldCursor");
        AchClear("ach_rainbowCursor");
        AchClear("ach_purpleCursor");
        AchClear("ach_knife");
        AchClear("ach_boulder");
        AchClear("ach_spikes");
        AchClear("ach_shurikens");
        AchClear("ach_bounceBall");
        AchClear("ach_boomerangs");
        AchClear("ach_spears");
        AchClear("ach_lasers");
        AchClear("ach_bigBall");
        AchClear("ach_arrows");
        AchClear("ach_spikeBall");
        AchClear("ach_bullets");
        AchClear("ach_projectiles1");
        AchClear("ach_projectiles2");
        AchClear("ach_projectiles3");
        AchClear("ach_crit1");
        AchClear("ach_crit2");
        AchClear("ach_AOE1");
        AchClear("ach_AEO2");
        AchClear("ach_clickscensionCoins1");
        AchClear("ach_clickscensionCoins2");
        AchClear("ach_clickscensionCoins3");
        AchClear("ach_clickscensionLevel1");
        AchClear("ach_clickscensionLevel2");
        AchClear("ach_clickscensionUpgrades1");
        AchClear("ach_clickscensionUpgrades2");
        AchClear("ach_clickscensionUpgrade3");
        AchClear("ach_allClickscension");
        AchClear("ach_purchaseSpike");
        AchClear("ach_purchaseLaser");
        AchClear("ach_purchaseSpikeBall");
        AchClear("ach_purchaseTurret");
    }
    #endregion
}
