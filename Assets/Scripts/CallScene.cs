using UnityEngine;
using System.Collections;


public class CallScene : Singleton<CallScene>
{
    private string          _stage;

    public IEnumerator LoadingSceneCall(string stage, float delay)
    {
        _stage      = stage;

        yield return new WaitForSeconds(delay);

        Application.LoadLevel(Macro.SCENE_LOADING);
    }

    public IEnumerator SceneCall(float delay)
    {
        if (_stage == null)
            yield return null;

        Debug.Log(_stage);

        yield return new WaitForSeconds(delay);

        Application.LoadLevelAsync(_stage);
    }

    public string Stage
    {
        get { return _stage; }
        set { _stage = value; }
    }
}
