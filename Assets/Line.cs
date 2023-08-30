using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Line : Sprite
{
    public enum Alignment
    {
        Horizontal, Vertical
    }
    struct LineFragment
    {
        public Vector2 firstPos;
        public Vector2 secondPos;
        public Alignment alignment;

        public override string ToString()
        {
            return firstPos.ToString() + " " + secondPos.ToString() + " " + (alignment == Alignment.Horizontal ? "Horizontal" : "Vertical");
        }
    }

    List<LineFragment> m_LineFragments = new List<LineFragment>();
    LineFragment m_CurrentLineFragment;

    public Line(Vector2 playerPosition)
    {
        mesh = new Mesh();
        mesh.MarkDynamic();
        m_CurrentLineFragment = new LineFragment();
        m_CurrentLineFragment.alignment = Alignment.Horizontal;
        m_CurrentLineFragment.firstPos = playerPosition;
        m_CurrentLineFragment.secondPos = playerPosition;
        Create();
    }

    void Create()
    {
        int verticesCount = (4 * m_LineFragments.Count) + 4;
        int indicesCount = (6 * m_LineFragments.Count) + 6;

        m_Position = new Vector3[verticesCount];
        m_Uv = new Vector2[verticesCount];
        m_Colours = new Color[verticesCount];
        m_Triangles = new int[indicesCount];

        int indexVert = 0, indexInd = 0;

        foreach (LineFragment fragment in m_LineFragments)
        {
            AddLineGeometry(indexVert, indexInd, fragment);

            indexVert += 4;
            indexInd += 6;
        }

        AddLineGeometry(indexVert, indexInd, m_CurrentLineFragment);

        mesh.Clear();
        mesh.vertices = m_Position;
        mesh.uv = m_Uv;
        mesh.colors = m_Colours;

        mesh.triangles = m_Triangles;
    }

    void AddLineGeometry(int indexVert, int indexInd, LineFragment fragment)
    {
        m_Triangles[indexInd + 0] = indexVert + 0;
        m_Triangles[indexInd + 1] = indexVert + 1;
        m_Triangles[indexInd + 2] = indexVert + 2;
        m_Triangles[indexInd + 3] = indexVert + 2;
        m_Triangles[indexInd + 4] = indexVert + 1;
        m_Triangles[indexInd + 5] = indexVert + 3;

        float firstX = fragment.firstPos.x;
        float firstY = fragment.firstPos.y;
        float secondX = fragment.secondPos.x;
        float secondY = fragment.secondPos.y;
        
        if (fragment.alignment == Alignment.Horizontal)
        {
            m_Position[indexVert + 0].x = firstX - 0.5f;
            m_Position[indexVert + 0].y = firstY + 0.5f;
            m_Position[indexVert + 1].x = secondX + 0.5f;
            m_Position[indexVert + 1].y = secondY + 0.5f;
            m_Position[indexVert + 2].x = firstX - 0.5f;
            m_Position[indexVert + 2].y = firstY - 0.5f;
            m_Position[indexVert + 3].x = secondX + 0.5f;
            m_Position[indexVert + 3].y = secondY - 0.5f;
        }
        else
        {
            m_Position[indexVert + 0].x = firstX - 0.5f;
            m_Position[indexVert + 0].y = firstY + 0.5f;
            m_Position[indexVert + 1].x = firstX + 0.5f;
            m_Position[indexVert + 1].y = firstY + 0.5f;
            m_Position[indexVert + 2].x = secondX - 0.5f;
            m_Position[indexVert + 2].y = secondY - 0.5f;
            m_Position[indexVert + 3].x = secondX + 0.5f;
            m_Position[indexVert + 3].y = secondY - 0.5f;
        }

        m_Uv[0].x = 0f;
        m_Uv[0].y = 1f;
        m_Uv[1].x = 1f;
        m_Uv[1].y = 1f;
        m_Uv[2].x = 0f;
        m_Uv[2].y = 0f;
        m_Uv[3].x = 1f;
        m_Uv[3].y = 0f;
    }

    public void PlayerChangedDirection(Vector2 playerDirection, Vector2 playerPosition)
    {
        m_LineFragments.Add(m_CurrentLineFragment);
        Create();

        m_CurrentLineFragment = new LineFragment();

        if (playerDirection == Vector2.left)
        {
            m_CurrentLineFragment.secondPos = playerPosition;
            m_CurrentLineFragment.alignment = Alignment.Horizontal;
        }
        else if (playerDirection == Vector2.right)
        {
            m_CurrentLineFragment.firstPos = playerPosition;
            m_CurrentLineFragment.alignment = Alignment.Horizontal;
        }
        else if (playerDirection == Vector2.up)
        {
            m_CurrentLineFragment.secondPos = playerPosition;
            m_CurrentLineFragment.alignment = Alignment.Vertical;
        }
        else
        {
            m_CurrentLineFragment.firstPos = playerPosition;
            m_CurrentLineFragment.alignment = Alignment.Vertical;
        }
    }

    public void UpdateLine(Vector2 playerDirection, Vector2 playerPosition)
    {
        if (playerDirection == Vector2.left)
        {
            m_CurrentLineFragment.firstPos = playerPosition;
        }
        else if (playerDirection == Vector2.right)
        {
            m_CurrentLineFragment.secondPos = playerPosition;
        }
        else if (playerDirection == Vector2.up)
        {
            m_CurrentLineFragment.firstPos = playerPosition;
        }
        else
        {
            m_CurrentLineFragment.secondPos = playerPosition;
        }

        int verticesCount = (4 * m_LineFragments.Count);
        int indicesCount = (6 * m_LineFragments.Count);
        AddLineGeometry(verticesCount, indicesCount, m_CurrentLineFragment);
        mesh.Clear();
        mesh.vertices = m_Position;
        mesh.uv = m_Uv;

        mesh.triangles = m_Triangles;
    }
}
