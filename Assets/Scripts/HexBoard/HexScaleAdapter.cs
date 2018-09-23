using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HexScaleAdapter : MonoBehaviour
{
    public HexBoard ScaleBoard;
    public HexBoard RootBoard;
    ScaleProvider scaleProvider;

	void Start ()
    {
        scaleProvider = GetComponent<ScaleProvider>();
        RootBoard.KeySelected(RootBoard.Keys[scaleProvider.StartingIndex]);

        foreach(var note in scaleProvider.Notes)
        {
            ScaleBoard.KeySelected(ScaleBoard.Keys[note]);
        }

        ScaleBoard.OnKeysChanged += ScaleBoard_OnKeysChanged;
        RootBoard.OnKeysChanged += RootBoard_OnKeysChanged;
	}

    private void RootBoard_OnKeysChanged(object sender, System.EventArgs e)
    {
        for(int i=0;i<RootBoard.Keys.Length;i++)
        {
            if(RootBoard.Keys[i].IsSelected)
            {
                scaleProvider.StartingIndex = i;
                break;
            }
        }
    }

    private void ScaleBoard_OnKeysChanged(object sender, System.EventArgs e)
    {
        List<int> notes = new List<int>();
        for(int i=0;i<ScaleBoard.Keys.Length;i++)
        {
            if(ScaleBoard.Keys[i].IsSelected)
            {
                notes.Add(i);
            }
        }
        scaleProvider.Notes = notes.ToArray();
    }

    void Update ()
    {
	
	}
}
