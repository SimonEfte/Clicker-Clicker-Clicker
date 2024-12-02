// (C) 2021 Alfish. All rights not expressly granted are reserved.

using UnityEngine;
using UnityEditor;

namespace Alfish.StickerStyle.BasicPack.Editor {
  [CustomEditor(typeof(ColorIndicator), true)]
  public class ColorIndicatorEditor : IndicatorEditor
  {
    static readonly GUIContent labelTargets = new GUIContent("Targets",
      "The images to be recolored according to the indicated value.");
    static readonly GUIContent labelColorIntervals = new GUIContent("Color Intervals",
      "The reference color intervals.");
    static readonly GUIContent labelLastColor = new GUIContent("Last Color",
      "The last reference color, which indicates the highest value.");
    static readonly GUIContent labelColor = new GUIContent("",
      "The last reference color, which indicates the highest value.");
    protected override void DrawProperty(SerializedProperty property) {
      switch (property.propertyPath) {
      case "targets":
        EditorGUILayout.PropertyField(property, labelTargets, property.isExpanded);
        return;
      case "colorIntervals":
        EditorGUILayout.PropertyField(property, labelColorIntervals, property.isExpanded);
        return;
      case "lastColor":
        var position = EditorGUILayout.GetControlRect();
        var rpos = EditorGUI.PrefixLabel(position, labelLastColor);
        rpos.width /= 3;
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;
        EditorGUI.LabelField(rpos, labelColor);
        property.colorValue = EditorGUI.ColorField(rpos, GUIContent.none, property.colorValue);
        EditorGUI.indentLevel = indent;
        return;
      }
      base.DrawProperty(property);
    }
  }
  
  [CustomPropertyDrawer(typeof(ColorInterval), true)]
  public class ColorIntervalDrawer : PropertyDrawer
  {
    static readonly GUIContent labelColor = new GUIContent("",
      "The color in the start of the interval.");
    static readonly GUIContent labelSmooth = new GUIContent("Smooth",
      "If true, the color has a smooth transition to the next color along this interval.");
    static readonly GUIContent labelLength = new GUIContent("",
      "The size of the interval.");
    
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
      EditorGUI.BeginProperty(position, label, property);
      //> Prefix
      var rpos = EditorGUI.PrefixLabel(position, label);
      var w = rpos.width / 3;
      var ripos = new Rect(rpos.x, rpos.y, w, rpos.height);
      var indent = EditorGUI.indentLevel;
      EditorGUI.indentLevel = 0;
      //> Fields
      var propColor = property.FindPropertyRelative("color");
      EditorGUI.LabelField(ripos, labelColor);
      propColor.colorValue = EditorGUI.ColorField(ripos, GUIContent.none, propColor.colorValue);
      ripos.x += w;
      var propSmooth = property.FindPropertyRelative("smooth");
      propSmooth.boolValue = EditorGUI.ToggleLeft(ripos, labelSmooth, propSmooth.boolValue);
      ripos.x += w;
      var propLength = property.FindPropertyRelative("length");
      EditorGUI.LabelField(ripos, labelLength);
      propLength.floatValue = Mathf.Max(0, EditorGUI.FloatField(ripos, GUIContent.none, propLength.floatValue));
      //> End
      EditorGUI.indentLevel = indent;
      EditorGUI.EndProperty();
    }
  }
}
