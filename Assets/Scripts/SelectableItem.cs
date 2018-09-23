using UnityEngine;
using System.Collections;

public class SelectableItem : MonoBehaviour
{
    public int Value;
    public SingleSelectControl Parent;
    
    void ControllerTriggerClicked(UIController controller)
    {
        Parent.SetItem(this);
    }
}
