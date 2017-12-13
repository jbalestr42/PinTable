using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTable : MonoBehaviour {

    public int _width;
    public int _height;
    public float _size;
    public float _offset;

    void Start()
    {
        Mesh mesh = new Mesh();
        mesh.name = "PinTable";
        GetComponent<MeshFilter>().mesh = mesh;


        List<Vector3> vertices = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();
        List<int> triangles = new List<int>();
        List<Color> colors = new List<Color>();

        for (int x = 0; x < _width; x++) {
            for (int y = 0; y < _height; y++) {
                CreateCube(vertices, colors, triangles, uvs, x, y);
            }
        }

        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.colors = colors.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
    }

    void CreateCube(List<Vector3> p_vertices, List<Color> p_colors, List<int> p_triangles, List<Vector2> p_uvs, int p_x, int p_y) {
        float half = _size / 2f;
        Vector3 origin = new Vector3(p_x * (_size + _offset), half, p_y * (_size + _offset));
        int index = p_vertices.Count;

        p_vertices.Add(origin + new Vector3(-half, -half, -half));
        p_vertices.Add(origin + new Vector3(half, -half, -half));
        p_vertices.Add(origin + new Vector3(half, half, -half));
        p_vertices.Add(origin + new Vector3(-half, half, -half));
        p_vertices.Add(origin + new Vector3(-half, half, half));
        p_vertices.Add(origin + new Vector3(half, half, half));
        p_vertices.Add(origin + new Vector3(half, -half, half));
        p_vertices.Add(origin + new Vector3(-half, -half, half));

        p_colors.Add(Color.black);
        p_colors.Add(Color.black);
        p_colors.Add(Color.white);
        p_colors.Add(Color.white);
        p_colors.Add(Color.white);
        p_colors.Add(Color.white);
        p_colors.Add(Color.black);
        p_colors.Add(Color.black);

        for (int i = 0; i < 8; i++) {
            p_uvs.Add(new Vector2((float)p_x / (float)_width, (float)p_y / (float)_height));
        }

        CreateQuad(p_triangles, index, index + 1, index + 2, index + 3); // front
        CreateQuad(p_triangles, index + 2, index + 5, index + 4, index + 3); // top
        CreateQuad(p_triangles, index + 1, index + 6, index + 5, index + 2); // right
        CreateQuad(p_triangles, index, index + 3, index + 4, index + 7); // left
        CreateQuad(p_triangles, index + 5, index + 6, index + 7, index + 4); // back
        CreateQuad(p_triangles, index, index + 7, index + 6, index + 1); // bottom
        /*
        p_triangles[]

            0, 2, 1, //face front
			0, 3, 2,
			2, 3, 4, //face top
			2, 4, 5,
			1, 2, 5, //face right
			1, 5, 6,
			0, 7, 4, //face left
			0, 4, 3,
			5, 4, 7, //face back
			5, 7, 6,
			0, 6, 7, //face bottom
			0, 1, 6*/
    }

    void CreateQuad(List<int> p_triangles, int p_index0, int p_index1, int p_index2, int p_index3) {
        p_triangles.Add(p_index0);
        p_triangles.Add(p_index2);
        p_triangles.Add(p_index1);

        p_triangles.Add(p_index0);
        p_triangles.Add(p_index3);
        p_triangles.Add(p_index2);
    }
}
