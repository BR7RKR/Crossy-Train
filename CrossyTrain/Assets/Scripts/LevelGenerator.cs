using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int _maxSegmentsAtOnce;
    [SerializeField] private List<SegmentModel> _segmentVariants;
    [SerializeField] private Transform _segmentsHolder;
    [SerializeField] private Transform _targetForObserving;
    [SerializeField] private float _minDistanceFromTarget;
    [SerializeField] private int _obstaclesToMove = 2;
    
    private Vector3 _currentPosition = new Vector3(0, 0, 0);
    private Queue<GameObject> _levelSegments = new Queue<GameObject>();
    private Vector3 _currentDeletePosition;
    private Vector3 _previousTargetPosition;

    void Start()
    {
        _previousTargetPosition = _targetForObserving.position;
        _currentDeletePosition = _targetForObserving.position;
        Debug.Assert(_segmentsHolder, "No Segment Holder!");
        for (int i = 0; i < _maxSegmentsAtOnce; i++)
            SpawnSegment(Random.Range(0, _segmentVariants.Count), ref _currentPosition);
    }
    
    void Update()
    {
        if (_targetForObserving.position.z >= _previousTargetPosition.z+1)
        {
            SpawnSegment(Random.Range(0, _segmentVariants.Count), ref _currentPosition);
            _previousTargetPosition = _targetForObserving.position;
        }
        if (_targetForObserving.position.z - _minDistanceFromTarget >= _currentDeletePosition.z) // использовать систему ивентов для получения информации о нажатии от игрока
        {
            Destroy(_levelSegments.Dequeue());
            _currentDeletePosition.z++;
        }
    }

    private void SpawnSegment(int index, ref Vector3 position)
    {
        var minInRow = 1;
        var segmentsInRow = Random.Range(minInRow, _segmentVariants[index].maxInRow);
        for (var i = 0; i < segmentsInRow; i++)
        {
            var segmentModel = Instantiate(_segmentVariants[index].segment, position, Quaternion.identity, _segmentsHolder);
            position.z++;
            _levelSegments.Enqueue(segmentModel);
        }
    }
}
