using UnityEngine;
using System.Collections;

public class SingleSelectControl : MonoBehaviour
{
    public Transform Indicator;

    public Vector3 targetPosition;

    private int _value;
    public int Value
    {
        get { return _value; }
        set
        {
            _value = value;
            foreach(var item in Items)
            {
                if (item.Value == value) SetItem(item);
            }
        }
    }

    SelectableItem[] Items;
    
    public void SetItem(SelectableItem item)
    {
        targetPosition = item.transform.localPosition;
        _value = item.Value;
    }

    void Start()
    {
        Items = GetComponentsInChildren<SelectableItem>();
        Value = 2;
    }

    void FixedUpdate()
    {
        Indicator.localPosition = Vector3.Lerp(Indicator.localPosition, targetPosition, 0.2f);
    }
}
