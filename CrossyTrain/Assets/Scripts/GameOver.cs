using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private PlayerController _player;
    
    void Start()
    {
        _player = FindObjectOfType<PlayerController>();
    }
    
    void Update()
    {
        if (_player.IsGameOver)
        {
            Time.timeScale = 0;
        }
    }
}
