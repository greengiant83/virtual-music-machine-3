using UnityEngine;
using System.Collections;

public class StrokePlayer : MonoBehaviour
{
    public PlinkInstrument Instrument;

    StrokeRecording recording;
    int index;
    float timeOffset;
    bool isStartPending;
    bool isPlaying;

	void Start ()
    {
        var clock = FindObjectOfType<Clock>();
        clock.OnTick.AddListener(OnClockTick);
    }
	
    public void Play(StrokeRecording recording)
    {
        this.recording = recording;
        isStartPending = true;
    }

    public void Stop()
    {
        isPlaying = false;
        isStartPending = false;
        Instrument.IsPlaying = false;
        CancelInvoke("playerTick");
    }

    public void Rewind()
    {
        isStartPending = true;
    }

    void OnClockTick()
    {
        if (isStartPending && recording != null && recording.Data.Count > 0)
        {
            isStartPending = false;

            index = 0;
            timeOffset = recording.Data[0].Timestamp;
            Instrument.Clear();
            Instrument.IsPlaying = true;
            isPlaying = true;
            playerTick();
        }
    }

    void playerTick()
    {
        if (!isPlaying) return;
        if (index < recording.Data.Count)
        {
            Instrument.SetPosition(recording.Data[index].Position, recording.Data[index].IsMute);
        }

        index++;
        if (index < recording.Data.Count)
        {
            var delay = recording.Data[index].Timestamp - recording.Data[index - 1].Timestamp;
            Invoke("playerTick", delay);
        }
        else
        {
            Rewind();
        }
    }
}
