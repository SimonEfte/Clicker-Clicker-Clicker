using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using TMPro;
public class LocalizationStrings : MonoBehaviour
{
    public static string million, billion, trillion, quadrillion, quintillion, sextillion, septillion, octillion, nonillion, decillion, undecillion, duodecillion, tredecillion, quattuordecillion, quindecillion, sexdecillion, septendecillion, octodecillion, novemdecillion, vigintillion, unvigintillion, duovigintillion, trevigintillion, quattuorvigintillion, quinvigintillion, sexvigintillion, septvigintillion, octovigintillion, nonvigintillion;

    public static int highNumberDisplay;

    public static int languageSelected;

    void Start()
    {
        highNumberDisplay = 1;

        #region Display high numbers
        if (highNumberDisplay == 1)
        {
            million = "M";
            billion = "B";
            trillion = "T";
            quadrillion = "Qa";
            quintillion = "Qi";
            sextillion = "Sx";
            septillion = "Sp";
            octillion = "O";
            nonillion = "N";
            decillion = "Dc";
            undecillion = "Ud";
            duodecillion = "Dd";
            tredecillion = "Td";
            quattuordecillion = "Qat";
            quindecillion = "Qid";
            sexdecillion = "Sxd";
            septendecillion = "Spd";
            octodecillion = "Od";
            novemdecillion = "Nd";
            vigintillion = "Vg";
            unvigintillion = "UVg";
            duovigintillion = "Dv";
            trevigintillion = "Tv";
            quattuorvigintillion = "Qtv";
            quinvigintillion = "Qtg";
            sexvigintillion = "Sxv";
            septvigintillion = "Spv";
            octovigintillion = "Oct";
            nonvigintillion = "Non";
        }

        if (highNumberDisplay == 2)
        {
            million = "e6";
            billion = "e9";
            trillion = "e12";
            quadrillion = "e15";
            quintillion = "e18";
            sextillion = "e21";
            septillion = "e24";
            octillion = "e27";
            nonillion = "e30";
            decillion = "e33";
            undecillion = "e36";
            duodecillion = "e39";
            tredecillion = "e42";
            quattuordecillion = "e45";
            quindecillion = "e48";
            sexdecillion = "e51";
            septendecillion = "e54";
            octodecillion = "e57";
            novemdecillion = "e60";
            vigintillion = "e63";
            unvigintillion = "e66";
            duovigintillion = "e69";
            trevigintillion = "e72";
        }
        #endregion

        if (PlayerPrefs.HasKey("languageSelected"))
        {
            languageSelected = PlayerPrefs.GetInt("languageSelected");

            if (languageSelected == 1) { ChooseLanguage("English"); }
            if (languageSelected == 2) { ChooseLanguage("French"); }
            if (languageSelected == 3) { ChooseLanguage("Italian"); }
            if (languageSelected == 4) { ChooseLanguage("German"); }
            if (languageSelected == 5) { ChooseLanguage("Spanish"); }
            if (languageSelected == 6) { ChooseLanguage("Japanese"); }
            if (languageSelected == 7) { ChooseLanguage("Korean"); }
            if (languageSelected == 8) { ChooseLanguage("Polish"); }
            if (languageSelected == 9) { ChooseLanguage("Portuguese"); }
            if (languageSelected == 10) { ChooseLanguage("Russian"); }
            if (languageSelected == 11) { ChooseLanguage("Chinese"); }
        }

        if (!PlayerPrefs.HasKey("languageSelected"))
        {
            // Determine the user's culture
            CultureInfo userCulture = CultureInfo.CurrentCulture;
            RegionInfo region = new RegionInfo(userCulture.Name);

            // Use the region name to set the language
            string regionName = region.Name;

            SetLanguageByRegion(regionName);
        }
    }

    #region Check Country
    void SetLanguageByRegion(string regionName)
    {
        switch (regionName)
        {
            case "US":
            case "CA":
                ChooseLanguage("English");
                break;
            case "RU":
                ChooseLanguage("Russian");
                break;
            case "JP":
                ChooseLanguage("Japanese");
                break;
            case "KR":
                ChooseLanguage("Korean");
                break;
            case "CN":
                ChooseLanguage("Chinese");
                break;
            case "DE":
                ChooseLanguage("German");
                break;
            case "FR":
                ChooseLanguage("French");
                break;
            case "ES":
                ChooseLanguage("Spanish");
                break;
            case "PT":
            case "BR":  // For Brazil
                ChooseLanguage("Portuguese");
                break;
            case "IT":
                ChooseLanguage("Italian");
                break;
            case "PL":
                ChooseLanguage("Polish");
                break;

            default:
                // Set a default language if none of the cases match
                ChooseLanguage("English");
                break;
        }
    }

    public void ChooseLanguage(string language)
    {
        if (language == "English")
        {
            English();
            languageSelected = 1;
        }
        if (language == "French")
        {
            French(); languageSelected = 2;
        }
        if (language == "Italian")
        {
            Italian(); languageSelected = 3;
        }
        if (language == "German")
        {
            German(); languageSelected = 4;
        }
        if (language == "Spanish")
        {
            Spanish(); languageSelected = 5;
        }
        if (language == "Japanese")
        {
            Japanese(); languageSelected = 6;
        }
        if (language == "Korean")
        {
            Korean(); languageSelected = 7;
        }
        if (language == "Polish")
        {
            Polish(); languageSelected = 8;
        }
        if (language == "Portuguese")
        {
            Portugese(); languageSelected = 9;
        }
        if (language == "Russian")
        {
            Russian(); languageSelected = 10;
        }
        if (language == "Chinese")
        {
            Chinese(); languageSelected = 11;
        }
    }
    #endregion

    public GameObject english, french, italian, german, spanish, japanese, korean, polish, portugese, russian, chinese;

    public void SetAllFalse()
    {
        english.SetActive(false);
        french.SetActive(false);
        italian.SetActive(false);
        german.SetActive(false);
        japanese.SetActive(false);
        korean.SetActive(false);
        polish.SetActive(false);
        portugese.SetActive(false);
        russian.SetActive(false);
        chinese.SetActive(false);
        spanish.SetActive(false);
    }

    public AudioManager audioManager;

    #region Open lagnuages frame
    public GameObject languagesFrame;
    public void OpenLanguages()
    {
        languagesFrame.SetActive(true);
        audioManager.Play("UI_Click1");
    }
    public void CloseLanguages()
    {
        languagesFrame.SetActive(false);
        audioManager.Play("UI_Click1");
    }
    #endregion

    public static string[] projectileName = new string[12];

    public static string crit, chanceC, chanceS, speed, cps, size, price, cgs, cgc, chance, increase;

    public static string projectileBonanzaFist, plussClickGoldFist, clickscensionCoinsFist, fallingCursorGoldFist, activeGoldFist, passiveGoldFist, secondsFist;

    public static string totalCritGold, totalAutoClikcs, totalCritClicks, clickscensionStats, totalClickscensionCoins, totalCLickscensionSpent, totalClickscensionUnlocked, totalClickscensionPurchased, totalDiamond, totalEmerald, totalRainbow, totalPurple, totalKnifes, totalBouldes, totalSpikes, totslShurikens, totalBoomerangs, totalSpears, totalLasers, totalArrows, totalSpikeBalls, totalBullets, totalBounce, totalBigBounce;

    public static string tier, tierExplain, diamondExplain, emeraldExplain, rainbowExplain, purpleExplain, secondsPrestige;

    public static string fullGameSteam, wishlistOnSteam;

    public static string clickLocked, clickscensionLocked, achLocked, projectileLocked;

    public static string levelText, need;

    public static string max;

