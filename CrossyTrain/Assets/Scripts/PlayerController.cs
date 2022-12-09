using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _borderX;

    private Menu _gameMenu;
    private Animator _animator;
    private bool _isJumping;
    private bool _isForwardBlocked;
    private bool _isLeftBlocked;
    private bool _isRightBlocked;
    
    private static readonly int Jump = Animator.StringToHash("jump");
    
    public int Score { get; private set; }
    public bool IsGameOver { get; private set; }

    private void Awake()
    {
        Time.timeScale = 1;
        IsGameOver = false;
        _animator = GetComponent<Animator>();
        _gameMenu = FindObjectOfType<Menu>();

        SwipeDetection.SwipeEvent += MovementLogic;
    }

    private void MovementLogic(Vector2 direction) //TODO: возможно стоит на пк оставить управление wasd
    {
        if (_gameMenu.IsGameStarted)
        {
            AreaScan();
            if (direction == Vector2.up && !_isJumping && !_isForwardBlocked)
            {
                float xDelta = 0;
                if (transform.position.x % 1 != 0)
                {
                    xDelta = Mathf.Round(transform.position.x) - transform.position.x;
                }

                MovePlayer(new Vector3(xDelta, 0, 1));
                Score++;
            }
            else if (direction == Vector2.right && !_isJumping && !_isRightBlocked)
                MovePlayer(new Vector3(1, 0, 0));
            else if (direction == Vector2.left && !_isJumping && !_isLeftBlocked)
                MovePlayer(new Vector3(-1, 0, 0));
        }
    }

    private void MovePlayer(Vector3 delta)
    {
        _animator.SetTrigger(Jump);
        _isJumping = true;
        transform.Translate(CheckForBorders(delta));
    }

    private void AreaScan()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.red);
        RaycastHit hitForward, hitLeft, hitRight;
        if (Physics.Raycast(transform.position, transform.forward, out hitForward, 1))
        {
            if (hitForward.collider.gameObject.transform.CompareTag("SafeObstacle") && !_isForwardBlocked)
            {
                _isForwardBlocked = true;
            }
        }
        else _isForwardBlocked = false;

        if (Physics.Raycast(transform.position, -transform.right, out hitLeft, 1))
        {
            if (hitLeft.collider.gameObject.transform.CompareTag("SafeObstacle") && !_isLeftBlocked)
            {
                _isLeftBlocked = true;
            }
        }
        else _isLeftBlocked = false;

        if (Physics.Raycast(transform.position, transform.right, out hitRight, 1))
        {
            if (hitRight.collider.gameObject.transform.CompareTag("SafeObstacle") && !_isRightBlocked)
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
        _isJumping = false;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Obstacle"))
        {
            IsGameOver = true;
            SwipeDetection.SwipeEvent -= MovementLogic;
        }
    }
}