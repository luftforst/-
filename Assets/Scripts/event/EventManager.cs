using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;


public class EventManager : Singleton<EventManager> 
{

    public  GameObject[]    _eventObj;
    private int             _objectSize     = 0;

    public GameObject[]     _eventClear;

    // singleton
    private GameData        _data;
    
    void Start()
    {
        _data = GameData.Instance;
        _objectSize     = _eventObj.Length;

        Utils.ArrayActive(_eventObj, false);
        Utils.ArrayActive(_eventClear, false);
    }

    public GameObject CreateEvent(int scriptIndex)
    {
        string evt      = _data._eventData[scriptIndex].eventName;
        //Debug.Log("event : " + evt);

        for (int i = 0; i < _objectSize; i++)
        {
            //Debug.Log("event name : " + evt_obj[i].name);
            if (evt.Equals(_eventObj[i].name))
            {
                return _eventObj[i];
            }
        }

        return null;
    }

    public void SetClear(int clear_number)
    {
        DynamicDataManager.Instance.SetEffect(Macro.SOUND_CLEAR);
        _eventClear[clear_number].SetActive(true);
    }

    public void InitImageState(bool isSetActive)
    {
        Utils.ArrayActive(_eventClear, isSetActive);
    }
}
