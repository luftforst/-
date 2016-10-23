using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SoundOption : MonoBehaviour 
{
    // singleton
    private DynamicDataManager      _data;

    // toggle
    public Toggle                   _soundToggleOn;
    public Toggle                   _soundToggleOff;

    public Toggle                   _effectToggleOn;
    public Toggle                   _effectToggleOff;


	void Start () 
    {
        _data                   = DynamicDataManager.Instance;

        if (_data._isSoundOn == "true")
        {
            _soundToggleOn.isOn         = true;
            _soundToggleOff.isOn        = false;
        }
        else
        {
            _soundToggleOn.isOn         = false;
            _soundToggleOff.isOn        = true;
        }

        if (_data._isEffectOn == "true")
        {
            _effectToggleOn.isOn        = true;
            _effectToggleOff.isOn       = false;
        }
        else
        {
            _effectToggleOn.isOn        = false;
            _effectToggleOff.isOn       = true;
        }
	}
}
