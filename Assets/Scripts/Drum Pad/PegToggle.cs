using UnityEngine;
using System.Collections;

public class PegToggle : MonoBehaviour
{
    Quaternion onRot;
    Quaternion offRot;
    Quaternion targetRot;

    private bool _isOn;
    public bool IsOn
    {
        get { return _isOn; }
        set
        {
            _isOn = value;
            targetRot = value ? onRot : offRot;
        }
    }

	// Use this for initialization
	void Start ()
    {
        onRot = Quaternion.AngleAxis(180, Vector3.up);
        offRot = Quaternion.identity;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRot, 0.2f);
	}

    public void ControllerTriggerClicked(UIController sender)
    {
        IsOn = !IsOn;
    }
}
