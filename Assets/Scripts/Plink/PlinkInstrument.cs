using UnityEngine;
using System.Collections;

public class PlinkInstrument : MonoBehaviour
{
    public VolumeProvider MasterVolume;
    public string AudioClipsPath = "Sounds/Primary Color/Piano/Piano";
    public int AudioClipCount = 25;

    public AudioClip[] AudioClips;
    public Material DrawMaterial;
    public Material DrawMaterialInactive;
    public float SegmentSize;
    public int TicksPerBeat = 4;
    public bool PlayOnlyOnChange = false;
    public float Volume = 1;
    public bool IsLooped = true;
    
    public BlockStroker Visualizer { get; private set; }
    public StrokeAudio AudioPlayer { get; private set; }
    public int CurrentBeat { get; private set; }
    public bool IsPlaying { get; set; }
    public Clock Clock { get; private set; }

    public Transform baseline { get; private set; }

    int currentTick = 1;

    void Start()
    {
        baseline = GameObject.Find("Baseline").transform;
        Visualizer = GetComponentInChildren<BlockStroker>();
        AudioPlayer = GetComponentInChildren<StrokeAudio>();

        Clock = FindObjectOfType<Clock>();
        Clock.OnTick.AddListener(OnTick);

        loadAudioClips();
    }

    void loadAudioClips()
    {
        AudioClipCount = (int)(AudioClipCount / 12f) * 12;
        AudioClips = new AudioClip[AudioClipCount];
        for(int i=1;i<=AudioClipCount;i++)
        {
            string path = AudioClipsPath + i;
            AudioClips[i-1] = Resources.Load<AudioClip>(path);
        }
    }

    public float GetAdjustedVolume()
    {
        return MasterVolume.Volume * Volume;
    }

    public int GetSegmentIndex(Vector3 worldPosition)
    {
        var position = worldPosition.y - baseline.position.y;
        if (position < 0) return -1;
        return (int)(position / SegmentSize);
    }

    public void SetPosition(Vector3 position, bool isMute)
    {
        AudioPlayer.SetPosition(position, isMute);
        Visualizer.AddPosition(position, isMute);
    }

    public void Clear()
    {
        Visualizer.Clear();
    }
    
    void OnTick()
    {
        if (!IsPlaying) return;

        if(currentTick == 1)
        {
            OnBeat();
        }
        currentTick++;
        if (currentTick > TicksPerBeat) currentTick = 1;
    }

    void OnBeat()
    {
        AudioPlayer.Tick();

        CurrentBeat++;
        if (CurrentBeat > Clock.BeatsPerMeasure) CurrentBeat = 1;

    }
}
