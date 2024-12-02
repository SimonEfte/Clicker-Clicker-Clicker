// (C) 2021 Alfish. All rights not expressly granted are reserved.

using UnityEngine;
using UnityEditor;

namespace Alfish.StickerStyle.BasicPack.Editor {
  [CustomEditor(typeof(RangedRotationIndicator), true)]
  public class RangedRotationIndicatorEditor : IndicatorEditor
  {
    static readonly GUIContent labelRotationLimits = new GUIContent("Rotation Limits",
      "The angles, in degrees, for when the indicated value is at min (0.0) / max (1.0).");
    static readonly GUIContent[] labelsRotationLimits = {
      new GUIContent("0", "Min"),
      new GUIContent("1", "Max"),
    };
    static readonly GUIContent labelDelay = new GUIContent("Delay",
      "The time it takes for the indicator to reach the indicated value.");
    protected override void DrawProperty(SerializedProperty property) {
      switch (property.propertyPath) {
      case "minRotation":
        return;
      case "maxRotation":
        var prevProperty = serializedObject.FindProperty("minRotation");
        var position = EditorGUILayout.GetControlRect();
        var rpos = EditorGUI.PrefixLabel(position, labelRotationLimits);
        var vals = new[] { prevProperty.floatValue, property.floatValue };
        EditorGUI.MultiFloatField(rpos, labelsRotationLimits, vals);
        prevProperty.floatValue = Mathf.Clamp(vals[0], -360, 360);
        property.floatValue = Mathf.Clamp(vals[1], -360, 360);
        return;
      case "delay":
        EditorGUILayout.PropertyField(property, labelDelay, property.isExpanded);
        return;
      }
      base.DrawProperty(property);
    }
  }
}
