using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioOptionsManager : MonoBehaviour
{
    public static float soundEffectolume { get; private set; }
    public static float musicVolume { get; private set; }

    public Slider audioSlider;
    public Slider musicSlider;

    public void Start()
    {
        if (!PlayerPrefs.HasKey("saveAudio"))
        { 
            if (MobileScript.isMobile == false) { audioSlider.value = 0.5f; }
            else { audioSlider.value = 1f; }
        }
        else { audioSlider.value = PlayerPrefs.GetFloat("saveAudio"); }

        if (!PlayerPrefs.HasKey("saveMusic")) 
        { 
            if(MobileScript.isMobile == false) { musicSlider.value = 0.5f; }
            else { musicSlider.value = 1; }
        }
        else { musicSlider.value = PlayerPrefs.GetFloat("saveMusic"); }

        musicValue = musicSlider.value;
        audioValue = audioSlider.value;

        if (audioSlider.value == 0.0001f) { soundEffectolume = -80; }
        if (musicSlider.value == 0.0001f) { musicVolume = -80; }
    }

    public float musicValue, audioValue;

    public void OnAudioSliderValueChange(float value)
    {
        soundEffectolume = value;
        PlayerPrefs.SetFloat("saveAudio", audioSlider.value);
        AudioManager.Instance.UpdateMixerVolume();
        audioValue = audioSlider.value;
    }

    public void OnMusicSliderValueChance(float value)
    {
        musicVolume = value;
        PlayerPrefs.SetFloat("saveMusic", musicSlider.value);
        AudioManager.Instance.UpdateMixerVolume();
        musicValue = musicSlider.value;
    }

    public static bool altTabbed;

    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            UnmuteAudio();
        }
        else
        {
            altTabbed = true;
            MuteAudio();
        }
    }


    void MuteAudio()
    {
        soundEffectolume = 0.0001f;
        AudioManager.Instance.UpdateMixerVolume();

        musicVolume = 0.0001f;
        AudioManager.Instance.UpdateMixerVolume();
    }

    void UnmuteAudio()
    {
        soundEffectolume = audioValue;
        AudioManager.Instance.UpdateMixerVolume();

        musicVolume = musicValue;
        AudioManager.Instance.UpdateMixerVolume();
    }
}
