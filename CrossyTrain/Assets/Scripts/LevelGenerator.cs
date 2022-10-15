using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int _maxSegmentsAtOnce;
    [SerializeField] private List<GameObject> _segmentVariants;
    [Tooltip("Число в диапазоне от 0 до длины коллекции включительно")]
    [SerializeField] private int _firstSegmentIndex;
    
    private Vector3 _currentPosition = new Vector3(0, 0, 0);
    private Queue<GameObject> _levelSegments = new Queue<GameObject>();

    void Start()
    {
        SpawnSegment(_firstSegmentIndex, ref _currentPosition);
        for (int i = 0; i < _maxSegmentsAtOnce; i++)
            SpawnSegment(Random.Range(0, _segmentVariants.Count), ref _currentPosition);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) // использовать систему ивентов для получения информации о нажатии от игрока
        {
            SpawnSegment(Random.Range(0, _segmentVariants.Count), ref _currentPosition);
            Destroy(_levelSegments.Dequeue());
        }
    }

    private void SpawnSegment(int index, ref Vector3 position)
    {
        var segment = Instantiate(_segmentVariants[index], position, Quaternion.identity);
        position.z++;
        _levelSegments.Enqueue(segment);
    }
}
