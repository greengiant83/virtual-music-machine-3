using System;
using System.Collections.Generic;
using UnityEngine;

public static class SteamVRExtensions
{
    public static Vector2 GetPadPosition(this SteamVR_TrackedController controller)
    {
        var device = SteamVR_Controller.Input((int)controller.controllerIndex);
        var position = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);
        return position;

        
    }

    public static void PulseController(this SteamVR_TrackedController controller, ushort durationMicroSec = 500)
    {
        var device = SteamVR_Controller.Input((int)controller.controllerIndex);
        device.TriggerHapticPulse(durationMicroSec);
    }
}
