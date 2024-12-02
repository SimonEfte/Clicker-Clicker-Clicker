// (C) 2021 Alfish. All rights not expressly granted are reserved.

using UnityEngine;
using UnityEngine.UI;

namespace Alfish.StickerStyle.BasicPack {
  /// <summary>An indicator that changes amount of filling in the image according to its value.</summary>
#if UNITY_EDITOR
  //This allows you to place a script anywhere in the "Component" menu, instead of just the "Scripts" menu.
  [AddComponentMenu("Indicator/Fill Indicator")]
#endif
  public class FillIndicator : FloatIndicator
  {
    /// <summary>The image with a filling to be changed according to the indicated value.</summary>
    public Image target;
    
    /// <summary>Sets the indicated value.</summary>
    /// <param name="value">The new value [0.0 ~ 1.0].</param>
    public override void SetValue(float value) {
      target.fillAmount = value;
    }
  }
}
