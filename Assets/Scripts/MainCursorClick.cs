using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainCursorClick : MonoBehaviour, IDataPersistence
{
    public GameObject mainCursor;
    public float cursorStartSize;
    public static double totalClickPoints;
    public static double cursorClickPoint, totalPassivePoints;
    public AudioManager audioManager;
    public OverlappingSounds overlappingScript;
    public Achievements achScript;


    public void Awake()
    {
        FallingCursorDown.totalHitCursors = 500;
        totalPassivePoints = 0;
        cursorClickPoint = 1;
        cursorStartSize = 5.4f;

        number = 0;
    }

    int number;
    public static double currentGold;
    public bool startCheckingSaves;
    public float currentGoldIncrement;

    private void Start()
    {
        if(MobileScript.isMobile == true) { cursorStartSize = 7.6f; }

        currentGoldIncrement = 1.75f;
        passiveCoroutine = StartCoroutine(PassiveClickGold());

        currentGold = totalClickPoints;
        if(currentGold == 0) { currentGold = 100; }
        currentGold += (currentGold * currentGoldIncrement);
        startCheckingSaves = true;
    }

    public Coroutine passiveCoroutine;

    IEnumerator PassiveClickGold()
    {
        while (number != 5)
        {
            yield return new WaitForSeconds(0.01f);

            double prestigePassive = 1 + Prestige.passiveGoldIncrease;
            double totalGold = totalPassivePoints * prestigePassive;

            double gold = ((totalGold * (1 + GoldenFistMechanics.passiveGoldBonusIncrease)) / 100f);

            totalClickPoints += gold;
            LevelUp.currentPrestigeCoins += gold;
            Stats.totalGoldPassive += gold;
            Stats.totalGold += gold;

            achScript.CheckAchievementsProgress(1);
        }
    }

    #region Update
    public GameObject clickObject, clickObject2, clickObject3, clickObject4, clickObject5, clickObject6, aouCursor;

    public TextMeshProUGUI totalPointsText, totalPointsClickUpgrade, totalPointsProjectile;

    public static bool isAlotOfCursors;
    public bool startedCoroutine, reachedGold;

    IEnumerator SetSave()
    {
        yield return new WaitForSeconds(5);
        didSave = false; startedCoroutine = false;
    }

    public void Update()
    {
        if (didSave == true && startedCoroutine == false)
        {
            startedCoroutine = true;
            StartCoroutine(SetSave());
        }

        if(isAlotOfCursors == true)
        {
            isAlotOfCursors = false;
            SaveAgain();
        }

        if(startCheckingSaves == true)
        {
            if (totalClickPoints > currentGold)
            {
                currentGold += (currentGold * currentGoldIncrement);
                CheckSaving(); 
            }
        }
     
        if(totalClickPoints < 100)
        {
            totalPointsText.text = totalClickPoints.ToString("F1");
            totalPointsProjectile.text = totalClickPoints.ToString("F1");

            if(DemoScript.isDemo == false) { totalPointsClickUpgrade.text = totalClickPoints.ToString("F1"); }
        }
        else
        {
            totalPointsText.text = ScaleNumbers.FormatPoints(totalClickPoints);
            totalPointsProjectile.text = ScaleNumbers.FormatPoints(totalClickPoints);

            if (DemoScript.isDemo == false) { totalPointsClickUpgrade.text = ScaleNumbers.FormatPoints(totalClickPoints); }
        }

        //Change this for mobile?
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Stats.totalClicks += 1;
            achScript.CheckAchievementsProgress(10);
        }

        if (SettingsAndUI.isInAnyFrame == false)
        {
            Vector3 worldPosition = new Vector3(0,0,0);

            if (MobileScript.isMobile == false)
            {
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = Camera.main.nearClipPlane; // Set this to the distance from the camera to the object.
                worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            }
            else
            {
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);

                    Vector3 touchPosition = touch.position;
                    touchPosition.z = Camera.main.nearClipPlane;
                    worldPosition = Camera.main.ScreenToWorldPoint(touchPosition);
                }
            }

            clickObject.transform.position = worldPosition;
            clickObject2.transform.position = worldPosition;
            clickObject3.transform.position = worldPosition;
            clickObject4.transform.position = worldPosition;
            clickObject5.transform.position = worldPosition;
            clickObject6.transform.position = worldPosition;

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                int randomLayer = Random.Range(0,6);
                if(randomLayer == 0) { clickObject.SetActive(true);  }
                else if (randomLayer == 1) { clickObject2.SetActive(true); }
                else if (randomLayer == 2) { clickObject3.SetActive(true); }
                else if (randomLayer == 3) { clickObject4.SetActive(true); }
                else if (randomLayer == 4) { clickObject5.SetActive(true); }
                else if (randomLayer == 5) { clickObject6.SetActive(true); }

                if (coroutineClickOBject != null) { StopCoroutine(coroutineClickOBject); coroutineClickOBject = null; }
                coroutineClickOBject = StartCoroutine(ClickObjectOff());

                if (Upgrades.isAOEclicksPurchased == true)
                {
                    GameObject aoe = ObjectPool.instance.GetAOEfromPool();
                    Vector3 aoePosition = aoe.transform.position;
                    aoePosition.z = 0f;  // Set the Z position to the desired value, like 0
                    aoe.transform.position = new Vector3(worldPosition.x, worldPosition.y, aoePosition.z);
                }
            }
        }
    }
    #endregion

    #region Saving at intervals
    public DataPersistenceManager dataPersistanceScript;
    public bool isSaved, isSavedOther;
    public static bool didSave;

    public void CheckSaving()
    {
        if(isSaved == false) 
        {
            if(didSave == false) { dataPersistanceScript.SaveGame(); }

            isSaved = true; 
            StartCoroutine(WaitToSaveAgain());
        }
    }

    IEnumerator WaitToSaveAgain()
    {
        yield return new WaitForSeconds(6);
        isSaved = false;
    }

    public void SaveAgain()
    {
        if(isSavedOther == false)
        {
            if (didSave == false) { dataPersistanceScript.SaveGame(); }

            isSavedOther = true;
            StartCoroutine(WaitToSaveAgainForOther());
        }
    }

    IEnumerator WaitToSaveAgainForOther()
    {
        yield return new WaitForSeconds(6);
        isSavedOther = false;
    }
    #endregion

    #region Click Main Cursor
    public Upgrades upgradeScript;
    public FallingCurosrs fallingCursorScript;

    public void ClickCursor()
    {
        overlappingScript.PlaySound(2);
        double points = 0;
        bool hitCrit = false;
        float randomCrit = Random.Range(0, 99);
        if(randomCrit < Upgrades.critChance)
        {
            hitCrit = true;
            Stats.totalCritClciks += 1;
        }

        double prestigeIncrease = (Prestige.activeGoldIncrease + 1);
        double cursorPlussPrestige = cursorClickPoint * prestigeIncrease;

        if (hitCrit == false) { points = cursorPlussPrestige * (1 + GoldenFistMechanics.activeGoldBonusIncrease); }
        else { points = (cursorPlussPrestige * (Upgrades.critIncrease + 1)) * (1 + GoldenFistMechanics.activeGoldBonusIncrease); }

        if (hitCrit == false) { Stats.totalGoldActive += points; }
        else 
        {
            achScript.CheckAchievementsProgress(40);
            Stats.totalGoldCrit += points - cursorPlussPrestige;

            Stats.totalGoldActive += cursorPlussPrestige;
        }

        Stats.totalGold += points;
        totalClickPoints += points;
        LevelUp.currentPrestigeCoins += points;
      
        Stats.totalCursorClicks += 1;

        float random = Random.Range(1f, 2f);
        float randomRotation = Random.Range(-15f, 15f);

        float randomText = Random.Range(0f,1f);
        if(randomText < SettingsAndUI.mainChance) 
        { 
            StartCoroutine(TextPopUpAnim(points, hitCrit));
        }
       
        fallingCursorScript.FallCursorActive();

        if(GoldenFistMechanics.isKnifeBonanza == false)
        {
            if (Upgrades.isKnifePurchased == true) { upgradeScript.ShootKnife(); }
        }
        if (GoldenFistMechanics.isShurikenBonanza == false)
        {
            if (Upgrades.isShurikenPurchased == true) { upgradeScript.ShootShuriken(); }
        }
        if (GoldenFistMechanics.isBoomerangBonanza == false)
        {
            if (Upgrades.isBoomerangPurchased == true) { upgradeScript.ShootBoomerang(); }
        }
        if (GoldenFistMechanics.isSpearBonanza == false)
        {
            if (Upgrades.isSpearPurchased == true) { upgradeScript.ShootSpear(); }
        }
        if (GoldenFistMechanics.isArrowBonanza == false)
        {
            if (Upgrades.isArrowsPurchased == true) { upgradeScript.ShootArrows(); }
        }

     

        if (moveCursorDownCoroutine != null) 
        { 
            StopCoroutine(moveCursorDownCoroutine); 
            moveCursorDownCoroutine = null;
            moveCursorDownCoroutine = StartCoroutine(MoveCursorDown(random, randomRotation));
        }
        else { moveCursorDownCoroutine = StartCoroutine(MoveCursorDown(random, randomRotation)); }
    }
    #endregion

    #region Points pop up
    IEnumerator TextPopUpAnim(double points, bool isCrit)
    {
        TextMeshProUGUI pointText = ObjectPool.instance.GetTextPopUpFromPool();
        pointText.color = new Color(pointText.color.r, pointText.color.g, pointText.color.b, Mathf.Lerp(1f, 1f, 1f));

        Transform childTransform = pointText.transform.Find("CursorIcon");

        if(points < 100)
        {
            if (isCrit == false) { pointText.text = "+" + points.ToString("F1"); }
            else { pointText.text = LocalizationStrings.crit + "\n+" + points.ToString("F1"); }
        }
        else
        {
            if (isCrit == false) { pointText.text = "+" + ScaleNumbers.FormatPoints(points); }
            else { pointText.text = LocalizationStrings.crit + "\n+" + ScaleNumbers.FormatPoints(points); }
        }
       

        float preferredWidth = pointText.GetPreferredValues().x;
        pointText.rectTransform.sizeDelta = new Vector2(preferredWidth, pointText.rectTransform.sizeDelta.y);

        pointText.transform.localScale = new Vector3(0f, 0f, 0f);

        float maxOffset;
        if(MobileScript.isMobile == true) { maxOffset = 1.4f; }
        else { maxOffset = 1.85f; }

        Vector3 buttonPosition = mainCursor.transform.position;

        // Randomly offset the pointText position
        float xOffset = Random.Range(-maxOffset, maxOffset);
        float yOffset = Random.Range(-maxOffset, maxOffset);

        // Calculate the new position for pointText
        Vector3 newPosition = new Vector3(
            buttonPosition.x + xOffset,
            buttonPosition.y + yOffset,
            buttonPosition.z
        );

        pointText.transform.position = newPosition;


        // Start scaling up
        float scaleStartTime = Time.time;
        float scaleEndTime = scaleStartTime + 0.15f;

        float randomTexTSize = Random.Range(0.3f, 0.65f);

        while (Time.time < scaleEndTime)
        {
            float t = (Time.time - scaleStartTime) / (scaleEndTime - scaleStartTime);
            pointText.transform.localScale = Vector3.Lerp(Vector3.zero, new Vector3(randomTexTSize, randomTexTSize, randomTexTSize), t);
            Color childColor = childTransform.GetComponent<Image>().color;
            childTransform.GetComponent<Image>().color = new Color(childColor.r, childColor.g, childColor.b, Mathf.Lerp(0f, 1f, t));
            yield return null;
        }

        yield return new WaitForSecondsRealtime(0.6f);

        float moveUpStartTime = Time.time;
        float moveUpDuration = 0.48f; // Total move up duration
        Vector3 initialPosition = pointText.transform.position;
        Vector3 targetPosition = initialPosition + 11 * Vector3.up * (moveUpDuration - 0.2f); // Adjust the direction as needed

        while (Time.time - moveUpStartTime < moveUpDuration)
        {
            float t = (Time.time - moveUpStartTime) / moveUpDuration;
            pointText.transform.position = Vector3.Lerp(initialPosition, targetPosition, t);

            if (Time.time - moveUpStartTime > 0.2f)
            {
                float fadeT = (Time.time - moveUpStartTime - 0.2f) / (moveUpDuration - 0.2f);
                pointText.color = new Color(pointText.color.r, pointText.color.g, pointText.color.b, Mathf.Lerp(1f, 0f, fadeT));
                Color childColor = childTransform.GetComponent<Image>().color;
                childTransform.GetComponent<Image>().color = new Color(childColor.r, childColor.g, childColor.b, Mathf.Lerp(1f, 0f, fadeT));
            }

            yield return null;
        }

        ObjectPool.instance.ReturnTextPopUpFromPool(pointText);
    }
    #endregion

    #region Cursor down and rotation
    public Coroutine moveCursorDownCoroutine;
    IEnumerator MoveCursorDown(float randomSize, float randomRotation)
    {
        mainCursor.transform.localScale = new Vector2(cursorStartSize, cursorStartSize);
        mainCursor.transform.rotation = Quaternion.Euler(0, 0, 0);

        float targetDifference = randomSize / 2;
        float targetRotation = randomRotation / 2;

        float duration = 0.07f;  

        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            float currentSize = cursorStartSize - Mathf.Lerp(0, targetDifference, elapsedTime / duration);
            float currentRotation = Mathf.Lerp(0, targetRotation, elapsedTime / duration);

            mainCursor.transform.localScale = new Vector2(currentSize, currentSize);
            mainCursor.transform.rotation = Quaternion.Euler(0, 0, currentRotation);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0;

        while (elapsedTime < duration)
        {
            float currentSize = cursorStartSize - targetDifference + Mathf.Lerp(0, targetDifference, elapsedTime / duration);
            float currentRotation = targetRotation - Mathf.Lerp(0, targetRotation, elapsedTime / duration);

            mainCursor.transform.localScale = new Vector2(currentSize, currentSize);
            mainCursor.transform.rotation = Quaternion.Euler(0, 0, currentRotation);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCursor.transform.localScale = new Vector2(cursorStartSize, cursorStartSize);
        mainCursor.transform.rotation = Quaternion.Euler(0, 0, 0);

        moveCursorDownCoroutine = null;
    }
    #endregion


    #region Reset
    public void ResetMain()
    {
        isSaved = false;
        totalClickPoints = 0;
        Upgrades.exclSpawned = false;
        cursorClickPoint = 1;
        totalPassivePoints = 0;
    }
    #endregion

    public Coroutine coroutineClickOBject;

    IEnumerator ClickObjectOff()
    {
        yield return new WaitForSeconds(0.071f);
        clickObject.SetActive(false);
        clickObject2.SetActive(false);
        clickObject3.SetActive(false);
        clickObject4.SetActive(false);
        clickObject5.SetActive(false);
        clickObject6.SetActive(false);
        coroutineClickOBject = null;
    }

    #region Load Data
    public void LoadData(GameData data)
    {
        totalClickPoints = data.totalClickPoints;
        cursorClickPoint = data.cursorClickPoint;
        totalPassivePoints = data.totalPassivePoints;
    }
    #endregion

    #region Save Data
    public void SaveData(ref GameData data)
    {
        data.totalClickPoints = totalClickPoints;
        data.cursorClickPoint = cursorClickPoint;
        data.totalPassivePoints = totalPassivePoints;
    }
    #endregion
}
