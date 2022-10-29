using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _smoothness;

    private void Awake()
    {
        transform.position = _player.position + _offset;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _player.position + _offset, _smoothness);
    }
}
