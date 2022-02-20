using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public static AIManager Instance;

    public Node[] nodes;
    public Character[] characters;

    public Monitor monitorScript;

    private void Awake()
    {
        Instance = this;
    }
    public void TransitionOccured()
    {
        Debug.Log("Transition occured");
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].Transition();
        }
        if(monitorScript.camerasOpen)
        {
            StartCoroutine(monitorScript.ShowError());
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
