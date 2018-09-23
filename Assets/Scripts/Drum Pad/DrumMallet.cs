using UnityEngine;
using System.Collections;

public class DrumMallet : MonoBehaviour
{
    public AudioSource Gong;

    HingeJoint hinge;
    Quaternion targetRot;
    new Rigidbody rigidbody;
    
	void Start ()
    {
        rigidbody = GetComponent<Rigidbody>();
        hinge = GetComponent<HingeJoint>();
        
        Unthump();
    }
    
    public void Thump(float Volume)
    {
        Gong.volume = Volume;
        Gong.Play();

        var spring = hinge.spring;
        spring.targetPosition = -90;
        hinge.spring = spring;
        
        Invoke("Unthump", 0.15f);
    }

    void Unthump()
    {
        var spring = hinge.spring;
        spring.targetPosition = 0;
        hinge.spring = spring;
    }
}
