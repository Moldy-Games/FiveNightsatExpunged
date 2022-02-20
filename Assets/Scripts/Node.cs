using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Node[] nodes;
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        for (int i = 0; i < nodes.Length; i++)
        {
            Gizmos.DrawLine(transform.position, nodes[i].transform.position);
        }
    }
}
