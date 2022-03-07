using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenduCheck : MonoBehaviour
{
    public float patience;
    SpriteRenderer benduSprite;

    public GameObject cam5;
    public AudioSource lookAtMyKeyboard;

    bool inCove;
    public Monitor monitor;
    void Start()
    {
        benduSprite = GetComponentInChildren<SpriteRenderer>();
        patience = 300 - (PlayerPrefs.GetInt("Night") * 25);
        if(PlayerPrefs.GetInt("Night") > 1 && PlayerPrefs.GetInt("Night") != 5 || GameManager.Instance.customNight)
        {
            StartCoroutine(BenduSequence());
            inCove = true;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        if(inCove && gameObject.activeInHierarchy)
        {
            if (monitor.uiOpen && monitor.currentCam == 5)
            {
                lookAtMyKeyboard.mute = false;
            }
            else
            {
                lookAtMyKeyboard.mute = true;
            }
        }
        else
        {
            lookAtMyKeyboard.mute = true;
        }
    }

    IEnumerator BenduSequence()
    {
        while(patience > 0)
        {
            if(!monitor.camerasOpen && monitor.currentCam == 5 && !PowerManager.Instance.powerOut)
            {
                patience -= Time.deltaTime;
            }
            yield return null;
        }
        StartCoroutine(UhOh());
    }
    IEnumerator UhOh()
    {
        inCove = false;
        benduSprite.enabled = false;
        yield return new WaitForSeconds(10);
        GameManager.Instance.characterWhoKill = "Bendu";
        GameManager.Instance.GameOver = true;
    }
}
