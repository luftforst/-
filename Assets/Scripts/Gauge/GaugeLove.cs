using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GaugeLove : Singleton<GaugeLove> 
{
    private Scrollbar   _gauge;

    private float       _point = 0;
    private float       _eventPoint = 0;
    private float       _touchPoint = 0;

    private float       TOUCH_MAX = 0;
    private float       EVENT_MAX = 0;

    // singleton
    private GameData    _gameData;

    void Start()
    {
        _gameData       = GameData.Instance;

        _gauge          = this.gameObject.GetComponent<Scrollbar>();

        _point          = 0;

        TOUCH_MAX       = _gameData.getCurrentStageData().touchMax;
        EVENT_MAX       = 1000 - TOUCH_MAX;

        SetGauge();
    }

    public void SetGauge()
    {
        _point          = _eventPoint + _touchPoint;
        Debug.Log("point : " + _point + " = event : " + _eventPoint + " + touch : " + _touchPoint);

        if (_point < 0)
            _point = 0;

        else if (_point > 1000)
            _point = 1000;

        _gauge.size     = _point / 1000;
        Debug.Log("point : " + _point);
    }

    public void SetEventGauge(int event_point)
    {
        _eventPoint     += event_point;

        if (_eventPoint > EVENT_MAX)
            _eventPoint = EVENT_MAX;

        if (_eventPoint < 0)
            _eventPoint = 0;

        SetGauge();
    }

    public void SetTouchGauge(int touch_point)
    {
        _touchPoint     += touch_point;

        if (_touchPoint > TOUCH_MAX)
            _touchPoint = TOUCH_MAX;

        if (_touchPoint < 0)
            _touchPoint = 0;

        SetGauge();
    }

    public float Point
    {
        get { return _point; }
        set { _point = value; }
    }

    public void setLovePoint(int idx)
    {
        GameData.SelectType type    = _gameData._selectType[idx];

        switch (type)
        {
            case GameData.SelectType.Positive:
                SetEventGauge(40);
                break;

            case GameData.SelectType.Negative:
                SetEventGauge(-10);
                break;
        }

    }


}
