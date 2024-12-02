// (C) 2021 Alfish. All rights not expressly granted are reserved.

using UnityEngine;
using UnityEditor;

namespace Alfish.StickerStyle.BasicPack.Editor {
  [CustomEditor(typeof(FadeIndicator), true)]
  public class FadeIndicatorEditor : IndicatorEditor
  {
    static readonly GUIContent labelTarget = new GUIContent("Target",
      "The canvas group whose opacity is to be changed depending on the indicated value.");
    static readonly GUIContent labelFadeDelay = new GUIContent("Fade Delay",
      "The time it takes before a fade out/in begins.");
    static readonly GUIContent labelFadeDuration = new GUIContent("Fade Duration",
      "The time it takes for a fade out/in transition to complete, after it begins.");
    static readonly GUIContent[] labelsFade = {
      new GUIContent("O", "Fade Out"),
      new GUIContent("I", "Fade In"),
    };
    protected override void DrawProperty(SerializedProperty property) {
      SerializedProperty prevProperty;
      Rect position, rpos;
      float[] vals;
      switch (property.propertyPath) {
      case "target":
        EditorGUILayout.PropertyField(property, labelTarget, property.isExpanded);
        return;
      case "fadeOutDelay":
        return;
      case "fadeInDelay":
        prevProperty = serializedObject.FindProperty("fadeOutDelay");
        position = EditorGUILayout.GetControlRect();
        rpos = EditorGUI.PrefixLabel(position, labelFadeDelay);
        vals = new[] { prevProperty.floatValue, property.floatValue };
        EditorGUI.MultiFloatField(rpos, labelsFade, vals);
        prevProperty.floatValue = Mathf.Max(0, vals[0]);
        property.floatValue = Mathf.Max(0, vals[1]);
        return;
      case "fadeOutDuration":
        return;
      case "fadeInDuration":
        prevProperty = serializedObject.FindProperty("fadeOutDuration");
        position = EditorGUILayout.GetControlRect();
        rpos = EditorGUI.PrefixLabel(position, labelFadeDuration);
        vals = new[] { prevProperty.floatValue, property.floatValue };
        EditorGUI.MultiFloatField(rpos, labelsFade, vals);
        prevProperty.floatValue = Mathf.Max(0, vals[0]);
        property.floatValue = Mathf.Max(0, vals[1]);
        return;
      }
      base.DrawProperty(property);
    }
  }
}
