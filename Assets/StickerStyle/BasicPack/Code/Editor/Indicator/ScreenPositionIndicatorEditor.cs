// (C) 2021 Alfish. All rights not expressly granted are reserved.

using UnityEngine;
using UnityEditor;

namespace Alfish.StickerStyle.BasicPack.Editor {
  [CustomEditor(typeof(ScreenPositionIndicator), true)]
  public class ScreenPositionIndicatorEditor : IndicatorEditor
  {
    static readonly GUIContent labelScreenCamera = new GUIContent("Screen Camera",
      "The camera used for converting world coordinates into screen coordinates.");
    static readonly GUIContent labelTrackingObject = new GUIContent("Tracking Object",
      "The object being followed, whose position should be indicated on the screen.");
    static readonly GUIContent labelOffset = new GUIContent("Offset",
      "The offset from the object's world position to use as the reference position.");
    static readonly GUIContent labelDefaultPosition = new GUIContent("Default Position",
      "Optional screen position where the indicator stays when the object is outside" +
      " the tracking area or behind the camera.");
    static readonly GUIContent labelTrackingArea = new GUIContent("Tracking Area",
      "Optional area in the screen where the indicator can be following the object." +
      " If the object is outside that area, the indicator switches to the default position instead.");
    static readonly GUIContent labelAnimationDuration = new GUIContent("Animation Duration",
      "The time it takes for the indicator to switch between the tracked position and the default position.");
    protected override void DrawProperty(SerializedProperty property) {
      switch (property.propertyPath) {
      case "screenCamera":
        EditorGUILayout.PropertyField(property, labelScreenCamera, property.isExpanded);
        return;
      case "trackingObject":
        EditorGUILayout.PropertyField(property, labelTrackingObject, property.isExpanded);
        return;
      case "offset":
        EditorGUI.indentLevel++;
        EditorGUILayout.PropertyField(property, labelOffset, property.isExpanded);
        EditorGUI.indentLevel--;
        return;
      case "defaultPosition":
        EditorGUILayout.PropertyField(property, labelDefaultPosition, property.isExpanded);
        return;
      case "trackingArea":
        EditorGUI.indentLevel++;
        EditorGUILayout.PropertyField(property, labelTrackingArea, property.isExpanded);
        EditorGUI.indentLevel--;
        return;
      case "animationDuration":
        EditorGUI.indentLevel++;
        EditorGUILayout.PropertyField(property, labelAnimationDuration, property.isExpanded);
        EditorGUI.indentLevel--;
        return;
      }
      base.DrawProperty(property);
    }
  }
}
