using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public static AIManager Instance;

    public Node[] nodes;
    public Character[] characters;

    public Character expunged, ringi;

    private void Awake()
    {
        Instance = this;
    }
    public void TransitionOccured()
    {
        Debug.Log("Transition occured");
        if(PlayerPrefs.GetInt("Night") != 5 || GameManager.Instance.customNight)
        {
            for (int i = 0; i < characters.Length; i++)
            {
                characters[i].Transition();
            }
        }
        if (PlayerPrefs.GetInt("Night") == 5 || GameManager.Instance.customNight)
        {
            expunged.Transition();
        }
        if (PlayerPrefs.GetInt("Night") > 2 && PlayerPrefs.GetInt("Night") != 5 || GameManager.Instance.customNight)
        {
            ringi.Transition();
        }
    }
}
