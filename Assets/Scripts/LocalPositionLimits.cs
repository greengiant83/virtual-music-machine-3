using UnityEngine;
using System.Collections;

public enum AxisName
{
    X,
    Y,
    Z
}

public class LocalPositionLimits : MonoBehaviour
{
    public AxisName Axis = AxisName.Y;
    public float Minimum = 0;
    public float Maximum = 1;    
}
