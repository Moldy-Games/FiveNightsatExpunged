using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CreditsScroll : MonoBehaviour
{
    public Transform credits;
    public Fade fade;
    bool scrolling = true;
    public Vector3 thing;
    IEnumerator Scroll()
    {
        scrolling = false;
        yield return new WaitForSeconds(5);
        fade.FadeToLevel("Menu");
    }
    private void Update()
    {
        if(thing.y < 1800)
        {
            thing = credits.position;
            thing.y += 100f * Time.deltaTime;
            credits.position = thing;
        }
        else
        {
            if(scrolling)
            {
                StartCoroutine(Scroll());
            }
        }
    }
}
