// (C) 2021 Alfish. All rights not expressly granted are reserved.

using UnityEngine;

namespace Alfish.StickerStyle_BasicPack_Example {
  public class CollectedCoin : MonoBehaviour
  {
    void Awake() {
      g = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }
    GameController g;
    
    void OnDestroy() {
      var cc = g.coinsCounter;
      cc.text = (int.Parse(cc.text) + 1).ToString("D2");
    }
  }
}