    #region English
    public void English()
    {
        SetAllFalse();
        english.SetActive(true);
        languageSelected = 1;
        PlayerPrefs.SetInt("languageSelected", languageSelected);

        #region Prestige strings
        tier = "Tier ";
        diamondExplain = $"Diamond cursors give {Prestige.diamondIncrease}X the gold from golden cursors";
        emeraldExplain = $"Emerald cursors give {Prestige.emeraldIncrease}X the gold from golden cursors";
        rainbowExplain = $"Hitting a rainbow cursor increases the gold gain from the next {Prestige.rainbowTotalCursors} falling cursors by {Prestige.rainbowMinIncrease}-{Prestige.rainbowMaxIncrease}X";
        secondsPrestige = "seconds";
        #endregion

        #region projectile names and fist
        projectileName[0] = "KNIFES";
        projectileName[1] = "BOULDERS";
        projectileName[2] = "SPIKES";
        projectileName[3] = "SHURIKENS";
        projectileName[4] = "BALLS";
        projectileName[5] = "BOOMERANGS";
        projectileName[6] = "SPEARS";
        projectileName[7] = "LASERS";
        projectileName[8] = "BIG BALLS";
        projectileName[9] = "ARROWS";
        projectileName[10] = "SPIKE BALLS";
        projectileName[11] = "BULLETS";

        activeGoldFist = $"{GoldenFistMechanics.activeGoldBonusIncreaseORIGINAL}X ACTIVE CLICK GOLD";
        passiveGoldFist = $"{GoldenFistMechanics.passiveGoldBonusIncreaseORIGINAL}X PASSIVE CLICK GOLD";
        clickscensionCoinsFist = "CLICKSCENSION COINS!";
        fallingCursorGoldFist = $"{GoldenFistMechanics.fallingGoldBonusIncreaseORIGINAL + 1}X FALLING CURSOR CLICK GOLD";
        projectileBonanzaFist = "PROJECTILE BONANZA!";
        plussClickGoldFist = "CLICK GOLD";
        secondsFist = "SECONDS";
        #endregion

        #region Stats
        totalCritGold = "Total crit click gold";
        totalAutoClikcs = "Total auto clicks";
        totalCritClicks = "Total crit clicks";
        clickscensionStats = "Clickscension Stats";
        totalClickscensionCoins = "Total clickscension coins acquired";
        totalCLickscensionSpent = "Total clickscension coins spent";
        totalClickscensionUnlocked = "Total clickscension upgrades unlocked";
        totalClickscensionPurchased = "Total clickscension upgrades purchased";
        totalDiamond = "Total falling diamond cursors spawned";
        totalEmerald = "Total falling emerald cursors spawned";
        totalRainbow = "Total falling rainbow cursors spawned";
        totalPurple = "Total falling purple cursors spawned";
        totalKnifes = "Total knives spawned";
        totalBouldes = "Total boulders spawned";
        totalSpikes = "Total spikes spawned";
        totslShurikens = "Total shurikens spawned";
        totalBoomerangs = "Total boomerangs spawned";
        totalSpears = "Total spears spawned";
        totalLasers = "Total lasers spawned";
        totalArrows = "Total arrows spawned";
        totalSpikeBalls = "Total spike balls spawned";
        totalBullets = "Total bullets shot";

        totalBounce = "Total ball bounces";
        totalBigBounce = "Total big ball bounces";
        #endregion

        #region Demo locked strings
        clickLocked = "Click upgrades are locked in the demo";
        clickscensionLocked = "Clickscension is locked in the demo";
        achLocked = "Achievements are locked in the demo";
        projectileLocked = "Projectile locked in the demo";
        #endregion

        max = "MAX";

        levelText = "Level ";

        crit = "CRIT!";
        chanceC = "Chance/C";
        chanceS = "Chance/S";
        speed = "Speed";
        cps = "cps";
        size = "Size";
        price = "PRICE: ";
        cgs = "cg/s";
        cgc = "cg/c";
        chance = "chance";
        increase = "increase";

        fullGameSteam = "Check out the full game on steam!";
        wishlistOnSteam = "Wishlist on steam!";

        need = "NEED ";

        SetText();
    }
    #endregion

    #region French
    public void French()
    {
        SetAllFalse();
        french.SetActive(true);

        languageSelected = 2;
        PlayerPrefs.SetInt("languageSelected", languageSelected);

        #region Prestige strings
        tier = "Niveau ";
        diamondExplain = $"Les curseurs en diamant donnent {Prestige.diamondIncrease} fois plus d'or que les curseurs en or";
        emeraldExplain = $"Les curseurs en émeraude donnent {Prestige.emeraldIncrease} fois plus d'or que les curseurs en or";
        rainbowExplain = $"Toucher un curseur arc-en-ciel augmente les gains d'or des {Prestige.rainbowTotalCursors} curseurs suivants de {Prestige.rainbowMinIncrease} à {Prestige.rainbowMaxIncrease} fois";
        secondsPrestige = "secondes";
        #endregion

        #region projectile names and fist
        projectileName[0] = "COUTEAUX";
        projectileName[1] = "ROCHERS";
        projectileName[2] = "PICS";
        projectileName[3] = "SHURIKENS";
        projectileName[4] = "BALLES";
        projectileName[5] = "BOOMERANGS";
        projectileName[6] = "LANCES";
        projectileName[7] = "LASERS";
        projectileName[8] = "GROSSES BALLES";
        projectileName[9] = "FLÈCHES";
        projectileName[10] = "Balle À Pointes";
        projectileName[11] = "MUNITIONS";

        activeGoldFist = $"{GoldenFistMechanics.activeGoldBonusIncreaseORIGINAL}X OR ACTIF";
        passiveGoldFist = $"{GoldenFistMechanics.passiveGoldBonusIncreaseORIGINAL}X OR PASSIF";
        clickscensionCoinsFist = "PIÈCES CLICS-ASCENSION!";
        fallingCursorGoldFist = $"{GoldenFistMechanics.fallingGoldBonusIncreaseORIGINAL + 1}X CURSEURS OR PAR CLIC";
        projectileBonanzaFist = "PROJECTILE BONANZA!";
        plussClickGoldFist = "CURSEUR OR!";
        secondsFist = "SECONDES";
        #endregion

        #region Stats
        totalClickscensionCoins = "Total des pièces clics-ascension acquises";
        totalCLickscensionSpent = "Total des pièces clics-ascension dépensées";
        totalClickscensionUnlocked = "Total des améliorations clics-ascension débloquées";
        totalClickscensionPurchased = "Total des améliorations clics-ascension achetées";
        totalDiamond = "Nombre total de curseurs diamant générés";
        totalEmerald = "Nombre total de curseurs émeraude générés";
        totalRainbow = "Nombre total de curseurs arc-en-ciel générés";
        totalPurple = "Nombre total de curseurs violets générés";
        totalKnifes = "Total des couteaux généréss";
        totalBouldes = "Total des rochers générés";
        totalSpikes = "Total des pics générés";
        totslShurikens = "Total des shurikens générés";
        totalBoomerangs = "Total des boomerangs générés";
        totalSpears = "Total des lances générées";
        totalLasers = "Total des lasers générés";
        totalArrows = "Total des flèches générées";
        totalSpikeBalls = "Total des champs de pics générés";
        totalBullets = "Total des munitions générées";

        totalBounce = "Nombre total de rebonds de balle";
        totalBigBounce = "Nombre total de rebonds de grosse balle";
        #endregion

        #region Demo locked strings
        clickLocked = "Les améliorations de clic sont verrouillées dans la démo";
        clickscensionLocked = "Les clics-ascension sont verrouillés dans la démo";
        achLocked = "Les réalisations sont verrouillées dans la démo.";
        projectileLocked = "Les projectiles sont verrouillés dans la démo.";
        #endregion

        max = "MAX";

        levelText = "Niveau ";

        crit = "CRITIQUE!";
        chanceC = "Chance/C";
        chanceS = "Chance/S";
        speed = "Vitesse";
        cps = "cps";
        size = "taille";
        price = "PRIX: ";
        cgs = "cg/s";
        cgc = "cg/c";
        chance = "chance";
        increase = "d'augmentation";

        fullGameSteam = "Découvrez le jeu complet sur Steam !";
        wishlistOnSteam = "Liste de souhaits sur Steam !";

        need = "BESOIN DE ";

        SetText();
    }
    #endregion

