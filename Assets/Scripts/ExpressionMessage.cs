using UnityEngine;
using System.Collections;

public class ExpressionMessage : MonoBehaviour
{
    private float       _time       = 0;
    public float        _destroy_time;

    private float       _fade_time  = 0;
    private float       _play_time  = 1f;

    private Animator    _anim;

    void Start()
    {
        _anim = this.gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        _time += Time.deltaTime;

        if (_time >= _destroy_time)
        {
            _anim.SetBool("fade_out", true);

            _fade_time += Time.deltaTime;
            if (_fade_time >= _play_time)
            {
                this.gameObject.SetActive(false);
                _time = 0;
                _fade_time = 0;
            }
        }
	}
}
