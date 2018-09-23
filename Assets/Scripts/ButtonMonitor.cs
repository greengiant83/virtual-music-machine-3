using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LaserButton))]
public abstract class ButtonMonitor : MonoBehaviour
{
    private LaserButton _button;
    protected LaserButton Button
    {
        get
        {
            if(_button == null) _button = GetComponent<LaserButton>();
            return _button;
        }
    }

    // Use this for initialization
    protected virtual void Start()
    {
        Button.OnPress += Button_OnPress;
    }

    protected virtual void Button_OnPress(object sender, PressEventArgs e)
    {
    }
}
