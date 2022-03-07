using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    public Fade fade;
    public Sprite win, pinkSlip;
    public Image lol;
    void Start()
    {
        if(PlayerPrefs.GetInt("CustomNight") == 1)
        {
            lol.sprite = pinkSlip;
        }
        else
        {
            lol.sprite = win;
        }
        StartCoroutine(EndSequence());
    }
    IEnumerator EndSequence()
    {
        yield return new WaitForSeconds(10);
        fade.FadeToLevel("Credits");
    }
}
