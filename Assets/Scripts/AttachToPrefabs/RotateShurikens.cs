using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateShurikens : MonoBehaviour
{
    int speed;
    bool isBoomerangOut;

    public Vector3 startPositionBoomrang = new Vector3(0, 0, 0);
    public Vector3 endPositionBoomrang = new Vector3(-1200, 0, 0);

    private void Awake()
    {
        speed = -1000;
        if (gameObject.name == "SpikeBall(Clone)") { speed = -100; }
        if (gameObject.name == "ProjectileBall") { speed = -200; }
        if (gameObject.name == "GoldenFistGlow") { speed = -40; }
        if (gameObject.name == "Boomerang") { speed = -800; }
        if (gameObject.name == "BoomerangPrefab(Clone)") { speed = -300;  }
        if (gameObject.name == "BoomerangOut") { speed = 0; isBoomerangOut = true; }
    }

    private void OnEnable()
    {
        if(isBoomerangOut == true) 
        {
            float randomSpeed = Random.Range(4.5f, 5.5f);
            StartCoroutine(MoveBoomerangOut(startPositionBoomrang, endPositionBoomrang, randomSpeed)); 
        }
        else
        {
            StartCoroutine(Rotate());
        }
    }

    private IEnumerator Rotate()
    {
        while (true)
        {
            gameObject.transform.Rotate(0, 0, speed * Time.deltaTime);

            yield return null;
        }
    }

    private IEnumerator MoveBoomerangOut(Vector3 start, Vector3 end, float time)
    {
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            gameObject.transform.localPosition = Vector3.Lerp(start, end, elapsedTime / time);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.position = end;

        int random = 0;
        if (Achievements.achSaves[28] == true)
        {
            random = Random.Range(0,7);
        }

        if(random == 6)
        {
            StartCoroutine(MoveBoomerangBack(endPositionBoomrang, startPositionBoomrang, 2f)); 
        }
        else
        {
            ObjectPool.instance.ReturnBoomerangFromPool(gameObject);
        }
       
    }

    IEnumerator MoveBoomerangBack(Vector3 start, Vector3 end, float time)
    {
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            gameObject.transform.localPosition = Vector3.Lerp(start, end, elapsedTime / time);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.position = end;
        ObjectPool.instance.ReturnBoomerangFromPool(gameObject);
    }
}
