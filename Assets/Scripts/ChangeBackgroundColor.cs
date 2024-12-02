using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBackgroundColor : MonoBehaviour
{
    public void Awake()
    {
        StartCoroutine(ColorWait());
    }

    public Image background;
    public int currentColor;

    IEnumerator ColorWait()
    {
        Color currentColor = background.color;

        Color targetColor = HexToColor("6A6464");
        float originalAlpha = currentColor.a;

        int randomColor = Random.Range(1,13);
        if(randomColor == 1) { targetColor = HexToColor("504343"); }
        else if (randomColor == 2) { targetColor = HexToColor("B9000A"); }
        else if (randomColor == 3) { targetColor = HexToColor("B90073"); }
        else if (randomColor == 4) { targetColor = HexToColor("8E00B9"); }
        else if (randomColor == 5) { targetColor = HexToColor("0035B9"); }
        else if (randomColor == 6) { targetColor = HexToColor("AB5A00"); }
        else if (randomColor == 7) { targetColor = HexToColor("0C5718"); }
        else if (randomColor == 8) { targetColor = HexToColor("573100"); }
        else if (randomColor == 9) { targetColor = HexToColor("281EBF"); }
        else if (randomColor == 10) { targetColor = HexToColor("254125"); }
        else if (randomColor == 11) { targetColor = HexToColor("592DDE"); }
        else if (randomColor == 12) { targetColor = HexToColor("C6681D"); }

        // Time to fade
        float duration = 0.5f;
        float elapsedTime = 0f;

        // Lerp the background color over the duration
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            Color lerpedColor = Color.Lerp(currentColor, targetColor, elapsedTime / duration);

            lerpedColor.a = originalAlpha;
            background.color = lerpedColor;

            yield return null; // Wait for the next frame.
        }

        // Ensure the color is set to the target color after the loop.
        background.color = targetColor;
        targetColor.a = originalAlpha;
        //yield return new WaitForSeconds(200);
        yield return new WaitForSeconds(180);
        ChangeColor();
    }

    Color HexToColor(string hex)
    {
        if (hex.StartsWith("#")) hex = hex.Substring(1);

        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        return new Color32(r, g, b, 192); // 255 is the alpha channel, fully opaque.
    }


    public void ChangeColor()
    {
        StartCoroutine(ColorWait());
    }


    //B9000A

}
