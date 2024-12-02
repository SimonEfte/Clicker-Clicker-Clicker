// (C) 2021 Alfish. All rights not expressly granted are reserved.

using UnityEngine;
using UnityEngine.UI;

namespace Alfish.StickerStyle.BasicPack {
  /// <summary>Enumerates the possible methods to be used when rounding a float value to an integer.</summary>
  public enum RoundingMode : byte { Floor, Round, Ceiling }
  
  /// <summary>An indicator that sets a sprite image depending on the indicated value.</summary>
#if UNITY_EDITOR
  //This allows you to place a script anywhere in the "Component" menu, instead of just the "Scripts" menu.
  [AddComponentMenu("Indicator/Single Image Indicator")]
#endif
  public class SingleImageIndicator : FloatIndicator
  {
    /// <summary>The image with a sprite to be changed depending on the indicated value.</summary>
    public Image target;
    /// <summary>The method to be used when rounding the indicated value [0.0 ~ 1.0]
    /// to its corresponding sprite index (integer).</summary>
    public RoundingMode roundingMode;
    /// <summary>The sprite for when the image has no fractions..</summary>
    public Sprite spriteEmpty;
    /// <summary>The sprites representing the possible fractions for the image, in ascending order.
    /// The last fraction represents 1.0 in the indicated value.</summary>
    public Sprite[] sprites;
    
    /// <summary>Sets the indicated value.</summary>
    /// <param name="value">The new value [0.0 ~ 1.0].</param>
    public override void SetValue(float value) {
      var v = value * sprites.Length;
      switch (roundingMode) {
      case RoundingMode.Floor:
        SetSprite(Mathf.FloorToInt(v));
        break;
      case RoundingMode.Round:
        SetSprite((int)System.Math.Round(v, System.MidpointRounding.AwayFromZero));
        break;
      case RoundingMode.Ceiling:
        SetSprite(Mathf.CeilToInt(v));
        break;
      }
    }
    void SetSprite(int s) {
      target.sprite = s > 0 ? sprites[s - 1] : spriteEmpty;
    }
  }
}
