// (C) 2021 Alfish. All rights not expressly granted are reserved.

using UnityEngine;
using UnityEditor;

namespace Alfish.StickerStyle.BasicPack.Editor {
  [CustomEditor(typeof(ScreenCollectibleIndicator), true)]
  public class ScreenCollectibleIndicatorEditor : IndicatorEditor
  {
    static readonly GUIContent labelScreenCamera = new GUIContent("Screen Camera",
      "The camera used for converting world coordinates into screen coordinates.");
    static readonly GUIContent labelAnimationDuration = new GUIContent("Animation Duration",
      "The time it takes for a collectible to reach this position on the screen.");
    static readonly GUIContent labelEndSize = new GUIContent("End Size",
      "The final screen size a collectible has at the end of the animation.");
    protected override void DrawProperty(SerializedProperty property) {
      switch (property.propertyPath) {
      case "screenCamera":
        EditorGUILayout.PropertyField(property, labelScreenCamera, property.isExpanded);
        return;
      case "animationDuration":
        EditorGUILayout.PropertyField(property, labelAnimationDuration, property.isExpanded);
        return;
      case "endSize":
        EditorGUILayout.PropertyField(property, labelEndSize, property.isExpanded);
        return;
      }
      base.DrawProperty(property);
    }
  }
}
