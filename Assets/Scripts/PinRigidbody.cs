using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PinRigidbody : MonoBehaviour
{
    public Transform target;

    float positionMagic = 20000f;
    float rotMagic = 50f;
    new Rigidbody rigidbody;

    void Start ()
    {
        rigidbody = GetComponent<Rigidbody>();	
	}
	
	void Update ()
    {
        var positionDelta = target.position - transform.position;

        if(positionDelta.magnitude > 0.5f) //If things get really out of whack just fix it instead of hoping the physics engine will get it sorted out
        {
            rigidbody.position = target.position;
            rigidbody.rotation = target.rotation;
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
            return;
        }

        rigidbody.velocity = positionDelta * positionMagic * Time.fixedDeltaTime;

        float angle;
        Vector3 axis;
        var rotationDelta = target.rotation * Quaternion.Inverse(transform.rotation);
        rotationDelta.ToAngleAxis(out angle, out axis);

        if (angle > 180) angle -= 360;
        float maxAngularVelocity = rigidbody.maxAngularVelocity;
        rigidbody.maxAngularVelocity = float.MaxValue;
        var rot = (Time.fixedDeltaTime * angle * axis) * rotMagic;
        if (!float.IsNaN(rot.x)) rigidbody.angularVelocity = rot;
    }
}
