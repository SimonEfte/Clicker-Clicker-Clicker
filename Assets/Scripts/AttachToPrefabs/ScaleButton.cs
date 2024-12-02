using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScaleButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Vector3 minScale = new Vector3(0.8f, 0.8f, 0.8f);
    public float scaleDuration = 0.1f;

    private Vector3 originalScale;
    private Coroutine scaleCoroutine;
    private bool isHovering = false;

    private void Start()
    {
        originalScale = transform.localScale;

        if(MobileScript.isMobile == true)
        {
            minScale = new Vector3(0.94f, 0.94f, 0.94f);

            if (gameObject.name == "ClickAOEupgrade" || gameObject.name == "AutoClickerUpgrade" || gameObject.name == "CritClickIncreaseUpgradde" || gameObject.name == "CritClickChanceUpgrade")
            {
                minScale = new Vector3(1.5f, 1.5f, 1.5f);
            }

            if (gameObject.name == "ShopBTN" || gameObject.name == "CursorShop" || gameObject.name == "PrestigeBTN" || gameObject.name == "StatsBTN" || gameObject.name == "AchBTN")
            {
                minScale = new Vector3(1.35f, 1.35f, 1.35f);
            }

            if (gameObject.name == "AscendButton")
            {
                minScale = new Vector3(0.06f, 0.06f, 0.06f);
            }

            if (gameObject.name == "LevelUpBTN")
            {
                minScale = new Vector3(0.11f, 0.11f, 0.11f);
            }

            if (gameObject.name == "UpgradePassiveButton" || gameObject.name == "UpgradeActiveButton")
            {
                minScale = new Vector3(1.2f, 1.2f, 1.2f);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(DemoScript.isDemo == true)
        {
            if(gameObject.name != "CursorShop" && gameObject.name != "PrestigeBTN" && gameObject.name != "AchBTN") { isHovering = true; }
        }
        else
        {
            isHovering = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;

        // Reset scale when pointer exits, if not holding down
        if (scaleCoroutine != null)
            StopCoroutine(scaleCoroutine);

        scaleCoroutine = StartCoroutine(ScaleTo(originalScale));
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isHovering)
        {
            if (scaleCoroutine != null)
                StopCoroutine(scaleCoroutine);

            scaleCoroutine = StartCoroutine(ScaleTo(minScale));
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isHovering)
        {
            if (scaleCoroutine != null)
                StopCoroutine(scaleCoroutine);

            scaleCoroutine = StartCoroutine(ScaleTo(originalScale));
        }
    }

    private IEnumerator ScaleTo(Vector3 targetScale)
    {
        Vector3 currentScale = transform.localScale;
        float timeElapsed = 0f;

        while (timeElapsed < scaleDuration)
        {
            transform.localScale = Vector3.Lerp(currentScale, targetScale, timeElapsed / scaleDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
    }
}
