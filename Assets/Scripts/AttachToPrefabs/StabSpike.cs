using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StabSpike : MonoBehaviour
{
    private void OnEnable()
    {
        gameObject.transform.localPosition = new Vector2(-260,0);
     
        StartCoroutine(StabTheSpike());
    }

    //to -20

    IEnumerator StabTheSpike()
    {
        float reachPoint = 30f;
        float startPoint = -260f;
        float duration = 0.2f; 
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float newPositionX = Mathf.Lerp(startPoint, reachPoint, elapsedTime / duration);
            gameObject.transform.localPosition = new Vector2(newPositionX, 0);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        gameObject.transform.localPosition = new Vector2(reachPoint, 0);

        yield return new WaitForSeconds(1f);

        StartCoroutine(SetSpikeBack());
    }

    IEnumerator SetSpikeBack()
    {
        float reachPoint = -260;
        float startPoint = 30;
        float duration = 0.2f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float newPositionX = Mathf.Lerp(startPoint, reachPoint, elapsedTime / duration);
            gameObject.transform.localPosition = new Vector2(newPositionX, 0);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        gameObject.transform.localPosition = new Vector2(reachPoint, 0);
    }
}
