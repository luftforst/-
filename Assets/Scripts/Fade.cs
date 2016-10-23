using UnityEngine;
using System.Collections;

/*
 * 이 게임은 로딩속도가 너무 빨라서 로딩화면 나온 다음에 씬 호출하는 것으로 구현
 */

public class Fade : Singleton<Fade> 
{
    public GameObject       _NightToDay;
    public GameObject       _DayToNight;

    private float           _rotate             = 0;
    private float           _progress           = 0;

    private bool            isDayToNight        = false;
    private bool            isNightToDay        = false;

    private bool            isLoad              = false;

    private const string    _sound              = "귀뚜라미";     

    // camera
    public Camera           _camera;

	void Start() 
    {
        _camera.aspect = 720f / 1280f; 
        Screen.SetResolution(720, 1280, false);

        DynamicDataManager.Instance._sound.GetComponent<SoundManager>().Play(_sound);

        //StartCoroutine(Load());
        _NightToDay.SetActive(false);
        _DayToNight.SetActive(false);

        if (CallScene.Instance.Stage == Macro.SCENE_DATING)
        {
            isNightToDay = true;
            _NightToDay.SetActive(true);
        }

        else if (CallScene.Instance.Stage == Macro.SCENE_TITLE)
        {
            isDayToNight = true;
            _rotate = 180f;
            _DayToNight.SetActive(true);
        }
	}

    IEnumerator Load()
    {
        AsyncOperation async = Application.LoadLevelAsync(CallScene.Instance.Stage);

        while (!async.isDone) // || _rotate <= 180f)
        {
            _progress = async.progress;
            //Debug.Log(_progress);

            yield return null;
        }

        yield return null;
    }

    void Update()
    {
        if (isDayToNight)
        {
            showDayToNight();
            return;
        }

        if (isNightToDay)
        {
            showNightToDay();
            return;
        }
    }

    void showDayToNight()
    {
        if (_rotate >= 180f && _rotate <= 360f)
        {
            _rotate += 1.5f;
            this.gameObject.transform.rotation = Quaternion.Euler(0, 0, _rotate);
        }

        else
        {
            StartCoroutine("Load");
            isDayToNight = false;

            // data는 반드시 하나만 존재해야하므로 title로 돌아가면 삭제 + 재생성
            DynamicDataManager.Instance._state = DynamicDataManager.SceneStage.Title;
        }
    }

    void showNightToDay()
    {
        if (_rotate < 180f)
        {
            _rotate += 2f;
            this.gameObject.transform.rotation = Quaternion.Euler(0, 0, _rotate);
        }

        else
        {
            StartCoroutine("Load");
            isNightToDay = false;
        }
    }

    public bool IsDayToNight
    {
        get { return isDayToNight; }
        set { isDayToNight = value; }
    }

    public bool IsNightToDay
    {
        get { return isNightToDay; }
        set { isNightToDay = value; }
    }
}
