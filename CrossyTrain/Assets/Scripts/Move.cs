using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private float _speed;
    void Update()
    {
        MoveRight();
    }

    private void MoveRight()
    {
        transform.Translate(transform.right.normalized * _speed * Time.deltaTime);
    }
}
