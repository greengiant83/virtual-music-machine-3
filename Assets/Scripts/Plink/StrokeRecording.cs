using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StrokeRecording
{
    public List<StrokeDataPoint> Data = new List<StrokeDataPoint>();
}

public struct StrokeDataPoint
{
    public Vector3 Position;
    public bool IsMute;
    public float Timestamp;
}
