using UnityEngine;
using System.Collections;

public class DeadZone : MonoBehaviour
{
    public Material ActiveMaterial;
    public Material InactiveMaterial;
    public Transform ObjectToTrack;

    public bool IsActive { get; set; }

    public Vector3 Center { get { return transform.position; } }
    public float Radius {  get { return transform.localScale.x / 2;  } }

    new Renderer renderer;
    
	void Start ()
    {
        renderer = GetComponent<Renderer>();	
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        var delta = ObjectToTrack.position - Center;
        delta.y = 0;
        IsActive = delta.magnitude < Radius;
        renderer.material = IsActive ? ActiveMaterial : InactiveMaterial;	
	}
}
