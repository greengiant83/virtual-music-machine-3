using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Clock : MonoBehaviour
{
    public UnityEvent OnTick;
    public float BPM = 100;
    public int BeatsPerMeasure = 4;
    public int CurrentBeat = 1;

    bool isBPMNew = false;

    public void UpdateBPM()
    {
        isBPMNew = true;
    }

	void Start ()
    {
        //Tick();
        InvokeRepeating("Tick", 0, 60f / BPM);
	}

    void Tick()
    {
        OnTick.Invoke();

        CurrentBeat++;
        if (CurrentBeat > BeatsPerMeasure) CurrentBeat = 1;

        //Invoke("Tick", 60f / BPM);
        if(isBPMNew)
        {
            CancelInvoke("Tick");
            InvokeRepeating("Tick", 0, 60f / BPM);
            isBPMNew = false;
        }
    }
}
