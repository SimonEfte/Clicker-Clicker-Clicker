using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBall : MonoBehaviour
{
    public float bounceSpeed = 9f;
    private Rigidbody2D rb;
    Vector3 lastVelocity;
    public bool isBouncy, isBigBouncy, isTurret;

    public static int bouncyballDirection, bouncyballDirection2;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        StartCoroutine(Waititn());
    }

    IEnumerator Waititn()
    {
        float randomWait = Random.Range(0.1f, 2f);
        yield return new WaitForSeconds(randomWait);

        PlaceBallAndShoot();
    }

    IEnumerator CorrectBallPos()
    {
        isSpeed = false;

        gameObject.transform.localPosition = new Vector2(0, 0);

        rb = GetComponent<Rigidbody2D>();

        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        if (isBouncy == true) { rb.velocity = rb.velocity.normalized * 0; }
        if (isBigBouncy == true) { rb.velocity = rb.velocity.normalized * 0; }
        yield return new WaitForSeconds(1f);

        rb.constraints = RigidbodyConstraints2D.None;
        PlaceBallAndShoot();
    }

    public void PlaceBallAndShoot()
    {
        isSpeed = true;
        int random = Random.Range(0, 4);

        if (gameObject.name == "BouncyBall") { bouncyballDirection = random; }
        if (gameObject.name == "BouncyBallFromACH")
        {
            if (bouncyballDirection == 0 && random == 0) { random = 1; bouncyballDirection = random; }
            if (bouncyballDirection == 1 && random == 1) { random = 0; bouncyballDirection = random; }
            if (bouncyballDirection == 2 && random == 2) { random = 3; bouncyballDirection = random; }
            if (bouncyballDirection == 3 && random == 3) { random = 2; bouncyballDirection = random; }
        }

        if (gameObject.name == "BigBouncyBall") { bouncyballDirection2 = random; }
        if (gameObject.name == "BigBouncyBallFromACH")
        {
            if (bouncyballDirection2 == 0 && random == 0) { random = 1; bouncyballDirection2 = random; }
            if (bouncyballDirection2 == 1 && random == 1) { random = 0; bouncyballDirection2 = random; }
            if (bouncyballDirection2 == 2 && random == 2) { random = 3; bouncyballDirection2 = random; }
            if (bouncyballDirection2 == 3 && random == 3) { random = 2; bouncyballDirection2 = random; }
        }

        int randomPos = Random.Range(1, 5);
        if (randomPos == 1) { gameObject.transform.localPosition = new Vector2(50, 50); }
        if (randomPos == 1) { gameObject.transform.localPosition = new Vector2(-50, -50); }
        if (randomPos == 1) { gameObject.transform.localPosition = new Vector2(-50, 50); }
        if (randomPos == 1) { gameObject.transform.localPosition = new Vector2(50, -50); }

        gameObject.transform.localPosition = new Vector2(0, 0);

        if (gameObject.name == "BigBouncyBall" || gameObject.name == "BigBouncyBallFromACH") { bounceSpeed = Upgrades.projectileChance[8]; isBigBouncy = true; }
        else if (gameObject.name == "ProjectileBall") { bounceSpeed = 9; isTurret = true; }
        else { bounceSpeed = Upgrades.projectileChance[4]; isBouncy = true; }

        if (random == 0) { rb.velocity = new Vector2(-1, -1) * bounceSpeed; }
        if (random == 1) { rb.velocity = new Vector2(1, 1) * bounceSpeed; }
        if (random == 2) { rb.velocity = new Vector2(1, -1) * bounceSpeed; }
        if (random == 3) { rb.velocity = new Vector2(-1, 1) * bounceSpeed; }
    }

    bool isSpeed;

    private void FixedUpdate()
    {
        lastVelocity = rb.velocity;

        if(isSpeed == true)
        {
            if (isBouncy)
            {
                float currentBounceSpeed = Upgrades.projectileChance[4];

                rb.velocity = rb.velocity.normalized * currentBounceSpeed;
            }
            if (isBigBouncy)
            {
                float currentBounceSpeed = Upgrades.projectileChance[8];

                rb.velocity = rb.velocity.normalized * currentBounceSpeed;
            }
        }
    }

    int hitCorner;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 10 || collision.gameObject.layer == 17)
        {
            if(collision.gameObject.layer == 17)
            {
                hitCorner += 1;
                if(hitCorner > 2)
                {
                    hitCorner = 0;
                    StartCoroutine(CorrectBallPos());
                }
            }
            else { hitCorner = 0; }

            var speed = lastVelocity.magnitude;
            var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

            rb.velocity = direction * Mathf.Max(speed, 0);

            if(isBouncy == true) { Stats.ballBounced += 1; }
            if (isBigBouncy == true) { Stats.bigBallBounced += 1; }
        }
    }
}
