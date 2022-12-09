using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject _inGameCanvas;

    public bool IsGameStarted { get; private set; }
    
    private void Awake()
    {
        _inGameCanvas.SetActive(false);
        
    }

    public void StartGame()
    {
        IsGameStarted = true;
        transform.gameObject.SetActive(false);
        _inGameCanvas.SetActive(true);
    }
}
