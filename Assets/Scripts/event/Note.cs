using UnityEngine;
using System.Collections;

public class Note : MonoBehaviour 
{
    enum Type
    {
        up = 0,
        down,
        right,
        left
    }

    public GameObject           _clearEffect;
    public GameObject           _missEffect;

    private GameObject          _effect;
    private GameObject          _parent;

    private const float         NOTE_SPEED = 5f;
    private Type                type;

    void Start()
    {
        _parent = GameObject.Find("note_effect");
    }

	void Update () 
    {
        transform.Translate(new Vector2(0, Time.deltaTime * -NOTE_SPEED));
	}

    public void setType(int _type)
    {
        type = (Type)_type;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "collider_destroy")
            Destroy(this.gameObject);
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.name == "collider_judge")
        {
            //Debug.Log("judge");
            //Debug.Log("button : " + (Type)NoteManager.Instance.btType + ", note : " + type);
            if ((Type)NoteManager.Instance.btType == type)
            {
                NoteManager.Instance.noteClear++;
                Debug.Log("clear : " + NoteManager.Instance.noteClear);

                NoteManager.Instance.btType = -1;

                DynamicDataManager.Instance.SetEffect(Macro.SOUND_TOUCH);

                _effect = (GameObject)Instantiate(_clearEffect, _parent.transform.position, Quaternion.identity) as GameObject;
                _effect.transform.parent = _parent.transform;

                Destroy(this.gameObject);
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.name == "collider_judge")
        {
            Debug.Log("miss");

            DynamicDataManager.Instance.SetEffect(Macro.SOUND_SMASH);

            _effect = (GameObject)Instantiate(_missEffect, _parent.transform.position, Quaternion.identity) as GameObject;
            _effect.transform.parent = _parent.transform;
        }
    }
}
