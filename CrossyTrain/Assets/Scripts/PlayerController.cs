using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    private Animator _animator;
    
    private static readonly int Jump = Animator.StringToHash("jump");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.W))
        {
            _animator.SetTrigger(Jump);
        }
    }
}
