// (C) 2021 Alfish. All rights not expressly granted are reserved.

using UnityEngine;
using UnityEditor;

namespace Alfish.StickerStyle.BasicPack.Editor {
  [CustomEditor(typeof(FillIndicator), true)]
  public class FillIndicatorEditor : IndicatorEditor
  {
    static readonly GUIContent labelTarget = new GUIContent("Target",
      "The image with a filling to be changed according to the indicated value.");
    protected override void DrawProperty(SerializedProperty property) {
      switch (property.propertyPath) {
      case "target":
        EditorGUILayout.PropertyField(property, labelTarget, property.isExpanded);
        return;
      }
      base.DrawProperty(property);
    }
  }
}