    #region Italian
    public void Italian()
    {
        SetAllFalse();
        italian.SetActive(true);

        languageSelected = 3;
        PlayerPrefs.SetInt("languageSelected", languageSelected);

        #region Prestige strings
        tier = "Livello ";
        diamondExplain = $"I cursori diamante danno {Prestige.diamondIncrease}X l'oro dei cursori dorati";
        emeraldExplain = $"I cursori smeraldo danno {Prestige.emeraldIncrease}X l'oro dei cursori dorati";
        rainbowExplain = $"Colpire un cursore arcobaleno aumenta il guadagno in oro dai successivi {Prestige.rainbowTotalCursors} cursori di {Prestige.rainbowMinIncrease}-{Prestige.rainbowMaxIncrease}X";
        secondsPrestige = "secondi";
        #endregion

        #region projectile names and fist
        projectileName[0] = "COLTELLI";
        projectileName[1] = "MASSI";
        projectileName[2] = "ARPIONI";
        projectileName[3] = "SHURIKENS";
        projectileName[4] = "PALLE";
        projectileName[5] = "BOOMERANG";
        projectileName[6] = "LANCE";
        projectileName[7] = "LASER";
        projectileName[8] = "PALLE GRANDI";
        projectileName[9] = "FRECCE";
        projectileName[10] = "Palla chiodata";
        projectileName[11] = "PROIETTILI";

        activeGoldFist = $"{GoldenFistMechanics.activeGoldBonusIncreaseORIGINAL}X ORO ATTIVO";
        passiveGoldFist = $"{GoldenFistMechanics.passiveGoldBonusIncreaseORIGINAL}X ORO PASSIVO";
        clickscensionCoinsFist = "MONETE CLICKASCESA!";
        fallingCursorGoldFist = $"{GoldenFistMechanics.fallingGoldBonusIncreaseORIGINAL + 1}X CLICK ORO CURSORE IN CADUTA ";
        projectileBonanzaFist = "BONANZA PROIETTILI!";
        plussClickGoldFist = "CLICK ORO!";
        secondsFist = "SECONDI";
        #endregion

        #region Stats
        totalClickscensionCoins = "Totale monete clickascesa acquisite";
        totalCLickscensionSpent = "Totale monete clickascesa spese";
        totalClickscensionUnlocked = "Totale aggiornamenti clickascesa sbloccati";
        totalClickscensionPurchased = "Totale upgrade di clickascesa acquistati";
        totalDiamond = "Totale cursori diamante in caduta generati";
        totalEmerald = "Totale cursori smeraldo in caduta generati";
        totalRainbow = "Totale cursori arcobaleno in caduta generati";
        totalPurple = "Totale cursori viola in caduta generati";
        totalKnifes = "Totale coltelli generati";
        totalBouldes = "Totale massi generati";
        totalSpikes = "Totale arpioni generati";
        totslShurikens = "Totale shuriken generati";
        totalBoomerangs = "Totale boomerang generati";
        totalSpears = "Totale lance generate";
        totalLasers = "Totale laser generati";
        totalArrows = "Totale frecce generate";
        totalSpikeBalls = "Totale palle chiodate generate";
        totalBullets = "Totale proiettili sparati";

        totalBounce = "Rimbalzi totali della palla";
        totalBigBounce = "Rimbalzi totali della palla grande";
        #endregion

        #region Demo locked strings
        clickLocked = "Gli aggiornamenti dei clic sono bloccati nella demo";
        clickscensionLocked = "La clickascesa è bloccata nella demo";
        achLocked = "Gli obiettivi sono bloccati nella demo";
        projectileLocked = "Proiettile bloccato nella demo";
        #endregion

        max = "MAX";

        levelText = "Livello ";

        crit = "CRITICO!";
        chanceC = "Probabilità/C";
        chanceS = "Probabilità/S";
        speed = "velocità";
        cps = "cps";
        size = "misura";
        price = "PREZZO: ";
        cgs = "cg/s";
        cgc = "cg/c";
        chance = "di probabilità";
        increase = "Aumento del";

        fullGameSteam = "Scopri il gioco completo su Steam!";
        wishlistOnSteam = "Lista dei desideri su Steam!";

        need = "NECESSARI ";

        SetText();
    }
    #endregion

    #region German
    public void German()
    {
        SetAllFalse();
        german.SetActive(true);

        languageSelected = 4;
        PlayerPrefs.SetInt("languageSelected", languageSelected);

        #region Prestige strings
        tier = "Stufe ";
        diamondExplain = $"Diamant-Cursors geben {Prestige.diamondIncrease}X das Gold von goldenen Cursors";
        emeraldExplain = $"Smaragd-Cursors geben {Prestige.emeraldIncrease}X das Gold von goldenen Cursors";
        rainbowExplain = $"Wenn du einen Regenbogen-Cursor triffst, erhöht sich der Goldgewinn für die nächsten {Prestige.rainbowTotalCursors} Cursor um das  {Prestige.rainbowMinIncrease}-{Prestige.rainbowMaxIncrease}-fache.";
        secondsPrestige = "Sekunden";
        #endregion

        #region projectile names and fist
        projectileName[0] = "MESSER";
        projectileName[1] = "FELSBROCKEN";
        projectileName[2] = "STACHELN";
        projectileName[3] = "SCHURIKEN";
        projectileName[4] = "BÄLLE";
        projectileName[5] = "BOOMERANGS";
        projectileName[6] = "SPEERE";
        projectileName[7] = "LASER";
        projectileName[8] = "GROSSE BÄLLE";
        projectileName[9] = "PFEILE";
        projectileName[10] = "Stacheball";
        projectileName[11] = "KUGELN";

        activeGoldFist = $"{GoldenFistMechanics.activeGoldBonusIncreaseORIGINAL}X AKTIVES GOLD";
        passiveGoldFist = $"{GoldenFistMechanics.passiveGoldBonusIncreaseORIGINAL}X PASSIVES GOLD";
        clickscensionCoinsFist = "KLICKAUFSTIEG-MÜNZEN!";
        fallingCursorGoldFist = $"{GoldenFistMechanics.fallingGoldBonusIncreaseORIGINAL + 1}X FALLENDES CURSOR-KLICK-GOLD";
        projectileBonanzaFist = "PROJEKTIL-BELOHNUNG!";
        plussClickGoldFist = "KLICK-GOLD!";
        secondsFist = "SEKUNDEN";
        #endregion

        #region Stats
        totalClickscensionCoins = "Insgesamt erworbene Klickaufstieg-Münzen";
        totalCLickscensionSpent = "Insgesamt ausgegebene Klickaufstieg-Münzen";
        totalClickscensionUnlocked = "Insgesamt freigeschaltete Klickaufstieg-Upgrades";
        totalClickscensionPurchased = "Insgesamt gekaufte Klickaufstieg-Upgrades";
        totalDiamond = "Insgesamt erzeugte fallende Diamant-Cursor";
        totalEmerald = "Insgesamt erzeugte fallende Smaragd-Cursor";
        totalRainbow = "Insgesamt erzeugte fallende Regenbogen-Cursor";
        totalPurple = "Insgesamt erzeugte fallende lila Cursor";
        totalKnifes = "Insgesamt erzeugte Messer";
        totalBouldes = "Insgesamt erzeugte Felsbrocken";
        totalSpikes = "Insgesamt erzeugte Stacheln";
        totslShurikens = "Insgesamt erzeugte Shurikens";
        totalBoomerangs = "Insgesamt erzeugte Bumerangs";
        totalSpears = "Insgesamt erzeugte Speere";
        totalLasers = "Insgesamt erzeugte Laser";
        totalArrows = "Insgesamt erzeugte Pfeile";
        totalSpikeBalls = "Insgesamt erzeugte Stachelbälle";
        totalBullets = "Insgesamt geschossene Kugeln";

        totalBounce = "Gesamte Ballabpraller";
        totalBigBounce = "Gesamte Abpraller des großen Balls";
        #endregion

        #region Demo locked strings
        clickLocked = "Klick-Upgrades sind in der Demo gesperrt";
        clickscensionLocked = "Klickaufstieg ist in der Demo gesperrt";
        achLocked = "Errungenschaften sind in der Demo gesperrt";
        projectileLocked = "Projektil in der Demo gesperrt";
        #endregion

        max = "MAX";

        levelText = "Stufe ";

        crit = "KRITISCH!";
        chanceC = "Chance/C";
        chanceS = "Chance/S";
        speed = "Geschwindigkeit";
        cps = "cps";
        size = "groß";
        price = "PREIS: ";
        cgs = "cg/s";
        cgc = "cg/c";
        chance = "chance";
        increase = "Erhöhung";

        fullGameSteam = "Schau dir das komplette Spiel auf Steam an!";
        wishlistOnSteam = "Wunschliste auf Steam!";

        need = "BENÖTIGT ";

        SetText();
    }
    #endregion

