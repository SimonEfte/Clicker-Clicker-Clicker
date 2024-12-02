// (C) 2021 Alfish. All rights not expressly granted are reserved.

using UnityEngine;

namespace Alfish.StickerStyle.BasicPack {
  /// <summary>An indicator that fades an element in or out.</summary>
#if UNITY_EDITOR
  //This allows you to place a script anywhere in the "Component" menu, instead of just the "Scripts" menu.
  [AddComponentMenu("Indicator/Fade Indicator")]
#endif
  public class FadeIndicator : Indicator
  {
    /// <summary>The canvas group whose opacity is to be changed.</summary>
    public CanvasGroup target;
    /// <summary>The time it takes before a fade out begins.</summary>
    public float fadeOutDelay;
    /// <summary>The time it takes before a fade in begins.</summary>
    public float fadeInDelay;
    /// <summary>The time it takes for a fade out transition to complete, after it begins.</summary>
    public float fadeOutDuration;
    /// <summary>The time it takes for a fade in transition to complete, after it begins.</summary>
    public float fadeInDuration;
    
    void Start() {
      delayStartTime = float.NegativeInfinity;
      desiredOpacity = target.alpha;
    }
    
    float delayStartTime, desiredOpacity;
    void Update() {
      if (target != null) {
        if (desiredOpacity < target.alpha) {
          if (Time.time - delayStartTime < fadeOutDelay)
            return;
          // no more delay, fade out
          if (fadeOutDuration > 0) {
            // fade transition
            var dAlpha = Time.deltaTime / fadeOutDuration;
            target.alpha = Mathf.Max(desiredOpacity, target.alpha - dAlpha);
          } else
            target.alpha = desiredOpacity;
        } else if (desiredOpacity > target.alpha) {
          if (Time.time - delayStartTime < fadeInDelay)
            return;
          // no more delay, fade in
          if (fadeInDuration > 0) {
            // fade transition
            var dAlpha = Time.deltaTime / fadeInDuration;
            target.alpha = Mathf.Min(target.alpha + dAlpha, desiredOpacity);
          } else
            target.alpha = desiredOpacity;
        }
      }
    }
    
    /// <summary>Sets the desired alpha for the target.
    /// It triggers a fade out when alpha decreases, and a fade in when it increases.</summary>
    /// <param name="newDesiredOpacity">The desired alpha value [0.0 ~ 1.0].</param>
    public void SetTargetOpacity(float newDesiredOpacity) {
      if (newDesiredOpacity != desiredOpacity) {
        delayStartTime = Time.time;
        desiredOpacity = newDesiredOpacity;
      }
    }
  }
}
