using UnityEngine;
using System.Collections;

public class InstrumentController : MonoBehaviour
{
    public PlinkController plinkController { get; set; }
    public PlinkInstrument instrument { get; private set; }

    StrokeRecording recording;
    StrokePlayer player;
    bool isLive = false;
    
    void Start()
    {
        instrument = transform.GetComponent<PlinkInstrument>();
        player = GetComponentInChildren<StrokePlayer>();
        player.Instrument = instrument;
    }

    void FixedUpdate()
    {
        return;
        bool isFocus = isLive; 
        float targetVolume = isFocus ? 1f : 0.25f;
        float currentVolume = instrument.Volume;
        instrument.Volume = Mathf.Lerp(currentVolume, targetVolume, 0.01f);
    }

    public void Clear()
    {
        player.Stop();
        instrument.Clear();
        instrument.IsPlaying = false;
    }
        
    public void StartStroke()
    {
        player.Stop();
        recording = new StrokeRecording();
        instrument.Clear();
        instrument.IsPlaying = true;
        isLive = true;
    }

    public void AddPosition(Vector3 position, bool isMute)
    {
        recording.Data.Add(new StrokeDataPoint()
        {
            Position = position,
            IsMute = isMute,
            Timestamp = Time.time
        });

        instrument.SetPosition(position, isMute);
    }

    public void EndStroke()
    {
        instrument.IsPlaying = false;
        isLive = false;

        if(instrument.IsLooped) player.Play(recording);
    }
}