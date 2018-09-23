//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using System;

//public class PushButton : MonoBehaviour 
//{
//    public delegate void OnPressHandler(object sender, PressEventArgs e);
//    public event OnPressHandler OnPress;

//    public Transform PushSurface;
//    public Material TravelingMaterial;
//    public Material PressedMaterial;
//    public bool PlayClickOnPress;
//    public float DebounceSeconds = 0.25f;

//    [Range(0, 0.5f)]
//    public float MaxTravel = 0.03f;

//    [Range(0, 0.5f)]
//    float SensorRange = 0.04f;

//    public float PercentPressed { get; private set; }
//    public bool IsTraveling { get; private set; }
//    public bool IsPressed { get; private set; }


//    float sensorDepth { get { return PushSurface.localPosition.z + SensorRange; } }

//    float triggerPoint = 0.99f;
//    float travelStartTime;
//    bool isDebouncing;

//    new Renderer renderer;
//    Material idleMaterial;
//    AudioSource clickSound;

//	void Start () 
//    {
//        renderer = PushSurface.GetComponent<Renderer>();
//        clickSound = GetComponent<AudioSource>();

//        idleMaterial = renderer.material;
//	}
    	
//	void Update () 
//    {
//        updatePosition();
//        checkTrigger();
//        updateState();
//	}

//    void checkTrigger()
//    {
//        if (isDebouncing) return;

//        bool wasPressed = IsPressed;
//        IsPressed = PercentPressed >= triggerPoint;
//        if (IsPressed && !wasPressed)
//        {
//            triggerPress();
//        }

//        if(!IsPressed && wasPressed)
//        {
//            travelStartTime = Time.realtimeSinceStartup;
//        }
//    }

//    void beginDebounce()
//    {
//        isDebouncing = true;
//        Invoke("endDebounce", DebounceSeconds);
//    }

//    void endDebounce()
//    {
//        isDebouncing = false;
//    }

//    void updateState()
//    {
//        Material showMaterial = null;
//        if (IsPressed)
//            showMaterial = PressedMaterial;
//        else if (IsTraveling)
//            showMaterial = TravelingMaterial;
//        else
//            showMaterial = idleMaterial;

//        renderer.material = showMaterial != null ? showMaterial : idleMaterial;
//    }
    
//    void updatePosition()
//    {
//        RaycastHit hitInfo;
//        var boxScale = PushSurface.lossyScale / 2;
//        var thickness = PushSurface.lossyScale.z / 2;
        
//        //Cast a box tunnel starting past the furthest position the button could be looking back at where it starts
//        var startingPoint = transform.position + transform.forward * sensorDepth;
//        if (Physics.BoxCast(startingPoint, boxScale, -transform.forward, out hitInfo, transform.rotation, sensorDepth))
//        {
//            var localHitPoint = transform.InverseTransformPoint(hitInfo.point);
//            var depth = Mathf.Clamp(localHitPoint.z, -thickness, MaxTravel-thickness);
//            PushSurface.localPosition = new Vector3(0, 0, depth + PushSurface.lossyScale.z / 2);
//        }
//        else PushSurface.localPosition = Vector3.zero;

//        //Update calculated values
//        bool wasTraveling = IsTraveling;
//        IsTraveling = PushSurface.localPosition.z > 0;
//        PercentPressed = PushSurface.localPosition.z / MaxTravel;

//        if(!wasTraveling && IsTraveling)
//        {
//            travelStartTime = Time.realtimeSinceStartup;
//        }
//    }


//    private void triggerPress()
//    {
//        float velocity = Time.realtimeSinceStartup - travelStartTime;
//        velocity = velocity.Remap(0, 0.1f, 1, 0);
//        velocity = Mathf.Clamp(velocity, 0, 1);

//        beginDebounce();

//        if (PlayClickOnPress && clickSound != null) clickSound.PlayOneShot(clickSound.clip, velocity.Remap(0, 1, 0.1f, 2f));

//        if (OnPress != null) OnPress(this, new PressEventArgs(velocity));
//    }
    

//    void OnDrawGizmosSelected()
//    {
//        //Draw fully pressed position
//        var pos = transform.TransformPoint(Vector3.forward * (MaxTravel + PushSurface.localScale.z / 2));
//        var scale = PushSurface.lossyScale;
//        scale.z = 0.001f;

//        Gizmos.color = Color.red;
//        Matrix4x4 rotMat = Matrix4x4.TRS(pos, PushSurface.rotation, Vector3.one);
//        Gizmos.matrix = rotMat;

//        Gizmos.DrawCube(Vector3.zero, scale);
//    }
//}


//public class PressEventArgs : EventArgs
//{
//    public float Velocity;

//    public PressEventArgs(float Velocity)
//    {
//        this.Velocity = Velocity;
//    }
//}
