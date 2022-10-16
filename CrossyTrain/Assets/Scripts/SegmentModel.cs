using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Segment Model", menuName = "Segment Model")]
public class SegmentModel : ScriptableObject
{
    public GameObject segment;
    public int maxInRow;
}
