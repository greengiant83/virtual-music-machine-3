using UnityEngine;
using System.Collections;

public class SetScaleButton : LaserButton
{
    public ScaleProvider Scale;

    protected override void OnButtonPress(UIController sender)
    {
        if(ScaleProvider.Instance != null)
        {
            ScaleProvider.Instance.GetComponentInChildren<RandomSpin>().IsActive = false;
        }
        ScaleProvider.Instance = Scale;
        ScaleProvider.Instance.GetComponentInChildren<RandomSpin>().IsActive = true;
    }
}
