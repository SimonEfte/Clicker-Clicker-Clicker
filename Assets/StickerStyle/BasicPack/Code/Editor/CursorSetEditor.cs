// (C) 2021 Alfish. All rights not expressly granted are reserved.

using UnityEngine;
using UnityEditor;

namespace Alfish.StickerStyle.BasicPack.Editor {
  [CustomEditor(typeof(CursorSet), true)]
  public class CursorSetEditor : BaseEditor
  {
    /// <summary>Creates a new cursor asset in the project.</summary>
    [MenuItem("Assets/Create/StickerStyle/Cursor Set", false, 10000)]
    public static void CreateAsset() {
      var asset = ScriptableObject.CreateInstance<CursorSet>();
      ProjectWindowUtil.CreateAsset(asset, "Cursors.asset");
    }
    
    public override void OnInspectorGUI() {
      previewShow = false;
      base.OnInspectorGUI();
      if (previewShow)
        CursorDefinitionDrawer.DrawCursorPreview(previewPos, previewTexture, previewHotspot, 4, colorHotspot);
    }
    static Rect previewPos;
    static Texture previewTexture = null;
    static Vector2 previewHotspot;
    static readonly Color colorHotspot = new Color(0.5f, 0.5f, 0.5f, 0.25f);
    static bool previewShow;
    public static void ShowCursorPreview(Rect pos, Texture texture, Vector2 hotspot) {
      pos.min -= pos.size * 3;
      if (pos.y < 0) pos.y = 0;
      previewPos = pos;
      previewTexture = texture;
      previewHotspot = hotspot;
      previewShow = true;
    }
    
    static readonly GUIContent labelCursorMode = new GUIContent("Cursor Mode",
      "Allow the cursors to render as hardware cursors on supported platforms, or force software cursors.\n"
      + "It might be better to force software cursors if their size is greater than 32x32 px."
    );
    static readonly GUIContent labelCursors = new GUIContent("Cursors",
      "The available cursor definitions, containing texture and hotspot data.");
    protected override void DrawProperty(SerializedProperty property) {
      switch (property.propertyPath) {
      case "cursorMode":
        EditorGUILayout.PropertyField(property, labelCursorMode, property.isExpanded);
        return;
      case "cursors":
        EditorGUILayout.PropertyField(property, labelCursors, property.isExpanded);
        return;
      }
      base.DrawProperty(property);
    }
  }
  
  [CustomPropertyDrawer(typeof(CursorDefinition), true)]
  public class CursorDefinitionDrawer : PropertyDrawer
  {
    static readonly GUIContent labelTexture = new GUIContent("Texture",
      "The texture to use for the cursor. Note that, in order to be used as a cursor, a texture needs to be imported"
      + " with \"Read/Write enabled\" in the texture importer (or using the \"Cursor\" defaults)."
    );
    const string hintHotspot = "The offset from the top left of the texture to use as the target point."
      + " Must be within the bounds of the cursor.\n";
    static readonly GUIContent labelHotspotRelative = new GUIContent("Hotspot", hintHotspot
      + "* This value is stored relative to the texture size.");
    static readonly GUIContent labelHotspotAbsolute = new GUIContent("(px)", hintHotspot
      + "* This field shows the absolute value, in pixels.");
    static readonly Color colorHotspot = new Color(0.5f, 0.5f, 0.5f, 0.75f);
    static readonly Color colorBg = new Color(0.5f, 0.5f, 0.5f, 0.25f);
    
