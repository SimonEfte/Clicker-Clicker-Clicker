using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class OfflineProgression : MonoBehaviour
{
    public TextMeshProUGUI offlineGainsText, timePassedText;
    public GameObject offlineBar;
    public double offlineGoldAmoint;
    public AudioManager audioManager;
    public int minutesGone;
    public bool isGameLoaded;

    void Start()
    {
        if (DemoScript.isDemo == false)
        {
            if (PlayerPrefs.HasKey("OfflineProgression"))
            {
                StartCoroutine(Wait());
            }

            SettingTime();
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.1f);

        DateTime lastLogIn = DateTime.Parse(PlayerPrefs.GetString("OfflineProgression"));
        TimeSpan timeSpan = DateTime.Now - lastLogIn;

        #region text
        if (LocalizationStrings.languageSelected == 1) { timePassedText.text = String.Format("Time gone:<color=green> {0} Days {1} Hours {2} Minutes {3} seconds", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds); }

        //French
        else if (LocalizationStrings.languageSelected == 2) { timePassedText.text = String.Format("Temps écoulé :<color=green> {0} jours {1} heures {2} minutes {3} secondes", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds); }

        //Italian
        else if (LocalizationStrings.languageSelected == 3) { timePassedText.text = String.Format("<size=6.5>Tempo trascorso:<color=green> {0} Giorni {1} Ore {2} Minuti {3} secondi", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds); }

        //German
        else if (LocalizationStrings.languageSelected == 4) { timePassedText.text = String.Format("<size=6.5>Vergangene Zeit:<color=green> {0} Tage {1} Stunden {2} Minuten {3} Sekunden", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds); }

        //Spanish
        else if (LocalizationStrings.languageSelected == 5) { timePassedText.text = String.Format("<size=6.5>Tiempo transcurrido:<color=green> {0} días {1} horas {2} minutos {3} segundos", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds); }

        //Japanese
        else if (LocalizationStrings.languageSelected == 6) { timePassedText.text = String.Format("経過時間：<color=green> {0} 日 {1} 時間 {2} 分 {3} 秒", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds); }

        //Korean
        else if (LocalizationStrings.languageSelected == 7) { timePassedText.text = String.Format("지난 시간:<color=green> {0} 일 {1} 시간 {2} 분 {3} 초", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds); }

        //Polish
        else if (LocalizationStrings.languageSelected == 8) { timePassedText.text = String.Format("Czas nieobecności:<color=green> {0} Dni {1} Godzin {2} Minut {3} Sekund", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds); }

        //Portugese NEW!!!
        else if (LocalizationStrings.languageSelected == 9) { timePassedText.text = String.Format("Tempo ausente:<color=green> {0} Dias {1} Horas {2} Minutos {3} segundos", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds); }

        //Russian
        else if (LocalizationStrings.languageSelected == 10) { timePassedText.text = String.Format("Прошедшее время:<color=green> {0} дня {1} часов {2} минуты {3} секунд", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds); }

        //Chinese
        else if (LocalizationStrings.languageSelected == 11) { timePassedText.text = String.Format("时间过去了:<color=green> {0} 天 {1} 小时 {2} 分钟 {3} 秒", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds); }


        #endregion

        if (((int)timeSpan.TotalMinutes) > 1500)
        {
            minutesGone = 1500;
        }
        else
        {
            minutesGone = ((int)timeSpan.TotalMinutes);
        }

        offlineGoldAmoint = 0;

        double prestigePassive = 1 + Prestige.passiveGoldIncrease;
        double totalGold = MainCursorClick.totalPassivePoints * prestigePassive;

        if(minutesGone > 0)
        {
            int activeTimes = 72;
            int passiveTimes = 6;

            if(Prestige.fallingCursorTier > 1)
            {
                activeTimes += (5 * Prestige.fallingCursorTier);
                passiveTimes += (3 * Prestige.fallingCursorTier);
            }

            offlineGoldAmoint += (MainCursorClick.cursorClickPoint * (1 + Prestige.activeGoldIncrease)) * minutesGone;
            offlineGoldAmoint *= activeTimes;
            offlineGoldAmoint += (totalGold) * (minutesGone * passiveTimes);
        }
        else { offlineGoldAmoint = 0; }

        offlineGainsText.text = "+" + ScaleNumbers.FormatPoints(offlineGoldAmoint) + " cg";

        offlineBar.SetActive(true);
    }

    public void Claim()
    {
        audioManager.Play("Claim");

        offlineBar.SetActive(false);
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

        MainCursorClick.totalClickPoints += offlineGoldAmoint;
        Stats.totalGold += offlineGoldAmoint;

        if (offlineGoldAmoint > (LevelUp.goldNeeded - LevelUp.currentPrestigeCoins))
        {
            StartCoroutine(OverlappingGold());
        }
        else
        {
            LevelUp.currentPrestigeCoins += offlineGoldAmoint;
        }
    }

    IEnumerator OverlappingGold()
    {
        double goldLeftWith = 0;
        double currentPrestigeCoins = LevelUp.currentPrestigeCoins;
        double goldNeededCurrent = LevelUp.goldNeeded;

        while (offlineGoldAmoint > (goldNeededCurrent - currentPrestigeCoins))
        {
            offlineGoldAmoint -= (goldNeededCurrent - currentPrestigeCoins);
            goldLeftWith = (goldNeededCurrent - currentPrestigeCoins);
            currentPrestigeCoins = 0;
            goldNeededCurrent *= 2;
            LevelUp.currentPrestigeCoins += goldLeftWith;
            yield return new WaitForSeconds(0.2f);
        }
        LevelUp.currentPrestigeCoins += offlineGoldAmoint;
    }

    public void SettingTime()
    {
        StartCoroutine(SetTimeWait());
    }

    IEnumerator SetTimeWait()
    {
        yield return new WaitForSeconds(2);
        isGameLoaded = true;
        yield return new WaitForSeconds(8);
        SetTime();
        SettingTime();
    }

    public void SetTime()
    {
        PlayerPrefs.SetString("OfflineProgression", DateTime.Now.ToString());
    }

    private void OnApplicationQuit()
    {
        if (DemoScript.isDemo == false) { SetTime(); }
    }

    public DataPersistenceManager dataPersistanceScript;

    private void OnApplicationPause(bool isPaused)
    {
        if (isGameLoaded == true)
        {
            if (MobileScript.isMobile == true)
            {
                if (isPaused)
                {
                    dataPersistanceScript.SaveTheGameData();
                }
            }
        }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (isGameLoaded == true)
        {
            if (MobileScript.isMobile == true)
            {
                if (!hasFocus)
                {
                    dataPersistanceScript.SaveTheGameData();
                }
            }
        }
    }
}
