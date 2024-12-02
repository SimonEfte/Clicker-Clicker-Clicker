// (C) 2021 Alfish. All rights not expressly granted are reserved.

using UnityEngine;

namespace Alfish.StickerStyle.BasicPack {
  /// <summary>A component that represents the value of a property.</summary>
  public abstract class Indicator : MonoBehaviour
  {
  }
  
  /// <summary>An indicator representing an integer value.</summary>
  public abstract class IntegerIndicator : Indicator
  {
    /// <summary>Sets the indicated value.</summary>
    /// <param name="value">The new value.</param>
    public abstract void SetValue(int value);
  }
  
  /// <summary>An indicator representing an integer value with a maximum.</summary>
  public abstract class BoundedIntegerIndicator : IntegerIndicator
  {
    /// <summary>Sets the maximum indicated value.</summary>
    /// <param name="maxValue">The new maximum value.</param>
    public abstract void SetMaxValue(int maxValue);
  }
  
  /// <summary>An indicator representing a float value.</summary>
  public abstract class FloatIndicator : Indicator
  {
    /// <summary>Sets the indicated value.</summary>
    /// <param name="value">The new value.</param>
    public abstract void SetValue(float value);
  }
  
  /// <summary>An indicator representing a float value with a maximum.</summary>
  public abstract class BoundedFloatIndicator : FloatIndicator
  {
    /// <summary>Sets the maximum indicated value.</summary>
    /// <param name="maxValue">The new maximum value.</param>
    public abstract void SetMaxValue(float maxValue);
  }
}
