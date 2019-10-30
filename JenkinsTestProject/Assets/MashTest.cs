using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MashTest : MonoBehaviour
{

    private Mesh m_mesh;
    private MeshFilter m_filter;

    // Start is called before the first frame update
    void Start()
    {
        m_filter = GetComponent<MeshFilter>();

        m_mesh = new Mesh();
        m_filter.mesh = m_mesh;

        InitMesh1();
    }

    private void InitMesh()
    {
        m_mesh.name = "MyMesh";

        Vector3[] vertices = {
            //new Vector3(1, 1, 0),
            new Vector3(-1, 1, 0),
            new Vector3(1, -1, 0),
            new Vector3(-1, -1, 0)
        };

        m_mesh.vertices = vertices;

        int[] triangles = {
             0, 1, 2
         };
        m_mesh.triangles = triangles;

        Vector2[] uv =
        {
            new Vector2(0, 1),
            new Vector2(1, 0),
            new Vector2(0, 0)
        };
        m_mesh.uv = uv;
    }

    private void InitMesh1()
    {
        m_mesh.name = "MyMesh";

        Vector3[] vertices = {
            //new Vector3(1, 1, 0),
            new Vector3(-1, 1, 0),
            new Vector3(0f, 0.5f, 0),
            new Vector3(1, -1, 0),
            new Vector3(-1, -1, 0)
        };

        m_mesh.vertices = vertices;

        int[] triangles = {
             0, 1, 2, 2, 3, 0
         };
        m_mesh.triangles = triangles;

        Vector2[] uv =
        {
            new Vector2(0, 1),
            new Vector2(1, 1),
            new Vector2(1, 0),
            new Vector2(0, 0)
        };
        m_mesh.uv = uv;

        //Vector3[] normals =
        //{
        //    new Vector3(1, 1, 1),
        //    new Vector3(0, 0, 0),
        //    new Vector3(1, 1, 1),
        //    new Vector3(0, 0, 0)
        //};
        //m_mesh.normals = normals;
        Debug.Log(m_mesh.normals);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
