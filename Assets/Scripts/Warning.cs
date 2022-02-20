using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Warning : MonoBehaviour
{
    public TMP_Text text;
    public string[] lines;
    public float textSpeed;

    int index;
    bool aboutToCrash = false;

    public AudioSource beep;

    void Start()
    {
        lines[7] = IPManager.GetIP(ADDRESSFAM.IPv4);
        text.text = string.Empty;
        StartText();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if(text.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                text.text = lines[index];
            }
        }
        if (text.text == lines[7] && !aboutToCrash)
        {
            StartCoroutine(CrashGame(2));
            aboutToCrash = true;
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Menu");
        }
    }
    void StartText()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }
    void NextLine()
    {
        if(index < lines.Length - 1)
        {
            index++;
            text.text = string.Empty;
            StartCoroutine(TypeLine());
        }
    }
    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            text.text += c;
            beep.Play();
            yield return new WaitForSeconds(textSpeed);
        }
    }
    IEnumerator CrashGame(float time)
    {
        yield return new WaitForSeconds(time);
        Application.Quit();
    }
}