    #region Spanish
    public void Spanish()
    {
        SetAllFalse();
        spanish.SetActive(true);

        languageSelected = 5;
        PlayerPrefs.SetInt("languageSelected", languageSelected);

        #region Prestige strings
        tier = "Nivel ";
        diamondExplain = $"Los cursores de diamante dan {Prestige.diamondIncrease} veces el oro de los cursores dorados.";
        emeraldExplain = $"Los cursores de esmeralda dan {Prestige.emeraldIncrease} veces el oro de los cursores dorados.";
        rainbowExplain = $"Al golpear un cursor arcoíris se incrementa la ganancia de oro de los siguientes {Prestige.rainbowTotalCursors} cursores en {Prestige.rainbowMinIncrease}-{Prestige.rainbowMaxIncrease} veces.";
        secondsPrestige = "segundos";
        #endregion

        #region projectile names and fist
        projectileName[0] = "CUCHILLOS";
        projectileName[1] = "ROCAS";
        projectileName[2] = "PICOS";
        projectileName[3] = "SHURIKENS";
        projectileName[4] = "PELOTAS";
        projectileName[5] = "BUMERANES";
        projectileName[6] = "LANZAS";
        projectileName[7] = "LÁSERES";
        projectileName[8] = "BOLAS GRANDES";
        projectileName[9] = "FLECHAS";
        projectileName[10] = "Bola de púas";
        projectileName[11] = "BALAS";

        activeGoldFist = $"{GoldenFistMechanics.activeGoldBonusIncreaseORIGINAL}X ORO ACTIVO";
        passiveGoldFist = $"{GoldenFistMechanics.passiveGoldBonusIncreaseORIGINAL}X ORO PASIVO";
        clickscensionCoinsFist = "¡MONEDAS DE CLICKSCENSION!";
        fallingCursorGoldFist = $"{GoldenFistMechanics.fallingGoldBonusIncreaseORIGINAL + 1}X ORO POR CLIC DE CURSORES CAÍDOS";
        projectileBonanzaFist = "¡BONANZA DE PROYECTILES!";
        plussClickGoldFist = "¡ORO POR CLIC!";
        secondsFist = "SEGUNDOS";
        #endregion

        #region Stats
        totalClickscensionCoins = "Total de monedas de clickscension adquiridas";
        totalCLickscensionSpent = "Total de monedas de clickscension gastadas";
        totalClickscensionUnlocked = "Total de mejoras de clickscension desbloqueadas";
        totalClickscensionPurchased = "Total de mejoras de clickscension compradas";
        totalDiamond = "Total de cursores de diamante caídos generados";
        totalEmerald = "Total de cursores de esmeralda caídos generados";
        totalRainbow = "Total de cursores arcoíris caídos generados";
        totalPurple = "Total de cursores morados caídos generados";
        totalKnifes = "Total de cuchillos generadas";
        totalBouldes = "Total de rocas generadas";
        totalSpikes = "Total de picos generados";
        totslShurikens = "Total de shurikens generados";
        totalBoomerangs = "Total de bumeranes generados";
        totalSpears = "Total de lanzas generadas";
        totalLasers = "Total de láseres generados";
        totalArrows = "Total de flechas generadas";
        totalSpikeBalls = "Total de bolas con pinchos generadas";
        totalBullets = "Total de balas disparadas";

        totalBounce = "Total de rebotes de la pelota";
        totalBigBounce = "Total de rebotes de la pelota grande";
        #endregion

        #region Demo locked strings
        clickLocked = "Las mejoras de clic están bloqueadas en la demo.";
        clickscensionLocked = "Clickscension está bloqueado en la demo.";
        achLocked = "Los logros están bloqueados en la demo.";
        projectileLocked = "Proyectiles bloqueados en la demo.";
        #endregion

        max = "MAX";

        levelText = "Nivel ";

        crit = "¡CRIT!";
        chanceC = "Probabilidad/C";
        chanceS = "Probabilidad/S";
        speed = "Velocidad";
        cps = "cps";
        size = "tamaño";
        price = "PRECIO: ";
        cgs = "cg/s";
        cgc = "cg/c";
        chance = "probabilidad";
        increase = "aumento";

        fullGameSteam = "¡Echa un vistazo al juego completo en Steam!";
        wishlistOnSteam = "¡Lista de deseos en Steam!";

        need = "NECESITAS ";

        SetText();
    }
    #endregion

    #region Japanese
    public void Japanese()
    {
        SetAllFalse();
        japanese.SetActive(true);

        languageSelected = 6;
        PlayerPrefs.SetInt("languageSelected", languageSelected);

        #region Prestige strings
        tier = "Tier ";
        diamondExplain = $"ダイヤモンドカーソルはゴールデンカーソルの{Prestige.diamondIncrease}倍のゴールドを与える";
        emeraldExplain = $"エメラルドカーソルはゴールデンカーソルの{Prestige.emeraldIncrease}倍のゴールドを得る";
        rainbowExplain = $"レインボーカーソルに当たると、次の{Prestige.rainbowTotalCursors}個のカーソルからのゴールド獲得量が{Prestige.rainbowMinIncrease}-{Prestige.rainbowMaxIncrease}倍になる";
        secondsPrestige = "秒";
        #endregion

        #region projectile names and fist
        projectileName[0] = "ナイフ";
        projectileName[1] = "岩";
        projectileName[2] = "スパイク";
        projectileName[3] = "手裏剣";
        projectileName[4] = "ボール";
        projectileName[5] = "ブーメラン";
        projectileName[6] = "槍";
        projectileName[7] = "レーザー";
        projectileName[8] = "ビッグボール";
        projectileName[9] = "矢";
        projectileName[10] = "スパイクボール";
        projectileName[11] = "銃弾";

        activeGoldFist = $"{GoldenFistMechanics.activeGoldBonusIncreaseORIGINAL}倍アクティブゴールド";
        passiveGoldFist = $"{GoldenFistMechanics.passiveGoldBonusIncreaseORIGINAL}倍パッシブゴールド";
        clickscensionCoinsFist = "クリックセンションコイン";
        fallingCursorGoldFist = $"落下カーソルクリックのゴールド{GoldenFistMechanics.fallingGoldBonusIncreaseORIGINAL + 1}倍";
        projectileBonanzaFist = "プロジェクタイルボナンザ";
        plussClickGoldFist = "クリックゴールド!";
        secondsFist = "秒";
        #endregion

        #region Stats
        totalClickscensionCoins = "獲得したクリックセンション・コイン合計";
        totalCLickscensionSpent = "使用したクリックセンション・コイン合計";
        totalClickscensionUnlocked = "アンロックしたクリックセンション・アップグレード合計";
        totalClickscensionPurchased = "購入したクリックセンション・アップグレード合計";
        totalDiamond = "落ちてくるダイヤモンドカーソルの生成数合計";
        totalEmerald = "落ちてくるエメラルドカーソルの生成数合計";
        totalRainbow = "落ちてくる虹色カーソルの生成数合計";
        totalPurple = "落ちてくる紫色カーソルの生成数合計";
        totalKnifes = "ナイフ生成数合計";
        totalBouldes = "ボルダー生成数合計";
        totalSpikes = "スパイク生成数合計";
        totslShurikens = "手裏剣の生成数合計";
        totalBoomerangs = "ブーメランの生成数合計";
        totalSpears = "槍の生成数合計";
        totalLasers = "レーザーの生成数合計";
        totalArrows = "矢の生成数合計";
        totalSpikeBalls = "スパイクボールの生成数合計";
        totalBullets = "総発射弾数";

        totalBounce = "ボールの総バウンド回数";
        totalBigBounce = "大きなボールの総バウンド回数";
        #endregion

        #region Demo locked strings
        clickLocked = "クリックアップグレードはデモでロックされています";
        clickscensionLocked = "クリックセンションはデモでロックされています";
        achLocked = "実績はデモでロックされています";
        projectileLocked = "デモでは投射がロックされています";
        #endregion

        max = "最大";

        levelText = "レベル ";

        crit = "クリティカル";
        chanceC = "Chance/C";
        chanceS = "Chance/S";
        speed = "スピード";
        cps = "クリック/秒";
        size = "サイズ";
        price = "価格 ";
        cgs = "cg/s";
        cgc = "cg/c";
        chance = "の確率";
        increase = "増加";

        fullGameSteam = "steamでゲーム本編をチェックする！";
        wishlistOnSteam = "Steamのウィッシュリスト！";

        need = "ターゲット ";

        SetText();
    }
    #endregion

