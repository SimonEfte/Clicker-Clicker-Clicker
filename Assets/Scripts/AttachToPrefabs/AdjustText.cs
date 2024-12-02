using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AdjustText : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;

    public void Awake()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        float preferredWidth = textMeshPro.GetPreferredValues().x;
        textMeshPro.rectTransform.sizeDelta = new Vector2(preferredWidth, textMeshPro.rectTransform.sizeDelta.y);
    }

    private void Update()
    {
        float preferredWidth = textMeshPro.GetPreferredValues().x;
        textMeshPro.rectTransform.sizeDelta = new Vector2(preferredWidth, textMeshPro.rectTransform.sizeDelta.y);
    }
}
