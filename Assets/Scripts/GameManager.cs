using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

[Serializable]
public class GameManager : MonoBehaviour
{
    public TMP_Text timeText, nightText;

    public GameObject jumpscare, uiObj;

    public int startTime, endTime;

    public float levelDuration;
    public float timeMultiplier;
    [SerializeField] float actualTime;

    public float gameTime;

    public int minTransitions, maxTransitions;

    [SerializeField] float[] transitionTimes;

    int transitions = 0;
    private int currentTransition = 0;

    private bool gameOver;
    public bool GameOver
    {
        get { return gameOver; }
        set { gameOver = value; Jumpscare(gameOver); }
    }

    public static GameManager Instance;

    public GameObject bambom, brob;
    public Monitor monitorScript;
    public Flashlight flashlight;
    public Collider bambomCollision;
    public CharacterAudio[] charactersAudio;
    public AudioSource powerOutAud;

    public Animator jumpscareAnimator;

    public string characterWhoKill;

    public int brobChance;

    public bool demoMode;
    void Start()
    {
        Instance = this;
        GameOver = false;
        if(!demoMode)
        {
            minTransitions = UnityEngine.Random.Range(10, 15) * PlayerPrefs.GetInt("Night");
            maxTransitions = UnityEngine.Random.Range(16, 23) * PlayerPrefs.GetInt("Night");
        }
        AISetup();
        if(PlayerPrefs.GetInt("Night") <= 5)
        {
            nightText.text = $"Night {PlayerPrefs.GetInt("Night")}";
        }
        if(demoMode)
        {
            nightText.text = "Demo Night";
        }
    }
    void Update()
    {
        if(!GameOver)
        {
            TimeSet();
        }
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
        if(brobChance == 1234)
        {
            brob.SetActive(true);
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    public void AISetup()
    {
        transitions = UnityEngine.Random.Range(minTransitions, maxTransitions);
        transitionTimes = new float[transitions];
        jumpscare.SetActive(false);
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
        if (currentTransition < transitionTimes.Length && gameTime >= transitionTimes[currentTransition] && PowerManager.Instance.power > 0)
        {
            currentTransition++;
            AIManager.Instance.TransitionOccured();
            DoTransitionStuff();
        }
        if(gameTime >= endTime)
        {
            SceneManager.LoadScene("Win");
        }
    }
    public void DoTransitionStuff()
    {
        if(bambom.activeInHierarchy)
        {
            characterWhoKill = "Bambom";
            GameOver = true;
        }
        if (monitorScript.camerasOpen)
        {
            monitorScript.ShowError();
        }
        int funnyBambom = UnityEngine.Random.Range(1, 10);
        if(funnyBambom == 5)
        {
            bambom.SetActive(true);
        }
        for (int i = 0; i < charactersAudio.Length; i++)
        {
            charactersAudio[i].PlaySound();
        }
    }
    public void Jumpscare(bool thing)
    {
        if(thing)
        {
            if (monitorScript.camerasOpen)
            {
                monitorScript.camerasOpen = false;
                StartCoroutine(monitorScript.CameraOpen());
            }
            uiObj.SetActive(false);
            jumpscare.SetActive(true);
            jumpscareAnimator.SetTrigger(characterWhoKill);
            StartCoroutine(JumpscareCoroutine());
        }
    }
    IEnumerator JumpscareCoroutine()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("GameOver");
    }
}
