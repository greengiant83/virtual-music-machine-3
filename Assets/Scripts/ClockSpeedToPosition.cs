using UnityEngine;
using System.Collections;

public class ClockSpeedToPosition : MonoBehaviour
{
    public Clock Clock;
    float MinClockSpeed = 100;
    float MaxClockSpeed = 700;

    float lastClockSpeed;
    LocalPositionLimits limits;

	void Start ()
    {
        limits = GetComponent<LocalPositionLimits>();
        var position = transform.localPosition;
        position.y = Clock.BPM.Remap(MinClockSpeed, MaxClockSpeed, limits.Minimum, limits.Maximum, true);
        transform.localPosition = position;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        var newClockSpeed = transform.localPosition.y.Remap(limits.Minimum, limits.Maximum, MinClockSpeed, MaxClockSpeed, true);
        if(Mathf.Abs(newClockSpeed - lastClockSpeed) > 10)
        {
            Clock.BPM = newClockSpeed;
            Clock.UpdateBPM();
        }
        lastClockSpeed = Clock.BPM;	
	}
}
