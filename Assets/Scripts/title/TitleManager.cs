using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;


public class TitleManager : MonoBehaviour
{
    // opening animation
    public GameObject               _lights;
    public GameObject               _background;
    public GameObject               _tree;

    public GameObject               _button;
    public GameObject               _title;
    public GameObject               _copyright;

    public GameObject               _menu;
    public GameObject               _quit;

    public Text                     _stage;
    public Image                    _nameBox;

    private float                   BACK_SPEED      = 11f;
    private float                   TREE_SPEED      = 9.42f;
    private const float             TREE_MIN        = -9f;
    private const float             BACK_MIN        = -9f;

    // singleton
    private DynamicDataManager      _data;
    private GameData.StageData      _stageData;

    // sound
    private string                  _sound;
    private string                  _soundEffect;

    // camera
    public Camera                   _camera;

    IEnumerator Start()
    {
        // screen, camera init
        _camera.aspect      = 720f / 1280f; 
        Screen.             SetResolution(720, 1280, false);

        // data init, load
        _data               = DynamicDataManager.Instance;
        _data.              LoadGameData();

        yield return new WaitForSeconds(0.5f);      // data load time

        _stageData          = GameData.Instance.getCurrentStageData();

        _stage.text         = Macro.STAGE[_data._curStage];

        _sound              = Macro.SOUND_TITLE;
        _soundEffect        = Macro.SOUND_TOUCH;

        _data._sound.GetComponent<SoundManager>().Play(_sound);

        // title image
        yield return new WaitForSeconds(3.7f);
        _title.             SetActive(true);
        _copyright.         SetActive(true);

        // button
        yield return new WaitForSeconds(2f);
        _button.            SetActive(true);

        // lights effect
        yield return new WaitForSeconds(0.5f);
        _lights.            SetActive(true);
    }

    void Update()
    {
        if (_tree.transform.position.y > BACK_MIN)
        {
            TREE_SPEED -= Time.deltaTime * 2.35f;
            _tree.transform.Translate(new Vector2(0, -TREE_SPEED * Time.deltaTime));
        }

        if (_background.transform.position.y > BACK_MIN)
        {
            BACK_SPEED -= Time.deltaTime * 1.9f;
            _background.transform.Translate(new Vector2(0, -BACK_SPEED * Time.deltaTime));
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            _menu.SetActive(true);
            _quit.SetActive(true);
        }
    }

    public void NewStart()
    {
        PlayerPrefs.SetInt("stage", 1);
        PlayerPrefs.Save();

        _data._curStage = PlayerPrefs.GetInt("stage");
    }

    public void CallLoading()
    {
        StartCoroutine(CallScene.Instance.LoadingSceneCall(Macro.SCENE_DATING, 0.2f));
    }

    public void SoundEffect()
    {
        _data._soundEffect.GetComponent<SoundManager>().Play(_soundEffect);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
