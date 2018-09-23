using UnityEngine;
using System.Collections;

public class StrokeSynthAudio : StrokeAudio
{
    int lastIndex;
    OscillatorSoundGenerator generator;
    AudioClip clip;
    int sampleRate;
    float frequency = 440;
    int audioPosition;


    protected override void Start()
    {
        base.Start();

        generator = new OscillatorSoundGenerator();
        generator.NoteDuration = 0.15f;
        generator.AttackPercentage = 0.1f;
        generator.DecayPercentage = 0.8f;
        generator.WaveForm = WaveFormType.Sawtooth;

        sampleRate = AudioSettings.outputSampleRate;
        clip = AudioClip.Create("Streaming Sound", sampleRate * 60 * 60, 1, sampleRate, true, OnAudioRead, OnAudioSetPosition);
        Audio.clip = clip;
        Audio.Play();

        Audio.priority = 1;
    }

    void OnAudioRead(float[] data)
    {
        Debug.Log("OnAudioRead: " + frequency);
        int count = 0;
        while(count < data.Length)
        {
            if (!isMute)
                data[count] = Mathf.Sign(Mathf.Sin(2 * Mathf.PI * frequency * audioPosition / sampleRate));
            else
                data[count] = 0;

            audioPosition++;
            count++;
        }
    }

    public override void SetPosition(Vector3 position, bool isMute)
    {
        base.SetPosition(position, isMute);

        frequency = position.y * 1000 + 100;
        Debug.Log("SetPosition: " + frequency);
    }

    void OnAudioSetPosition(int newPosition)
    {
        //audioPosition = 0;
    }

    override public void Tick()
    {
        return;
        if (!instrument.IsPlaying || isMute) return;

        var currentIndex = ActiveSegment + 67;

        if (!instrument.PlayOnlyOnChange || currentIndex != lastIndex)
        {
            generator.PlayNote(Audio, currentIndex);
        }

        lastIndex = currentIndex;
    }
}
