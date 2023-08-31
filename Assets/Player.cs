using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player
{
    public struct CellCoord
    {
        public float x, y;

        public CellCoord (float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }
    Mesh m_Mesh;
    Material m_Material;
    Material m_LineMaterial;

    protected Vector2 m_Direction;
    public Vector3 position;
    public CellCoord currentCell;
    float m_Velocity = 4f;
    float m_Acceleration = 0.01f;

    Line m_Line;

    public Player(Vector2 position, Vector2 direction)
    {
        m_Direction = direction;
        m_Line = new Line(position);
    }

    public void SetMesh(Mesh mesh)
    {
        m_Mesh = mesh;
    }

    public void SetMaterial(Material playerMaterial, Material lineMaterial)
    {
        m_Material = playerMaterial;
        m_LineMaterial = lineMaterial;
    }

    public void SetDirection(bool key)
    {
        if (m_Direction == Vector2.up || m_Direction == Vector2.down)
        {
            if (key)
            {
                m_Direction = Vector2.right;
            }
            else
            {
                m_Direction = Vector2.left;
            }
        }
        else
        {
            if (key)
            {
                m_Direction = Vector2.up;
            }
            else
            {
                m_Direction = Vector2.down;
            }
        }
        m_Line.PlayerChangedDirection(m_Direction, position);
    }

    public Vector2 GetDirection()
    {
        return m_Direction;
    }

    public void Update()
    {
        position.x += m_Direction.x * Time.deltaTime * m_Velocity; position.y += m_Direction.y * Time.deltaTime * m_Velocity;
        m_Line.UpdateLine(m_Direction, position);
    }

    public void FixedUpdate()
    {
        m_Velocity += m_Acceleration;
    }

    public void Render()
    {
        if (!m_LineMaterial)
            return;
        if (!m_Material)
            return;

        m_LineMaterial.SetPass(0);
        Graphics.DrawMeshNow(m_Line.mesh, Vector2.zero, Quaternion.identity);

        m_Material.SetPass(0);
        Graphics.DrawMeshNow(m_Mesh, position, Quaternion.identity);
    }
}
