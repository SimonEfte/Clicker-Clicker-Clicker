using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LaserMechanics : MonoBehaviour
{
    public Transform laserImage, laserCollider, laserImage2, laserCollider2, laserImage3, laserCollider3, laserImage4, laserCollider4;
    public Animation animationLaser, animationLaser2, animationLaser3, animationLaser4;
    public Image laserImageComponent, laserImageComponent2, laserImageComponent3, laserImageComponent4;

    public void Awake()
    {
        laserCollider = transform.Find("LaserCollider");
        laserImage = transform.Find("LaserImage");
        animationLaser = laserCollider.GetComponent<Animation>();
        laserImageComponent = laserImage.GetComponent<Image>();

        laserCollider2 = transform.Find("LaserCollider2");
        laserImage2 = transform.Find("LaserImage2");
        animationLaser2 = laserCollider2.GetComponent<Animation>();
        laserImageComponent2 = laserImage2.GetComponent<Image>();

        laserCollider3 = transform.Find("LaserCollider3");
        laserImage3 = transform.Find("LaserImage3");
        animationLaser3 = laserCollider3.GetComponent<Animation>();
        laserImageComponent3 = laserImage3.GetComponent<Image>();

        laserCollider4 = transform.Find("LaserCollider4");
        laserImage4 = transform.Find("LaserImage4");
        animationLaser4 = laserCollider4.GetComponent<Animation>();
        laserImageComponent4 = laserImage4.GetComponent<Image>();
    }

    public float laserFillAmount;
    public bool shot4Laser;

    private void OnEnable()
    {
        shot4Laser = false;
        laserImageComponent.fillAmount = 0;
        laserFillAmount = 0;

        laserCoroutine = StartCoroutine(ShootLaser(laserImageComponent, animationLaser, 1));
        rotationCoroutine = StartCoroutine(SetRotation());

        animationLaser2.gameObject.SetActive(false);
        animationLaser3.gameObject.SetActive(false);
        animationLaser4.gameObject.SetActive(false);

        laserImageComponent2.fillAmount = 0;
        laserImageComponent3.fillAmount = 0;
        laserImageComponent4.fillAmount = 0;

        if (Achievements.achSaves[30] == true)
        {
            int random = Random.Range(1,10);
            if(random == 5)
            {
                animationLaser2.gameObject.SetActive(true);
                animationLaser3.gameObject.SetActive(true);
                animationLaser4.gameObject.SetActive(true);

                shot4Laser = true;
                laserCoroutine2 = StartCoroutine(ShootLaser(laserImageComponent2, animationLaser2, 4));

                laserCoroutine3 = StartCoroutine(ShootLaser(laserImageComponent3, animationLaser3, 3));

                laserCoroutine4 = StartCoroutine(ShootLaser(laserImageComponent4, animationLaser4, 2));
            }
        }

        StartCoroutine(SetGameObjectInactive());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public Coroutine laserCoroutine, laserCoroutine2, laserCoroutine3, laserCoroutine4, rotationCoroutine;

    IEnumerator ShootLaser(Image fillOBject, Animation anim, int laser)
    {
        yield return new WaitForSeconds(0.2f);
        if(laser == 1) { anim.Play("LaserAnim"); }
        if (laser == 2) { anim.Play("LaserAnim2"); }
        if (laser == 3) { anim.Play("LaserAnim3"); }
        if (laser == 4) { anim.Play("LaserAnim4"); }

        fillOBject.fillAmount = 0;

        float duration = 0.3f; 
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            fillOBject.fillAmount = Mathf.Lerp(0f, 1f, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        fillOBject.fillAmount = 1f;
    }

    IEnumerator SetRotation()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float angle = Mathf.Atan2(randomDirection.y, randomDirection.x) * Mathf.Rad2Deg;
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        while (true) //6
        {
            gameObject.transform.Rotate(0, 0, 10 * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator SetGameObjectInactive()
    {
        yield return new WaitForSeconds(3f);
        StopCoroutine(rotationCoroutine);
        //StopCoroutine(laserCoroutine);
        ObjectPool.instance.ReturnLaserFromPool(gameObject);
    }
}
