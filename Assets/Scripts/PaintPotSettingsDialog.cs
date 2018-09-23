using UnityEngine;
using System.Collections;

public class PaintPotSettingsDialog : MonoBehaviour
{
    public static PaintPotSettingsDialog Instance;

    public PegToggle LoopToggle;
    public PegToggle RepeatToggle;
    public SingleSelectControl BeatSelector;
    public PlinkInstrument instrument;
    public bool IsVisible;

    bool isHidePending = true;

    public void Show(PlinkInstrument instrument)
    {
        this.instrument = instrument;
        LoopToggle.IsOn = instrument.IsLooped;
        RepeatToggle.IsOn = !instrument.PlayOnlyOnChange;
        BeatSelector.Value = instrument.TicksPerBeat;
        IsVisible = true;
        transform.SetParent(instrument.transform, true);
        transform.localPosition = Vector3.zero;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        IsVisible = false;
        gameObject.SetActive(false);
        instrument = null;
    }

	void Start ()
    {
        Instance = this;
    }
	
	void Update ()
    {
        if (isHidePending)
        {
            Hide();
            isHidePending = false;
        }

        if (instrument != null)
        {
            instrument.IsLooped = LoopToggle.IsOn;
            instrument.PlayOnlyOnChange = !RepeatToggle.IsOn;
            instrument.TicksPerBeat = BeatSelector.Value;
        }
	}
}
