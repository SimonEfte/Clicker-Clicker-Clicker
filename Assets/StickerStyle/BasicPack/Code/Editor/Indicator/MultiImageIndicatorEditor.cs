// (C) 2021 Alfish. All rights not expressly granted are reserved.

using UnityEngine;
using UnityEditor;

namespace Alfish.StickerStyle.BasicPack.Editor {
  [CustomEditor(typeof(MultiImageIndicator), true)]
  public class MultiImageIndicatorEditor : IndicatorEditor
  {
    static readonly GUIContent labelPrefab = new GUIContent("Prefab",
      "An optional prefab to use when instantiating images." +
      " If not specified, a default Image object is generated." +
      " The images are instantiated as children of this game object.");
    static readonly GUIContent labelSpriteEmpty = new GUIContent("Sprite (Empty)",
      "The sprite for when the image has no fractions.");
    static readonly GUIContent labelSprites = new GUIContent("Sprites",
      "The sprites representing the possible fractions for one image, in ascending order." +
      " Each fraction corresponds to 1 unit in the indicated value.");
    protected override void DrawProperty(SerializedProperty property) {
      switch (property.propertyPath) {
      case "prefab":
        EditorGUILayout.PropertyField(property, labelPrefab, property.isExpanded);
        return;
      case "spriteEmpty":
        EditorGUILayout.PropertyField(property, labelSpriteEmpty, property.isExpanded);
        return;
      case "sprites":
        EditorGUILayout.PropertyField(property, labelSprites, property.isExpanded);
        if (property.arraySize < 1)
          EditorGUILayout.HelpBox("The list of sprites must not be empty!", MessageType.Error);
        return;
      }
      base.DrawProperty(property);
    }
  }
}
