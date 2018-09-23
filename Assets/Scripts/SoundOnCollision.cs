using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SoundOnCollision : MonoBehaviour
{
    new AudioSource audio;
    new Rigidbody rigidbody;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        rigidbody = GetComponent<Rigidbody>();

        rigidbody.sleepThreshold = 0.000001f;
    }    

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ignore Sound") return;
        audio.volume = collision.relativeVelocity.sqrMagnitude.Remap(0, 150, 0, 1, true);
        audio.Play();

        var gripMove = collision.gameObject.GetComponent<GripMoveRigidBody>();
        if (gripMove != null && gripMove.steamController != null)
        {
            //gripMove.steamController.PulseController((ushort)collision.relativeVelocity.sqrMagnitude.Remap(0, 300, 500, 2000, true));
            gripMove.steamController.PulseController(3999);
        }
    }
}