    #region Korean
    public void Korean()
    {
        SetAllFalse();
        korean.SetActive(true);

        languageSelected = 7;
        PlayerPrefs.SetInt("languageSelected", languageSelected);

        #region Prestige strings
        tier = "단계 ";
        diamondExplain = $"다이아몬드 커서는 황금 커서의 골드를 {Prestige.diamondIncrease}X 배로 높여줍니다";
        emeraldExplain = $"에메랄드 커서는 황금 커서의 골드를 {Prestige.emeraldIncrease}X 배 높여줍니다";
        rainbowExplain = $"무지개 커서를 명중하면, 다음 {Prestige.rainbowTotalCursors} 개의 커서에서 얻는 골드 획득이  {Prestige.rainbowMinIncrease}-{Prestige.rainbowMaxIncrease}배 높아집니다.";
        secondsPrestige = "초";
        #endregion

        #region projectile names and fist
        projectileName[0] = "칼";
        projectileName[1] = "바위";
        projectileName[2] = "스파이크 ";
        projectileName[3] = "표창";
        projectileName[4] = "공";
        projectileName[5] = "부메랑";
        projectileName[6] = "창";
        projectileName[7] = "레이저";
        projectileName[8] = "큰 공";
        projectileName[9] = "화살 ";
        projectileName[10] = "스파이크 공";
        projectileName[11] = "총알";

        activeGoldFist = $"{GoldenFistMechanics.activeGoldBonusIncreaseORIGINAL}배 직접 골드 ";
        passiveGoldFist = $"{GoldenFistMechanics.passiveGoldBonusIncreaseORIGINAL}배 간접 골드 ";
        clickscensionCoinsFist = "클릭센션 코인!";
        fallingCursorGoldFist = $"{GoldenFistMechanics.fallingGoldBonusIncreaseORIGINAL + 1}배 하강 커서 클릭 골드 ";
        projectileBonanzaFist = "발사체 노다지! ";
        plussClickGoldFist = "클릭 골드!";
        secondsFist = "초";
        #endregion

        #region Stats
        totalClickscensionCoins = "총 획득한 클릭센션 코인 ";
        totalCLickscensionSpent = "총 사용한 클릭센션 코인 ";
        totalClickscensionUnlocked = "총 잠금 해제한 클릭센션 업그레이드 ";
        totalClickscensionPurchased = "총 구매한 클릭센션 업그레이드 ";
        totalDiamond = "총 생성된 다이아몬드 하강 커서 ";
        totalEmerald = "총 생성된 에메랄드 하강 커서 ";
        totalRainbow = "총 생성된 무지개 하강 커서 ";
        totalPurple = "총 생성된 보라색 하강 커서 ";
        totalKnifes = "총 생성된 칼";
        totalBouldes = "총 생성된 바위 ";
        totalSpikes = "총 생성된 스파이크 ";
        totslShurikens = "총 생성된 표창  ";
        totalBoomerangs = "총 생성된 부메랑";
        totalSpears = "총 생성된 창 ";
        totalLasers = "총 생성된 레이저 ";
        totalArrows = "총 생성된 화살 ";
        totalSpikeBalls = "총 생성된 스파이크 공 ";
        totalBullets = "총 발사된 총알 ";

        totalBounce = "공의 총 바운스 횟수";
        totalBigBounce = "큰 공의 총 바운스 횟수";
        #endregion

        #region Demo locked strings
        clickLocked = "데모 내 클릭 업데이트는 잠금 설정되어 있습니다. ";
        clickscensionLocked = "데모 내 클릭센션은 잠금 설정되어있습니다. ";
        achLocked = "데모 내 업적은 잠금 설정되어 있습니다. ";
        projectileLocked = "데모 내 잠금 설정된 발사체 ";
        #endregion

        max = "최대";

        levelText = "레벨 ";

        crit = "치명타!";
        chanceC = "확률/클릭";
        chanceS = "확률/초 ";
        speed = "스피드";
        cps = "초당 클릭";
        size = "크기 ";
        price = "가격: ";
        cgs = "cg/s";
        cgc = "cg/c";
        chance = "확률";
        increase = "증가 ";

        fullGameSteam = "스팀에서 전체 게임을 확인해 보세요!";
        wishlistOnSteam = "스팀 위시리스트!";
        need = "요구 사항 ";

        SetText();
    }
    #endregion

