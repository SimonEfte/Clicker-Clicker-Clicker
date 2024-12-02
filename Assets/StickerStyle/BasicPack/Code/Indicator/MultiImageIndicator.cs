// (C) 2021 Alfish. All rights not expressly granted are reserved.

using UnityEngine;
using UnityEngine.UI;

namespace Alfish.StickerStyle.BasicPack {
  /// <summary>An indicator that instantiates sprite images to represent the indicated value.</summary>
#if UNITY_EDITOR
  //This allows you to place a script anywhere in the "Component" menu, instead of just the "Scripts" menu.
  [AddComponentMenu("Indicator/Multi Image Indicator")]
#endif
  public class MultiImageIndicator : BoundedIntegerIndicator
  {
    /// <summary>An optional prefab to use when instantiating images.
    /// If not specified, a default Image object is generated.
    /// The images are instantiated as children of this game object.</summary>
    public Image prefab;
    /// <summary>The sprite for when the image has no fractions.</summary>
    public Sprite spriteEmpty;
    /// <summary>The sprites representing the possible fractions for one image, in ascending order.
    /// Each fraction corresponds to 1 unit in the indicated value.</summary>
    public Sprite[] sprites;
    
    /// <summary>Sets the maximum indicated value.</summary>
    /// <param name="maxValue">The new maximum value.</param>
    public override void SetMaxValue(int maxValue) {
      //> Destroy all children
      foreach (Transform child in base.transform) {
        var childObj = child.gameObject;
        childObj.SetActive(false);
        Object.Destroy(childObj);
      }
      //> Instantiate enough children for the new max value
      int i = 0, n = sprites.Length;
      while (i < maxValue) {
        i += n;
        if (prefab)
          Object.Instantiate(prefab, base.transform, false).name = prefab.name + " (" + i + ")";
        else
          new GameObject(base.name + " Image (" + i + ")", typeof(Image)).transform.SetParent(base.transform, false);
      }
      this.SetValue(maxValue);
    }
    /// <summary>Sets the indicated value.</summary>
    /// <param name="value">The new value [0 .. max].</param>
    public override void SetValue(int value) {
      var n = sprites.Length;
      foreach (Transform child in base.transform) {
        //> Ignore children that are being destroyed
        if (!child.gameObject.activeInHierarchy)
          continue;
        //> Set the sprite on each child image
        var childImg = child.GetComponent<Image>();
        if (value >= n)
          childImg.sprite = sprites[n - 1];
        else if (value >= 1)
          childImg.sprite = sprites[value - 1];
        else
          childImg.sprite = spriteEmpty;
        //> Count n parts for each image (n = amount of sprites minus 1)
        value -= n;
      }
    }
  }
}
