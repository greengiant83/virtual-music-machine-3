using UnityEngine;
using System.Collections;
using System;

public class LaserButton : MonoBehaviour
{
    public UIControllerButton Button = UIControllerButton.Trigger;
    public delegate void OnPressHandler(object sender, PressEventArgs e);
    public event OnPressHandler OnPress;
    public Vector3 PushAxis;
    public float HoverSizeMultiplier = 1.25f;
    public bool AnimateOnHover = true;

    UIController controller;
    
    Vector3 restingPosition;
    Vector3 targetScale;

    Vector3 _restingScale;
    public Vector3 restingScale
    {
        get { return _restingScale; }
        set
        {
            _restingScale = value;
            updateTargetScale();
        }
    }

    float _scaleMultiplier = 1;
    float scaleMultiplier
    {
        get { return _scaleMultiplier; }
        set
        {
            _scaleMultiplier = value;
            updateTargetScale();
        }

    }

    Vector3 _targetPosition;
    Vector3 targetPosition
    {
        get { return _targetPosition; }
        set
        {
            if (_targetPosition != value)
            {
                _targetPosition = value;
                isSeekingPosition = true;
            }
        }
    }

    void updateTargetScale()
    {
        targetScale = restingScale * scaleMultiplier;
        if (isButtonPressed) targetScale *= scaleMultiplier;
    }

    bool isSeekingPosition;
    

    bool isHovering;

    bool isButtonPressed
    {
        get
        {
            if (controller == null) return false;

            switch(Button)
            {
                case UIControllerButton.Grip:
                    return controller.SteamController.gripped;
                case UIControllerButton.Menu:
                    return controller.SteamController.menuPressed;
                case UIControllerButton.Pad:
                    return controller.SteamController.padPressed;
                case UIControllerButton.Trigger:
                    return controller.SteamController.triggerPressed;
                default:
                    return false;
            }
        }
    }

	protected virtual void Start ()
    {
        restingPosition = transform.localPosition;
        if(restingScale == Vector3.zero) restingScale = transform.localScale;
        targetScale = restingScale;
	}
	
	protected virtual void Update ()
    {
        if (isHovering)
        {
            Vector3 localPushDirection;
            if (PushAxis == Vector3.zero)
                localPushDirection = (transform.position - controller.transform.position) * 0.1f; // transform.forward * -0.01f;
            else
                localPushDirection = transform.TransformDirection(PushAxis); // * -0.01f;

            /*if (transform.parent != null) localPushDirection = transform.parent.InverseTransformDirection(localPushDirection);
            targetPosition = isButtonPressed ? restingPosition + localPushDirection : restingPosition;*/
            updateTargetScale();
        }
        else targetPosition = restingPosition;

        /*if(isSeekingPosition)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, 0.15f);
            if((transform.localPosition - targetPosition).magnitude < 0.01f)
            {
                transform.localPosition = targetPosition;
                isSeekingPosition = false;
            }
        }*/

        if(AnimateOnHover) transform.localScale = Vector3.Lerp(transform.localScale, targetScale, 0.1f);
    }


    void buttonClicked(UIController sender, UIControllerButton button)
    {
        if (button == this.Button)
        {
            var audio = GetComponent<AudioSource>();
            if (audio != null) audio.Play();
            if (OnPress != null) OnPress(this, new PressEventArgs(sender));
            OnButtonPress(sender);
        }
    }

    protected virtual void OnButtonPress(UIController sender)
    {

    }
    
    void ControllerHoverStart(UIController sender)
    {
        isHovering = true;
        scaleMultiplier = HoverSizeMultiplier;
        controller = sender;
        restingPosition = transform.localPosition;
    }

    void ControllerHoverEnd()
    {
        isHovering = false;
        scaleMultiplier = 1;
        controller = null;
    }

    void ControllerGripClicked(UIController sender)
    {
        buttonClicked(sender, UIControllerButton.Grip);
    }

    void ControllerPadClicked(UIController sender)
    {
        buttonClicked(sender, UIControllerButton.Pad);
    }

    void ControllerMenuClicked(UIController sender)
    {
        buttonClicked(sender, UIControllerButton.Menu);
    }

    void ControllerTriggerClicked(UIController sender)
    {
        buttonClicked(sender, UIControllerButton.Trigger);
    }
}

public class PressEventArgs : EventArgs
{
    public UIController Controller;

    public PressEventArgs(UIController Controller)
    {
        this.Controller = Controller;
    }
}
