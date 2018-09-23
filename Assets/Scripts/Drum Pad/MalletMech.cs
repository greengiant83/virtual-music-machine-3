using UnityEngine;
using System.Collections;

public class MalletMech : MonoBehaviour
{
    public DrumMallet Mallet;
    public PegToggle Toggle;

    public bool IsActive { get; set; }
    
    public void Trigger(float Volume)
    {
        if(Toggle.IsOn) Mallet.Thump(Volume);
    }
}
