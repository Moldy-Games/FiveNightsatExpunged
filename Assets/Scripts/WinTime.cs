using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class WinTime : MonoBehaviour
{
    MeshRenderer meshRenderer;
    MeshFilter meshFilter;

    public Material winMaterial;

    public float speed = 1;
    public bool startWin = false;
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshFilter = GetComponent<MeshFilter>();

        Vector3[] vertices = new Vector3[8];
        Vector2[] uv = new Vector2[8];
        int[] indices = new int[12];

        // this is really bad code 
        vertices[0] = new Vector3(-2f, -0.5f, 0f);
        vertices[1] = new Vector3(-2f, 0.5f, 0f);
        vertices[2] = new Vector3(0f, 0.5f, 0f);
        vertices[3] = new Vector3(0f, -0.5f, 0f);

        vertices[4] = new Vector3(0f, -0.5f, 0f);
        vertices[5] = new Vector3(0f, 0.5f, 0f);
        vertices[6] = new Vector3(2f, 0.5f, 0f);
        vertices[7] = new Vector3(2f, -0.5f, 0f);

        uv[0] = new Vector2(0f, 0.5f);
        uv[1] = new Vector2(0f, 0.75f);
        uv[2] = new Vector2(0.5f, 0.75f);
        uv[3] = new Vector2(0.5f, 0.5f);

        uv[4] = new Vector2(0.5f, 0.5f);
        uv[5] = new Vector2(0.5f, 0.75f);
        uv[6] = new Vector2(1f, 0.75f);
        uv[7] = new Vector2(1f, 0.5f);


        indices[0] = 0;
        indices[1] = 1;
        indices[2] = 3;
        indices[3] = 1;
        indices[4] = 2;
        indices[5] = 3;
        indices[6] = 4;
        indices[7] = 5;
        indices[8] = 7;
        indices[9] = 5;
        indices[10] = 6;
        indices[11] = 7;

        // god help me

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.SetIndices(indices, MeshTopology.Triangles, 0);

        meshFilter.mesh = mesh;
        meshRenderer.material = winMaterial;
    }
    public void ChangeTime()
    {
        StartCoroutine(Change());
    }
    IEnumerator Change()
    {
        Mesh mesh = meshFilter.mesh;
        Vector2[] coords = mesh.uv;
        float moveDist = 0.3203125f;
        float crawlSpeed = moveDist / speed;
        float offsetTotal = 0;

        while(offsetTotal < moveDist)
        {
            float deltaOffset = Time.deltaTime * crawlSpeed;
            offsetTotal += deltaOffset;

            for (int i = 0; i < 4; i++)
            {
                coords[i] -= new Vector2(0, deltaOffset);
            }
            mesh.uv = coords;
            yield return null;
        }
        for (int i = 0; i < 4; i++)
        {
            coords[0] = new Vector2(0f, 0.5f - moveDist);
            coords[1] = new Vector2(0f, 0.75f - moveDist);
            coords[2] = new Vector2(0.5f, 0.75f - moveDist);
            coords[3] = new Vector2(0.5f, 0.5f - moveDist);
        }

        mesh.uv = coords;
    }
    private void Update()
    {
        if(startWin)
        {
            ChangeTime();
            startWin = false;
        }
    }
}
