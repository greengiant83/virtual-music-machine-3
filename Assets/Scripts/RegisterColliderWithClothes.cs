using UnityEngine;
using System.Collections;

public class RegisterColliderWithClothes : MonoBehaviour
{
	void Start ()
    {
        var clothes = GameObject.FindObjectsOfType<Cloth>();
        var collider = GetComponent<SphereCollider>();
        foreach(var cloth in clothes)
        {
            cloth.sphereColliders = new ClothSphereColliderPair[] { new ClothSphereColliderPair(collider) };
        }
	}
	
	void Update ()
    {
	
	}
}
