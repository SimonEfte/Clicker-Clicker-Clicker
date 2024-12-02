// (C) 2021 Alfish. All rights not expressly granted are reserved.

using UnityEngine;
using UnityEditor;

namespace Alfish.StickerStyle.BasicPack.Editor {
  [CustomEditor(typeof(SingleImageIndicator), true)]
  public class SingleImageIndicatorEditor : IndicatorEditor
  {
    static readonly GUIContent labelTarget = new GUIContent("Target",
      "The image with a sprite to be changed depending on the indicated value.");
    static readonly GUIContent labelRoundingMode = new GUIContent("Rounding Mode",
      "The method to be used when rounding the indicated value [0.0 ~ 1.0]" +
      " to its corresponding sprite index (integer).");
    static readonly GUIContent labelSpriteEmpty = new GUIContent("Sprite (Empty)",
      "The sprite for when the image has no fractions.");
    static readonly GUIContent labelSprites = new GUIContent("Sprites",
      "The sprites representing the possible fractions for the image, in ascending order." +
      " The last fraction represents 1.0 in the indicated value.");
    protected override void DrawProperty(SerializedProperty property) {
      switch (property.propertyPath) {
      case "target":
        EditorGUILayout.PropertyField(property, labelTarget, property.isExpanded);
        return;
      case "roundingMode":
        EditorGUILayout.PropertyField(property, labelRoundingMode, property.isExpanded);
        return;
      case "spriteEmpty":
        EditorGUILayout.PropertyField(property, labelSpriteEmpty, property.isExpanded);
        return;
      case "sprites":
        EditorGUILayout.PropertyField(property, labelSprites, property.isExpanded);
        return;
      }
      base.DrawProperty(property);
    }
  }
}
