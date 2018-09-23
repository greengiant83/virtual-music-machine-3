using UnityEngine;
using System.Collections;

public class ToggleMaterial : MonoBehaviour
{
    public Material OnMaterial;
    public Material OffMaterial;

    new Renderer renderer;

    private bool _isOn = true;
    public bool IsOn
    {
        get { return _isOn; }
        set
        {
            _isOn = value;
            renderer.material = value ? OnMaterial : OffMaterial;
        }
    }

    void Start ()
    {
        renderer = GetComponent<Renderer>();	
	}

    public void ControllerTriggerClicked(UIController sender)
    {
        IsOn = !IsOn;
    }
}
