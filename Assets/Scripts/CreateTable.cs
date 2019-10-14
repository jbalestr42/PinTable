using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Create a pin table
/// </summary>
public class CreateTable : MonoBehaviour
{
    [SerializeField]
    private int _tableSize;

    [SerializeField]
    private float _pinSize;

    [SerializeField]
    private float _spaceBetweenPin;

    [SerializeField]
    private Material _pintableMat;

    void Start()
    {
        // Compute the chunk size
        int chunkCount = 1;
        int chunkSize = _tableSize;
        while (chunkSize * chunkSize * 8 >= 65000) 
        {
            chunkSize /= 2;
            chunkCount *= 2;
        }

        GameObject chunks = new GameObject();
        chunks.name = "PinTableChunks";
        chunks.transform.SetParent(transform);
        for (int x = 0; x < chunkCount; x++) 
        {
            for (int y = 0; y < chunkCount; y++)
            {
                CreateChunk(chunks, chunkSize, x, y);
            }
        }
        Debug.Log((chunkCount * chunkCount) + " chunks created. Each chunk contains " + (chunkSize * chunkSize) + " pins.");
    }

    void CreateChunk(GameObject p_parent, int p_size, int p_offsetX, int p_offsetY)
    {
        GameObject go = new GameObject();
        go.transform.SetParent(p_parent.transform, false);
        go.isStatic = true;
        go.name = "Chunk_" + p_offsetX + "_" + p_offsetY;
        Mesh chunkMesh = new Mesh();
        MeshFilter filter = go.AddComponent<MeshFilter>();
        MeshRenderer renderer = go.AddComponent<MeshRenderer>();

        List<Vector3> vertices = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();
        List<int> triangles = new List<int>();
        List<Color> colors = new List<Color>();

        for (int x = 0; x < p_size; x++) 
        {
            for (int y = 0; y < p_size; y++) 
            {
                CreateCube(vertices, colors, triangles, uvs, p_offsetX * p_size + x, p_offsetY * p_size + y);
            }
        }

        chunkMesh.Clear();
        chunkMesh.vertices = vertices.ToArray();
        chunkMesh.colors = colors.ToArray();
        chunkMesh.uv = uvs.ToArray();
        chunkMesh.triangles = triangles.ToArray();
        chunkMesh.RecalculateNormals();
        chunkMesh.name = go.name;
        renderer.material = _pintableMat;
        filter.mesh = chunkMesh;
    }

    void CreateCube(List<Vector3> p_vertices, List<Color> p_colors, List<int> p_triangles, List<Vector2> p_uvs, int p_x, int p_y)
    {
        Vector3 pivot = new Vector3(_tableSize * _pinSize + (_tableSize - 1) * _spaceBetweenPin, 0f, _tableSize * _pinSize + (_tableSize - 1) * _spaceBetweenPin) / 2.0f;

        float halfSize = _pinSize / 2f;
        Vector3 origin = new Vector3(p_x * (_pinSize + _spaceBetweenPin), halfSize, p_y * (_pinSize + _spaceBetweenPin));
        int index = p_vertices.Count;

        p_vertices.Add(origin - pivot + new Vector3(-halfSize, -halfSize, -halfSize));
        p_vertices.Add(origin - pivot + new Vector3(halfSize, -halfSize, -halfSize));
        p_vertices.Add(origin - pivot + new Vector3(halfSize, halfSize, -halfSize));
        p_vertices.Add(origin - pivot + new Vector3(-halfSize, halfSize, -halfSize));
        p_vertices.Add(origin - pivot + new Vector3(-halfSize, halfSize, halfSize));
        p_vertices.Add(origin - pivot + new Vector3(halfSize, halfSize, halfSize));
        p_vertices.Add(origin - pivot + new Vector3(halfSize, -halfSize, halfSize));
        p_vertices.Add(origin - pivot + new Vector3(-halfSize, -halfSize, halfSize));

        p_colors.Add(Color.black);
        p_colors.Add(Color.black);
        p_colors.Add(Color.white);
        p_colors.Add(Color.white);
        p_colors.Add(Color.white);
        p_colors.Add(Color.white);
        p_colors.Add(Color.black);
        p_colors.Add(Color.black);

        for (int i = 0; i < 8; i++) 
        {
            p_uvs.Add(new Vector2((float)p_x / (float)_tableSize, (float)p_y / (float)_tableSize));
        }

        CreateQuad(p_triangles, index, index + 1, index + 2, index + 3); // front
        CreateQuad(p_triangles, index + 2, index + 5, index + 4, index + 3); // top
        CreateQuad(p_triangles, index + 1, index + 6, index + 5, index + 2); // right
        CreateQuad(p_triangles, index, index + 3, index + 4, index + 7); // left
        CreateQuad(p_triangles, index + 5, index + 6, index + 7, index + 4); // back
        CreateQuad(p_triangles, index, index + 7, index + 6, index + 1); // bottom
    }

    void CreateQuad(List<int> p_triangles, int p_index0, int p_index1, int p_index2, int p_index3)
    {
        p_triangles.Add(p_index0);
        p_triangles.Add(p_index2);
        p_triangles.Add(p_index1);

        p_triangles.Add(p_index0);
        p_triangles.Add(p_index3);
        p_triangles.Add(p_index2);
    }

    public Material PintableMat
    {
        get { return _pintableMat; }
    }
}
