using UnityEngine;
using System.Collections;

public class LFaceManager : Singleton<LFaceManager> 
{
    private bool isFace = false;

    public string getFace(int name_index)
    {
        return GameData.Instance.getCurrentStageData().dialogData[name_index].expression;
        //DataSave.Instance.expression[name_index];
    }

    public bool IsFace
    {
        get { return isFace; }
        set { isFace = value; }
    }
}
