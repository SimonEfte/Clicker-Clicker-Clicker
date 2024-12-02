// (C) 2021 Alfish. All rights not expressly granted are reserved.

using UnityEngine;

namespace Alfish.StickerStyle.BasicPack {
  [System.Serializable]
  /// <summary>A definition containing texture and hotspot data for a cursor.</summary>
  public struct CursorDefinition
  {
    /// <summary>The texture to use for the cursor or "None" to set the default cursor.
    /// Note that a texture needs to be imported with "Read/Write enabled" in the texture importer
    /// (or using the "Cursor" defaults), in order to be used as a cursor.</summary>
    public Texture2D texture;
    /// <summary>The offset from the top left of the texture to use as the target point
    /// (must be within the bounds of the cursor).</summary>
    public Vector2 hotspot;
    
    /// <summary>Specify this to use as a cursor.</summary>
    /// <param name="cursorMode">Allow this cursor to render as a hardware cursor on supported platforms,
    /// or force software cursor.</param>
    public void SetAsCursor(CursorMode cursorMode) {
      Cursor.SetCursor(texture,
        texture == null ? default : new Vector2(hotspot.x * texture.width, hotspot.y * texture.height),
        cursorMode);
    }
  }
  
  //[CreateAssetMenu(fileName="Cursors.asset", menuName="StickerStyle/Cursor Set", order=10000)]
  /// <summary>An object that defines cursors.</summary>
  public class CursorSet : ScriptableObject
  {
    /// <summary>Allow the cursors to render as hardware cursors on supported platforms,
    /// or force software cursors.</summary>
    public CursorMode cursorMode;
    /// <summary>The available cursor definitions, containing texture and hotspot data.</summary>
    public CursorDefinition[] cursors;
    /// <summary>Specify a custom cursor from this object that you wish to use as a cursor.</summary>
    /// <param name="cursorNum">The index of the cursor to use.</param>
    public void SetCursor(int cursorNum) {
      cursors[cursorNum].SetAsCursor(cursorMode);
    }
    /// <summary>Specify a custom cursor from this object that you wish to use as a cursor.</summary>
    /// <param name="cursorNum">The index of the cursor to use.</param>
    /// <param name="cursorMode">Allow the cursor to render as a hardware cursor on supported platforms,
    /// or force software cursor.</param>
    public void SetCursor(int cursorNum, CursorMode cursorMode) {
      cursors[cursorNum].SetAsCursor(cursorMode);
    }
  }
}
