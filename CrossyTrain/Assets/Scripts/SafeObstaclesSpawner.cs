using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SafeObstaclesSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _obstacles;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private int _obstaclesLimit;
    [SerializeField] private Transform _obstaclesStorage;

    private float _obstaclesSpawned;

    private void Awake()
    {
        SpawnObstacles();
    }

    private void SpawnObstacles()
    {
        for(int i = 0; i < _spawnPoints.Count; i++)
        {
            GameObject obst;
            if (Random.Range(-1f,1f) < 0 && IsEnoughSpace())
            {
                obst = Instantiate(_obstacles[Random.Range(0, _obstacles.Count)], _spawnPoints[i].position, _spawnPoints[i].rotation);
                obst.transform.SetParent(_obstaclesStorage);
                _obstaclesSpawned++;
            }
        }
    }

    private bool IsEnoughSpace()
    {
        return _obstaclesLimit >= _obstaclesSpawned;
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _obstaclesStorage.childCount; i++)
        {
            Destroy(_obstaclesStorage.GetChild(0).gameObject);
        }
    }
}
