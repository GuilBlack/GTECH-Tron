using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Sprite
{
    int count = 0;
    protected Vector3[] m_Position;
    protected Vector2[] m_Uv;
    protected Color[] m_Colours;
    protected int[] m_Triangles;
    public Mesh mesh;

    public void Create(Color colour)
    {
        m_Position = new Vector3[4];
        m_Uv = new Vector2[4];
        m_Triangles = new int[6] {
            count+0, count+1, count+2,
            count+2, count+1, count+3
        };

        m_Position[0].x = -0.5f;
        m_Position[0].y = 0.5f;
        m_Position[1].x = 0.5f;
        m_Position[1].y = 0.5f;
        m_Position[2].x = -0.5f;
        m_Position[2].y = -0.5f;
        m_Position[3].x = 0.5f;
        m_Position[3].y = -0.5f;

        m_Uv[0].x = 0f;
        m_Uv[0].y = 1f;
        m_Uv[1].x = 1f;
        m_Uv[1].y = 1f;
        m_Uv[2].x = 0f;
        m_Uv[2].y = 0f;
        m_Uv[3].x = 1f;
        m_Uv[3].y = 0f;

        m_Colours = new Color[4] {
            colour,
            colour,
            colour,
            colour
        };

        mesh = new Mesh();
        mesh.vertices = m_Position;
        mesh.uv = m_Uv;
        mesh.colors = m_Colours;

        mesh.triangles = m_Triangles;
    }
}
