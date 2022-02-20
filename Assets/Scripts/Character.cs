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
    public Node startLocation;
    [SerializeField] Node currentLocation;

    public CharacterNodeData[] nodeData;
    public Dictionary<string, CharacterNodeData> nodeNameToData;
    void Start()
    {
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
        Node[] outgoingNodes = currentLocation.nodes;

        List<CharacterNodeData> validNodes = new List<CharacterNodeData>();

        for (int i = 0; i < outgoingNodes.Length; i++)
        {
            CharacterNodeData temp = nodeNameToData[outgoingNodes[i].name];
            if(temp.weight)
            {
                validNodes.Add(temp);
            }
        }

        if(validNodes.Count > 0)
        {
            int travelToIndex = UnityEngine.Random.Range(0, validNodes.Count);

            currentLocation = validNodes[travelToIndex].node;

            transform.position = currentLocation.transform.position;
            transform.rotation = currentLocation.transform.rotation;
        }
    }
}
