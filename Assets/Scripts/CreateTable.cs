using System.Collections.Generic;
using UnityEngine;

public class CreateTable : MonoBehaviour
{
    public int _width;
    public float _size;
    public float _offset;
    public Material _pintableMat;

    void Start()
    {
        int meshCount = 1;
        int subWidth = _width;
        while (subWidth * subWidth * 8 >= 65000) 
        {
            subWidth /= 2;
            meshCount *= 2;
        }

        GameObject chunks = new GameObject();
        chunks.name = "PinTableChunks";
        chunks.transform.SetParent(transform);
        for (int i = 0; i < meshCount; i++) 
        {
            for (int j = 0; j < meshCount; j++)
            {
                GameObject go = new GameObject();
                go.transform.SetParent(chunks.transform, false);
                go.isStatic = true;
                go.name = "Chunk_" + i + "_" + j;
                Mesh subMesh = new Mesh();
                MeshFilter filter = go.AddComponent<MeshFilter>();
                MeshRenderer renderer = go.AddComponent<MeshRenderer>();

                List<Vector3> vertices = new List<Vector3>();
                List<Vector2> uvs = new List<Vector2>();
                List<int> triangles = new List<int>();
                List<Color> colors = new List<Color>();

                for (int x = 0; x < subWidth; x++) 
                {
                    for (int y = 0; y < subWidth; y++) 
                    {
                        CreateCube(vertices, colors, triangles, uvs, i * subWidth + x, j * subWidth + y);
                    }
                }

                subMesh.Clear();
                subMesh.vertices = vertices.ToArray();
                subMesh.colors = colors.ToArray();
                subMesh.uv = uvs.ToArray();
                subMesh.triangles = triangles.ToArray();
                subMesh.RecalculateNormals();
                subMesh.name = go.name;
                renderer.material = _pintableMat;
                filter.mesh = subMesh;
            }
        }
    }

    void CreateCube(List<Vector3> p_vertices, List<Color> p_colors, List<int> p_triangles, List<Vector2> p_uvs, int p_x, int p_y)
    {
        Vector3 pivot = new Vector3(_width * _size + (_width - 1) * _offset, 0f, _width * _size + (_width - 1) * _offset) / 2.0f;

        float half = _size / 2f;
        Vector3 origin = new Vector3(p_x * (_size + _offset), half, p_y * (_size + _offset));
        int index = p_vertices.Count;

        p_vertices.Add(origin - pivot + new Vector3(-half, -half, -half));
        p_vertices.Add(origin - pivot + new Vector3(half, -half, -half));
        p_vertices.Add(origin - pivot + new Vector3(half, half, -half));
        p_vertices.Add(origin - pivot + new Vector3(-half, half, -half));
        p_vertices.Add(origin - pivot + new Vector3(-half, half, half));
        p_vertices.Add(origin - pivot + new Vector3(half, half, half));
        p_vertices.Add(origin - pivot + new Vector3(half, -half, half));
        p_vertices.Add(origin - pivot + new Vector3(-half, -half, half));

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
            p_uvs.Add(new Vector2((float)p_x / (float)_width, (float)p_y / (float)_width));
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
}
