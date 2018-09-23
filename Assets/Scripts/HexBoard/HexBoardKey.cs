using UnityEngine;
using System.Collections;

public class HexBoardKey : ButtonMonitor
{
    public Material PrimaryColor;
    public Material SecondaryColor;

    private bool _isSelected;
    public bool IsSelected
    {
        get { return _isSelected; }
        set
        {
            _isSelected = value;
            renderer.material = value ? parentBoard.SelectedMaterial : parentBoard.DeselectedMaterial;
        }
    }

    private float _length;
    public float Length
    {
        get { return _length; }
        set
        {
            _length = value;
            Button.restingScale = new Vector3(1, 1, Length);
        }
    }

    HexBoard _parentBoard;
    HexBoard parentBoard
    {
        get
        {
            if (_parentBoard == null) _parentBoard = GetComponentInParent<HexBoard>();
            return _parentBoard;
        }
    }

    Renderer _renderer;
    new Renderer renderer
    {
        get
        {
            if (_renderer == null) _renderer = GetComponent<Renderer>();
            return _renderer;
        }
    }

    protected override void Start()
    {
        base.Start();

        IsSelected = IsSelected; //Updates material
    }

    protected override void Button_OnPress(object sender, PressEventArgs e)
    {
        parentBoard.KeySelected(this);
    }
}
