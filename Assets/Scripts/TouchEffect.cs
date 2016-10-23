using UnityEngine;
using System.Collections;

public class TouchEffect : MonoBehaviour
{
    private GameObject      _obj;
    public GameObject       _effect;

    //private float           _time       = 0;

    void Update()
    {
        int cnt = Input.touchCount;

            for (int i = 0; i < cnt; ++i)
            {
                Touch touch = Input.GetTouch(i);
                Vector2 pos = touch.position;

                if (touch.phase == TouchPhase.Began)
                {
                    //Debug.Log(pos);

                    Vector3 p = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, Camera.main.nearClipPlane));
                    _obj = (GameObject)Instantiate(_effect, p, _effect.transform.rotation) as GameObject;
                    //_obj = (GameObject)Instantiate(_effect) as GameObject;
                    _obj.transform.parent = this.gameObject.transform;
                    //_obj.transform.position = Input.mousePosition;

                }

                else if (touch.phase == TouchPhase.Moved)
                {

                }

                else if (touch.phase == TouchPhase.Ended)
                {
                    //_obj = (GameObject)Instantiate(_effect) as GameObject;
                    //_obj.transform.parent = this.gameObject.transform;
                    //_obj.transform.position = Input.mousePosition;
                }
            }
       

    }


}
