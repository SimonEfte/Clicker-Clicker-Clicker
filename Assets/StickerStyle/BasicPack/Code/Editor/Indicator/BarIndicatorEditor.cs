// (C) 2021 Alfish. All rights not expressly granted are reserved.

using UnityEngine;
using UnityEditor;

namespace Alfish.StickerStyle.BasicPack.Editor {
  [CustomEditor(typeof(BarIndicator), true)]
  public class BarIndicatorEditor : IndicatorEditor
  {
    static readonly GUIContent labelBarStart = new GUIContent("Bar Start",
      "An optional game object to activate only when the bar is not empty.");
    static readonly GUIContent labelBarEnd = new GUIContent("Bar End",
      "An optional game object to activate only when the bar is full.");
    static readonly GUIContent labelBarValue = new GUIContent("Bar Value",
      "The screen element to be resized according to the bar value (filling).");
    static readonly GUIContent labelBarMax = new GUIContent("Bar Max",
      "An optional screen element to be resized according to the bar maximum (background).");
    static readonly GUIContent labelWidthMultiplier = new GUIContent("Width Multiplier",
      "The screen size of one unit in the bar.");
    protected override void DrawProperty(SerializedProperty property) {
      switch (property.propertyPath) {
      case "barStart":
        EditorGUILayout.PropertyField(property, labelBarStart, property.isExpanded);
        return;
      case "barEnd":
        EditorGUILayout.PropertyField(property, labelBarEnd, property.isExpanded);
        return;
      case "barValue":
        EditorGUILayout.PropertyField(property, labelBarValue, property.isExpanded);
        return;
      case "barMax":
        EditorGUILayout.PropertyField(property, labelBarMax, property.isExpanded);
        return;
      case "widthMultiplier":
        EditorGUILayout.PropertyField(property, labelWidthMultiplier, property.isExpanded);
        return;
      }
      base.DrawProperty(property);
    }
  }
}
