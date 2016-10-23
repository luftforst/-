using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Smash : MonoBehaviour 
{
    public Scrollbar            _gauge;
    public Text                 _limitTime;
    public GameObject           _image;

    public GameObject[]           _smashImage;
    public GameObject[]           _parents;

    private float               _point          = 100;
    private const float         _setPoint       = -2f;

    private float               _time           = 10f;

    void Start()
    {
        _image.SetActive(true);
    }

	void Update () 
    {
        _time -= Time.deltaTime;

        if (_time <= 4f)
            _limitTime.color = new Color(0.7f, 0, 0);

        if (_time <= 0f)
        {
            SceneManager.Instance.IsEvent = true;
            _time = 0;

            if (_point <= 0f)
            {
                _point = -5f;
                Debug.Log("destiny point ++");
                GaugeDestiny.Instance.setDestinyPoint(true);
            }
            else
            {
                Debug.Log("destiny point --");
                GaugeDestiny.Instance.setDestinyPoint(false);
            }

            this.gameObject.SetActive(false);
            _image.SetActive(false);
        }

        _limitTime.text = ((int)_time).ToString();

        _point += 0.4f;
        if (_point > 100)
            _point = 100;
        _gauge.size = _point / 100;

        Touch();
	}

    void Touch()
    {
        for (int i = 0; i < 2; ++i)
        {
            Touch touch = Input.GetTouch(i);
            Vector2 pos = touch.position;

            if (touch.phase == TouchPhase.Ended)
            {
                SetGauge();

                DynamicDataManager.Instance.SetEffect(Macro.SOUND_SMASH);

                int j = Random.Range(0, 2);
                GameObject smash_effect = (GameObject)Instantiate(_smashImage[j], _parents[j].transform.position, Quaternion.identity) as GameObject;
                smash_effect.transform.parent = _parents[j].transform;
            }
        }
    }

    public void SetGauge()
    {
        _point += _setPoint;

        if (_point < -10f)
            _point = -10f;

        else if(_point > 100)
            _point = 100;

        _gauge.size = _point / 100;
        //Debug.Log("point : " + _point);
    }
}
