using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private PlayerController _player;

    private void Start()
    {
        _score.text = "Score: 0";
    }

    private void Update()
    {
        _score.text = $"Score: {_player.Score}";
    }
}
