// (C) 2021 Alfish. All rights not expressly granted are reserved.

using UnityEngine;
using System.Collections.Generic;

namespace Alfish.StickerStyle.BasicPack {
  /// <summary>An indicator that pulls other elements towards this element on the screen,
  /// and then destroys them.</summary>
#if UNITY_EDITOR
  //This allows you to place a script anywhere in the "Component" menu, instead of just the "Scripts" menu.
  [AddComponentMenu("Indicator/Screen Collectible Indicator")]
#endif
  //This automatically adds required components as dependencies.
  [RequireComponent(typeof(RectTransform))]
  public class ScreenCollectibleIndicator : Indicator
  {
    struct MovingElement
    {
      internal RectTransform rectTransform;
      internal float startTime;
      internal Vector2 startPosition, startSize;
    }
    
    /// <summary>The camera used for converting world coordinates into screen coordinates.</summary>
    public Camera screenCamera;
    /// <summary>The time it takes for a collectible to reach this position on the screen.</summary>
    public float animationDuration;
    /// <summary>The final screen size a collectible has at the end of the animation.</summary>
    public Vector2 endSize;
    
    readonly List<MovingElement> elements = new List<MovingElement>();
    
    static float NearClipPlaneHeight(Camera camera) {
      return camera.orthographic ?
        2 * camera.orthographicSize :
        2 * camera.nearClipPlane * Mathf.Tan(Mathf.Deg2Rad * camera.fieldOfView / 2);
    }
    
    /// <summary>Adds a collected element. It's automatically pulled to this screen position, then destroyed.</summary>
    /// <param name="collectedObj">The screen element to be pulled.</param>
    /// <param name="worldPosition">Position where the collected object was in the world.</param>
    /// <param name="worldSize">Size the object had in the world.</param>
    public void Add(RectTransform collectedObj, Vector3 worldPosition, Vector2 worldSize) {
      var screenPosition = screenCamera.WorldToScreenPoint(worldPosition);
      var objDistToCamera = screenPosition.z;
      if (objDistToCamera <= screenCamera.nearClipPlane) {
        // safeguard against small, zero and negative objDistToCamera (obj not in front of camera)
        addElement(collectedObj, base.transform.position, endSize);
        return;
      }
      var nearSize = worldSize * (screenCamera.orthographic ? 1 : screenCamera.nearClipPlane / objDistToCamera);
        // nearSize: size of the obj projection in the near plane of the camera, in world units
        // approximation assuming nearClipPlane very small (close to the camera) relative to objDistToCamera
        // and fieldOfView not too big (< 90 degrees)
      var nearPlaneHeight = NearClipPlaneHeight(screenCamera);
        // nearPlaneHeight: height of the near clip pane of the camera in world units
      var canvasScale = collectedObj.lossyScale.y;
      var screenSize = nearSize * (screenCamera.pixelHeight / (canvasScale * nearPlaneHeight));
        // screenSize: size of the obj projection in reference canvas pixels
      addElement(collectedObj, screenPosition, screenSize);
    }
    void addElement(RectTransform collectedObj, Vector2 screenPosition, Vector2 screenSize) {
      collectedObj.position = screenPosition;
      collectedObj.sizeDelta = screenSize;
      elements.Add(new MovingElement {
        rectTransform = collectedObj,
        startTime = Time.time,
        startPosition = screenPosition,
        startSize = screenSize,
      });
    }
    
    void Update() {
      var now = Time.time;
      var endPosition = base.transform.position;
      for (int i = 0; i < elements.Count; i++) {
        var element = elements[i];
        var animationTime = Mathf.Min((now - element.startTime) / animationDuration, 1);
          // animationTime: time of element's animation relative to its beggining [0 ~ 1]
        var t = 2 * animationTime - animationTime * animationTime;
          // t: parabola to make movement brake smoothly near the end [0 ~ 1]
        if (t < 1) {
          element.rectTransform.position = Vector2.Lerp(element.startPosition, endPosition, t);
          element.rectTransform.sizeDelta = Vector2.Lerp(element.startSize, endSize, t);
        } else {
          var obj = element.rectTransform.gameObject;
          Object.Destroy(obj); // will trigger any OnDestroy callbacks it might have
          elements.RemoveAt(i--);
        }
      }
    }
  }
}
