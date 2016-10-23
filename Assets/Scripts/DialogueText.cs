using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text;

public class DialogueText : Singleton<DialogueText> 
{
    public enum State
    {
        IDLE            = 0, 
        PRINTING        = 1,
        PRINT_END       = 2,
    }

    private State   _state          = State.IDLE;
    private int     _count          = 0;
    private float   _elapsedTime    = 0;

    private Text    _text           = null;
    private string  _dialog;

    // talk animation
    private bool    _talk = false;

    void Start()
    {
        _text       = this.GetComponent<Text>();
    }

    void Update()
    {
        if (_dialog == null)
            return;

        switch (_state)
        {
            case State.IDLE:

                ChangeState( State.PRINTING );

                break;

            case State.PRINTING:

                _elapsedTime += Time.deltaTime * 20f;

                if (_elapsedTime >= 1.0f)
                {
                    ++_count;

                    _elapsedTime    = 0.0f;
                    //Debug.Log( "Count : " + _count );

                    if (_count >= _dialog.Length)
                    {
                        ChangeState( State.PRINT_END );
                        _count = 0;
                    }
                    else
                    {
                        _text.text = _dialog.Substring(0, _count);
                    }
                }

                break;

            case State.PRINT_END:

                break;
        }
    }

    // 한번만 실행하기 위해 switch를 따로 빼줌
    public void ChangeState(State state)
    {
        switch( state )
        {
            case State.PRINTING :

                // talk expression
                Talk = true;

                break;

            case State.PRINT_END :

                // end talk expression
                Talk = false;
                LFaceManager.Instance.IsFace = true;

                _text.text      = _dialog;
                _count          = 0;
                _elapsedTime    = 0;

                break;
        }

        _state  = state;
    }

    public bool IsNextDialog()
    {
        if( _state == State.PRINT_END )
        {
            return true;
        }

        return false;
    }

    public void setDialog(string script)
    {
        //Debug.Log( script );
        _dialog = script;
    }

    public State DialState
    {
        get { return _state; }
        set { _state = value; }
    }
    
    public bool Talk
    {
        get { return _talk; }
        set { _talk = value; }
    }
}
