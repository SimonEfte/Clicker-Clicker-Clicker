using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingCurosrs : MonoBehaviour
{
    public static bool fallCursor;
    public static int totalFallingCursorsOnScreen;
    public static float fallDownWait;

    public void Start()
    {
        clicksNeeded = 6;

        fallDownWait = 1f;

        fallCursor = true;
        //FallCursor();

        if(Upgrades.passiveUpgradeCount > 0) { FallCursorPassive(); }
    }

    private void Update()
    {
        if(Upgrades.passiveUpgradeCount == 0)
        {
            fallDownWait = 1;
        }
        else if(Upgrades.passiveUpgradeCount < 170 && Upgrades.passiveUpgradeCount > 0)
        {
            if (MobileScript.isMobile == true) { fallDownWait = 5f / Upgrades.passiveUpgradeCount; }
            else { fallDownWait = 6.25f / Upgrades.passiveUpgradeCount; }
        }
        else if (Upgrades.passiveUpgradeCount >= 170)
        {
            if (MobileScript.isMobile == true) { fallDownWait = 5f / Upgrades.passiveUpgradeCount; }
            else { fallDownWait = 6.25f / Upgrades.passiveUpgradeCount; }
        }
        else { }
    }

    public int clicksNeeded, clicks;
    public void FallCursorActive()
    {
        if(MobileScript.isMobile == true) { clicksNeeded = 4; }

        clicks += 1;
        if(clicks >= clicksNeeded) { SpawnCursor(); clicks = 0;  }
    }

    public void FallCursorPassive()
    {
        if (fallCursor == true)
        {
            if (Upgrades.passiveUpgradeCount > 0) { SpawnCursor(); }
            StartCoroutine(WaitForNextFall());
            fallCursor = false;
        }
    }

    public GameObject spawnObject;

    public void SpawnCursor()
    {
        if(MobileScript.isMobile == false)
        {
            GameObject goldObject = ObjectPool.instance.GetFallingCursorFromPool();

            int random = Random.Range(-920, 920);
            goldObject.transform.localPosition = new Vector2(random, 0);
        }
        else
        {
            GameObject goldObject = ObjectPool.instance.GetFallingCursorFromPool();

            int random = Random.Range(-525, 525);
            goldObject.transform.localPosition = new Vector2(random, 630);
        }

        totalFallingCursorsOnScreen += 1;
        Stats.totalFallingCursors += 1;
    }

    IEnumerator WaitForNextFall()
    {
        yield return new WaitForSeconds(fallDownWait);
        fallCursor = true;
        FallCursorPassive();
    }
}
