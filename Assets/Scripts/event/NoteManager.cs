using UnityEngine;
using System.Collections;

public class NoteManager : Singleton<NoteManager> 
{
    public GameObject           _noteButtons;
    public GameObject[]         _notes;
    public GameObject[]         _parents;

    private GameObject          _note;

    private bool                isPlay          = false;

    private int                 clear_count = 0;

    private int                 bt_type = -1;
   
    private float               create_time = 0;
    private float               judge_time = 0;
    private float               play_time = 0;

    private const float         PLAY_TIME = 15f;
    private const float         CREATE_TIME = 1f;
    private const float         JUDGE_TIME = 0.5f;

    void Start()
    {
        SceneManager.Instance._dialogue.SetActive(false);

        _noteButtons.       SetActive(true);
        isPlay              = true;
    }

    void Update()
    {        
        if (play_time > PLAY_TIME)
        {
            isPlay = false;

            SceneManager.Instance.IsEvent = true;
            play_time = 0;  // 초기화 안해주면 게이지가 계속 오름

            // 총점 판정
            if (clear_count >= 11)
            {
                Debug.Log("destiny point ++");
                GaugeDestiny.Instance.setDestinyPoint(true);
            }
            else
            {
                Debug.Log("destiny point --");
                GaugeDestiny.Instance.setDestinyPoint(false);
            }

            // 판정 결과
            _noteButtons.SetActive(false);
            this.gameObject.SetActive(false);
            //Destroy(this.gameObject);
        }

        if (isPlay)
        {
            play_time += Time.deltaTime;
            create_time += Time.deltaTime;
            //Debug.Log("time : " + time);

            // note create time
            if (create_time >= CREATE_TIME)
            {
                Debug.Log("create note");

                int i = Random.Range(0, 3);
                _note = (GameObject)Instantiate(_notes[i], _parents[i].transform.position, Quaternion.identity) as GameObject;
                _note.transform.parent = _parents[i].transform;
                _note.GetComponent<Note>().setType(i);

                create_time = 0;
            }

            // judge time
            if (bt_type != -1)
            {
                judge_time += Time.deltaTime;
                if (judge_time >= JUDGE_TIME)
                {
                    bt_type = -1;
                    judge_time = 0;
                }
            }
        }

    }

    public void buttonType(int _type)
    {
        bt_type = _type;
    }

    public int btType
    {
        get { return bt_type; }
        set { bt_type = value; }
    }

    public int noteClear
    {
        get { return clear_count; }
        set { clear_count = value; }
    }

}
