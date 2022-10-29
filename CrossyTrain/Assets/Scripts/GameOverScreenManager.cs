using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private PlayerController _player;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        if (_player.IsGameOver)
            _gameOverScreen.SetActive(true);
    }
}
