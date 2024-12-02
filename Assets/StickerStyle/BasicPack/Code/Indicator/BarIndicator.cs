// (C) 2021 Alfish. All rights not expressly granted are reserved.

using UnityEngine;

namespace Alfish.StickerStyle.BasicPack {
  /// <summary>An horizontal bar indicator that changes width according to its value.</summary>
#if UNITY_EDITOR
  //This allows you to place a script anywhere in the "Component" menu, instead of just the "Scripts" menu.
  [AddComponentMenu("Indicator/Bar Indicator")]
#endif
  public class BarIndicator : BoundedFloatIndicator
  {
    /// <summary>An optional game object to activate only when the bar is not empty.</summary>
    public GameObject barStart;
    /// <summary>An optional game object to activate only when the bar is full.</summary>
    public GameObject barEnd;
    /// <summary>The screen element to be resized according to the bar value (filling).</summary>
    public RectTransform barValue;
    /// <summary>An optional screen element to be resized according to the bar maximum (background).</summary>
    public RectTransform barMax;
    /// <summary>The screen size of one unit in the bar.</summary>
    public float widthMultiplier;
    
    float maxValue;
    /// <summary>Sets the maximum indicated value.</summary>
    /// <param name="maxValue">The new maximum value.</param>
    public override void SetMaxValue(float maxValue) {
      this.maxValue = maxValue;
      if (barMax) {
        var x = Mathf.Round(maxValue * widthMultiplier);
        barMax.sizeDelta = new Vector2(x, barMax.sizeDelta.y);
      }
    }
    /// <summary>Sets the indicated value.</summary>
    /// <param name="value">The new value [0.0 ~ max].</param>
    public override void SetValue(float value) {
      var x = Mathf.Round(Mathf.Clamp(value, 0, maxValue) * widthMultiplier);
      if (barStart)
        barStart.SetActive(x > 0);
      if (barEnd)
        barEnd.SetActive(x >= maxValue * widthMultiplier);
      barValue.sizeDelta = new Vector2(x, barValue.sizeDelta.y);
    }
  }
}
