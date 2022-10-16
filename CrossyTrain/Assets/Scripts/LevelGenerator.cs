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

    private Vector3 _currentPosition = new Vector3(0, 0, 0);
    private Queue<GameObject> _levelSegments = new Queue<GameObject>();

    void Start()
    {
        Debug.Assert(_segmentsHolder, "No Segment Holder!");
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
