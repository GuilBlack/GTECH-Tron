using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game
{
    struct Cell
    {
        public Vector3 position;
        public ushort takenBy;
    }

    private Sprite m_Sprite = new Sprite();
    private Material m_PlayerMaterial;
    private Player[] m_Player;
    public bool isRunning = true;

    ushort[,] m_Grid = new ushort[40,40];
    List<Cell> m_TakenPos = new List<Cell>(800);
    Material m_LineP1;
    Material m_LineP2;

    public void InitGame()
    {
        m_Player = new Player[2]
        {
            new Player(new Vector3(-5f, 0f, 0f), Vector2.left, Color.cyan),
            new Player(new Vector3(5f, 0f, 0f), Vector2.right, Color.yellow)
        };
        m_Sprite.Create(Color.red);
        m_Sprite.Create(Color.blue);
        m_PlayerMaterial = new Material(Shader.Find("Unlit/Texture"));
        m_PlayerMaterial.mainTexture = Resources.Load<Texture2D>("Textures/moto");
        m_LineP1 = new Material(Shader.Find("Unlit/Texture"));
        m_LineP1.mainTexture = Resources.Load<Texture2D>("textures/blue_square");
        m_LineP2 = new Material(Shader.Find("Unlit/Texture"));
        m_LineP2.mainTexture = Resources.Load<Texture2D>("textures/red_square");

        m_Player[0].SetMesh(m_Sprite.mesh);               m_Player[1].SetMesh(m_Sprite.mesh);
        m_Player[0].SetMaterial(m_PlayerMaterial, m_LineP1);        m_Player[1].SetMaterial(m_PlayerMaterial, m_LineP2);
        m_Player[0].position = new Vector3(-5f, 0f, 0f);  m_Player[1].position = new Vector3(5f, 0f, 0f);
    }

    public void Update()
    {
        UpdateInputs();
        ushort playerIndex = 1;
        foreach (var player in m_Player)
        {
            player.Update();
            int cellX = (int)(player.position.x + 20f);
            int cellY = (int)(player.position.y + 20f);
            ushort takenBy = m_Grid[cellX, cellY];
            if (takenBy == 0)
            {
                m_Grid[cellX,cellY] = playerIndex;
                Cell cell = new Cell();
                cell.position = new Vector3((int)player.position.x, (int)player.position.y, 0f);
                cell.takenBy = playerIndex;
                m_TakenPos.Add(cell);
                player.currentCell = new Player.CellCoord(cellX, cellY);
            }
            else if (takenBy == 1)
            {
                if (playerIndex == 2)
                {
                    isRunning = false;
                } 
                else
                {
                    if (player.currentCell.x != cellX || player.currentCell.y != cellY)
                        isRunning = false;
                }
            }
            else
            {
                if (playerIndex == 1)
                {
                    isRunning = false;
                }
                else
                {
                    if (player.currentCell.x != cellX || player.currentCell.y != cellY)
                        isRunning = false;
                }
            }

            if (player.position.y >= 19.5f || player.position.y <= -19.5f
                || player.position.x >= 19.5f || player.position.x <= -19.5f)
            {
                isRunning = false;
            }
            ++playerIndex;
        }
    }

    void UpdateInputs()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            m_Player[0].SetDirection(false);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            m_Player[0].SetDirection(true);
        }

        if (Input.GetKeyUp(KeyCode.J))
        {
            m_Player[1].SetDirection(false);
        }
        else if (Input.GetKeyUp(KeyCode.L))
        {
            m_Player[1].SetDirection(true);
        }
    }

    public void FixedUpdate()
    {
        foreach (var player in m_Player)
        {
            player.FixedUpdate();
        }
    }

    public void Render()
    {
        foreach (var player in m_Player)
        {
            player.Render();
        }
    }
}
