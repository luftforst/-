using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Gallary : MonoBehaviour 
{
    public Image[]                  _endingImage;
    public Button[]                 _endingButton;
    public Button                   _bigImage;

    private DynamicDataManager      _data;
    private ImageManager            _image;

    void Start()
    {
        _data               = DynamicDataManager.Instance;
        _image              = ImageManager.Instance;

        for (int i = 0; i < _endingImage.Length; i++)
        {
            //Debug.Log(i + " : " + DynamicDataManager.Instance._endingData[i]);
            if (_data._endingData[Macro.ENDING_STAGE[i]] == "true")
            {
                _image.setImage(_endingImage[i].gameObject, "ending/" + Macro.ENDING_STAGE[i] + Macro.IMAGE_ENDING);
                _endingButton[i].interactable = true;
            }
            else
            {
                _image.setImage(_endingImage[i].gameObject, "ending/" + "default");
                _endingButton[i].interactable = false;
            }
        }
    }

    public void SetEndingImage(int index)
    {
        _image.setImage(_bigImage.gameObject, "ending/" + Macro.ENDING_STAGE[index] + Macro.IMAGE_ENDING);
        _bigImage.gameObject.SetActive(true);
    }
}
