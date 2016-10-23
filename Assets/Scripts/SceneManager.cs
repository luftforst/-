using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class SceneManager : Singleton<SceneManager>
{    
    enum PlayerAction
    {
        ShowDialogue = 0,
        ShowSelect,
        ShowEvent,
        ShowEnding,
        GoToNextStage,
        Default
    }

    enum Stage
    {
        Normal = 0,
        Ending
    }

    enum EventClear
    {
        Clear = 0,
        Fail
    }

    private PlayerAction        action          = PlayerAction.Default;

    private bool                isEvent         = false;
    private bool                bSelect         = false;
    private bool                isNextStage     = false;

    private int                 _selectIndex    = 0;
    private int                 _scriptIndex    = 0;

    public string               ClearName { get; set;}

    // UI 
    public GameObject           _dialogue;
    public GameObject           _select;

    // menu
    public GameObject           _menu;
    public GameObject           _nextStage;
    public GameObject           _quit;
    public GameObject           _endPopup;

    // Gauge
    public GameObject           _destinyGauge;

    // Dialog
    public Text                 _dialName;
    public Text                 _dialText;

    // Select
    public Button[]             _selectNext     = new Button[3];    // game object utils
    public Text[]               _selectText     = new Text[3];

    // Stage
    public GameObject[]         _stage;
    public GameObject[]         _stageImage;
    public GameObject           _stageFade;

    public GameObject           _liveCanvas;
    public GameObject[]         _liveHeroines;
    public GameObject           _others;

    // event
    public GameObject           _eventFrame;

    // Text
    public GameObject                   _saveText;              // 저장중입니다
    public GameObject                   _heroineMessage;        // 히로인 기분
    public Text                         _message;

    public GameObject                   _subjectImage;          // 제목

    // Singleton Object
    private DialogueText                _dialogText;

    private DynamicDataManager          _gameData;

    private GameData.StageData          _stageData;
    private GameData                    _data;

    private ImageManager                _image;

    // string
    private string                      _curSound;
    private string                      _curSoundEffect;

    private string                      _heroine    = "자운영";  // default

    // camera
    public Camera                       _camera;


    IEnumerator Start()
    {
        // screen, camera init
        _camera.aspect      = 720f / 1280f; 
        Screen.             SetResolution(720, 1280, false);
        
        _dialogText         = DialogueText.Instance;
        _gameData           = DynamicDataManager.Instance;

        // data load
        _gameData.          LoadGameData();

        _data               = GameData.Instance;
        _stageData          = GameData.Instance.getCurrentStageData();
        _image              = ImageManager.Instance;

        _message            = _heroineMessage.GetComponent<Text>();

        //Debug.Log(_gameData._curStage);

        //_heroine = Macro.START_HEROINE[_gameData._curStage];
        //Debug.Log("heroine : " + _heroine);

        // live2d
        _liveCanvas         = GameObject.Find(_heroine);

        // title
        _image.             setImage(_subjectImage, _gameData._curStage + "/" + Macro.IMAGE_TITLE);

        // sound
        _curSound           = _stageData.sound;
        _curSoundEffect     = Macro.SOUND_TOUCH;

        _gameData.          _sound.GetComponent<SoundManager>().Play(_curSound);

        // ui
        _stageFade.         SetActive(true);
        _saveText.          SetActive(false);
        _heroineMessage.    SetActive(false);

        // stage
        Utils.              ArrayActive(_stage, false);
        _eventFrame.        SetActive(false);
        Utils.              ArrayActive(_liveHeroines, false);

        // dialog
        _dialogue.          SetActive(false);
        _select.            SetActive(false);

        for (int i = 0; i < 3; i++)
            _selectNext[i].gameObject.      SetActive(false);

        _stage[(int)Stage.Normal].      SetActive(true);
        _stage[(int)Stage.Ending].      SetActive(false);

        _subjectImage.      SetActive(true);

        // set live2d
        //if (_liveCanvas != null)
        _liveCanvas.        SetActive(false);

        StartCoroutine(action.ToString());

        yield return null;
    }


    void Update()
    {
        if (_stageData.dialogData[_scriptIndex].sound != null)
        {
            string sound_name   = _stageData.dialogData[_scriptIndex].sound;

            _gameData.          SetSound(sound_name);
            //Debug.Log("sound : " + sound_name);
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            _menu.          SetActive(true);
            _quit.          SetActive(true);
        }
    }

    IEnumerator Default()
    {
        float time = 0;
 
        do
        {
            yield return null;

            string backgroundName   = _stageData.dialogData[1].backgroundName;

            _image.                 setSprite(_stageImage[(int)Stage.Normal], _gameData._curStage + "/" + backgroundName);

            // 몇초 뒤에 이미지 사라지고 다이얼로그 창, 1페이지 시작
            time += Time.deltaTime;
            if (time > 2.5f)
            {
                //_scriptIndex++;
                _stageFade.         SetActive(false);
                _subjectImage.      SetActive(false);
                _dialogue.          SetActive(true);

                action              = PlayerAction.ShowDialogue;

                yield return null;
            }

        } while (action == PlayerAction.Default);

        yield return null;
    }

    bool setLiveCanvas(string name)
    {
        //Debug.Log(speaker);
        for (int i = 0; i < Macro.HEROINE.Length; i++)
        {
            if (name == Macro.HEROINE[i])
            {
                //Debug.Log(speaker);
                _heroine        = name;
                _liveCanvas     = _liveHeroines[i];

                LAppLive2DManager.Instance.     ChangeScene(i);
                return true;
            }
        }
        return false;
    }

    IEnumerator ShowDialogue()
    {
        Debug.Log(_scriptIndex);

        _dialogue.GetComponent<Button>().       interactable   = true;

        _dialogue.      SetActive(true);
        _select.        SetActive(false);   

        for (int i = 0; i < 3; i++)
            _selectNext[i].gameObject.      SetActive(false);

        string  dialog          = _stageData.dialogData[_scriptIndex].dialog;
        string  character       = null;
        string  speaker         = _stageData.dialogData[_scriptIndex].speaker;
        string  backgroundName  = _stageData.dialogData[_scriptIndex].backgroundName;
        string  expression      = _stageData.dialogData[_scriptIndex].expression;
        int     change          = _stageData.dialogData[_scriptIndex].change;

        if (dialog == null)
        {
            Debug.Log("dialogue null");
            yield return null;
        }

        _image.setSprite(_stageImage[(int)Stage.Normal], _gameData._curStage + "/" + backgroundName);

        if (_stageData.dialogData[_scriptIndex].character != null)
            character       = _stageData.dialogData[_scriptIndex].character;

        // type = dialog
        SetDialog(speaker);
        setLiveCanvas(character);
        SetHerione(character);

        // 1 word print
        _dialogText.        setDialog(dialog);

        // 자운영
        if (_stageData.index == 1 && _scriptIndex == 114)
        {
            // 호감도를 채우지 못했을때
            if (GaugeLove.Instance.Point < 999)
            {
                _scriptIndex        = change;
                _scriptIndex--;
            }
            else
            {
                _scriptIndex++;
            }
            yield return null;
        }

        // 흑화/을씨년
        if (_stageData.index == 2 && (change == 275 || change == 258))
        {
            // 호감도를 채우지 못했을때
            if (GaugeLove.Instance.Point < 900)
            {
                //Debug.Log("point : " + GaugeLove.Instance.Point);
                _scriptIndex        = change;
                _scriptIndex--;
            }
            else 
            {
                //Debug.Log("point : " + GaugeLove.Instance.Point);
                _scriptIndex++;
            }
            yield return null;
        }

        // 안대례
        if (_stageData.index == 3 && _scriptIndex == 115)
        {
            if (GaugeLove.Instance.Point < 900)
            {
                // 게임오버
                if (GaugeDestiny.Instance.Point < 100)
                {
                    _scriptIndex        = 124;
                    _scriptIndex--;
                }

                // 다음 스테이지
                else
                {
                    _scriptIndex        = 172;
                    _scriptIndex--;
                }
            }

            // 해피
            else if (GaugeDestiny.Instance.Point < 100 && GaugeLove.Instance.Point >= 900)
            {
                _scriptIndex++;
            }
            yield return null;
        }

        // 이왕
        if (_stageData.index == 4 && _scriptIndex == 109)
        {
            // 해피
            if (GaugeLove.Instance.Point >= 900)
            {
                _scriptIndex++;
            }

            // 배드
            else
            {
                _scriptIndex        = 113;
                _scriptIndex--;
            }
            yield return null;
        }

        if (change != 0 && DialogueText.Instance.DialState == DialogueText.State.PRINT_END)
        {
            _scriptIndex        = change;
            _scriptIndex--;
        }

        do
        {
            yield return null;

        } while (action == PlayerAction.ShowDialogue);

        yield return null;
    }

    void SetHerione(string name)
    {
        //Debug.Log("name : " + name);

        if (name == _heroine)
        {
            _others.    SetActive(false);

            // heroine live2d expression
            LFaceManager.Instance.      IsFace = true;

            // prevent to called out heroine's animation all the time
            if (_liveCanvas.activeSelf == false)
                _liveCanvas.    SetActive(true);

            return;
        }

        // heroine is not active
        _liveCanvas.    SetActive(false);

        _others.        SetActive(true);
        _image.         setSprite(_others, name);
    }

    void SetDialog(string speaker)
    {
        if (speaker == null)
            _dialName.text      = speaker;
        else
            _dialName.text      = speaker + "_";

        if (speaker == _heroine)
        {
            // dialog image
            _image.setImage(_dialogue, Macro.IMAGE_DIALOG + Macro.IMAGE_DIALOG_IN);

            return;
        }

        switch (speaker)
        {
            case Macro.HERO:
                _image.setImage(_dialogue, Macro.IMAGE_DIALOG + Macro.IMAGE_DIALOG_OUT);
                break;

            default:
                if (speaker == null)
                {
                    _image.setImage(_dialogue, Macro.IMAGE_DIALOG + Macro.IMAGE_DIALOG_NARRATION);
                    break;
                }

                _image.setImage(_dialogue, Macro.IMAGE_DIALOG + Macro.IMAGE_DIALOG_IN);
                break;
        }
    }

    IEnumerator ShowSelect()
    {
        _dialogue.GetComponent<Button>().interactable   = false;

        string speaker      = _stageData.dialogData[_scriptIndex - 1].speaker;

        yield return new WaitForSeconds(1f);

        if (speaker == null)
            _dialName.text = "";
        else
            _dialogue.SetActive(false);

        // Initialization
        _select.    SetActive(true);

        int[] select        = new int[3];

        for (int i = 0; i < 3; i++)
        {
            select[i]       = _scriptIndex + i;
        }

        // show select

        int idx = 0;

        while (_stageData.dialogData[_scriptIndex].type == GameData.DialogType.select)
        {
            string dialog   = _stageData.dialogData[_scriptIndex].dialog;

            _selectNext[idx].gameObject.SetActive(true);
            _selectText[idx].text = dialog;

            //Debug.Log(_scriptIndex + dialog + idx);
            
            idx++;
            _scriptIndex++;
        }

        do
        {
            yield return null;

            // change page
            if (bSelect)
            {
                int change = _stageData.dialogData[select[_selectIndex]].change;
                Debug.Log("change : " + change);

                ChangePage(change);

                // 왕 스테이지 호감도
                if (_stageData.index == 4)
                    GaugeLove.Instance.setLovePoint(select[_selectIndex]);

                _selectIndex = 0;
                bSelect = false;
            }

        } while (action == PlayerAction.ShowSelect);

        yield return null;
    }

    // Event
    IEnumerator ShowEvent()
    {
        _liveCanvas.    SetActive(false);
        _dialogue.      SetActive(true);
        _select.        SetActive(false);
        _eventFrame.    SetActive(true);

        _dialName.text  = "";

        _dialogue.GetComponent<Button>().interactable = false;
        //Debug.Log("event idx : " + _scriptIndex);
        yield return new WaitForSeconds(0.5f);
        do
        {
            GameObject obj      = EventManager.Instance.CreateEvent(_scriptIndex);

            if (!obj)
            {
                Debug.Log("object is null");
            }

            if (DialogueText.Instance.DialState == DialogueText.State.PRINT_END)
            {
                yield return new WaitForSeconds(1f);

                obj.        SetActive(true);

            }

            if (IsEvent)
            {

                obj.        SetActive(false);

                // ChangePage
                if (GaugeDestiny.Instance.IsClear)
                {
                    GaugeLove.Instance.         SetEventGauge(_data._eventData[_scriptIndex].successLovePoint);
                    EventManager.Instance.      SetClear((int)EventClear.Clear);

                    yield return new WaitForSeconds(1.5f);

                    ChangePage(_data._eventData[_scriptIndex].successPage);
                }

                else
                {
                    GaugeLove.Instance.         SetEventGauge(_data._eventData[_scriptIndex].failLovePoint);
                    EventManager.Instance.      SetClear((int)EventClear.Fail);

                    yield return new WaitForSeconds(1.5f);

                    ChangePage(_data._eventData[_scriptIndex].failPage);
                }

                _eventFrame.            SetActive(false);
                EventManager.Instance.  InitImageState(false);

                IsEvent = false;
            }

            yield return null;
        } while (action == PlayerAction.ShowEvent);

        yield return null;
    }

    IEnumerator ShowEnding()
    {
        if (DialogueText.Instance.DialState == DialogueText.State.PRINT_END)
        {
            _saveText.      SetActive(true);
            _stageFade.     SetActive(true);

            _stage[(int)Stage.Normal].      SetActive(false);
            _stage[(int)Stage.Ending].      SetActive(true);
        }

        if (_gameData._curStage == 4)
        {
            _gameData.      SaveGameData(1);

            if (GaugeLove.Instance.Point < 1000)
            {
                _heroine    = _heroine + "_배드";
            }
        }
        else
        {
            _gameData.      SaveGameData(1, (int)_destinyGauge.GetComponent<GaugeDestiny>().Point);
        }

        ClearName       = _heroine;

        // ending save
        _gameData.      SaveEndingData(_heroine, "true");

        // set ending image
        _image.         setImage(_stageImage[(int)Stage.Ending], "ending/" + _heroine + Macro.IMAGE_ENDING);

        yield return new WaitForSeconds(8f);

        _endPopup.      SetActive(true);

        do
        {
            yield return null;

        } while (action == PlayerAction.ShowEnding);
    }

    IEnumerator GoToNextStage()
    {
        _saveText.          SetActive(true);

        if (_gameData._curStage == 0)
        {
            _destinyGauge.GetComponent<GaugeDestiny>().Point = 0;

            _gameData.      SaveGameData(_gameData._curStage);
        }

        if (DialogueText.Instance.DialState == DialogueText.State.PRINT_END)
        {
            if (!isNextStage)
            {
                _gameData.  _curStage++;

                _gameData.  SaveGameData(_gameData._curStage, (int)_destinyGauge.GetComponent<GaugeDestiny>().Point);

                isNextStage = true;
            }

            yield return new WaitForSeconds(0.5f);

            _saveText.      SetActive(false);
            _dialogue.      SetActive(false);
            _nextStage.     SetActive(true);
        }

        do
        {
            yield return null;

        }while (action == PlayerAction.ShowEnding);
    }

    public void ChangePage(int pagenum)
    {
        //Debug.Log("change page");

        _scriptIndex        = pagenum;

        if (_dialogText.IsNextDialog())
        {
            action          = PlayerAction.ShowDialogue;

            StartCoroutine(action.ToString());

            _dialogText.    ChangeState(DialogueText.State.IDLE);
        }

        else
        {
            _dialogText.    ChangeState(DialogueText.State.PRINT_END);
        }

    }

    GameData.DialogType getDialogType(int scriptIndex)
    {
        if (_stageData.dialogData.ContainsKey(scriptIndex + 1))
        {
            return _stageData.dialogData[_scriptIndex + 1].type;
        }

        else if (_data._eventData.ContainsKey(scriptIndex + 1))
        {
            return GameData.DialogType.objectEvent;
        }

        return (GameData.DialogType)(-1);
    }

    // Button
    public void TurnPage()
    {
        GameData.DialogType nextScriptType      = getDialogType(_scriptIndex);

        //Debug.Log("next type : " + nextScriptType); 

        switch(nextScriptType)
        {
            case GameData.DialogType.dialog:

                if (_dialogText.IsNextDialog())
                {
                    _gameData.      SetEffect(Macro.SOUND_PAGE);

                    _scriptIndex++;

                    action          = PlayerAction.ShowDialogue;

                    StartCoroutine(action.ToString());

                    _dialogText.    ChangeState(DialogueText.State.IDLE);
                }
                else
                {
                    _dialogText.    ChangeState(DialogueText.State.PRINT_END);
                }

                break;

            case GameData.DialogType.select:

                _scriptIndex++;

                action      = PlayerAction.ShowSelect;

                StartCoroutine(action.ToString());

                break;

            case GameData.DialogType.objectEvent:

                _scriptIndex++;

                action      = PlayerAction.ShowEvent;

                StartCoroutine(action.ToString());

                break;

            case GameData.DialogType.ending:

                action      = PlayerAction.ShowEnding;

                StartCoroutine(action.ToString());

                break;

            case GameData.DialogType.nextStage:

                action      = PlayerAction.GoToNextStage;

                StartCoroutine(action.ToString());

                break;

            case GameData.DialogType.returnMain:

                CallLoading("Dating");

                _gameData.  SaveGameData(1);

                break;
        }
    }

    public void Select(int index)
    {
        _selectIndex    = index;

        bSelect         = true;
    }

    public bool IsEvent
    {
        get { return isEvent; }

        set { isEvent = value; }
    }

    public int ScriptIdx
    {
        get { return _scriptIndex; }

        set { _scriptIndex = value; }
    }

    public string getCurHeroine()
    {
        return  _heroine;
    }

    // UI
    public void CallLoading(string sceneName)
    {
        StartCoroutine(CallScene.Instance.LoadingSceneCall(sceneName, 0f));
    }

    public void SoundEffect()
    {
        _gameData.      SetEffect(_curSoundEffect);
    }

    public void SetFontSize(int size)
    {
        _dialText.      fontSize            = size;
        _dialText.      resizeTextMaxSize   = size;
    }
}
