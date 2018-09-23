using UnityEngine;
using System.Collections;

public class BlockStroker : MonoBehaviour
{
    public GameObject StrokeObject;

    PlinkInstrument instrument;
    Vector3 prevPosition;
    
	void Start ()
    {
        instrument = transform.parent.GetComponent<PlinkInstrument>();
	}

    void FixedUpdate()
    {
        for(int i=0;i<transform.childCount;i++)
        {
            var child = transform.GetChild(i);
            child.localScale *= 0.99f;
            if (child.localScale.magnitude < 0.02f) Destroy(child.gameObject);
        }
    }

    public void Clear()
    {
        return;
        for(int i=0;i<transform.childCount;i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
	
    public void AddPosition(Vector3 position, bool isMute)
    {
        int maxChildren = 400;
        int overage = transform.childCount - maxChildren;
        for(int i=0;i<overage;i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        var particle = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Destroy(particle.GetComponent<Collider>());
        particle.transform.SetParent(this.transform);
        particle.transform.position = position;
        particle.transform.localScale = Random.insideUnitSphere * 0.13f;
        particle.transform.rotation = Random.rotation;
        particle.GetComponent<Renderer>().material = isMute ? instrument.DrawMaterialInactive : instrument.DrawMaterial;

        var spin = particle.AddComponent<Spin>();
        spin.RotStep = Quaternion.AngleAxis(Random.Range(-2, 2), Random.onUnitSphere);
        spin.MoveStep = (position - prevPosition).normalized * 0.0015f + Vector3.up * 0.003f;
        //spin.MoveStep += Vector3.up * 0.003f;

        prevPosition = position;
    }
}
