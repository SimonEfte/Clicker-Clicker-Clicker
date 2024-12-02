using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEcircle : MonoBehaviour
{
    public Animation anim;

    private void Awake()
    {
        anim = gameObject.GetComponent<Animation>();
    }

    private void OnEnable()
    {
        int randomLayer = Random.Range(0,3);
        if(randomLayer == 0) { gameObject.layer = 8; }
        else if (randomLayer == 1) { gameObject.layer = 12; }
        else if (randomLayer == 2) { gameObject.layer = 13; }

        anim.Play();
        gameObject.transform.localScale = new Vector2(Upgrades.cursorAOE, Upgrades.cursorAOE);
        StartCoroutine(SetCirlceOff());
    }

    IEnumerator SetCirlceOff()
    {
        yield return new WaitForSeconds(0.367f);
        ObjectPool.instance.ReturnAOEfromPool(gameObject);
    }
}
