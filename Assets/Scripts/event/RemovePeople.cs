using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RemovePeople : MonoBehaviour
{
    enum PersonType
    {
        Hero = 0,
        Others = 1,
        Default = 2,
    }

    public GameObject           _heroines;

    // ui
    public GameObject           _image;
    public GameObject[]         _people;

    public GameObject[]         _touchData;
    
    public Text                 _limitTime;

    // values
    private int[]               _values         = { 1, 1, 1, 
                                                    1, 1, 1, 
                                                    1, 1, 1,
                                                    1, 1, 1, 
                                                    1, 1, 0 };

    private float               _time           = 6f;
    private float               _subTime        = 0;

    private int                 _point;
    private int                 _puzzleNum;

    void Start()
    {
        _puzzleNum = _people.Length;
        _point = _puzzleNum - 1;

        _heroines.SetActive(false);
        _image.SetActive(true);

        SetImage();
    }

    void Update()
    {
        _time -= Time.deltaTime;
        _limitTime.text = ((int)_time).ToString();

        if (_point <= 0)
            _time = 0;

        if (_time <= 4f)
            _limitTime.color = new Color(0.7f, 0, 0);

        if (_time <= 0f)
        {
            SceneManager.Instance.IsEvent = true;
            _time = 0;

            if (_point <= 2)
            {
                Debug.Log("destiny point ++");
                GaugeDestiny.Instance.setDestinyPoint(true);
            }
            else
            {
                Debug.Log("destiny point --");
                GaugeDestiny.Instance.setDestinyPoint(false);
            }

            _heroines.SetActive(true);
            _image.SetActive(false);
            this.gameObject.SetActive(false);
        }

        _subTime += Time.deltaTime;

        if (_subTime >= 0.7f)
        {
            SetImage();
            _subTime = 0;
        }

        Pick();
	}

    int[] SetChange(int[] value)
    {
        int temp;

        for (int i = 0; i < _puzzleNum; i++)
        {
            int seed = Random.Range(1, _puzzleNum);

            temp = value[i];
            value[i] = value[seed];
            value[seed] = temp;
        }

        return value;
    }

    void SetImage()
    {
        int[] result = SetChange(_values);

        for (int i = 0; i < _puzzleNum; i++)
        {
            _people[i].SetActive(true);

            if (result[i] == (int)PersonType.Hero)
            {
                ImageManager.Instance.setSprite(_people[i], "3/event_remove/인물터치_천민");
            }

            else if (result[i] == (int)PersonType.Others)
            {
                int j = Random.Range(1, 4);
                ImageManager.Instance.setSprite(_people[i], "3/event_remove/인물터치_" + j);
            }

            else if (result[i] == (int)PersonType.Default)
            {
                _people[i].SetActive(false);
            }
        }
    }

    int SearchObject(GameObject obj)
    {
        for (int i = 0; i < _puzzleNum; i++)
        {
            int j = int.Parse(obj.name);

            if (i == j)
                return _values[i - 1];
        }
        return 1;
    }

    void Remove(GameObject obj)
    {
        int i = int.Parse(obj.name);
        _values[i - 1] = 2;

        obj.SetActive(false);

        _point--;
    }

    void Pick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if (hit.collider == null)
                return;

            if (SearchObject(hit.collider.gameObject) == 0)
            {
                _time = 0;
                _point = 100;
            }
            else
            {
                Remove(hit.collider.gameObject);
            }
        }
    }

}
