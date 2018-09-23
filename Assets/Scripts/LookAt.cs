using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class LookAt : MonoBehaviour
{
    float FollowSpeed = 0.02f;
    public Transform Subject;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        var lookRot = Quaternion.LookRotation(Subject.position - transform.position);
        if (Application.isPlaying)
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookRot, FollowSpeed);
        else
            this.transform.rotation = lookRot;	
	}
}
