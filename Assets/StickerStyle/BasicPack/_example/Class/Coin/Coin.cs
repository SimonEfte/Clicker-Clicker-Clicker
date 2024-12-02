// (C) 2021 Alfish. All rights not expressly granted are reserved.

using UnityEngine;

namespace Alfish.StickerStyle_BasicPack_Example {
  public class Coin : MonoBehaviour
  {
    static float globalRotation;
    internal static void Rotate(float ro) {
      globalRotation += ro;
      while (globalRotation >= 360)
        globalRotation -= 360;
      while (globalRotation < 0)
        globalRotation += 360;
    }
    
    public RectTransform collectedCoin;
    
    void Awake() {
      g = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }
    GameController g;
    
    void FixedUpdate() {
      base.transform.localEulerAngles = new Vector3(0, Coin.globalRotation, 0);
    }
    
    void OnCollisionEnter(Collision collision) {
      //> Check if not paused
      if (g.gameState != GameState.Playing)
        return;
      var obj = collision.gameObject;
      //> When colliding with the player
      if (obj.tag == "Player") {
        var pos = base.transform.position;
        var player = obj.GetComponent<Player>();
        //> Add coin to the player
        player.AddCoins(1);
        //> Play coin sound
        if (g.coinSound != null)
          g.gameAudio.PlayOneShot(g.coinSound);
        //> Generate shine particles on this position
        var p = Object.Instantiate(g.shineParticles);
        p.transform.position = pos;
        //> Spawn a coin sprite in the collectible indicator
        var c = Object.Instantiate(collectedCoin);
        var rt = (RectTransform)c.transform;
        rt.SetParent(g.coinsIndicator.transform, false);
        g.coinsIndicator.Add(rt, pos, (Vector2)base.transform.localScale);
        //> Self Destruct
        Object.Destroy(base.gameObject);
      }
    }
  }
}
