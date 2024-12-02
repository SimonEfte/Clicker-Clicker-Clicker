using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsAndUI : MonoBehaviour
{
    public Animation upgradeAnim, clickUpgradeAnim, prestigeAnim, settingsAnim, achivementsAnim, statsAnim, skinsAnim;
    public Button shopBtn, closeShopBtn, clickUpgradeBtn, closeClickBtn, prestigeBtn, closePrestigeBtn, closeSettingsBtn, closeStatsBtn, closeAchBtn, settingBtn, statBtn, achBtn, skinsBtn, closeSkinsBtn;

    public static bool isInUpgradeFrame, isIntClickUpgradeFrame, isInPrestigeFrame, isInSettings, isInStats, isInAchievements, isInSkins;

    public static bool isInAnyFrame;

    private List<Resolution> resolutions = new List<Resolution>();
    public TMP_Dropdown resolutionDropdown;

    public Animation wishlistAnim;
    public GameObject progressSavesOVerText;

    public AudioManager audioManager;

    public int timesClickedTabs, timesTabClicksNeeded;
    public DataPersistenceManager dataPersistanceScript;

    #region awake
    public void Awake()
    {
        if(!PlayerPrefs.HasKey("saveMainChance"))
        {
            mainChance = 1f;
        }
        else
        { 
            mainChance = PlayerPrefs.GetFloat("saveMainChance");
        }

        if (!PlayerPrefs.HasKey("saveFallingChance"))
        {
            fallingChance = 1f;
        }
        else 
        { 
            fallingChance = PlayerPrefs.GetFloat("saveFallingChance"); 
        }

        mainSlider.value = mainChance;
        fallingSlider.value = fallingChance;
        mainPercent.text = $"{(mainChance * 100).ToString("F0")}%" ;
        fallingPercent.text = $"{(fallingChance * 100).ToString("F0")}%";

        triggerResolution = true;

        if (!PlayerPrefs.HasKey("particleOnSave"))
        {
            particleToggleOn.SetActive(true);
            particleToggleOff.SetActive(false);
        }
        else
        {
            particleOn = PlayerPrefs.GetInt("particleOnSave");
            if(particleOn == 0) { particleToggleOn.SetActive(true); particleToggleOff.SetActive(false); }
            else { particleToggleOn.SetActive(false); particleToggleOff.SetActive(true); }
        }

        if (!PlayerPrefs.HasKey("saveResIndex"))
        {
            FindResolutionIndex();
        }
        else
        {
            resolutionIndexSave = PlayerPrefs.GetInt("saveResIndex");
        }

        if (MobileScript.isMobile == true)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
        else
        {
            if (!PlayerPrefs.HasKey("SaveFullScreen"))
            {
                saveFullsScreen = 0;
            }
            else
            {
                saveFullsScreen = PlayerPrefs.GetInt("SaveFullScreen");
            }

            if (saveFullsScreen == 1)
            {
                Screen.fullScreenMode = FullScreenMode.Windowed;
                fullScreenToggleOn.SetActive(false);
                fullSreenToggleOff.SetActive(true);
            }
            else
            {
                fullScreenToggleOn.SetActive(true);
                fullSreenToggleOff.SetActive(false);
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            }


            if (!PlayerPrefs.HasKey("ScreenWidth"))
            {
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            }
            else
            {
                saveWidth = PlayerPrefs.GetInt("ScreenWidth");
                saveHeight = PlayerPrefs.GetInt("ScreenHeight");
                Screen.SetResolution(saveWidth, saveHeight, Screen.fullScreenMode);
            }
        }
    }
    #endregion

    public GameObject appStoreBtn, googlePlayBtn;
    public GameObject steamBtn, discordBtn, saveBtn, exitBtn, resetBtn;

    public void Start()
    {
        timesTabClicksNeeded = 5;

        if (MobileScript.isGooglePlayOut == false) { googlePlayBtn.SetActive(false); }
        else { googlePlayBtn.SetActive(true); }
        if (MobileScript.isAppStoreOut == false) { appStoreBtn.SetActive(false); }
        else { appStoreBtn.SetActive(true); }

        #region Resolution
        // Define a list of supported resolutions
        resolutions = new List<Resolution>
        {
            new Resolution { width = 800, height = 600 },
            new Resolution { width = 1024, height = 768 },
            new Resolution { width = 1280, height = 720 },
            new Resolution { width = 1280, height = 800 },
            new Resolution { width = 1280, height = 1024 },
            new Resolution { width = 1366, height = 768 },
            new Resolution { width = 1600, height = 900 },
            new Resolution { width = 1920, height = 1080 },
            new Resolution { width = 1920, height = 1200 },
            new Resolution { width = 2560, height = 1440 },
            new Resolution { width = 2560, height = 1600 },
            new Resolution { width = 2560, height = 1080 },
            new Resolution { width = 3440, height = 1440 },
            new Resolution { width = 3840, height = 1440 },
            new Resolution { width = 3840, height = 2160 },
            new Resolution { width = 3840, height = 2400 }
            // Add any other resolutions you want to support here
        };

        // Add the supported resolutions to the dropdown
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Count; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        resolutionDropdown.value = resolutionIndexSave;
        #endregion

        if (DemoScript.isDemo == false) 
        { 
            wishlistAnim.gameObject.SetActive(false);
            progressSavesOVerText.SetActive(false);
        }
        else
        {
            wishlistAnim.gameObject.SetActive(true);
            progressSavesOVerText.SetActive(true);
        }

        if(DemoScript.isDemo == false)
        {
            if(MobileScript.isThisGooglePlay == true || MobileScript.isThisAppStore == true)
            {
                gameSavedAnim.gameObject.transform.localPosition = new Vector2(0, -923f);
            }
            else
            {
                gameSavedAnim.gameObject.transform.localPosition = new Vector2(0, -368f);
            }

            if (MobileScript.isGooglePlayOut == false && MobileScript.isAppStoreOut == false)
            {
                if(MobileScript.isThisGooglePlay == false && MobileScript.isAppStoreOut == false)
                {
                    steamBtn.transform.localPosition = new Vector2(-446, -167);
                    discordBtn.transform.localPosition = new Vector2(-446, -292);
                    saveBtn.transform.localPosition = new Vector2(0, -187);
                    exitBtn.transform.localPosition = new Vector2(0, -303);
                    resetBtn.transform.localPosition = new Vector2(480, -239);

                    steamBtn.transform.localScale = new Vector2(1.1f, 1.1f);
                    discordBtn.transform.localScale = new Vector2(1.1f, 1.1f);
                    saveBtn.transform.localScale = new Vector2(1.13f, 1.13f);
                    exitBtn.transform.localScale = new Vector2(0.75f, 0.75f);
                    resetBtn.transform.localScale = new Vector2(0.74f, 0.74f);
                }
            }
            else if (MobileScript.isGooglePlayOut == true)
            {
                googlePlayBtn.transform.localPosition = new Vector2(446, -292);
                googlePlayBtn.transform.localScale = new Vector2(1.08f, 1.08f);
            }
            else if (MobileScript.isGooglePlayOut == true && MobileScript.isAppStoreOut == true) 
            {
                googlePlayBtn.transform.localPosition = new Vector2(446, -292);
                googlePlayBtn.transform.localScale = new Vector2(1.08f, 1.08f);

                appStoreBtn.transform.localPosition = new Vector2(446, -167);
                appStoreBtn.transform.localScale = new Vector2(1.08f, 1.08f);
            }

            if (MobileScript.isGooglePlayOut == true || MobileScript.isAppStoreOut == true)
            {
                steamBtn.transform.localPosition = new Vector2(-446, -167);
                discordBtn.transform.localPosition = new Vector2(-446, -292);
                saveBtn.transform.localPosition = new Vector2(0, -181);
                exitBtn.transform.localPosition = new Vector2(0, -297);
                resetBtn.transform.localPosition = new Vector2(455, -187);

                steamBtn.transform.localScale = new Vector2(1.1f, 1.1f);
                discordBtn.transform.localScale = new Vector2(1.1f, 1.1f);
                saveBtn.transform.localScale = new Vector2(1.13f, 1.13f);
                exitBtn.transform.localScale = new Vector2(0.75f, 0.75f);
                resetBtn.transform.localScale = new Vector2(0.74f, 0.74f);
            }
        }
        else
        {
            gameSavedAnim.gameObject.transform.localPosition = new Vector2(385f, -365f);
        }

        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        triggerResolution = false;
    }

    public bool pressEsc;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Prestige.isInPrestige == false)
            {
                if(pressEsc == false)
                {
                    if (settingsAnim.gameObject.activeInHierarchy == false)
                    {
                        if(isInAnyFrame == false)
                        {
                            timesClickedTabs += 1;
                            if (timesClickedTabs >= timesTabClicksNeeded) { mainCursorScript.CheckSaving(); timesClickedTabs = 0; }
                            WooshSound();
                            isInAnyFrame = true;
                            closeSettingsBtn.interactable = true; settingBtn.interactable = false;
                            isInSettings = true;
                            settingsAnim.gameObject.SetActive(true); settingsAnim.Play("SettingFadeIn"); if (DemoScript.isDemo == true) { wishlistAnim.Play(); }
                            FadeDark(true);
                            pressEsc = true;
                        }
                    }
                    else
                    {
                        WooshSound();
                        closeSettingsBtn.interactable = false; settingBtn.interactable = true;
                        isInSettings = false; settingsAnim.Play("SettingFadeOut");
                        FadeDark(false);
                        pressEsc = true;
                        isInAnyFrame = false;

                        if (waitCoroutine == null) { waitCoroutine = StartCoroutine(WaitForAnim(4)); }
                        else { StopCoroutine(waitCoroutine); waitCoroutine = null; }
                    }
                }
            }
        }
    }

    #region Open an close projectile upgrades

    public int frame;

    public Upgrades upgradeScript;

    public void CheckFrme(int frameClick)
    {
        frame = frameClick;
    }

    public void OpenAndCloseUpgrades(bool open)
    {
        WooshSound();
        if (open == true)
        {
            timesClickedTabs += 1;
            if(timesClickedTabs >= timesTabClicksNeeded) { mainCursorScript.CheckSaving(); timesClickedTabs = 0; }
            mainCursorScript.CheckSaving();
            if (waitCoroutine != null) { StopCoroutine(waitCoroutine); waitCoroutine = null; }

            FadeDark(true);
            isInAnyFrame = true;
            if (frame == 1) { isInUpgradeFrame = true;  }
            if (frame == 2) { isIntClickUpgradeFrame = true; }
            if (frame == 3) { isInPrestigeFrame = true; }
            if (frame == 4) { isInSettings = true; }
            if (frame == 5) { isInStats = true; }
            if (frame == 6) { isInAchievements = true; }
            if (frame == 7) { isInSkins = true; }

            if (frame == 1) { upgradeAnim.gameObject.SetActive(true); upgradeAnim.Play("UpgradeFadeIn"); }
            if (frame == 2) { clickUpgradeAnim.gameObject.SetActive(true); clickUpgradeAnim.Play("UpgradeFadeIn"); upgradeScript.ClickUpgradeTexts(); }
            if (frame == 3) { prestigeAnim.gameObject.SetActive(true); prestigeAnim.Play("UpgradeFadeIn"); }
            if (frame == 4) { settingsAnim.gameObject.SetActive(true); settingsAnim.Play("SettingFadeIn"); if (DemoScript.isDemo == true) { wishlistAnim.Play(); } }
            if (frame == 5) { statsAnim.gameObject.SetActive(true); statsAnim.Play("UpgradeFadeIn"); }
            if (frame == 6) { achivementsAnim.gameObject.SetActive(true); achivementsAnim.Play("UpgradeFadeIn"); }
            if (frame == 7) { skinsAnim.gameObject.SetActive(true); skinsAnim.Play("SettingFadeIn"); }

            if (frame == 1) { closeShopBtn.interactable = true; shopBtn.interactable = false; }
            if (frame == 2) { closeClickBtn.interactable = true; clickUpgradeBtn.interactable = false; }
            if (frame == 3) { closePrestigeBtn.interactable = true; prestigeBtn.interactable = false; }
            if (frame == 4) { closeSettingsBtn.interactable = true; settingBtn.interactable = false; }
            if (frame == 5) { closeStatsBtn.interactable = true; statBtn.interactable = false; }
            if (frame == 6) { closeAchBtn.interactable = true; achBtn.interactable = false; }
            if (frame == 7) { closeSkinsBtn.interactable = true; skinsBtn.interactable = false; }
        }
        else
        {
            FadeDark(false);
            if (frame == 1) { isInUpgradeFrame = false; upgradeAnim.Play("UpgradeFadeOut"); }
            if (frame == 2) { isIntClickUpgradeFrame = false; clickUpgradeAnim.Play("UpgradeFadeOut"); }
            if (frame == 3) { isInPrestigeFrame = false; prestigeAnim.Play("UpgradeFadeOut"); }
            if (frame == 4) { isInSettings = false; settingsAnim.Play("SettingFadeOut"); }
            if (frame == 5) { isInStats = false; statsAnim.Play("UpgradeFadeOut"); }
            if (frame == 6) { isInAchievements = false; achivementsAnim.Play("UpgradeFadeOut"); }
            if (frame == 7) { isInSkins = false; skinsAnim.Play("SettingFadeOut"); }

            if (frame == 1) { closeShopBtn.interactable = false; shopBtn.interactable = true; }
            if (frame == 2) { closeClickBtn.interactable = false; clickUpgradeBtn.interactable = true; }
            if (frame == 3) { closePrestigeBtn.interactable = false; prestigeBtn.interactable = true; }
            if (frame == 4) { closeSettingsBtn.interactable = false; settingBtn.interactable = true; }
            if (frame == 5) { closeStatsBtn.interactable = false; statBtn.interactable = true; }
            if (frame == 6) { closeAchBtn.interactable = false; achBtn.interactable = true; }
            if (frame == 7) { closeSkinsBtn.interactable = false; skinsBtn.interactable = true; }

            if (waitCoroutine == null) { waitCoroutine = StartCoroutine(WaitForAnim(frame)); }
            else { StopCoroutine(waitCoroutine); waitCoroutine = null; }
        }
    }
    public Coroutine waitCoroutine;

    IEnumerator WaitForAnim(int frame)
    {
        yield return new WaitForSeconds(0.24f);
        if(frame == 1) { upgradeAnim.gameObject.SetActive(false); }
        if (frame == 2) { clickUpgradeAnim.gameObject.SetActive(false); }
        if (frame == 3) { prestigeAnim.gameObject.SetActive(false); }
        if (frame == 4) { settingsAnim.gameObject.SetActive(false); }
        if (frame == 5) { statsAnim.gameObject.SetActive(false); }
        if (frame == 6) { achivementsAnim.gameObject.SetActive(false); }
        if (frame == 6) { skinsAnim.gameObject.SetActive(false); }

        isInAnyFrame = false;
        waitCoroutine = null;
    }

    public Animation dark;
    public void FadeDark(bool fadeIn)
    {
        if(fadeIn == true) { dark.gameObject.SetActive(true); dark.Play("FadeIn"); StartCoroutine(Waiting()); }
        else { StartCoroutine(SetDarkOff()); dark.Play("FadeOut"); }
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(0.24f);
        pressEsc = false;
    }

    IEnumerator SetDarkOff()
    {
        yield return new WaitForSeconds(0.24f);
        dark.gameObject.SetActive(false);
        pressEsc = false;
    }
    #endregion

    #region resolution 
    public int resolutionIndexSave;
    public bool triggerResolution;
    public int saveHeight, saveWidth;
    public int saveFullsScreen;

    public void SetResolution(int resolutionIndex)
    {
        if (triggerResolution == false)
        {
            Resolution resolution = resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

            saveWidth = resolution.width;
            saveHeight = resolution.height;

            PlayerPrefs.SetInt("ScreenWidth", saveWidth);
            PlayerPrefs.SetInt("ScreenHeight", saveHeight);

            resolutionIndexSave = resolutionIndex;
            PlayerPrefs.SetInt("saveIndex", resolutionIndexSave);
        }
    }

    public GameObject fullSreenToggleOff, fullScreenToggleOn;

    public void SetFullSCreen()
    {
        audioManager.Play("UI_Click1");
        if (saveFullsScreen == 0)
        {
            fullScreenToggleOn.SetActive(false);
            fullSreenToggleOff.SetActive(true);
            Screen.fullScreenMode = FullScreenMode.Windowed;

            saveFullsScreen = 1;
            PlayerPrefs.SetInt("SaveFullScreen", saveFullsScreen);

        }
        else
        {
            fullScreenToggleOn.SetActive(true);
            fullSreenToggleOff.SetActive(false);
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;

            saveFullsScreen = 0;
            PlayerPrefs.SetInt("SaveFullScreen", saveFullsScreen);
        }
    }

    public void FindResolutionIndex()
    {
        if (Screen.width == 600 && Screen.height == 800) { resolutionIndexSave = 0; }
        if (Screen.width == 1024 && Screen.height == 768) { resolutionIndexSave = 1; }
        if (Screen.width == 1280 && Screen.height == 720) { resolutionIndexSave = 2; }
        if (Screen.width == 1280 && Screen.height == 800) { resolutionIndexSave = 3; }
        if (Screen.width == 1280 && Screen.height == 1024) { resolutionIndexSave = 4; }
        if (Screen.width == 1366 && Screen.height == 768) { resolutionIndexSave = 5; }
        if (Screen.width == 1600 && Screen.height == 900) { resolutionIndexSave = 6; }
        if (Screen.width == 1920 && Screen.height == 1080) { resolutionIndexSave = 7; }
        if (Screen.width == 1920 && Screen.height == 1200) { resolutionIndexSave = 8; }
        if (Screen.width == 2560 && Screen.height == 1440) { resolutionIndexSave = 9; }
        if (Screen.width == 2560 && Screen.height == 1600) { resolutionIndexSave = 10; }
        if (Screen.width == 2560 && Screen.height == 1080) { resolutionIndexSave = 11; }
        if (Screen.width == 3440 && Screen.height == 1440) { resolutionIndexSave = 12; }
        if (Screen.width == 3840 && Screen.height == 1440) { resolutionIndexSave = 13; }
        if (Screen.width == 3840 && Screen.height == 2160) { resolutionIndexSave = 14; }
        if (Screen.width == 3840 && Screen.height == 2400) { resolutionIndexSave = 15; }
    }
    #endregion

    #region Particle toggle
    public GameObject particleToggleOff, particleToggleOn;
    public static int particleOn;

    public void SetPArticleToggle()
    {
        audioManager.Play("UI_Click1");
        if (particleOn == 0)
        {
            particleToggleOn.SetActive(false);
            particleToggleOff.SetActive(true);
            particleOn = 1;
            PlayerPrefs.SetInt("particleOnSave", particleOn);
        }
        else
        {
            particleOn = 0;
            PlayerPrefs.SetInt("particleOnSave", particleOn);
            particleToggleOn.SetActive(true);
            particleToggleOff.SetActive(false);
        }
    }
    #endregion

    #region Audio and pop up sliders
    public static float mainChance, fallingChance;
    public Slider mainSlider, fallingSlider;
    public TextMeshProUGUI mainPercent, fallingPercent;

    public void SliderTextMain()
    {
        mainChance = mainSlider.value;
        mainPercent.text = $"{(mainChance * 100).ToString("F0")}%";
        PlayerPrefs.SetFloat("saveMainChance", mainChance);
    }

    public void SliderTextFalling()
    {
        fallingChance = fallingSlider.value;
        fallingPercent.text = $"{(fallingChance * 100).ToString("F0")}%";
        PlayerPrefs.SetFloat("saveFallingChance", fallingChance);
    }
    #endregion

    public void OpenCCCGooglePlay()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.EagleEyeGames.ClickerClickerClicker");
    }

    public void Steam()
    {
        Application.OpenURL("https://store.steampowered.com/app/3187730/Clicker_Clicker_Clicker/");
    }

    public void OpenCCCAppStore()
    {
        Application.OpenURL("");
    }

    public void SteamAllGames()
    {
        Application.OpenURL("https://store.steampowered.com/curator/43674917");
    }

    public void Discord()
    {
        Application.OpenURL("https://discord.gg/qrBGyWkCgJ");
    }

    #region Save, quit and reset btn
    public void QuitGame()
    {
        Application.Quit();
    }

    public Animation gameSavedAnim;
    public Coroutine gameSavedCoroutine;
    

    public void SaveGame()
    {
        gameSavedAnim.gameObject.SetActive(true);
        gameSavedAnim.Play();

        if(gameSavedCoroutine == null) { gameSavedCoroutine = StartCoroutine(GameSavedCoroutine()); audioManager.Play("Saved"); } 
    }

    IEnumerator GameSavedCoroutine()
    {
        yield return new WaitForSeconds(0.85f);
        gameSavedAnim.gameObject.SetActive(false);
        gameSavedCoroutine = null;
    }

    public GameObject resetFrame;
    public void ResetGame()
    {
        resetFrame.SetActive(true);
        audioManager.Play("UI_Click1");
    }
    public void ResetNO()
    {
        resetFrame.SetActive(false);
        audioManager.Play("UI_Click1");
    }
    public void ResetYES()
    {
        resetFrame.SetActive(false);
        StartCoroutine(ResetCircle());
        audioManager.Play("UI_Click1");
    }

    public Image resetCirlce;
    IEnumerator ResetCircle()
    {
        audioManager.Play("Transition1");

        resetCirlce.fillAmount = 0;
        resetCirlce.gameObject.SetActive(true);
        float duration = 0.5f; // Total duration of the transition
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            resetCirlce.fillAmount = Mathf.Lerp(0f, 1f, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }
        resetCirlce.fillAmount = 1f;

        yield return new WaitForSeconds(0.1f);

        AllResetStuff();

        closeSettingsBtn.interactable = false; settingBtn.interactable = true;
        isInSettings = false; settingsAnim.Play("SettingFadeOut"); settingsAnim.gameObject.SetActive(false);
        pressEsc = true;
        isInAnyFrame = false;
        FadeDark(false);

        yield return new WaitForSeconds(0.5f);
        audioManager.Play("Transition1");
        AllResetStuff();

        elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            resetCirlce.fillAmount = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }
        resetCirlce.fillAmount = 0;
        resetCirlce.gameObject.SetActive(false);
    }
    #endregion

    public MainCursorClick mainCursorScript;
    public Stats statsScript;
    public GoldenFistMechanics fistScript;
    public Prestige prestigeScript;
    public Achievements achScript;
    public SkinScript skinsScript;
    public LevelUp levelScript;

    public void AllResetStuff()
    {
        mainCursorScript.ResetMain();
        statsScript.ResetStats();
        upgradeScript.ResetUpgrades();
        fistScript.ResetFirst();
        prestigeScript.ResetPrestige();
        skinsScript.ResetSkins();
        levelScript.ResetLevelUp();

        SetProjectilesInactive();
        MainCursorClick.currentGold = 1000;

        achScript.ResetAch();
    }

    public void SetProjectilesInactive()
    {
        GameObject[] Cursors = GameObject.FindGameObjectsWithTag("FallingCursor");
        foreach (GameObject cursor in Cursors)
        {
            if (cursor.activeSelf)
            {
                ObjectPool.instance.ReturnFallingCursorFromPool(cursor);
            }
        }

        GameObject[] Boulders = GameObject.FindGameObjectsWithTag("Boulder");
        foreach (GameObject boulder in Boulders)
        {
            if (boulder.activeSelf)
            {
                ObjectPool.instance.ReturnBoulderFromPool(boulder);
            }
        }

        GameObject[] Spikes = GameObject.FindGameObjectsWithTag("Spike");
        foreach (GameObject spike in Spikes)
        {
            if (spike.activeSelf)
            {
                ObjectPool.instance.ReturnSpikeFromPool(spike);
            }
        }

        GameObject[] laser = GameObject.FindGameObjectsWithTag("Laser");
        foreach (GameObject lasers in laser)
        {
            if (lasers.activeSelf)
            {
                ObjectPool.instance.ReturnLaserFromPool(lasers);
            }
        }

        GameObject[] spikeBall = GameObject.FindGameObjectsWithTag("SpikeBall");
        foreach (GameObject spikeBalls in spikeBall)
        {
            if (spikeBalls.activeSelf)
            {
                ObjectPool.instance.ReturnSpikeCircleFromPool(spikeBalls);
            }
        }

        GameObject[] boomerang = GameObject.FindGameObjectsWithTag("Boomerang");
        foreach (GameObject boomerangs in boomerang)
        {
            if (boomerangs.activeSelf)
            {
                ObjectPool.instance.ReturnBoomerangFromPool(boomerangs);
            }
        }
    }

    public void GiveGold()
    {
        MainCursorClick.totalClickPoints += 100000000000000000000000000000000000000f;
    }

    public double giveGold = 1000;
    public TextMeshProUGUI giveGoldText;

    public void WooshSound()
    {
        int randomSound = Random.Range(1,4);
        if(randomSound == 1) { audioManager.Play("Swoosh1"); }
        if (randomSound == 2) { audioManager.Play("Swoosh2"); }
        if (randomSound == 3) { audioManager.Play("Swoosh3"); }
    }

    #region Testign btns
    public void IncreaseGold()
    {
        giveGold *= 10;
        giveGoldText.text = ScaleNumbers.FormatPoints(giveGold) + "+gold";
    }

    public void GiveIncreaedGold()
    {
        MainCursorClick.totalClickPoints += giveGold;
    }

    public void ResetPlayerprefs()
    {
        PlayerPrefs.DeleteAll();
    }

    public void RemoveGold()
    {
        MainCursorClick.totalClickPoints = 0;
    }

    public GoldenFistMechanics goildenFistScript;
    public void SpawnFist()
    {
        goildenFistScript.SpawnFist();
    }

    public static bool spawnBonanza;
    public void SpawnFistBonanza()
    {
        spawnBonanza = true;
        goildenFistScript.SpawnFist();
    }

    public void SpecificStuff()
    {
        Prestige.clickscensionCoins += 100000;
        Stats.totalClicks += 500000;
        Stats.totalCursorClicks += 750000;
        Stats.totalAutoClicks += 25000;
        Stats.totalFallingCursorClciks += 1500000;
        Stats.totalGoldenFistClicks += 50;
        Stats.totalBonanzas += 5;
        Stats.totalDiamond += 500;
        Stats.totalEmerald += 500;
        Stats.totalRainbow += 500;
        Stats.totalPurple += 500;
        Stats.totalKnifes += 50000;
        Stats.totalBoulders += 60000;
        Stats.totalSpikes += 50000;
        Stats.totalShurikens += 50000;
        Stats.ballBounced += 50000;
        Stats.totalBoomerangs += 10000;
        Stats.totalSpear += 10000;
        Stats.totalLasers += 10000;
        Stats.bigBallBounced += 10000;
        Stats.totalArrows += 100000;
        Stats.totalSpikeballs += 100000;
        Stats.totalBullets += 100000;
        Stats.totalProjectiles += 100000000;
        Stats.totalCritClciks += 10000000;
    }

    public void PlussClickscension()
    {
        Prestige.clickscensionCoins += 2000;
    }

    public void ClickGoldPluss()
    {
        MainCursorClick.totalClickPoints += (MainCursorClick.cursorClickPoint * (1 + Prestige.activeGoldIncrease)) * 500;
    }
    #endregion
}
