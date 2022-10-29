using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _borderX;
    
    private Animator _animator;
    private bool _isJupming;
    private bool _isForwardBlocked;
    private bool _isLeftBlocked;
    private bool _isRightBlocked;
    public int Score { get; private set; }
    public bool IsGameOver { get; private set; }

    private static readonly int Jump = Animator.StringToHash("jump");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        AreaScan();
        if (Input.GetKeyDown(KeyCode.W) && !_isJupming && !_isForwardBlocked)
        {
            float xDelta = 0;
            if (transform.position.x % 1 != 0)
            {
                xDelta = Mathf.Round(transform.position.x) - transform.position.x;
            }
            MovePlayer(new Vector3(xDelta, 0, 1));
            Score++;
        }
        else if (Input.GetKeyDown(KeyCode.A) && !_isJupming && !_isLeftBlocked)
        {
            MovePlayer(new Vector3(-1, 0, 0));
        }
        else if (Input.GetKeyDown(KeyCode.D) && !_isJupming && !_isRightBlocked)
        {
            MovePlayer(new Vector3(1, 0, 0));
        }
    }

    private void MovePlayer(Vector3 delta)
    {
        _animator.SetTrigger(Jump);
        _isJupming = true;
        transform.Translate(CheckForBorders(delta));
    }
    
    private void AreaScan()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.red);
        RaycastHit hitForward, hitLeft, hitRight;
        if (Physics.Raycast(transform.position, transform.forward, out hitForward, 1))
        {
            if(hitForward.collider.gameObject.transform.CompareTag("SafeObstacle") && !_isForwardBlocked)
            {
                _isForwardBlocked = true;
            }
        }
        else _isForwardBlocked = false;

        if (Physics.Raycast(transform.position, -transform.right, out hitLeft,1))
        {
            if(hitLeft.collider.gameObject.transform.CompareTag("SafeObstacle") && !_isLeftBlocked)
            {
                _isLeftBlocked = true;
            }
        }
        else _isLeftBlocked = false;

        if (Physics.Raycast(transform.position, transform.right, out hitRight, 1))
        {
            if(hitRight.collider.gameObject.transform.CompareTag("SafeObstacle") && !_isRightBlocked)
            {
                _isRightBlocked = true;
            }
        }
        else _isRightBlocked = false;
    }
    
    private Vector3 CheckForBorders(Vector3 delta)
    {
        var nextPosZ = transform.position.x + delta.x;
        if (nextPosZ >= _borderX)
            return new Vector3(0, 0, 0);
        if (nextPosZ <= -_borderX)
            return new Vector3(0, 0, 0);
        return delta;
    }

    public void FinishJump()
    {
        _isJupming = false;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Obstacle"))
            IsGameOver = true;
    }
}
