using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour
{
    public float BeamRadius = 0.01f;
    public float MaxDistance = 30f;

    public RaycastHit HitInfo;
    public bool IsHit;

    GameObject beamVisual;
    int layerMask;

	void Start ()
    {
        beamVisual = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        beamVisual.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Laser");
        beamVisual.transform.SetParent(this.transform);
        beamVisual.transform.localRotation = Quaternion.AngleAxis(90, Vector3.right);
        //beamVisual.layer = LayerMask.NameToLayer("Audience Invisible");

        Destroy(beamVisual.GetComponent<Collider>());

        layerMask = LayerMask.GetMask("Interactive");
	}
	
	void Update ()
    {
        IsHit = Physics.Raycast(new Ray(transform.position, transform.forward), out HitInfo, MaxDistance, layerMask);
        
        if(IsHit)
        {
            var direction = HitInfo.point - transform.position;
            var midPoint = transform.position + direction / 2;
            beamVisual.transform.position = midPoint;
            beamVisual.transform.localScale = new Vector3(BeamRadius / 2, direction.magnitude / 2, BeamRadius / 2);
        }
        beamVisual.SetActive(IsHit);
    }
}
