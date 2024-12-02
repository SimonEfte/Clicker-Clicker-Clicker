using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinScript : MonoBehaviour, IDataPersistence
{
    public static bool isSkinsDLC;
    public GridLayoutGroup grid;

    void Start()
    {
        ChangeSkinStuff();

        SelectCursor(currentlySelected);
        StartCoroutine(PlayAudio());

        for (int i = 0; i < cursorUnlocked.Length; i++)
        {
            if(cursorUnlocked[i] == true) { UnlockSkin(i); }
        }
    }

    bool playAudio;
    IEnumerator PlayAudio()
    {
        yield return new WaitForSeconds(1);
        playAudio = true;
    }

    public void ChangeSkinStuff()
    {
        if (isSkinsDLC == true)
        {
            for (int i = 0; i < skinsButtons.Length; i++)
            {
                if(i > 11)
                {
                    skinsButtons[i].gameObject.SetActive(true);
                }
            }

            grid.padding.left = 5;
            grid.padding.right = 0;
            grid.padding.top = 10;
            grid.padding.bottom = 0;

            grid.spacing = new Vector2(0f, 27.91f);
        }
        else
        {
            grid.padding.left = 35;
            grid.padding.right = -20;
            grid.padding.top = 60;
            grid.padding.bottom = 0;

            grid.spacing = new Vector2(29f, 66.6f);
        }
     
    }

    public GameObject[] lockedSkin;
    public static bool[] cursorUnlocked = new bool[11];

    public Animation skinExclAnim;
    public void ExclAnim()
    {
        skinExclAnim.gameObject.SetActive(true);
        skinExclAnim.Play();
    }

    public void StopExlAnim()//
    {
        skinExclAnim.gameObject.SetActive(false);
    }

    public GameObject[] cursorEXCL;

    public void UnlockSkin(int skinNumber)
    {
        if(playAudio == true)
        {
            ExclAnim();
            cursorEXCL[skinNumber].SetActive(true);
        }

        cursorUnlocked[skinNumber] = true;
        lockedSkin[skinNumber].SetActive(false);
        skinsButtons[skinNumber + 1].GetComponent<Button>().interactable = true;
    }

    public GameObject selectedCursor;
    public Button[] skinsButtons;

    public Sprite[] sprites;
    public Image mainCursor;
    public AudioManager audioManager;
    public static int currentlySelected;

    public void SelectCursor(int cursorNumber)
    {
        if(playAudio == true) { audioManager.Play("UI_Click1"); }
        selectedCursor.transform.SetParent(skinsButtons[cursorNumber].transform);
        selectedCursor.transform.localPosition = new Vector2(0,0);
        currentlySelected = cursorNumber;

        if(cursorNumber != 0)
        {
            if(cursorNumber < 12)
            {
                if (cursorEXCL[cursorNumber - 1].activeInHierarchy == true) { cursorEXCL[cursorNumber - 1].SetActive(false); }
            }
        }

        mainCursor.sprite = sprites[cursorNumber];
    }

    public void ResetSkins()
    {
        currentlySelected = 0;

        for (int i = 0; i < cursorUnlocked.Length; i++)
        {
            cursorUnlocked[i] = false;
        }

        for (int i = 0; i < lockedSkin.Length; i++)
        {
            lockedSkin[i].SetActive(true);
        }

        selectedCursor.transform.SetParent(skinsButtons[currentlySelected].transform);
        selectedCursor.transform.localPosition = new Vector2(0, 0);
        mainCursor.sprite = sprites[currentlySelected];

        for (int i = 0; i < skinsButtons.Length; i++)
        {
            if(i < 12) { skinsButtons[i].interactable = false; }
        }

        skinsButtons[0].interactable = true;
    }

    #region Load Data
    public void LoadData(GameData data)
    {
        currentlySelected = data.currentlySelected;
        for (int i = 0; i < cursorUnlocked.Length; i++)
        {
            cursorUnlocked[i] = data.cursorUnlocked[i];
        }
    }
    #endregion

    #region Save Data
    public void SaveData(ref GameData data)
    {
        data.currentlySelected = currentlySelected;
        for (int i = 0; i < cursorUnlocked.Length; i++)
        {
            data.cursorUnlocked[i] = cursorUnlocked[i];
        }
    }
    #endregion
}
