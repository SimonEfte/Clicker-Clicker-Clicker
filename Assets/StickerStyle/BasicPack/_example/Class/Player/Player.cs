// (C) 2021 Alfish. All rights not expressly granted are reserved.

using UnityEngine;

namespace Alfish.StickerStyle_BasicPack_Example {
  public class Player : MonoBehaviour
  {
    [Header("Movement")]
    public float movementSpeed;
    public AudioClip collisionSound;
    public float collisionSoundIntensity;
    [Header("Health")]
    public int maxHealth;
    public int health { get; private set; }
    [Header("Stamina")]
    public float maxStamina;
    public float stamina { get; private set; }
    public float staminaRecovery;
    public string inputRun;
    public float runGain;
    public float runCost;
    [Header("Tension")]
    public float maxTension;
    public float targetTension { get; private set; }
    public float tension { get; private set; }
    public float tensionSpeed;
    public float tensionPenalty;
    public float tensionFreezeTime;
    
    public int coins { get; private set; }
    
    void Start() {
      body = base.GetComponent<Rigidbody>();
      g = GameObject.FindWithTag("GameController").GetComponent<GameController>();
      
      g.healthIndicator.SetMaxValue(health = maxHealth);
      
      stamina = maxStamina;
      SetStamina(1);
      
      g.inertiaBarIndicator.SetMaxValue(maxTension);
      g.inertiaBarIndicator.SetValue(tension);
      g.inertiaBarColorIndicator.SetValue(tension / maxTension);
      g.targetBarIndicator.SetMaxValue(maxTension);
      g.targetBarIndicator.SetValue(targetTension);
      
      coins = 0;
      
      g.speedIndicator.SetValue(0);
      g.speedCounter.text = "0.0 m/s";
    }
    Rigidbody body;
    GameController g;
    
    float currentPenalty;
    void FixedUpdate() {
      //> Show speed
      var v = body.linearVelocity;
      var speed = new Vector2(v.x, v.z).magnitude;
      g.speedIndicator.SetValue(speed / g.indicatorMaxSpeed);
      g.speedCounter.text = speed.ToString("N1") + " m/s";
      //> Check if not paused
      if (g.gameState != GameState.Playing)
        return;
      var dt = Time.deltaTime;
      //> Tension inertia
      if (tension < targetTension && currentPenalty <= 0)
        tension = Mathf.Min(tension + dt * tensionSpeed, targetTension);
      g.inertiaBarIndicator.SetValue(tension);
      g.inertiaBarColorIndicator.SetValue(tension / maxTension);
      currentPenalty = Mathf.Max(0, currentPenalty - Time.deltaTime);
      //> Movement Input
      var moveHorizontal = Input.GetAxis("Horizontal");
      var moveVertical = Input.GetAxis("Vertical");
      Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
      //> Run Movement / Stamina Recovery
      var isRunning = Input.GetButton(inputRun);
      if (isRunning && stamina > 0) {
        var dStamina = Mathf.Min(runCost * dt, stamina);
        stamina -= dStamina;
        SetStamina(stamina / maxStamina);
        movement *= 1 + runGain * dStamina / maxStamina;
      } else {
        stamina = Mathf.Min(stamina + staminaRecovery * dt, maxStamina);
        SetStamina(stamina / maxStamina);
      }
      body.AddForce(movement * movementSpeed);
    }
    
    void OnCollisionEnter(Collision collision) {
      //> Check if not paused
      if (g.gameState != GameState.Playing)
        return;
      var obj = collision.gameObject;
      //> Play sound when not colliding with a collectable
      if (collisionSound != null && obj.tag != "Respawn") {
        var pos = base.transform.position;
        var vol = collision.relativeVelocity.magnitude * collisionSoundIntensity;
        AudioSource.PlayClipAtPoint(collisionSound, pos, vol);
      }
    }
    
    internal void AddCoins(int amount) {
      coins += amount;
    }
    
    internal void AddDamage(int damage) {
      //> Tension penalty
      g.targetBarIndicator.SetValue(targetTension = (tension *= tensionPenalty));
      g.inertiaBarIndicator.SetValue(tension);
      g.inertiaBarColorIndicator.SetValue(tension / maxTension);
      currentPenalty = tensionFreezeTime;
      //> Decrease health and check game over
      g.healthIndicator.SetValue(health -= damage);
      if (health <= 0)
        g.GameOver(GameState.Failure);
    }
    
    internal void AddTargetTension(float targetTension) {
      //> Tension Bar is filled, unless there is still a penalty
      if (currentPenalty <= 0) {
        this.targetTension += targetTension;
        g.targetBarIndicator.SetValue(this.targetTension);
      }
    }
    
    void SetStamina(float rStamina) {
      g.staminaIndicator.SetValue(rStamina);
      g.staminaColorIndicator.SetValue(rStamina);
      g.staminaFadeIndicator.SetTargetOpacity(rStamina < 1 ? 1.0f : 0.0f);
    }
  }
}