    #region Polish
    public void Polish()
    {
        SetAllFalse();
        polish.SetActive(true);

        languageSelected = 8;
        PlayerPrefs.SetInt("languageSelected", languageSelected);

        #region Prestige strings
        tier = "Poziom ";
        diamondExplain = $"Diamentowe kursory zapewniają {Prestige.diamondIncrease} razy więcej złota niż złote kursory.";
        emeraldExplain = $"Szmaragdowe kursory zapewniają {Prestige.emeraldIncrease} razy więcej złota niż złote kursory.";
        rainbowExplain = $"Trafienie tęczowego kursora zwiększa przyrost złota z kolejnych {Prestige.rainbowTotalCursors}  kursorów od {Prestige.rainbowMinIncrease}do{Prestige.rainbowMaxIncrease} razy.";
        secondsPrestige = "sekund";
        #endregion

        #region projectile names and fist
        projectileName[0] = "NOŻE";
        projectileName[1] = "KAMIENIE";
        projectileName[2] = "KOLCE";
        projectileName[3] = "SHURIKENY";
        projectileName[4] = "PIŁKI";
        projectileName[5] = "BUMERANGI";
        projectileName[6] = "WŁÓCZNIE";
        projectileName[7] = "LASERY";
        projectileName[8] = "DUŻE PIŁKI";
        projectileName[9] = "STRZAŁKI";
        projectileName[10] = "Kula Kolczasta";
        projectileName[11] = "POCISKI";

        activeGoldFist = $"{GoldenFistMechanics.activeGoldBonusIncreaseORIGINAL}X AKTYWNE ZŁOTO";
        passiveGoldFist = $"{GoldenFistMechanics.passiveGoldBonusIncreaseORIGINAL}X PASYWNE ZŁOTO";
        clickscensionCoinsFist = "MONET WZNIESIENIA";
        fallingCursorGoldFist = $"{GoldenFistMechanics.fallingGoldBonusIncreaseORIGINAL + 1}X ZŁOTO ZA KLIKNIĘCIE SPADAJĄCEGO KURSORA";
        projectileBonanzaFist = "BONANZA POCISKÓW!";
        plussClickGoldFist = "ZŁOTO ZA KLIKNIĘCIE!";
        secondsFist = "SEKUND";
        #endregion

        #region Stats
        totalClickscensionCoins = "Łączna liczba zdobytych monet wzniesienia";
        totalCLickscensionSpent = "Łączna liczba wydanych monet wzniesienia";
        totalClickscensionUnlocked = "Łączna liczba odblokowanych ulepszeń wzniesienia";
        totalClickscensionPurchased = "Łączna liczba zakupionych ulepszeń wzniesienia";
        totalDiamond = "Łączna liczba spadających diamentowych kursorów";
        totalEmerald = "Łączna liczba spadających szmaragdowych kursorów";
        totalRainbow = "Łączna liczba spadających tęczowych kursorów";
        totalPurple = "Łączna liczba spadających fioletowych kursorów";
        totalKnifes = "Łączna liczba noży";
        totalBouldes = "Łączna liczba głazów";
        totalSpikes = "Łączna liczba kolców";
        totslShurikens = "Łączna liczba shurikenów";
        totalBoomerangs = "Łączna liczba bumerangów";
        totalSpears = "Łączna liczba włóczni";
        totalLasers = "Łączna liczba laserów";
        totalArrows = "Łączna liczba strzał";
        totalSpikeBalls = "Łączna libcza kul z kolcami";
        totalBullets = "Łączna liczba wystrzelonych pocisków";

        totalBounce = "Łączna liczba odbić piłki";
        totalBigBounce = "Łączna liczba odbić dużej piłki";
        #endregion

        #region Demo locked strings
        clickLocked = "Ulepszenia kliknięć są zablokowane w wersji demo";
        clickscensionLocked = "Wzniesienie jest zablokowane w wersji demo.";
        achLocked = "Osiągnięcia są zablokowane w wersji demo";
        projectileLocked = "Pocisk zablokowany w wersji demo";
        #endregion

        max = "MAKS";

        levelText = "Poziom ";

        crit = "KRYT!";
        chanceC = "Szansa/C";
        chanceS = "Szansa/S";
        speed = "prędkości";
        cps = "cps";
        size = "rozmiar";
        price = "CENA: ";
        cgs = "cg/s";
        cgc = "cg/c";
        chance = "szans";
        increase = "wzrost";

        fullGameSteam = "Osiągnięcia są zablokowane w wersji demo";
        wishlistOnSteam = "Dodaj do listy życzeń na Steamie!";

        need = "POTRZEBNE ";

        SetText();
    }
    #endregion

    #region Portugese
    public void Portugese()
    {
        SetAllFalse();
        portugese.SetActive(true);

        languageSelected = 9;
        PlayerPrefs.SetInt("languageSelected", languageSelected);

        #region Prestige strings
        tier = "Nível ";
        diamondExplain = $"Os cursores de diamante dão {Prestige.diamondIncrease}X o ouro dos cursores de ouro";
        emeraldExplain = $"Os cursores de esmeralda dão {Prestige.emeraldIncrease}X o ouro dos cursores de ouro";
        rainbowExplain = $"Atingir um cursor arco-íris aumenta o ganho de ouro dos próximos {Prestige.rainbowTotalCursors} cursores em {Prestige.rainbowMinIncrease}-{Prestige.rainbowMaxIncrease}";
        secondsPrestige = "segundos";
        #endregion

        #region projectile names and fist
        projectileName[0] = "FACAS";
        projectileName[1] = "PEDRAS";
        projectileName[2] = "ESPETOS";
        projectileName[3] = "SHURIKENS";
        projectileName[4] = "BOLAS";
        projectileName[5] = "BOOMERANGS";
        projectileName[6] = "LANÇAS";
        projectileName[7] = "LASERS";
        projectileName[8] = "BOLAS GRANDES";
        projectileName[9] = "SETAS";
        projectileName[10] = "Bola De Espinhos";
        projectileName[11] = "BALAS";

        activeGoldFist = $"{GoldenFistMechanics.activeGoldBonusIncreaseORIGINAL}X OURO ATIVO";
        passiveGoldFist = $"{GoldenFistMechanics.passiveGoldBonusIncreaseORIGINAL}X OURO PASSIVO";
        clickscensionCoinsFist = "MOEDAS DE CLIQUES!";
        fallingCursorGoldFist = $"{GoldenFistMechanics.fallingGoldBonusIncreaseORIGINAL + 1}X OURO DE CLIQUE DO CURSOR EM QUEDA";
        projectileBonanzaFist = "BONANÇA DE PROJÉTEIS!";
        plussClickGoldFist = "CLICK GOLD";
        secondsFist = "SEGUNDOS";
        #endregion

        #region Stats
        totalClickscensionCoins = "Total de moedas de clickscension adquiridas";
        totalCLickscensionSpent = "Total de moedas de clickscension gastas";
        totalClickscensionUnlocked = "Total de upgrades de clickscension desbloqueados";
        totalClickscensionPurchased = "Total de upgrades de clickscension comprados";
        totalDiamond = "Total de cursores de diamante em queda gerados";
        totalEmerald = "Total de cursores esmeralda em queda gerados";
        totalRainbow = "Total de cursores arco-íris em queda gerados";
        totalPurple = "Total de cursores roxos em queda gerados";
        totalKnifes = "Total de facas geradas";
        totalBouldes = "Total de pedras geradas";
        totalSpikes = "Total de espinhos gerados";
        totslShurikens = "Total de shurikens gerados";
        totalBoomerangs = "Total de bumerangues gerados";
        totalSpears = "Total de lanças geradas";
        totalLasers = "Total de lasers gerados";
        totalArrows = "Total de flechas geradas";
        totalSpikeBalls = "Total de bolas de espinhos geradas";
        totalBullets = "Total de balas disparadas";

        totalBounce = "Total de quicadas da bola";
        totalBigBounce = "Total de quicadas da bola grande";
        #endregion

        #region Demo locked strings
        clickLocked = "As atualizações de cliques estão bloqueadas na demonstração";
        clickscensionLocked = "O Clickscension está bloqueado na demonstração";
        achLocked = "As conquistas estão bloqueadas na demonstração";
        projectileLocked = "Projétil bloqueado na demonstração";
        #endregion

        max = "MAX";

        levelText = "Nível ";

        crit = "CRIT!";
        chanceC = "de chance/C";
        chanceS = "de chance/S";
        speed = "Velocidade";
        cps = "cps";
        size = "tamanho";
        price = "PREÇO: ";
        cgs = "cg/s";
        cgc = "cg/c";
        chance = "de chance";
        increase = "de aumento";

        fullGameSteam = "Confira o jogo completo no Steam!";
        wishlistOnSteam = "Lista de desejos no Steam!";

        need = "NECESSÁRIO ";

        SetText();
    }
    #endregion

