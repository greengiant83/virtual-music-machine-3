using UnityEngine;
using System.Collections;
using System;

public class TestSequencer : ControllerToolBase
{
    public MeshRenderer Indicator;

    int segment;
    PlinkInstrument instrument;
    StrokeSampleAudio audioPlayer;

    public override void OnToolActivate(MonoBehaviour Sender)
    {
        InvokeRepeating("Tick", 0.00001f, 0.75f);
        steamController.TriggerClicked += SteamController_TriggerClicked;
    }
    
    public override void OnToolDeactivate()
    {
        steamController.TriggerClicked -= SteamController_TriggerClicked;
    }

    private void SteamController_TriggerClicked(object sender, ClickedEventArgs e)
    {
        if(uiController.SelectedObject != null)
        {
            instrument = uiController.SelectedObject.GetComponent<PlinkInstrument>();
            if(instrument != null)
            {
                audioPlayer = (StrokeSampleAudio)instrument.AudioPlayer;
                segment = 0;
            }
        }
    }

    void Tick()
    {
        if (instrument == null) return;

        Debug.Log("Test Segment: " + segment);
        audioPlayer.PlaySample(segment);

        segment++;
        
    }

    void Start ()
    {
	
	}
	
	void Update ()
    {
	
	}
}
