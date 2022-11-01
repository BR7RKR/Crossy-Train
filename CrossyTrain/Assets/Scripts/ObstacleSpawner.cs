using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private float _spawnDelayMax;
    [SerializeField] private float _spawnDelayMin;
    [SerializeField] private float _spawnPosX;
    [SerializeField] private GameObject _obstacle;
    [SerializeField] private float _destroyDelay;

    private void Awake()
    {
        StartCoroutine(SpawnObstacle());
    }

    private IEnumerator SpawnObstacle()
    {
        RandomSignPosX();
        var rotationY = SetRotationY();
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(_spawnDelayMin, _spawnDelayMax));
            var obst = Instantiate(_obstacle, new Vector3(_spawnPosX, transform.position.y, transform.position.z), Quaternion.identity);
            obst.transform.Rotate(0,rotationY,0);
            Destroy(obst, _destroyDelay);
        }
    }
    
    private void RandomSignPosX()
    {
        if (Random.Range(-1f,1f) < 0)
            _spawnPosX *= -1;
    }

    private float SetRotationY()
    {
        if(_spawnPosX > 0)
            return 180;
        return 0;
    }
}