    static readonly GUIContent blankLabel = new GUIContent(" "), cachedLabel = new GUIContent();
    static readonly GUIContent[] xyLabels = new[] { new GUIContent("X"), new GUIContent("Y") };
    static readonly float[] xyVals = new float[2];
    static readonly GUIStyle styleLabel = new GUIStyle(EditorStyles.label) {
      margin = new RectOffset(),
      padding = new RectOffset(),
    };
    
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
      return property.isExpanded ? 64 : 16;
    }
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
      EditorGUI.BeginProperty(position, label, property);
      //> Title Prefix and Foldout
      var isExpanded = property.isExpanded;
      var pos = new Rect(position.x, position.y, position.width, 16);
      if (isExpanded)
        pos.width -= 64 + 4;
      var rPos = EditorGUI.PrefixLabel(pos, blankLabel);
      var lPos = new Rect(pos.x, pos.y, pos.width - rPos.width, pos.height);
      property.isExpanded = EditorGUI.Foldout(lPos, isExpanded, label, true);
      var indent = EditorGUI.indentLevel;
      var iconSize = EditorGUIUtility.GetIconSize();
      EditorGUIUtility.SetIconSize(new Vector2(16, 16));
      //> Title Label
      EditorGUI.indentLevel = 0;
      var propTexture = property.FindPropertyRelative("texture");
      var valTexture = propTexture.objectReferenceValue as Texture2D;
      float texWidth, texHeight;
      if (valTexture == null) {
        texWidth = texHeight = 1;
        cachedLabel.text = "<None>";
        cachedLabel.image = EditorGUIUtility.ObjectContent(null, typeof(Texture2D)).image;
      } else {
        texWidth = valTexture.width;
        texHeight = valTexture.height;
        cachedLabel.text = valTexture.name;
        cachedLabel.image = valTexture;
      }
      EditorGUI.LabelField(rPos, cachedLabel, styleLabel);
      //> Fields
      if (isExpanded) {
        //> Texture Field
        pos.y += 16;
        EditorGUI.indentLevel = indent + 1;
        rPos = EditorGUI.PrefixLabel(pos, labelTexture);
        EditorGUI.indentLevel = 0;
        var newTexture = EditorGUI.ObjectField(rPos, valTexture, typeof(Texture2D), false) as Texture2D;
        if (newTexture != valTexture)
          propTexture.objectReferenceValue = newTexture;
        //> Hotspot Fields
        var propHotspot = property.FindPropertyRelative("hotspot");
        var valHotspot = propHotspot.vector2Value;
        //>  Hotspot Relative
        pos.y += 16;
        EditorGUI.indentLevel = indent + 1;
        rPos = EditorGUI.PrefixLabel(pos, labelHotspotRelative);
        if (rPos.width > 200)
          rPos.width = 200;
        EditorGUI.indentLevel = 0;
        xyVals[0] = valHotspot.x;
        xyVals[1] = valHotspot.y;
        EditorGUI.MultiFloatField(rPos, xyLabels, xyVals);
        var newHotspot = new Vector2(xyVals[0], xyVals[1]);
        //>  Hotspot Absolute
        pos.y += 16;
        EditorGUI.indentLevel = indent + 2;
        rPos = EditorGUI.PrefixLabel(pos, labelHotspotAbsolute);
        if (rPos.width > 200)
          rPos.width = 200;
        EditorGUI.indentLevel = 0;
        xyVals[0] = valHotspot.x * texWidth;
        xyVals[1] = valHotspot.y * texHeight;
        EditorGUI.MultiFloatField(rPos, xyLabels, xyVals);
        var newAbsX = xyVals[0] / texWidth;
        if (newAbsX != valHotspot.x)
          newHotspot.x = newAbsX;
        var newAbsY = xyVals[1] / texHeight;
        if (newAbsY != valHotspot.y)
          newHotspot.y = newAbsY;
        //>  Hotspot Update
        if (newHotspot.x != valHotspot.x || newHotspot.y != valHotspot.y)
          propHotspot.vector2Value = newHotspot;
        //> Preview Image
        pos = new Rect(position.xMax - 64, position.y, 64, 64);
        DrawCursorPreview(pos, valTexture, newHotspot, 1, colorHotspot);
        EditorGUIUtility.AddCursorRect(pos, MouseCursor.Zoom);
        if (GUI.RepeatButton(pos, "", GUIStyle.none))
          CursorSetEditor.ShowCursorPreview(pos, valTexture, newHotspot);
      }
      EditorGUIUtility.SetIconSize(iconSize);
      EditorGUI.indentLevel = indent;
      EditorGUI.EndProperty();
    }
    public static void DrawCursorPreview(Rect pos, Texture texture, Vector2 hotspot, float lineSize, Color lineColor) {
      EditorGUI.DrawRect(pos, colorBg);
      if (texture != null)
        GUI.DrawTexture(pos, texture, ScaleMode.ScaleToFit);
      //> Preview Hotspot
      var xSpot = hotspot.x * pos.width;
      var xPos = new Rect(pos.x + xSpot, pos.y, lineSize, pos.height);
      EditorGUI.DrawRect(xPos, lineColor);
      var ySpot = hotspot.y * pos.height;
      var yPos = new Rect(pos.x, pos.y + ySpot, pos.width, lineSize);
      EditorGUI.DrawRect(yPos, lineColor);
    }
  }
}
