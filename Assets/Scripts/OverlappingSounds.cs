using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlappingSounds : MonoBehaviour
{
    public AudioSource projectileSource, clickSource, fallingSource, laserSource, boulderSource, spikeSource, arrowSource, spikeBallSource;
    public AudioClip projectilSound, clickSound, fallingSound, laserSound, arrowSound, spikeBallSound;
    public AudioClip boulder1, boulder2, boulder3, boulder4;
    public AudioClip spike1, spike2, spike3;

    public void PlaySound(int soundNumber)
    {
        if(soundNumber == 1) 
        {
            float randomPitch = Random.Range(0.75f, 1.35f);
            float randomVolume = Random.Range(0.7f, 0.8f);
            projectileSource.pitch = randomPitch;
            projectileSource.volume = randomVolume;
            projectileSource.PlayOneShot(projectilSound);
        }
        if (soundNumber == 2)
        {
            float randomPitch = Random.Range(0.8f, 1.2f);
            float randomVolume = Random.Range(0.75f, 0.96f);
            clickSource.pitch = randomPitch;
            clickSource.volume = randomVolume;
            clickSource.PlayOneShot(clickSound);
        }
        else if (soundNumber == 3)
        {
            float randomPitch = Random.Range(1f, 1.6f);
            float randomVolume = Random.Range(0.7f, 0.8f);
            fallingSource.pitch = randomPitch;
            fallingSource.volume = randomVolume;
            fallingSource.PlayOneShot(fallingSound);
        }
        else if (soundNumber == 4)
        {
            float randomPitch = Random.Range(1f, 1.23f);
            laserSource.pitch = randomPitch;
            laserSource.PlayOneShot(laserSound);
        }
        else if (soundNumber == 5)
        {
            float randomPitch = Random.Range(0.65f, 0.8f);
            boulderSource.pitch = randomPitch;

            int random = Random.Range(1,5);
            if(random == 1) { boulderSource.PlayOneShot(boulder1); }
            if (random == 2) { boulderSource.PlayOneShot(boulder2); }
            if (random == 3) { boulderSource.PlayOneShot(boulder3); }
            if (random == 4) { boulderSource.PlayOneShot(boulder4); }
        }
        else if (soundNumber == 6)
        {
            float randomPitch = Random.Range(1.1f, 1.5f);
            spikeSource.pitch = randomPitch;

            int random = Random.Range(1, 4);
            if (random == 1) { spikeSource.PlayOneShot(spike1); }
            if (random == 2) { spikeSource.PlayOneShot(spike1); }
            if (random == 3) { spikeSource.PlayOneShot(spike1); }
        }
        else if (soundNumber == 7)
        {
            float randomPitch = Random.Range(0.85f, 1.12f);
            arrowSource.pitch = randomPitch;

            arrowSource.PlayOneShot(arrowSound);
        }
        else if (soundNumber == 8)
        {
            float randomPitch = Random.Range(0.85f, 1.12f);
            spikeBallSource.pitch = randomPitch;

            spikeBallSource.PlayOneShot(spikeBallSound);
        }
    }
}
