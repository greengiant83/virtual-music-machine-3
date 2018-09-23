using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MixerInstrumentSettings
{
    public PlinkInstrument instrument;
    public float TargetVolume;
    public float BaseVolume;
    public bool IsSeeking;
}

public class MixerGroup : MonoBehaviour
{
    public List<MixerInstrumentSettings> instrumentSettings;
    public List<PlinkInstrument> soloInstruments = new List<PlinkInstrument>();

    public void StartInstrumentSolo(PlinkInstrument instrument)
    {
        if(!soloInstruments.Contains(instrument))
        {
            soloInstruments.Add(instrument);

            if (soloInstruments.Count == 1) updateBaseVolumes();
            
            updateTargetVolumes();
        }
    }

    public void EndInstrumentSolo(PlinkInstrument instrument)
    {
        if (soloInstruments.Contains(instrument))
        {
            soloInstruments.Remove(instrument);
            if (soloInstruments.Count == 0)
                restoreBaseVolumes();
            else
                updateTargetVolumes();
        }
    }

    void updateBaseVolumes()
    {
        foreach(var instrument in instrumentSettings)
        {
            bool isSolo = soloInstruments.Contains(instrument.instrument);
            instrument.BaseVolume = isSolo ? 1 : instrument.instrument.Volume;
        }
    }

    void restoreBaseVolumes()
    {
        foreach (var instrument in instrumentSettings)
        {
            instrument.TargetVolume = instrument.BaseVolume;
            instrument.IsSeeking = true;
        }
    }

    void updateTargetVolumes()
    {
        foreach(var instrument in instrumentSettings)
        {
            bool isSolo = soloInstruments.Contains(instrument.instrument);
            instrument.TargetVolume = isSolo ? 1 : instrument.instrument.Volume * 0.5f;
            instrument.IsSeeking = true;
        }
    }

	void Start ()
    {
        instrumentSettings = new List<MixerInstrumentSettings>();
        var instruments = GetComponentsInChildren<PlinkInstrument>();
        foreach(var instrument in instruments)
        {
            instrumentSettings.Add(new MixerInstrumentSettings()
            {
                instrument = instrument
            });
        }
	}

    void FixedUpdate()
    {
        foreach (var instrument in instrumentSettings)
        {
            if (instrument.IsSeeking)
            {
                instrument.instrument.Volume = Mathf.Lerp(instrument.instrument.Volume, instrument.TargetVolume, 0.1f);
                if(Mathf.Abs(instrument.instrument.Volume - instrument.TargetVolume) < 0.1f)
                {
                    instrument.instrument.Volume = instrument.TargetVolume;
                    instrument.IsSeeking = false;
                }
            }
        }
    }
}
