using UnityEngine;
using System.Collections;
using System.Linq;

public class DrumSequencer : MonoBehaviour
{
    public VolumeProvider VolumeControl;
    public ToggleMaterial Gong;
    public MalletMech[] Mallets;
    public int TicksPerBeat = 1;

    int index = 0;
    int currentTick = 1;

    void Start ()
    {
        Mallets = GetComponentsInChildren<MalletMech>().OrderBy(i => getOrdinalFromName(i.gameObject.name)).ToArray();
        
        var gongAudio = Gong.GetComponent<AudioSource>();
        foreach(var mallet in Mallets)
        {
            mallet.Mallet.Gong = gongAudio;
        }

        var Clock = FindObjectOfType<Clock>();
        Clock.OnTick.AddListener(OnTick);
    }
    
    void OnTick()
    {
        if (currentTick == 1)
        {
            OnBeat();
        }
        currentTick++;
        if (currentTick > TicksPerBeat) currentTick = 1;
    }

    void OnBeat()
    {
        if(Gong.IsOn) Mallets[index].Trigger(VolumeControl.Volume);
        index = (index + 1) % Mallets.Length;
    }

    int getOrdinalFromName(string name)
    {
        //Gets the number from the name of the object
        //Mallet Mechanism (6) <-- returns 6
        int start = name.LastIndexOf('(');
        int end = name.LastIndexOf(')');
        string s = name.Substring(start + 1, end - start - 1);
        return int.Parse(s);
    }
}
