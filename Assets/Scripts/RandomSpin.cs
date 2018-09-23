using UnityEngine;
using System.Collections;

public class RandomSpin : MonoBehaviour
{
    public bool IsActive;

    float speed = 0;
    float targetSpeed = 1;

    Vector3 axis = Vector3.up;
    Vector3 targetAxis;

	void Start ()
    {
        targetAxis = Random.onUnitSphere;
	}
	
	void FixedUpdate ()
    {
        targetSpeed = IsActive ? 1 : 0;
        speed = Mathf.Lerp(speed, targetSpeed, 0.05f);

        if (speed > 0.01)
        {
            axis = Vector3.Lerp(axis, targetAxis, 0.01f);
            if ((axis - targetAxis).magnitude < 0.1f) targetAxis = Random.onUnitSphere;

            transform.Rotate(axis, speed);
        }
	}
}
