using UnityEngine;
using System.Collections;

public class ToggleDrawerButton : LaserButton
{
    public Transform Drawer;

    public bool IsInitiallyOpen = false;

    public Vector3 ClosedLocalPosition = Vector3.zero;
    public Vector3 OpenLocalPosition = Vector3.forward;

    public Vector3 ClosedScale = Vector3.one;
    public Vector3 OpenScale = Vector3.one;

    

    bool isSeeking = false;
    Vector3 targetPosition;
    Vector3 targetSize; 

    private bool _isOpen;
    public bool IsOpen
    {
        get
        {
            return _isOpen;
        }
        set
        {
            _isOpen = value;

            targetPosition = value ? OpenLocalPosition : ClosedLocalPosition;
            targetSize = value ? OpenScale : ClosedScale;
            isSeeking = true;
        }
    }

    protected override void OnButtonPress(UIController sender)
    {
        IsOpen = !IsOpen;
    }

    protected override void Start()
    {
        base.Start();
        IsOpen = IsInitiallyOpen;
    }

    protected override void Update()
    {
        base.Update();

        if(isSeeking)
        {
            Drawer.localScale = Vector3.Lerp(Drawer.localScale, targetSize, 0.1f);
            Drawer.localPosition = Vector3.Lerp(Drawer.localPosition, targetPosition, 0.1f);
            if((Drawer.localPosition - targetPosition).magnitude < 0.01f)
            {
                Drawer.localPosition = targetPosition;
                Drawer.localScale = targetSize;
                isSeeking = false;
            }
        }
    }
}
