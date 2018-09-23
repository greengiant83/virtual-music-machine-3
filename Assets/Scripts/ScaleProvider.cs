using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ScaleProvider : MonoBehaviour
{
    public int StartingIndex = 0;
    public int[] Notes;
    public bool IsDefault;

    public static ScaleProvider Instance;
    
	void Start ()
    {
        if (IsDefault)
            Instance = this;
	}
    
    public int GetLowerBoundsIndex(int segmentIndex, int samplesLength)
    {
        return 0;
        return (int)(segmentIndex / Notes.Length) * Notes.Length;
    }

    public int GetUpperBoundsIndex(int segmentIndex, int samplesLength)
    {
        int octaveCount = samplesLength / 12;
        int lowerBounds = GetLowerBoundsIndex(segmentIndex, samplesLength);
        return lowerBounds + Notes.Length * octaveCount;
    }

    public AudioClip GetSample(int segmentIndex, AudioClip[] clips)
    {
        if (segmentIndex < 0) return null;

        int octaveCount = clips.Length / 12;
        int octaveIndex = (int)(segmentIndex / Notes.Length) % octaveCount;
        int scaleNoteIndex = segmentIndex % Notes.Length;
        int clipIndex = Notes[scaleNoteIndex] + octaveIndex * 12;
        clipIndex += StartingIndex;
        clipIndex = clipIndex % clips.Length;

        if (clipIndex < 0 || clipIndex >= clips.Length) return null;
        return clips[clipIndex];
    }

    public int GetSampleIndex(int segmentIndex, int samplesLength)
    {
        int index = StartingIndex;
        for(int i=0;i<segmentIndex;i++)
        {
            index += Notes[i % Notes.Length];
        }
        
        index = index % samplesLength;
        return index;
    }
}
