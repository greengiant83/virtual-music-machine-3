using UnityEngine;
using System.Collections;

public class GripMoveRigidBody : MonoBehaviour
{
    public Transform Proxy;
    public bool LockXAxis = false;
    public bool LockYAxis = false;
    public bool LockZAxis = false;
    public bool LockRotation = false;

    public bool IsMoving { get; private set; }

    public SteamVR_TrackedController steamController { get; private set; }
    GameObject anchorObject;
    
    bool isHovering = false;
    bool isSubscribed = false;

    LocalPositionLimits[] positionLimits;
    new Rigidbody rigidbody;

    void Start ()
    {
        if (Proxy == null) Proxy = this.transform;
        positionLimits = GetComponents<LocalPositionLimits>();
        rigidbody = Proxy.GetComponent<Rigidbody>();
        
	}
    
    private void Controller_Gripped(object sender, ClickedEventArgs e)
    {
        IsMoving = true;
        anchorObject = new GameObject("Temp Movement Proxy");
        anchorObject.transform.position = Proxy.position;
        anchorObject.transform.rotation = Proxy.rotation;
        anchorObject.transform.SetParent(steamController.transform, true);
    }

    void Update()
    {
        if(anchorObject != null)
        {
            float positionMagic = 5000;
            float rotationMagic = 50;

            rigidbody.velocity = (anchorObject.transform.position - rigidbody.position) * positionMagic * Time.fixedDeltaTime;

            float angle;
            Vector3 axis;
            var rotDelta = anchorObject.transform.rotation * Quaternion.Inverse(rigidbody.rotation);
            rotDelta.ToAngleAxis(out angle, out axis);

            if (angle > 180) angle -= 360;
            rigidbody.angularVelocity = (Time.deltaTime * angle * axis) * rotationMagic;

            if(steamController.padTouched)
            {
                var padPosition = steamController.GetPadPosition();
                var move = steamController.transform.forward * padPosition.y * 0.03f;
                if (padPosition.y > 0 || (steamController.transform.position - anchorObject.transform.position).magnitude > 0.3f)
                    anchorObject.transform.position += move;
            }
        }
    }

    private void Controller_Ungripped(object sender, ClickedEventArgs e)
    {
        Destroy(anchorObject);
        anchorObject = null;
        IsMoving = false;

        if (!isHovering) unsubsribe();
    }

    void ControllerHoverStart(UIController sender)
    {
        isHovering = true;
        subscribe(sender.SteamController);
    }

    void ControllerHoverEnd(UIController sender)
    {
        if (!IsMoving) unsubsribe();
        isHovering = false;
    }

    void subscribe(SteamVR_TrackedController steamController)
    {
        if (isSubscribed) return;

        this.steamController = steamController;
        steamController.Gripped += Controller_Gripped;
        steamController.Ungripped += Controller_Ungripped;
        isSubscribed = true;
    }

    void unsubsribe()
    {
        if (!isSubscribed) return;
        steamController.Gripped -= Controller_Gripped;
        steamController.Ungripped -= Controller_Ungripped;
        steamController = null;
        isSubscribed = false;
    }
}
