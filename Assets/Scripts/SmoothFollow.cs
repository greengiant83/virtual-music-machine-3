using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour {
    Transform Target;

    

	// Use this for initialization
	void Start ()
    {
        Target = new GameObject("Camera Target").transform;
        Target.position = transform.position;
        Target.rotation = transform.rotation;
        Target.SetParent(transform.parent);

        transform.SetParent(null);	
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = Vector3.Lerp(transform.position, Target.position, 0.01f);

        var targetRot = Target.rotation.eulerAngles;
        targetRot.z = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(targetRot), 0.01f);
	}
}
