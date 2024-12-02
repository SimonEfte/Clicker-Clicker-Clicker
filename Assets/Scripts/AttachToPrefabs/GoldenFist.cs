using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenFist : MonoBehaviour
{
    public GoldenFistMechanics goldenFistScript;
    public GameObject glow;
    public AudioManager audioManager;

    public void OnEnable()
    {
        deSpawnCoroutine = StartCoroutine(DeSpawn());
    }

    private void OnDisable()
    {
        StopCoroutine(deSpawnCoroutine); deSpawnCoroutine = null;
    }

    public Coroutine deSpawnCoroutine;
    public static bool fistDespawned;

    IEnumerator DeSpawn()
    {  
        yield return new WaitForSeconds(52);
        glow.SetActive(false);
        fistDespawned = true;
        gameObject.SetActive(false);
    }

    public static bool clickerFist;
    public static Vector2 fistPos;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 8 || collision.gameObject.layer == 12 || collision.gameObject.layer == 13 || collision.gameObject.layer == 14 || collision.gameObject.layer == 15 || collision.gameObject.layer == 16)
        {
            Stats.totalGoldenFistClicks += 1;
            RandomGoldenFistReward();
            clickerFist = true;
            fistPos = gameObject.transform.localPosition;
            gameObject.SetActive(false);
            audioManager.Play("Fist");
        }
    }

    public static bool hitJustGold, hitJustPrestigePoints, hitPassiveGold, hitActiveGold, hitFallingCursors, hitProjectileBonanza;

    public void RandomGoldenFistReward()
    {
        SetAllFalse();

        if (DemoScript.isDemo == true)
        {
            int randomDemo = Random.Range(1,4);
            if (randomDemo == 1) { hitJustGold = true; }
            if (randomDemo == 2) { hitPassiveGold = true; }
            if (randomDemo == 3) { hitActiveGold = true; }
        }
        else
        {
            int random = Random.Range(1, 12);

            if (SettingsAndUI.spawnBonanza == true) { random = 10; SettingsAndUI.spawnBonanza = false; }
            if(random == 1 || random == 2) { hitJustGold = true; }
            if(random == 3) { hitJustPrestigePoints = true; }
            if(random == 4 || random == 5) { hitPassiveGold = true; }
            if(random == 6 || random == 7) { hitActiveGold = true; }
            if(random == 8 || random == 9) { hitFallingCursors = true; }
            if(random > 9) 
            { 
                if(Upgrades.isKnifePurchased == false) { hitPassiveGold = true; }
                else { hitProjectileBonanza = true; }
            }
        }
    }

    public void SetAllFalse()
    {
        hitJustGold = false;
        hitJustPrestigePoints = false;
        hitPassiveGold = false;
        hitActiveGold = false;
        hitFallingCursors = false;
        hitProjectileBonanza = false;
    }
}
