using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextPopUp : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    private Image iconImage, prestigeImage;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();

        Transform iconTransform = transform.Find("Icon");
        if (iconTransform != null)
        {
            iconImage = iconTransform.GetComponent<Image>();
        }

        Transform prestigeIcon = transform.Find("IconPrestige");
        if (prestigeIcon != null)
        {
            prestigeImage = prestigeIcon.GetComponent<Image>();
        }
    }


    private void OnEnable()
    {
        StartCoroutine(MoveAndFade());
    }

    private IEnumerator MoveAndFade()
    {
        yield return new WaitForSeconds(0.05f);

        Image coin;

        if (transform.localScale.x > 0.5f)
        {
            iconImage.gameObject.SetActive(false);
            prestigeImage.gameObject.SetActive(true);
            coin = prestigeImage;
        }
        else
        {
            prestigeImage.gameObject.SetActive(false);
            iconImage.gameObject.SetActive(true);
            coin = iconImage;
        }

        Color textColor = textMesh.color;
        textColor.a = 1;
        textMesh.color = textColor;

        if (coin != null)
        {
            Color iconColor = coin.color;
            iconColor.a = 1;
            coin.color = iconColor;
        }

        Vector3 originalPosition = transform.position;
        Vector3 targetPosition = originalPosition + new Vector3(0, 1.0f, 0);  // Adjust the y value to control how far it moves up
        float duration = 0.6f;
        float elapsedTime = 0f;

        // Move upwards and after 0.45 seconds start fading out
        while (elapsedTime < duration)
        {
            // Move upwards
            transform.position = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / duration);

            if (elapsedTime >= 0.45f)
            {
                // Fade out from 100% to 0% alpha in the last 0.15 seconds
                float fadeOutTime = (elapsedTime - 0.45f) / 0.15f;
                textColor.a = Mathf.Lerp(1, 0, fadeOutTime);
                textMesh.color = textColor;

                if (coin != null)
                {
                    Color iconColor = coin.color;
                    iconColor.a = Mathf.Lerp(1, 0, fadeOutTime);
                    coin.color = iconColor;
                }
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure final position and alpha values are set correctly
        transform.position = targetPosition;

        textColor.a = 0;
        textMesh.color = textColor;

        if (coin != null)
        {
            Color iconColor = coin.color;
            iconColor.a = 0;
            coin.color = iconColor;
        }

        ObjectPool.instance.ReturnFallingTextPopUpFromPool(textMesh);
    }
}
