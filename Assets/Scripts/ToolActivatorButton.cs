using UnityEngine;
using System.Collections;

public class ToolActivatorButton : ButtonMonitor
{
    public string ToolResourcePath;
    
    protected override void Button_OnPress(object sender, PressEventArgs e)
    {
        e.Controller.ActivateTool(ToolResourcePath, this);
    }
}
