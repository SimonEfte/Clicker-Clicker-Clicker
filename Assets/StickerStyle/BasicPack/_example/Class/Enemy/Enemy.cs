// (C) 2021 Alfish. All rights not expressly granted are reserved.

using UnityEngine;

namespace Alfish.StickerStyle_BasicPack_Example {
  public class Enemy : MonoBehaviour
  {
    [Header("Movement")]
    public float speed;
    public float movementDuration;
    [Range(0, 1)] public float movementAgility;
    public float collisionSoundIntensity;
    [Header("Player Interaction")]
    public int damage;
    public float tensionIncrement;
    public Color tensionColor;
    
    void Start() {
      g = GameObject.FindWithTag("GameController").GetComponent<GameController>();
      body = GetComponent<Rigidbody>();
      material = base.GetComponent<MeshRenderer>().material;
      baseColor = Shader.PropertyToID("_BaseColor");
      md = movementDuration;
    }
    GameController g;
    Rigidbody body;
    Material material;
    int baseColor;
    void OnDestroy() {
      Object.Destroy(material);
    }
    
    Vector2 movement, intendedMovement;
    float t, md;
    void FixedUpdate() {
      //> Check if not paused
      if (g.gameState != GameState.Playing)
        return;
      t += Time.deltaTime;
      //> If it's time to change intended movement, choose direction randomly
      if (t >= md) {
        t -= md;
        md = (0.5f + Random.value / 2) * movementDuration;
        var r = Random.value * 2 * Mathf.PI;
        intendedMovement = new Vector2(Mathf.Cos(r), Mathf.Sin(r));
      }
      //> Converge movement to intended
      movement = Vector2.Lerp(movement, intendedMovement, movementAgility);
      //> Apply movement
      var velocity = movement * speed;
      body.AddForce(new Vector3(velocity.x, 0, velocity.y));
    }
    
    void OnCollisionEnter(Collision collision) {
      //> Check if not paused
      if (g.gameState != GameState.Playing)
        return;
      var obj = collision.gameObject;
      //> Invert intended movement when colliding with any object (except the ground)
      if (obj.name != "Floor")
        intendedMovement = -intendedMovement;
      //> Play sound when colliding
      if (g.collisionSound != null) {
        var pos = base.transform.position;
        var vol = collision.relativeVelocity.magnitude * collisionSoundIntensity;
        AudioSource.PlayClipAtPoint(g.collisionSound, pos, vol);
      }
      //> When colliding with the player
      if (obj.tag == "Player") {
        var player = obj.GetComponent<Player>();
        //> Damage the player
        player.AddDamage(damage);
        //> Generate impact particles on contact point
        var p = Object.Instantiate(g.impactParticles);
        p.transform.position = collision.contacts[0].point;
      }
    }
    
    void setColor(Color color) {
      material.color = color;
      material.SetColor(baseColor, color);
    }
    void OnTriggerEnter(Collider other) {
      var obj = other.gameObject;
      if (obj.tag == "Player")
        setColor(tensionColor);
    }
    void OnTriggerStay(Collider other) {
      //> Check if not paused
      if (g.gameState != GameState.Playing)
        return;
      //> Fill target tension bar of the player while near the enemy
      var obj = other.gameObject;
      if (obj.tag == "Player") {
        var player = obj.GetComponent<Player>();
        player.AddTargetTension(tensionIncrement * Time.deltaTime);
      }
    }
    void OnTriggerExit(Collider other) {
      var obj = other.gameObject;
      if (obj.tag == "Player")
        setColor(Color.white);
    }
  }
}
