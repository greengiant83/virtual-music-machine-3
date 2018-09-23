using UnityEngine;
using System.Collections;
using System;

public class HexBoard : MonoBehaviour
{
    public delegate void OnKeysChangedHandler(object sender, EventArgs e);
    public event OnKeysChangedHandler OnKeysChanged;

    public bool IsMultiSelect;
    public Material SelectedMaterial;
    public Material DeselectedMaterial;

    private HexBoardKey[] _keys;
    public HexBoardKey[] Keys
    {
        get
        {
            if(_keys == null) _keys = GetComponentsInChildren<HexBoardKey>();
            return _keys;
        }
    }

    void Start()
    {
        
    }

    public void KeySelected(HexBoardKey selectedKey)
    {
        if(IsMultiSelect)
        {
            selectedKey.IsSelected = !selectedKey.IsSelected;
            selectedKey.Length = selectedKey.IsSelected ? 2 : 1;
        }
        else
        {
            SelectedMaterial = selectedKey.PrimaryColor;
            DeselectedMaterial = selectedKey.SecondaryColor;

            foreach(var key in Keys)
            {
                key.IsSelected = key == selectedKey;
                //key.Length = key.IsSelected ? 2 : 1;
                key.Length = (key.transform.position - selectedKey.transform.position).magnitude.Remap(.2f, 0, 1, 2, true);
            }
        }

        if (OnKeysChanged != null) OnKeysChanged(this, EventArgs.Empty);
    }
}
