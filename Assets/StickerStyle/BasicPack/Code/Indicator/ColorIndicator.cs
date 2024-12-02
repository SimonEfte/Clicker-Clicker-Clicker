// (C) 2021 Alfish. All rights not expressly granted are reserved.

using UnityEngine;
using UnityEngine.UI;

namespace Alfish.StickerStyle.BasicPack {
  /// <summary>A section of the reference color intervals.</summary>
  [System.Serializable]
  public struct ColorInterval
  {
    /// <summary>The color in the start of the interval.</summary>
    public Color color;
    /// <summary>The size of the interval.</summary>
    public float length;
    /// <summary>If true, the color has a smooth transition to the next color along this interval.</summary>
    public bool smooth;
  }
  
  /// <summary>An indicator that changes color according to its value.</summary>
#if UNITY_EDITOR
  //This allows you to place a script anywhere in the "Component" menu, instead of just the "Scripts" menu.
  [AddComponentMenu("Indicator/Color Indicator")]
#endif
  public class ColorIndicator : FloatIndicator
  {
    /// <summary>The images to be recolored according to the indicated value.</summary>
    public Image[] targets;
    /// <summary>The reference color intervals.</summary>
    public ColorInterval[] colorIntervals;
    /// <summary>The last reference color, which indicates the highest value.</summary>
    public Color lastColor;
    
    /// <summary>Sets the indicated value. The max value is determined by the color intervals.</summary>
    /// <param name="value">The new value [0.0 ~ max].</param>
    public override void SetValue(float value) {
      for (int i = 0, n = colorIntervals.Length; i < n; i++) {
        var ci = colorIntervals[i];
        value -= ci.length;
        if (value < 0) {
          if (ci.smooth) {
            var nextColor = i < n - 1 ? colorIntervals[i + 1].color : lastColor;
            var t = ci.length + value;
            setColor(Color.Lerp(ci.color, nextColor, t / ci.length));
          } else
            setColor(ci.color);
          return;
        }
      }
      setColor(lastColor);
    }
    void setColor(Color color) {
      foreach (var target in targets)
        target.color = color;
    }
  }
}
