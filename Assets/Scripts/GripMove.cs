using UnityEngine;
using System.Collections;

public class GripMove : MonoBehaviour
{
    public Transform Proxy;
    public bool LockXAxis = false;
    public bool LockYAxis = false;
    public bool LockZAxis = false;
    public bool LockRotation = false;

    public bool IsMoving { get; private set; }

    SteamVR_TrackedController steamController;
    GameObject anchorObject;
    
    bool isHovering = false;
    bool isSubscribed = false;

    LocalPositionLimits[] positionLimits;

    void Start ()
    {
        positionLimits = GetComponents<LocalPositionLimits>();
        if (Proxy == null) Proxy = this.transform;
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
            var position = Proxy.position;
            if (!LockXAxis) position.x = anchorObject.transform.position.x;
            if (!LockYAxis) position.y = anchorObject.transform.position.y;
            if (!LockZAxis) position.z = anchorObject.transform.position.z;
            if (!LockRotation) Proxy.rotation = anchorObject.transform.rotation;
            
            position.y = anchorObject.transform.position.y;
            Proxy.position = position;

            if (positionLimits.Length > 0)
            {
                position = Proxy.localPosition;
                foreach (var limit in positionLimits)
                {
                    switch (limit.Axis)
                    {
                        case AxisName.X:
                            position.x = Mathf.Clamp(position.x, limit.Minimum, limit.Maximum);
                            break;
                        case AxisName.Y:
                            position.y = Mathf.Clamp(position.y, limit.Minimum, limit.Maximum);
                            break;
                        case AxisName.Z:
                            position.z = Mathf.Clamp(position.z, limit.Minimum, limit.Maximum);
                            break;
                    }
                }              
                Proxy.localPosition = position;
            }

            if (steamController.padTouched)
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
