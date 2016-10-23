using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageManager : Singleton<ImageManager>
{

    public void setSprite(GameObject obj, string target_name)
    {
        Sprite m_Object = (Sprite)Resources.Load("Dating/" + target_name, typeof(Sprite));
        obj.GetComponent<SpriteRenderer>().sprite = m_Object;
        //Debug.Log("Dating/" + DataSave.Instance.curStage + "/" + target_name);
    }

    public void setImage(GameObject obj, string target_name)
    {
        Sprite m_Object = (Sprite)Resources.Load("Dating/" + target_name, typeof(Sprite));
        obj.GetComponent<Image>().sprite = m_Object;
        //Debug.Log("Dating/" + target_name);
    }

    // live2d call
    public void setLiveImage(GameObject obj, string target_name)
    {
        obj.GetComponent<LAppModelProxy>().path = "live2d/" + target_name + "/" + target_name + ".model.json";
    }

    //public void InitJellySprite(GameObject obj, string targetName)
    //{
    //    Sprite m_Object = (Sprite)Resources.Load("Dating/" + DataSave.Instance.curStage + "/" + targetName, typeof(Sprite));
    //    obj.GetComponent<UnityJellySprite>().m_Sprite = m_Object;
    //}
}
