// (C) 2021 Alfish. All rights not expressly granted are reserved.

using UnityEngine;

namespace Alfish.StickerStyle.BasicPack {
  /// <summary>An indicator that rotates an element within a range, according to the indicated value.</summary>
#if UNITY_EDITOR
  //This allows you to place a script anywhere in the "Component" menu, instead of just the "Scripts" menu.
  [AddComponentMenu("Indicator/Ranged Rotation Indicator")]
#endif
  //Prevents MonoBehaviour of same type (or subtype) to be added more than once to a GameObject.
  [DisallowMultipleComponent]
  public class RangedRotationIndicator : FloatIndicator
  {
    /// <summary>The limit angle, in degrees, for the minimum indicated value (0.0).</summary>
    public float minRotation;
    /// <summary>The limit angle, in degrees, for the maximum indicated value (1.0).</summary>
    public float maxRotation;
    /// <summary>The time it takes for the indicator to reach the indicated value.</summary>
    public float delay;
    
    void Start() {
      base.enabled = false; // only enabled while animating
      if (delay > 0) {
        desiredValue = prevValue = currentValue();
        t = 1;
      }
      limitAngle = 360 + Mathf.Min(minRotation, maxRotation);
    }
    float prevValue, desiredValue, t, limitAngle;
    
    void Update() {
      //> Animate rotation using a parabola
      t += Time.deltaTime / delay;
      if (t >= 1)
        base.enabled = false;
      var value = Mathf.Lerp(prevValue, desiredValue, 2 * t - t * t);
      setRotation(value);
    }
    
    /// <summary>Sets the indicated value.</summary>
    /// <param name="value">The new value [0.0 ~ 1.0].</param>
    public override void SetValue(float value) {
      if (delay > 0) {
        prevValue = currentValue();
        desiredValue = value;
        t = 0;
        base.enabled = true;
      } else
        setRotation(value);
    }
    
    float currentValue() {
      var zRo = base.transform.localEulerAngles.z;
      if (zRo >= limitAngle)
        zRo -= 360;
      return (zRo - minRotation) / (maxRotation - minRotation);
    }
    void setRotation(float value) {
      var ro = base.transform.localEulerAngles;
      ro.z = Mathf.Lerp(minRotation, maxRotation, value);
      base.transform.localEulerAngles = ro;
    }
  }
}
