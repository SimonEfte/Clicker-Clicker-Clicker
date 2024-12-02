using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnParticle : MonoBehaviour
{
    public Transform particleGold;
    public ParticleSystem particleGoldParticle;
    public Transform particlePrestige;
    public ParticleSystem particlePrestigeParticle;
    public float size;

    private void Awake()
    {
        particleGold = transform.Find("Particle_GoldFalling");
        particleGoldParticle = particleGold.GetComponent<ParticleSystem>();

        particlePrestige = transform.Find("Particle_PrestigeFalling");
        particlePrestigeParticle = particlePrestige.GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        StartCoroutine(SetParticleBack());
    }

    IEnumerator SetParticleBack()
    {
        yield return new WaitForSeconds(0.05f);
        size = gameObject.transform.localScale.x;

        particleGold.gameObject.SetActive(false);
        particlePrestige.gameObject.SetActive(false);

        if (size < 0.33f)
        {
            particleGold.gameObject.SetActive(true);
            particleGoldParticle.Play();
        }
        else
        {
            particlePrestige.gameObject.SetActive(true);
            particlePrestigeParticle.Play();
        }

        yield return new WaitForSeconds(0.6f);
        ObjectPool.instance.ReturnParticleFromPool(gameObject);
    }
}
