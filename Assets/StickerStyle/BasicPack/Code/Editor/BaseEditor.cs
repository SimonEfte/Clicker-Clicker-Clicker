// (C) 2021 Alfish. All rights not expressly granted are reserved.

using UnityEngine;
using UnityEditor;

namespace Alfish.StickerStyle.BasicPack.Editor {
  public abstract class BaseEditor : UnityEditor.Editor
  {
    public override void OnInspectorGUI() {
      serializedObject.Update();
      var property = serializedObject.GetIterator();
      if (property.NextVisible(true)) {
        //< root property (contains children)
        do {
          //< property = child[i]
          if (property.propertyPath == "m_Script") {
            //< script property not shown, so it looks like a library
            continue;
          }
          //> draw according to method
          DrawProperty(property);
        } while (property.NextVisible(false));
      }
      serializedObject.ApplyModifiedProperties();
    }
    
    protected virtual void DrawProperty(SerializedProperty property) {
      EditorGUILayout.PropertyField(property, property.isExpanded);
    }
  }
  
  [CustomEditor(typeof(Indicator), true)]
  public class IndicatorEditor : BaseEditor
  {
    protected new Indicator target { get { return base.target as Indicator; } }
  }
}
