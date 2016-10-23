using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GaugeDestiny : Singleton<GaugeDestiny> 
{
    private const int   destinyPoint = 20;
    private float       _point = 0;

    private int         truePage;
    private int         falsePage;

    private bool        isClear;

    private Scrollbar   gauge;


    void Start()
    {
        _point      = DynamicDataManager.Instance._destinyPoint;
        gauge       = this.gameObject.GetComponent<Scrollbar>();

        setGauge(0);

        //Debug.Log("point : " + _point);
    }

    public void setGauge(int point)
    {
        //Debug.Log("???");
        _point      += point;
        gauge.size  = _point / 100;
    }

    public float Point
    {
        get { return _point; }
        set { _point = value; }
    }

    // destiny point
    public void setDestinyPoint(bool is_clear)
    {
        if (is_clear)
            setGauge(destinyPoint);
        isClear = is_clear;
    }

    public bool IsClear
    {
        get { return isClear; }
        set { isClear = value; }
    }
}
