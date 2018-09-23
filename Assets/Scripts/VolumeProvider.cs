using UnityEngine;
using System.Collections;

public class VolumeProvider : MonoBehaviour
{
    public float Volume = 1f;
    public float MinVolume = 0;
    public float MaxVolume = 1;

    LocalPositionLimits limits;

    // Use this for initialization
    void Start ()
    {
        limits = GetComponent<LocalPositionLimits>();
        var position = transform.localPosition;
        position.y = Volume.Remap(MinVolume, MaxVolume, limits.Minimum, limits.Maximum, true);
        transform.localPosition = position;
    }
	
	void FixedUpdate ()
    {
        Volume = transform.localPosition.y.Remap(limits.Minimum, limits.Maximum, MinVolume, MaxVolume, true);   
    }
}
