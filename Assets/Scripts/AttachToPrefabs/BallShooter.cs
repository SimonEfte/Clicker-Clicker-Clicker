using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooter : MonoBehaviour
{
    public int shooterNumber;
    public int speed;

    public static bool shootBullets, shootBullets2, shootBullets3, shootBullets4;
    public static bool shotBullet1, shotBullet2, shotBullet3, shotBullet4;

    private void OnEnable()
    {
        speed = 9;
    }

    private void Update()
    {
        if(shooterNumber == 1 && shootBullets == true)
        {
            shootBullets = false;
            Shoot();
        }
        if (shooterNumber == 2 && shootBullets2 == true)
        {
            shootBullets2 = false;
            Shoot();
        }
        if (shooterNumber == 3 && shootBullets3 == true)
        {
            shootBullets3 = false;
            Shoot();
        }
        if (shooterNumber == 4 && shootBullets4 == true)
        {
            shootBullets4 = false;
            Shoot();
        }
    }

    public void Shoot()
    {
        StartCoroutine(ShootChance());
    }

    IEnumerator ShootChance()
    {
        GameObject bullet = ObjectPool.instance.GetBulletFromPool();
        bullet.gameObject.transform.position = gameObject.transform.position;
        bullet.transform.rotation = transform.rotation;

        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        if (bulletRb != null)
        {
            // Add force in the direction the GameObject is facing (based on its rotation)
            bulletRb.velocity = transform.right * speed; // Assuming the bullet travels along the x-axis of the GameObject.
        }
        yield return new WaitForSeconds(0.1f);

        StartCoroutine(DeSpawnBullet(bullet));
    }

    IEnumerator DeSpawnBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(2f);
        ObjectPool.instance.ReturnBulletFromPool(bullet);
    }
}
