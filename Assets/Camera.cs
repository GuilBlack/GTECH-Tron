using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

public class Camera : MonoBehaviour
{
    Game m_Game = new Game();

    void Awake()
    {
        m_Game.InitGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Game.isRunning)
            m_Game.Update();
    }

    private void FixedUpdate()
    {
        m_Game.FixedUpdate();
    }

    public void OnPostRender()
    {
        m_Game.Render();
    }


}
