using UnityEngine;
using System.Collections;

public class StrokeAudio : MonoBehaviour
{
    public bool OverlapSamples;

    public AudioSource Audio { get; private set; }

    protected PlinkInstrument instrument;
    protected bool isMute = true;

    public int ActiveSegment
    {
        get
        {
            //return (int)(transform.position.y / instrument.SegmentSize);
            return instrument.GetSegmentIndex(transform.position);
        }
    }

    protected virtual void Start()
    {
        instrument = transform.parent.GetComponent<PlinkInstrument>();
        Audio = GetComponent<AudioSource>();
    }

    public virtual void SetPosition(Vector3 position, bool isMute)
    {
        transform.position = position;
        this.isMute = isMute;
    }
    
    public virtual void Tick() { }
}

public class StrokeSampleAudio : StrokeAudio
{
    int lastIndex;
    AudioClip lastClip;
    GameObject audioSamplePlayer;
    Vector3 lastPosition;

    protected override void Start()
    {
        base.Start();
        audioSamplePlayer = Resources.Load<GameObject>("Prefabs/AudioSample");
    }

    override public void Tick()
    {
        if (!instrument.IsPlaying || isMute) return;

        PlaySample(ActiveSegment);

        
    }

    public void PlaySample(int segment)
    {
        int currentIndex = 0;
        var clip = ScaleProvider.Instance.GetSample(segment, instrument.AudioClips);

        bool shouldPlayClip;
        shouldPlayClip = clip != null;

        if(shouldPlayClip && instrument.PlayOnlyOnChange)
        {
            var distanceToLastPosition = (lastPosition - transform.position).magnitude;
            if (lastClip != clip || distanceToLastPosition > instrument.SegmentSize)
                shouldPlayClip = true;
            else
                shouldPlayClip = false;
        }


        if (shouldPlayClip)
        {
            AudioSource audio = Audio;

            if(OverlapSamples)
            {
                var player = Instantiate<GameObject>(audioSamplePlayer);
                player.transform.SetParent(this.transform);
                player.transform.localPosition = Vector3.zero;
                audio = player.GetComponent<AudioSource>();
            }
                        
            audio.clip = clip;
            audio.volume = instrument.GetAdjustedVolume();
            audio.Play();

            lastPosition = transform.position;
            lastClip = clip;
            lastIndex = currentIndex;
        }
    }
}
