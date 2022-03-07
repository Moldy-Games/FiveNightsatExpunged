using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    public Fade fade;
    void Start()
    {
        if(PlayerPrefs.GetInt("Night") == 5)
        {
            PlayerPrefs.SetInt("GameComplete", 1);
        }
        if(PlayerPrefs.GetInt("Night") < 5)
        {
            PlayerPrefs.SetInt("Night", PlayerPrefs.GetInt("Night") + 1);
        }
        StartCoroutine(WinSequence());
    }
    IEnumerator WinSequence()
    {
        /*yield return new WaitForSeconds(2);
        winTime.startWin = true;*/
        yield return new WaitForSeconds(8);

        if(PlayerPrefs.GetInt("GameComplete") == 1)
            fade.FadeToLevel("End");
        else
            fade.FadeToLevel("Menu");
    }
}
