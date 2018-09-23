using UnityEngine;
using System.Collections;

public class ToolActivator : MonoBehaviour
{
    public string ToolResourcePath;

	void Start ()
    {
	
	}
	
	void Update ()
    {
	
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Tool Anchor")
        {
            var uicontroller = collider.transform.parent.parent.GetComponent<UIController>();
            if(uicontroller != null)
            {
                uicontroller.ActivateTool(ToolResourcePath, this);
            }
        }
    }
}
