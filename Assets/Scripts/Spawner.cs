using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    public GameObject Template;
    public float SpawnRateInSeconds = 0.5f;
    
	void Start ()
    {
        Tick();
	}

    void Tick()
    {
        var clone = Instantiate<GameObject>(Template);
        var localPoint = new Vector3(
            Random.Range(-0.5f, 0.5f),
            Random.Range(-0.5f, 0.5f),
            Random.Range(-0.5f, 0.5f)
            );

        clone.transform.position = transform.TransformPoint(localPoint);
        clone.transform.rotation = Random.rotation;

        Invoke("Tick", SpawnRateInSeconds);
    }
	
	void Update ()
    {
	
	}
}
