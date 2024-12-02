using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderScript : MonoBehaviour
{
    public TrailRenderer trail;

    private void Awake()
    {
        trail = gameObject.GetComponent<TrailRenderer>();      
    }

    private void OnEnable()
    {
        if(MobileScript.isMobile == false) { trail.startWidth = 2.0f; trail.endWidth = 0.1f; }
        else { trail.startWidth = 0.86f; trail.endWidth = 0.07f; }

        if (Achievements.achSaves[24] == true)
        {
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.1f);
        if (gameObject.transform.localScale.x > 45)
        {
            if (MobileScript.isMobile == false) { trail.startWidth = 3.75f; trail.endWidth = 1f; }
            else { trail.startWidth = 1.75f; trail.endWidth = 1f; }
        }
    }
}
