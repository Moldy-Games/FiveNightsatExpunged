using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsOut : Powered
{
    public SpriteRenderer expunged;
    public Light moonLight;

    public Sprite off, on;

    public AudioSource expungedSound;
    public GameObject ui;
    void Start()
    {
        expunged.enabled = false;
        moonLight.enabled = false;
    }
    public IEnumerator StartPowerOutSequence()
    {
        moonLight.enabled = true;
        yield return new WaitForSeconds(Random.Range(7f, 12f));
        expunged.enabled = true;
        StartCoroutine(LightShow());
        expungedSound.Play();
        yield return new WaitForSeconds(15);
        expungedSound.Stop();
        yield return new WaitForSeconds(Random.Range(8f, 12f));
        GameManager.Instance.characterWhoKill = "Expunged";
        GameManager.Instance.GameOver = true;
    }
    IEnumerator LightShow()
    {
        float currentDuration = 0;
        
        while(currentDuration < 15)
        {
            float stupidValue = Random.Range(0, 100);

            if(stupidValue < 85)
            {
                expunged.sprite = off;
            }
            else
            {
                expunged.sprite = on;
            }
            currentDuration += Time.deltaTime;
            yield return null;
        }
        StartCoroutine(FlickerMoonLights());
        expunged.enabled = false;
    }
    IEnumerator FlickerMoonLights()
    {
        // you should have expected bad code why did you think it was a good idea to reverse engineer this game
        float currentDuration = 0;

        float blinkTime = 0.125f;
        float blinkSpeed = moonLight.intensity / blinkTime;

        while(currentDuration < blinkTime)
        {
            moonLight.intensity += -blinkSpeed * Time.deltaTime;
            currentDuration += Time.deltaTime;
            yield return null;
        }

        currentDuration = 0;
        moonLight.color = Color.white;
        while (currentDuration < blinkTime)
        {
            moonLight.intensity += blinkSpeed * Time.deltaTime * 4;
            currentDuration += Time.deltaTime;
            yield return null;
        }
        currentDuration = 0;
        while (currentDuration < blinkTime)
        {
            moonLight.intensity += -blinkSpeed * Time.deltaTime;
            currentDuration += Time.deltaTime;
            yield return null;
        }

        currentDuration = 0;
        while (currentDuration < blinkTime)
        {
            moonLight.intensity += blinkSpeed * Time.deltaTime * 4;
            currentDuration += Time.deltaTime;
            yield return null;
        }
        currentDuration = 0;
        while (currentDuration < blinkTime)
        {
            moonLight.intensity += -blinkSpeed * Time.deltaTime;
            currentDuration += Time.deltaTime;
            yield return null;
        }

        moonLight.intensity = 0;
    }

    public override void OnOutage()
    {
        ui.SetActive(false);
        AudioSource[] sources = FindObjectsOfType<AudioSource>();
        for (int i = 0; i < sources.Length; i++)
        {
            sources[i].Stop();
        }
        StartCoroutine(StartPowerOutSequence());
    }
}
