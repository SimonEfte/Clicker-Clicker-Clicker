// (C) 2021 Alfish. All rights not expressly granted are reserved.

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Alfish.StickerStyle.BasicPack;

namespace Alfish.StickerStyle_BasicPack_Example {
  public enum GameState : byte { Playing, Paused, Failure, Success }
  
  public class GameController : MonoBehaviour
  {
    public GameState gameState { get; private set; }
    [Header("Audio")]
    public AudioSource gameAudio;
    public AudioClip collisionSound;
    public AudioClip coinSound;
    [Header("Cursors")]
    public CursorSet cursors;
    public int cursorDefault;
    [Header("Start/Pause Actions")]
    public string startButtonInput;
    public GameObject playButton, pauseButton;
    [Header("Time")]
    public float initialTime;
    public FloatIndicator timeIndicator;
    public Text timeCounter;
    [Header("Game Over")]
    public Image gameOverImage;
    public Sprite failureSprite, missedCoinsSprite, noBarSprite, gotHitSprite, successSprite;
    [Header("Health")]
    public BoundedIntegerIndicator healthIndicator;
    public ParticleSystem impactParticles;
    [Header("Stamina")]
    public FloatIndicator staminaIndicator;
    public ColorIndicator staminaColorIndicator;
    public FadeIndicator staminaFadeIndicator;
    [Header("Tension Bar")]
    public BoundedFloatIndicator inertiaBarIndicator;
    public ColorIndicator inertiaBarColorIndicator;
    public BoundedFloatIndicator targetBarIndicator;
    [Header("Coins")]
    public ScreenCollectibleIndicator coinsIndicator;
    public Text coinsCounter;
    public Coin coinPrefab;
    public int coinsToSpawn;
    public Vector3 coinSpawnPlane;
    public float coinAngularVelocity;
    public ParticleSystem shineParticles;
    [Header("Speed")]
    public float indicatorMaxSpeed;
    public FloatIndicator speedIndicator;
    public Text speedCounter;
    
    void Start() {
      time = initialTime;
      coinsLeftToSpawn = coinsToSpawn;
      cursors.SetCursor(cursorDefault);
      gameOverImage.gameObject.SetActive(false);
      Play();
    }
    
    float time;
    int coinsLeftToSpawn;
    
    void FixedUpdate() {
      //> Check if not paused
      if (gameState != GameState.Playing)
        return;
      var dt = Time.deltaTime;
      //> If game timer reached zero, it's game over
      time -= dt;
      if (time <= 0) {
        time = 0;
        GameOver(GameState.Success);
      } else {
        var it2 = initialTime / 2;
        while (coinsLeftToSpawn > 0 && time <= it2 + it2 * coinsLeftToSpawn / coinsToSpawn) {
          var coin = Object.Instantiate(coinPrefab);
          var x = (Random.value - 0.5f) * coinSpawnPlane.x;
          var z = (Random.value - 0.5f) * coinSpawnPlane.z;
          coin.transform.position = new Vector3(x, coinSpawnPlane.y, z);
          coinsLeftToSpawn--;
        }
      }
    }
    
    void Update() {
      var dt = Time.deltaTime;
      //> Spin all coins sync
      Coin.Rotate(coinAngularVelocity * dt);
      //> Display Timer
      var t = Mathf.CeilToInt(time);
      timeCounter.text = t.ToString("D2");
      timeIndicator.SetValue(t / initialTime);
      //> Start/Pause Actions (Play, Pause, Reset)
      if (Input.GetButtonDown(startButtonInput)) {
        switch (gameState) {
        case GameState.Playing:
          Pause();
          return;
        case GameState.Paused:
          Play();
          return;
        case GameState.Failure:
        case GameState.Success:
          Reset();
          return;
        }
      }
    }
    
    internal void Play() {
      playButton.SetActive(false);
      pauseButton.SetActive(true);
      gameState = GameState.Playing;
      Time.timeScale = 1;
    }
    internal void Pause() {
      playButton.SetActive(true);
      pauseButton.SetActive(false);
      gameState = GameState.Paused;
      Time.timeScale = 0;
    }
    internal void Reset() {
      var scene = SceneManager.GetActiveScene();
      SceneManager.LoadScene(scene.path);
    }
    internal void GameOver(GameState newState) {
      if (newState == GameState.Failure)
        gameOverImage.sprite = failureSprite;
      else if (newState == GameState.Success) {
        var player = GameObject.FindWithTag("Player").GetComponent<Player>();
        if (player.coins < coinsToSpawn)
          gameOverImage.sprite = missedCoinsSprite;
        else if (player.tension < player.maxTension)
          gameOverImage.sprite = noBarSprite;
        else if (player.health < player.maxHealth)
          gameOverImage.sprite = gotHitSprite;
        else
          gameOverImage.sprite = successSprite;
      } else return;
      staminaFadeIndicator.fadeOutDelay = 0;
      staminaFadeIndicator.SetTargetOpacity(0);
      playButton.SetActive(false);
      pauseButton.SetActive(false);
      gameOverImage.gameObject.SetActive(true);
      gameState = newState;
    }
  }
}
