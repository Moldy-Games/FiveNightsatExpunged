using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Character : MonoBehaviour
{
    [Serializable]
    public enum Actions
    {
        TPose,
        Stage,
        Standing,
        OutsideCamera,
        OutsideWindow,
        InHallway,
        Kill
    };
    [Serializable]
    public struct CharacterNodeData
    {
        public Node node;
        public bool weight;
        public Actions action;
    }
    public Node startLocation, gameOverLocation, leftDoorLocation, rightDoorLocation, inOfficeLocation;
    public DoorButton leftDoor, rightDoor;
    [SerializeField] Node currentLocation;

    public CharacterNodeData[] nodeData;
    public Dictionary<string, CharacterNodeData> nodeNameToData;
    void Start()
    {
        if(PlayerPrefs.GetInt("Night") == 5 && gameObject.name != "Expunged")
        {
            gameObject.SetActive(false);
        }
        if(PlayerPrefs.GetInt("Night") != 5 && gameObject.name == "Expunged")
        {
            gameObject.SetActive(false);
        }
        nodeNameToData = new Dictionary<string, CharacterNodeData>();
        for (int i = 0; i < nodeData.Length; i++)
        {
            nodeNameToData[nodeData[i].node.name] = nodeData[i];
        }
        currentLocation = startLocation;
        transform.position = currentLocation.transform.position;
        transform.rotation = currentLocation.transform.rotation;
    }
    public void Transition()
    {
        if (currentLocation.name == leftDoorLocation.name && !leftDoor.doorOpen)
        {
            currentLocation = startLocation;
            transform.position = currentLocation.transform.position;
            transform.rotation = currentLocation.transform.rotation;
        }
        else if(currentLocation.name == rightDoorLocation.name && !rightDoor.doorOpen)
        {
            currentLocation = startLocation;
            transform.position = currentLocation.transform.position;
            transform.rotation = currentLocation.transform.rotation;
        }
        else if (currentLocation.name == inOfficeLocation.name && FindObjectOfType<Monitor>().uiOpen)
        {
            currentLocation = startLocation;
            transform.position = currentLocation.transform.position;
            transform.rotation = currentLocation.transform.rotation;
        }
        else
        {
            Node[] outgoingNodes = currentLocation.nodes;

            List<CharacterNodeData> validNodes = new List<CharacterNodeData>();

            for (int i = 0; i < outgoingNodes.Length; i++)
            {
                CharacterNodeData temp = nodeNameToData[outgoingNodes[i].name];
                if (temp.weight)
                {
                    validNodes.Add(temp);
                }
            }

            if (validNodes.Count > 0)
            {
                int travelToIndex = UnityEngine.Random.Range(0, validNodes.Count);

                currentLocation = validNodes[travelToIndex].node;

                transform.position = currentLocation.transform.position;
                transform.rotation = currentLocation.transform.rotation;

                if (currentLocation.name == gameOverLocation.name)
                {
                    GameManager.Instance.characterWhoKill = gameObject.name;
                    GameManager.Instance.GameOver = true;
                }
            }
        }
    }
}
