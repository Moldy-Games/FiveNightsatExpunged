using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public static AIManager Instance;

    public Node[] nodes;
    public Character[] characters;

    public Character expunged;

    private void Awake()
    {
        Instance = this;
    }
    public void TransitionOccured()
    {
        Debug.Log("Transition occured");
        if(PlayerPrefs.GetInt("Night") != 5)
        {
            for (int i = 0; i < characters.Length; i++)
            {
                characters[i].Transition();
            }
        }
        else
        {
            expunged.Transition();
        }
    }
    void Start()
    {
        nodes = FindObjectsOfType<Node>();
        characters = FindObjectsOfType<Character>();
    }
    void Update()
    {
        
    }
}
