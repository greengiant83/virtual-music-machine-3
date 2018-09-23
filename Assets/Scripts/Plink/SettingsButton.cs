using UnityEngine;
using System.Collections;

public class SettingsButton : MonoBehaviour
{
    PlinkInstrument instrument;

    void Start()
    {
        instrument = GetComponent<PlinkInstrument>();
    }

	void ControllerMenuClicked(UIController controller)
    {
        var dialog = PaintPotSettingsDialog.Instance;
        if(!dialog.IsVisible || dialog.instrument != instrument)
        {
            dialog.Show(instrument);
        }
        else
        {
            dialog.Hide();
        }

    }
}
