using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine;

[CustomEditor(typeof(Character))]
public class CharacterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Character characterScript = (Character)target;
        characterScript.startLocation = (Node)EditorGUILayout.ObjectField("Starting Node: ", characterScript.startLocation, typeof(Node), true);
        AIManager[] aiManager = Resources.FindObjectsOfTypeAll<AIManager>();
        if (aiManager == null)
        {
            EditorGUILayout.HelpBox("Add an AI Manager to an object in the scene to populate nodes", MessageType.Error);
        }
        else
        {
            Node[] nodes = aiManager[0].nodes;

            if (characterScript.nodeData == null || characterScript.nodeData.Length != nodes.Length)
            {
                characterScript.nodeData = new Character.CharacterNodeData[nodes.Length];
                for (int i = 0; i < nodes.Length; i++)
                {
                    characterScript.nodeData[i] = new Character.CharacterNodeData();
                }
            }
            EditorGUILayout.BeginVertical();
            for (int i = 0; i < nodes.Length; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.TextArea(nodes[i].name);
                characterScript.nodeData[i].node = nodes[i];
                characterScript.nodeData[i].action = (Character.Actions)EditorGUILayout.EnumPopup(characterScript.nodeData[i].action);
                characterScript.nodeData[i].weight = EditorGUILayout.Toggle(characterScript.nodeData[i].weight);
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
        }
    }
}
