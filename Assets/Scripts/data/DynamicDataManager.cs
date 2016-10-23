using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class DynamicDataManager : Singleton<DynamicDataManager> 
{
    public enum SceneStage
    {
        Start       = 0,
        Title       = 1,
        Loading     = 2,
        Dating      = 3
    }

    public SceneStage           _state              = SceneStage.Start;


    public GameObject           _sound;
    public GameObject           _soundEffect;

    public int                  _curStage           = 0;
    public int                  _destinyPoint;

    public string               _isSoundOn;
    public string               _isEffectOn;


    public Dictionary<string, string>       _endingData         = new Dictionary<string, string>();


    void Update()
    {
        if (_state != SceneStage.Title)
        {
            DontDestroyOnLoad(this.gameObject);
        }

        else
        {
            Destroy(this.gameObject);
        }
    }

    // sound & effect data
    public void SetSound(string sound_name)
    {
        _sound.GetComponent<SoundManager>().        Play(sound_name);
    }

    public void SetEffect(string sound_name)
    {
        _soundEffect.GetComponent<SoundManager>().  Play(sound_name);
    }

    public void SaveSoundData(string sound = "true")
    {
        PlayerPrefs.        SetString("sound", sound);
        PlayerPrefs.        Save();

        _isSoundOn          = PlayerPrefs.GetString("sound");
    }

    public void SaveSoundEffectData(string effect = "true")
    {
        PlayerPrefs.        SetString("effect", effect);
        PlayerPrefs.        Save();

        _isEffectOn         = PlayerPrefs.GetString("effect");
    }

    void LoadSoundData()
    {
        _isSoundOn          = PlayerPrefs.GetString("sound");
        _isEffectOn         = PlayerPrefs.GetString("effect");

        PlayerPrefs.        SetString("sound", _isSoundOn);
        PlayerPrefs.        SetString("effect", _isEffectOn);
        PlayerPrefs.        Save();

        if (_isSoundOn == "false")
        {
            _sound.GetComponent<AudioSource>().mute         = true;
        }

        if (_isEffectOn == "false")
        {
            _soundEffect.GetComponent<AudioSource>().mute   = true;
        }
    }

    // ending data
    public void LoadEndingData()
    {
        for (int i = 0; i < Macro.ENDING_STAGE.Length; i++)
        {
            string data         = PlayerPrefs.GetString(Macro.ENDING_STAGE[i]);

            _endingData[Macro.ENDING_STAGE[i]]      = data;

            //Debug.Log("ending : " + data + ", _endingData : " + _endingData[i]);
        }
    }

    public void SaveEndingData(string name, string isClear = "false")
    {
        PlayerPrefs.        SetString(name, isClear);
        PlayerPrefs.        Save();
    }

    // game data
    public void SaveGameData(int stage_number, int destiny_point = 0)
    {
        PlayerPrefs.        SetInt("stage", stage_number);
        PlayerPrefs.        SetInt("destiny_point", destiny_point);

        PlayerPrefs.        Save();

        _curStage           = PlayerPrefs.GetInt("stage");
        _destinyPoint       = PlayerPrefs.GetInt("destiny_point");
    }

    public void LoadGameData()
    {
        // 처음 시작하는 사람일 때
        if (!PlayerPrefs.HasKey("stage"))
        {
            PlayerPrefs.DeleteAll();

            for (int i = 0; i < Macro.ENDING_STAGE.Length; i++)
                SaveEndingData(Macro.ENDING_STAGE[i]);

            SaveSoundData("true");
            SaveSoundEffectData("true");

            SaveGameData(0);
        }

        LoadSoundData();
        LoadEndingData();

        _curStage           = PlayerPrefs.GetInt("stage");
        _destinyPoint       = PlayerPrefs.GetInt("destiny_point");

        //Debug.Log("load stage : " + _curStage);
    }
}
