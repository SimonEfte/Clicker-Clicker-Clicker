// (C) 2021 Alfish. All rights not expressly granted are reserved.

using UnityEngine;

namespace Alfish.StickerStyle.BasicPack {
  /// <summary>Follows a world object's transform on the screen.</summary>
#if UNITY_EDITOR
  //This allows you to place a script anywhere in the "Component" menu, instead of just the "Scripts" menu.
  [AddComponentMenu("Indicator/Screen Position Indicator")]
#endif
  //Prevents MonoBehaviour of same type (or subtype) to be added more than once to a GameObject.
  [DisallowMultipleComponent]
  //This automatically adds required components as dependencies.
  [RequireComponent(typeof(RectTransform))]
  public class ScreenPositionIndicator : Indicator
  {
    /// <summary>The camera used for converting world coordinates into screen coordinates.</summary>
    public Camera screenCamera;
    /// <summary>The object being followed, whose position should be indicated on the screen.</summary>
    public Transform trackingObject;
    /// <summary>The offset from the object's world position to use as the reference position.</summary>
    public Vector3 offset;
    /// <summary>Optional screen position where the indicator stays when the object is outside
    /// the tracking area or behind the camera.</summary>
    public RectTransform defaultPosition;
    /// <summary>Optional area in the screen where the indicator can be following the object.
    /// If the object is outside that area, the indicator switches to the default position instead.</summary>
    public RectTransform trackingArea;
    /// <summary>The time it takes for the indicator to switch between the tracked position
    /// and the default position.</summary>
    public float animationDuration;
    
    void Start() {
      animationStartPosition = defaultPosition ?
        defaultPosition.position : // to ensure no initial animation when not initially tracking
        base.transform.position;
      animationEndTime = Time.time;
      isTracking = true; // to ensure no initial animation when initially tracking
    }
    
    Vector2 animationStartPosition;
    float animationEndTime;
    bool isTracking;
    
    void Update() {
      var wasTracking = isTracking;
      var pos = trackPosition();
      var now = Time.time;
      // check the moment when switching in or out of tracking area
      if (isTracking != wasTracking) {
        // trigger animation to switch between tracking position and default position
        animationStartPosition = base.transform.position;
        animationEndTime = now + animationDuration;
      }
      if (now >= animationEndTime)
        // no animation, just set position
        base.transform.position = pos;
      else {
        // animate smoothly with brake parabola
        var animationTime = 1 - Mathf.Min((animationEndTime - now) / animationDuration, 1);
        var t = 2 * animationTime - animationTime * animationTime;
        base.transform.position = Vector2.Lerp(animationStartPosition, pos, t);
      }
    }
    Vector2 trackPosition() {
      var screenPosition = screenCamera.WorldToScreenPoint(trackingObject.position + offset);
      var objDistToCamera = screenPosition.z;
      // check when it should not follow the object
      if (
        defaultPosition && ( // default position is set and ...
          objDistToCamera <= screenCamera.nearClipPlane // obj not in front of camera
          || // or
          (trackingArea && !rectContains(trackingArea, screenPosition)) // obj is outside the bounds of tracking area
        )
      ) {
        isTracking = false;
        return defaultPosition.position;
      }
      isTracking = true;
      return screenPosition;
    }
    
    static bool rectContains(RectTransform rt, Vector2 pos) {
      var s = Vector2.Scale(rt.lossyScale, rt.rect.size / 2);
      var p = (Vector2)rt.position;
      var a = p - s;
      var b = p + s;
      return pos.x >= a.x && pos.x <= b.x && pos.y >= a.y && pos.y <= b.y;
    }
  }
}
