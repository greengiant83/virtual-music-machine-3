using UnityEngine;
using System.Collections;

public enum UIControllerButton
{
    Trigger,
    Pad,
    Menu,
    Grip
}

public class UIController : MonoBehaviour
{
    public UIController OtherController;
    public Transform Raycaster;
    public Transform ToolAnchor;

    public Laser Laser;
    public GameObject SelectedObject;
    public SteamVR_TrackedController SteamController;

    GameObject currentToolObject;
    ControllerToolBase currentTool;
    
	void Start ()
    {
        Laser = Raycaster.gameObject.AddComponent<Laser>();
        SteamController = GetComponent<SteamVR_TrackedController>();

        SteamController.TriggerClicked += SteamController_TriggerClicked;
        SteamController.MenuButtonClicked += SteamController_MenuButtonClicked;
        SteamController.PadClicked += SteamController_PadClicked;
        SteamController.Gripped += SteamController_Gripped;
	}

    private void SteamController_Gripped(object sender, ClickedEventArgs e)
    {
        if (SelectedObject != null)
        {
            SelectedObject.SendMessage("ControllerGripClicked", this, SendMessageOptions.DontRequireReceiver);
        }
    }

    private void SteamController_PadClicked(object sender, ClickedEventArgs e)
    {
        if (SelectedObject != null)
        {
            SelectedObject.SendMessage("ControllerPadClicked", this, SendMessageOptions.DontRequireReceiver);
        }
    }

    private void SteamController_MenuButtonClicked(object sender, ClickedEventArgs e)
    {
        if (SelectedObject != null)
        {
            SelectedObject.SendMessage("ControllerMenuClicked", this, SendMessageOptions.DontRequireReceiver);
        }
    }

    private void SteamController_TriggerClicked(object sender, ClickedEventArgs e)
    {
        if(SelectedObject != null)
        {
            SelectedObject.SendMessage("ControllerTriggerClicked", this, SendMessageOptions.DontRequireReceiver);
        }
    }

    void FixedUpdate ()
    {
        if(Laser.IsHit)
        {
            if(SelectedObject != Laser.HitInfo.collider.gameObject)
            {
                if(SelectedObject != null) SelectedObject.SendMessage("ControllerHoverEnd", this, SendMessageOptions.DontRequireReceiver);
                SelectedObject = Laser.HitInfo.collider.gameObject;
                SelectedObject.SendMessage("ControllerHoverStart", this, SendMessageOptions.DontRequireReceiver);
            }
        }
        else
        {
            if(SelectedObject != null)
            {
                SelectedObject.SendMessage("ControllerHoverEnd", this, SendMessageOptions.DontRequireReceiver);
                SelectedObject = null;
            }
        } 
	}

    public void ActivateTool(string ToolResourcePath, MonoBehaviour Sender)
    {
        var toolPrefab = Resources.Load<GameObject>(ToolResourcePath);
        var newToolObject = Instantiate(toolPrefab);
        var newTool = newToolObject.GetComponent<ControllerToolBase>();

        bool canActivate = newTool.CanToolActivate(Sender);

        if (canActivate)
        {
            DeactivateCurrentTool();

            newToolObject.transform.SetParent(ToolAnchor);
            newToolObject.transform.localPosition = Vector3.zero;
            newToolObject.transform.localRotation = Quaternion.identity;
            newToolObject.transform.localScale = Vector3.one;

            newTool.uiController = this;
            newTool.steamController = SteamController;

            newTool.OnToolActivate(Sender);

            currentToolObject = newToolObject;
            currentTool = newTool;
        }
        else
        {
            Destroy(newToolObject);
        }
    }

    public void DeactivateCurrentTool()
    {
        if (currentToolObject == null) return;
        currentTool.OnToolDeactivate();
        Destroy(currentTool);
        Destroy(currentToolObject);
    }
}
