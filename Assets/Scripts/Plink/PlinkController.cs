using UnityEngine;
using System.Collections;
using System;

public class PlinkController : ControllerToolBase
{
    public InstrumentController instrumentController { get; private set; }

    new Renderer renderer;
    Material inactiveMaterial;
    Vector3 previousDetectorPosition;
    MixerGroup mixerGroup;

    public bool IsActive;
    
    void Start ()
    {
        mixerGroup = GameObject.Find("Paint Pots").GetComponent<MixerGroup>();
	}
    
    private void SteamController_TriggerClicked(object sender, ClickedEventArgs e)
    {
        if(!uiController.Laser.IsHit) StartStroke();
    }

    private void SteamController_TriggerUnclicked(object sender, ClickedEventArgs e)
    {
        EndStroke();
    }

    public override bool CanToolActivate(MonoBehaviour Sender)
    {
        var instrumentController = Sender.gameObject.GetComponent<InstrumentController>();
        return instrumentController.plinkController == null;
    }

    public override void OnToolActivate(MonoBehaviour Sender)
    {
        //Turn off the old one if we had one going
        if (instrumentController != null) instrumentController.EndStroke();

        instrumentController = Sender.gameObject.GetComponent<InstrumentController>();
        instrumentController.plinkController = this;
        renderer = transform.Find("Cube").GetComponent<MeshRenderer>();
        renderer.material = instrumentController != null ? instrumentController.instrument.DrawMaterial : inactiveMaterial;

        steamController.TriggerClicked += SteamController_TriggerClicked;
        steamController.TriggerUnclicked += SteamController_TriggerUnclicked;
    }

    public override void OnToolDeactivate()
    {
        EndStroke();
        instrumentController.plinkController = null;

        steamController.TriggerClicked -= SteamController_TriggerClicked;
        steamController.TriggerUnclicked -= SteamController_TriggerUnclicked;
    }
    

    void Update ()
    {
        if (instrumentController != null)
        {
            if (IsActive)
            {
                bool isMute = false;
                instrumentController.AddPosition(transform.position, isMute);
                previousDetectorPosition = transform.position;
            }
        }
	}
    
    void StartStroke()
    {
        if (instrumentController != null)
        {
            mixerGroup.StartInstrumentSolo(instrumentController.instrument);
            instrumentController.StartStroke();
            IsActive = true;
        }
    }

    void EndStroke()
    {
        if (instrumentController != null && IsActive)
        {
            mixerGroup.EndInstrumentSolo(instrumentController.instrument);
            instrumentController.EndStroke();
            IsActive = false;
        }
    }
}
