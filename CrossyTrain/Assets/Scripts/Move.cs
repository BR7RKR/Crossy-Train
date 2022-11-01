using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
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
        transform.Translate(Vector3.right * (_speed * Time.deltaTime), Space.Self);
    }
}
