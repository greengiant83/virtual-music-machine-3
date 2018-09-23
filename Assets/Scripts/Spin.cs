using UnityEngine;
using System.Collections;

public class Spin : MonoBehaviour
{
    public Quaternion RotStep;
    public Vector3 MoveStep;

	// Use this for initialization
	void Start ()
    {	
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        transform.rotation *= RotStep;
        transform.position += MoveStep;
	}
}
