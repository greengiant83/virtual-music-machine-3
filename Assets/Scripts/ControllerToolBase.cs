using UnityEngine;
using System.Collections;

public abstract class ControllerToolBase : MonoBehaviour
{
    public UIController uiController { get; set; }
    public SteamVR_TrackedController steamController { get; set; }

    public abstract void OnToolActivate(MonoBehaviour Sender);
    public abstract void OnToolDeactivate();
    public virtual bool CanToolActivate(MonoBehaviour Sender) { return true; }
}
