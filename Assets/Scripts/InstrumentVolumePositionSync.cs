using UnityEngine;
using System.Collections;

public class InstrumentVolumePositionSync : MonoBehaviour
{
    PlinkInstrument instrument;
    InstrumentController instrumentController;
    LocalPositionLimits positionLimits;

    float prevVolume;
    float prevLocalPosition;

	void Start ()
    {
        instrument = GetComponent<PlinkInstrument>();
        instrumentController = GetComponent<InstrumentController>();
        positionLimits = GetComponent<LocalPositionLimits>();
	}
	
	void Update ()
    {
        if(prevLocalPosition !=  transform.localPosition.y)
        {
            instrument.Volume = transform.localPosition.y.Remap(positionLimits.Minimum, positionLimits.Maximum, 0, 1);
            if (instrument.Volume == 0)
            {                
                instrumentController.Clear();
            }
        }
        else if(prevVolume != instrument.Volume)
        {
            var position = transform.localPosition;
            position.y = instrument.Volume.Remap(0, 1, positionLimits.Minimum, positionLimits.Maximum, true);
            transform.localPosition = position;
        }

        prevVolume = instrument.Volume;
        prevLocalPosition = transform.localPosition.y;
	}
}
