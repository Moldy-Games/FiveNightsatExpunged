using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

[Serializable]
public class GameManager : MonoBehaviour
{
    public TMP_Text timeText, nightText;

    public int startTime, endTime;

    public float levelDuration;
    public float timeMultiplier;
    [SerializeField] float actualTime;

    public float gameTime;

    public int minTransitions, maxTransitions;

    [SerializeField] float[] transitionTimes;

    int transitions = 0;
    private int currentTransition = 0;

    public GameObject bambom;
    public Monitor monitorScript;
    public Flashlight flashlight;
    public Collider bambomCollision;

    void Start()
    {
        AISetup();
        nightText.text = $"Night {PlayerPrefs.GetInt("Night")}";
    }
    void Update()
    {
        TimeSet();
        if(flashlight.flashlightEnabled && bambom.activeInHierarchy)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 50))
            {
                if(hit.collider == bambomCollision)
                {
                    bambom.SetActive(false);
                }
            }
        }
    }
    public void AISetup()
    {
        transitions = UnityEngine.Random.Range(minTransitions, maxTransitions);
        transitionTimes = new float[transitions];
        float delta = (endTime - startTime) / (transitions + 1f);

        for (int i = 0; i < transitions; i++)
        {
            transitionTimes[i] = delta * (i + 1) + UnityEngine.Random.Range(-0.375f * delta, 0.375f * delta);
        }
    }
    public void TimeSet()
    {
        actualTime += Time.deltaTime * timeMultiplier;
        gameTime = actualTime / (levelDuration * 60) * endTime - startTime;
        int truncated = (int)gameTime;
        if (truncated == 0)
        {
            timeText.text = "12 AM";
        }
        else
        {
            timeText.text = $"{truncated} AM";
        }
        if (currentTransition < transitionTimes.Length && gameTime >= transitionTimes[currentTransition])
        {
            currentTransition++;
            AIManager.Instance.TransitionOccured();
            DoTransitionStuff();
        }
    }
    public void DoTransitionStuff()
    {
        if (monitorScript.camerasOpen)
        {
            monitorScript.ShowError();
        }
        int funnyBambom = UnityEngine.Random.Range(1, 10);
        if(funnyBambom == 5)
        {
            bambom.SetActive(true);
        }
    }
}