    #region Russian
    public void Russian()
    {
        SetAllFalse();
        russian.SetActive(true);

        languageSelected = 10;
        PlayerPrefs.SetInt("languageSelected", languageSelected);

        #region Prestige strings
        tier = "Слой ";
        diamondExplain = $"Алмазные курсоры дают в {Prestige.diamondIncrease} раза больше золота, чем золотые курсоры";
        emeraldExplain = $"Изумрудные курсоры дают в {Prestige.emeraldIncrease} раз больше золота, чем золотые курсоры";
        rainbowExplain = $"Попадание по радужному курсору увеличивает прирост золота от следующих {Prestige.rainbowTotalCursors} курсоров в {Prestige.rainbowMinIncrease}-{Prestige.rainbowMaxIncrease} раз";
        secondsPrestige = "секунд";
        #endregion

        #region projectile names and fist
        projectileName[0] = "НОЖИ";
        projectileName[1] = "ВАЛУНЫ";
        projectileName[2] = "ШИПЫ";
        projectileName[3] = "СЮРИКЕНЫ";
        projectileName[4] = "ШАРЫ";
        projectileName[5] = "БУМЕРАНГИ";
        projectileName[6] = "КОПЬЯ";
        projectileName[7] = "ЛАЗЕРЫ";
        projectileName[8] = "БОЛЬШИЕ ШАРЫ";
        projectileName[9] = "СТРЕЛЫ";
        projectileName[10] = "Шипастый шар";
        projectileName[11] = "ПУЛИ";

        activeGoldFist = $"{GoldenFistMechanics.activeGoldBonusIncreaseORIGINAL}X АКТИВНОГО ЗОЛОТА";
        passiveGoldFist = $"{GoldenFistMechanics.passiveGoldBonusIncreaseORIGINAL}X ПАССИВНОГО ЗОЛОТА";
        clickscensionCoinsFist = "МОНЕТЫ ЗА КЛИКИ!";
        fallingCursorGoldFist = $"{GoldenFistMechanics.fallingGoldBonusIncreaseORIGINAL + 1}X ЗОЛОТА ЗА КЛИК НА ПАДАЮЩИЙ КУРСОР";
        projectileBonanzaFist = "БОНАНЗА СНАРЯДОВ!";
        plussClickGoldFist = "ЗОЛОТО ЗА КЛИК";
        secondsFist = "СЕКУНД";
        #endregion

        #region Stats
        totalClickscensionCoins = "Всего получено монет за клики";
        totalCLickscensionSpent = "Всего потрачено монет за клики";
        totalClickscensionUnlocked = "Всего разблокировано улучшений за клики";
        totalClickscensionPurchased = "Всего куплено улучшений за клики";
        totalDiamond = "Всего найдено падающих алмазных курсоров";
        totalEmerald = "Всего найдено падающих изумрудных курсоров";
        totalRainbow = "Всего найдено падающих радужных курсоров";
        totalPurple = "Всего найдено падающих фиолетовых курсоров";
        totalKnifes = "Всего создано ножей";
        totalBouldes = "Всего найдено валунов";
        totalSpikes = "Всего найдено шипов";
        totslShurikens = "Всего найдено сюрикенов";
        totalBoomerangs = "Всего найдено бумерангов";
        totalSpears = "Всего найдено копий";
        totalLasers = "Всего найдено лазеров";
        totalArrows = "Всего найдено стрел";
        totalSpikeBalls = "Всего найдено шипастых шаров";
        totalBullets = "Всего выпущено пуль";

        totalBounce = "Общее количество отскоков мяча";
        totalBigBounce = "Общее количество отскоков большого мяча";
        #endregion

        #region Demo locked strings
        clickLocked = "Улучшения кликов в демоверсии недоступны";
        clickscensionLocked = "Бонусы за клики в демоверсии недоступны";
        achLocked = "Достижения в демоверсии недоступны";
        projectileLocked = "Снаряды в демоверсии недоступны";
        #endregion

        max = "МАКС";

        levelText = "Уровень ";

        crit = "КРИТ!";
        chanceC = "шанс/C";
        chanceS = "шанс/S";
        speed = "скорость";
        cps = "клик/сек";
        size = "Размер ";
        price = "ЦЕНА: ";
        cgs = "cg/s";
        cgc = "cg/c";
        chance = "Шанс";
        increase = "Повышение";

        fullGameSteam = "См. полную версию игры в steam!";
        wishlistOnSteam = "Список желаний в стиме!";

        need = "ТРЕБУЕТСЯ ";

        SetText();
    }
    #endregion

    #region Chinese
    public void Chinese()
    {
        SetAllFalse();
        chinese.SetActive(true);

        languageSelected = 11;
        PlayerPrefs.SetInt("languageSelected", languageSelected);

        #region Prestige strings
        tier = "级 ";
        diamondExplain = $"钻石光标获得的金币是黄金光标的 {Prestige.diamondIncrease} 倍";
        emeraldExplain = $"绿宝石光标获得的金币是黄金光标的 {Prestige.emeraldIncrease} 倍";
        rainbowExplain = $"击中彩虹光标会让接下来 {Prestige.rainbowTotalCursors} 个光标的金币收益增加 {Prestige.rainbowMinIncrease}-{Prestige.rainbowMaxIncrease}倍";
        secondsPrestige = "秒";
        #endregion

        #region projectile names and fist
        projectileName[0] = "刀";
        projectileName[1] = "巨石";
        projectileName[2] = "尖刺";
        projectileName[3] = "飞镖";
        projectileName[4] = "球";
        projectileName[5] = "回旋镖";
        projectileName[6] = "长矛";
        projectileName[7] = "激光";
        projectileName[8] = "大球";
        projectileName[9] = "箭";
        projectileName[10] = "尖刺球";
        projectileName[11] = "子弹";

        activeGoldFist = $"{GoldenFistMechanics.activeGoldBonusIncreaseORIGINAL} 倍主动金币";
        passiveGoldFist = $"{GoldenFistMechanics.passiveGoldBonusIncreaseORIGINAL} 倍被动金币";
        clickscensionCoinsFist = "点击飞升币硬币!";
        fallingCursorGoldFist = $"{GoldenFistMechanics.fallingGoldBonusIncreaseORIGINAL + 1} 倍下落光标点击金币";
        projectileBonanzaFist = "弹射物暴富!";
        plussClickGoldFist = "点击金币";
        secondsFist = "秒";
        #endregion

        #region Stats
        totalClickscensionCoins = "获得的点击飞升币币总数";
        totalCLickscensionSpent = "花费的点击飞升币总数";
        totalClickscensionUnlocked = "已解锁的点击飞升升级总数";
        totalClickscensionPurchased = "购买的点击飞升升级总数";
        totalDiamond = "生成的掉落钻石光标总数";
        totalEmerald = "生成的掉落绿宝石光标总数";
        totalRainbow = "生成的掉落彩虹光标总数";
        totalPurple = "生成的掉落紫色光标总数";
        totalKnifes = "生成的刀总数";
        totalBouldes = "生成的巨石总数";
        totalSpikes = "生成的尖刺总数";
        totslShurikens = "生成的飞镖总数";
        totalBoomerangs = "生成的回旋镖总数";
        totalSpears = "生成的长矛总数";
        totalLasers = "生成的激光总数";
        totalArrows = "生成的箭总数";
        totalSpikeBalls = "生成的尖刺球总数";
        totalBullets = "子弹总数";

        totalBounce = "球的总弹跳次数";
        totalBigBounce = "大球的总弹跳次数";
        #endregion

        #region Demo locked strings
        clickLocked = "在试玩版中，点击升级被锁定";
        clickscensionLocked = "在试玩版中，点击飞升币被锁定";
        achLocked = "在试玩版中，成就被锁定";
        projectileLocked = "在试玩版中，弹射物被锁定";
        #endregion

        max = "最大";

        crit = "暴击!";
        chanceC = "机率/点击";
        chanceS = "机率/秒";
        speed = "速度";
        cps = "次点击/秒";
        size = "大小";
        price = "价格：";
        cgs = "cg/s";
        cgc = "cg/c";
        chance = "几率";
        increase = "增加";

        levelText = "级 ";

        fullGameSteam = "在 steam 上查看完整游戏！";
        wishlistOnSteam = "steam愿望清单！";

        need = "需要 ";

        SetText();
    }
    #endregion

    public TextMeshProUGUI needText;

    public void SetText()
    {
        #region English
        if (languageSelected == 1)
        {
            tierExplain = $"Every falling cursor tier increases the amount of times a falling cursor needs to be clicked before breaking. It also increases how much click gold is received per falling cursor. Current falling cursor tier = Tier {Prestige.fallingCursorTier}. Gold value = {(Prestige.minFallingCursorIncrease + 1) * 100}%-{(Prestige.maxFallingCursorIncrease + 1) * 100}% of the active click gold amount.";

            purpleExplain = $"Purple cursors give +{(1 * (1 + Prestige.clickscensionCoinIncrease)).ToString("F0")} clickscension coin.";
        }
        #endregion

        #region French
        if (languageSelected == 2)
        {
            //French
            tierExplain = $"Chaque niveau de curseurs qui tombent augmente le nombre de clics nécessaires pour le briser. Cela augmente également la quantité d'or par clic reçue par curseur tombant. Niveau actuel de curseur tombant = Niveau {Prestige.fallingCursorTier}. Valeur en or = {(Prestige.minFallingCursorIncrease + 1) * 100}%-{(Prestige.maxFallingCursorIncrease + 1) * 100}% de la quantité d'or par clic actif.";

            purpleExplain = $"Les curseurs violets donnent +{(1 * (1 + Prestige.clickscensionCoinIncrease)).ToString("F0")} pièce de clics-ascension.";
        }
        #endregion

        #region Italian
        if (languageSelected == 3)
        {
            //French
            tierExplain = $"Chaque niveau de curseurs qui tombent augmente le nombre de clics nécessaires pour le briser. Cela augmente également la quantité d'or par clic reçue par curseur tombant. Niveau actuel de curseur tombant = Niveau {Prestige.fallingCursorTier}. Valeur en or = {(Prestige.minFallingCursorIncrease + 1) * 100}%-{(Prestige.maxFallingCursorIncrease + 1) * 100}% de la quantité d'or par clic actif.";

            purpleExplain = $"Les curseurs violets donnent +{(1 * (1 + Prestige.clickscensionCoinIncrease)).ToString("F0")} pièce de clics-ascension.";
        }
        #endregion

        #region German
        if (languageSelected == 4)
        {
            //French
            tierExplain = $"Jede Stufe des fallenden Cursors erhöht die Anzahl der Klicks, die ein fallender Cursor benötigt, bevor er zerbricht. Sie erhöht auch die Menge an Klick-Gold, die man pro fallendem Cursor erhält. Aktuelle Stufe des fallenden Cursors = Stufe {Prestige.fallingCursorTier}. Goldwert = {(Prestige.minFallingCursorIncrease + 1) * 100}%-{(Prestige.maxFallingCursorIncrease + 1) * 100}% des aktiven Klick-Goldbetrags.";

            purpleExplain = $"Violette Cursors geben +{(1 * (1 + Prestige.clickscensionCoinIncrease)).ToString("F0")} Klickaufstieg-Münze.";
        }
        #endregion

        #region Spanish
        if (languageSelected == 5)
        {
            //French
            tierExplain = $"Cada nivel del cursor que cae aumenta la cantidad de veces que un cursor que cae necesita ser clicado antes de romperse. También incrementa cuánto oro por clic se recibe por cada cursor que cae. Nivel actual del cursor que cae = Nivel {Prestige.fallingCursorTier}. Valor de oro = {(Prestige.minFallingCursorIncrease + 1) * 100}%-{(Prestige.maxFallingCursorIncrease + 1) * 100}% de la cantidad de oro por clic activo.";

            purpleExplain = $"Los cursores morados dan +{(1 * (1 + Prestige.clickscensionCoinIncrease)).ToString("F0")} moneda de clickscension.";
        }
        #endregion

        #region Japanese
        if (languageSelected == 6)
        {
            //French
            tierExplain = $"落下カーソルのレベルが上がるごとに、落下カーソルが壊れるまでにクリックされる回数が増える。また、落下カーソル1つにつき受け取れるクリックゴールドの量も増加する。現在の落下カーソル階層 = Tier {Prestige.fallingCursorTier}. ゴールド値 = アクティブクリックゴールド量の {(Prestige.minFallingCursorIncrease + 1) * 100}%-{(Prestige.maxFallingCursorIncrease + 1) * 100}%";

            purpleExplain = $"紫のカーソルは+{(1 * (1 + Prestige.clickscensionCoinIncrease)).ToString("F0")}クリックセンションコインを与える";
        }
        #endregion

        #region Korean
        if (languageSelected == 7)
        {
            //French
            tierExplain = $"하강 커서 단계마다 파괴 전 하강 커서를 클릭해야 하는 횟수가 증가합니다. 또한, 하강 커서 당 받게 되는 클릭 골드의 수가 증가합니다. 현재 하강 커서 단계 = 단계 {Prestige.fallingCursorTier}. 골드 가치= 직접 클릭 골드 양의 {(Prestige.minFallingCursorIncrease + 1) * 100}%-{(Prestige.maxFallingCursorIncrease + 1) * 100}%";

            purpleExplain = $"보라색 커서는 클릭센션 코인 +{(1 * (1 + Prestige.clickscensionCoinIncrease)).ToString("F0")} 를 제공합니다. ";
        }
        #endregion

        #region Polish
        if (languageSelected == 8)
        {
            //French
            tierExplain = $"Każdy poziom spadającego kursora zwiększa liczbę kliknieć potrzebną na jego usunięcie. Zwiększa również ilość złota otrzymywanego za kliknięcie spadającego kursora. Obecny poziom spadającego kursora = poziom {Prestige.fallingCursorTier}. Wartość złota = {(Prestige.minFallingCursorIncrease + 1) * 100}%-{(Prestige.maxFallingCursorIncrease + 1) * 100}% za kliknięcie.";

            purpleExplain = $"Fioletowe kursory zapewniają +{(1 * (1 + Prestige.clickscensionCoinIncrease)).ToString("F0")} monetę wzniesienia.";
        }
        #endregion

        #region Portugese
        if (languageSelected == 9)
        {
            //French
            tierExplain = $"Cada nível de cursor em queda aumenta a quantidade de vezes que um cursor em queda precisa ser clicado antes de quebrar. Também aumenta a quantidade de ouro de clique recebida por cursor em queda. Nível atual do cursor em queda = Nível {Prestige.fallingCursorTier}. Valor do ouro = {(Prestige.minFallingCursorIncrease + 1) * 100}%-{(Prestige.maxFallingCursorIncrease + 1) * 100}% da quantidade de ouro do clique ativo.";

            purpleExplain = $"Os cursores roxos concedem +{(1 * (1 + Prestige.clickscensionCoinIncrease)).ToString("F0")} moeda de clique.";
        }
        #endregion

        #region Russian
        if (languageSelected == 10)
        {
            //French
            tierExplain = $"Каждый слой падающих курсоров повышает количество нажатий на падающий курсор до его разрушения. Он также увеличивает количество золота за клик по падающему курсору. Текущий слой падающих курсоров = Слой {Prestige.fallingCursorTier}. Цена золота = {(Prestige.minFallingCursorIncrease + 1) * 100}%-{(Prestige.maxFallingCursorIncrease + 1) * 100}% от суммы клика по активному золоту.";

            purpleExplain = $"Фиолетовые курсоры дают +{(1 * (1 + Prestige.clickscensionCoinIncrease)).ToString("F0")} монету за клик.";
        }
        #endregion

        #region Chinese
        if (languageSelected == 11)
        {
            //French
            tierExplain = $"每个光标下落等级都会增加击碎前需要点击的次数。它还会增加每个下落光标获得的点击金币的数量。当前下落光标等级 = 2 级 {Prestige.fallingCursorTier}. 金币数量 = {(Prestige.minFallingCursorIncrease + 1) * 100}%-{(Prestige.maxFallingCursorIncrease + 1) * 100}%的主动点击金币数量。";

            purpleExplain = $"紫色光标可获得+{(1 * (1 + Prestige.clickscensionCoinIncrease)).ToString("F0")}个 点击飞升币";
        }
        #endregion

        needText.text = $"({need}{Prestige.needForPrestige})";
    }
}
