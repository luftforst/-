using UnityEngine;
using System.Collections;

public class BackgroundFade : MonoBehaviour 
{
    private float       _time           = 0;
    public float        _destroy_time;

    private Animator    _anim;

    void Start()
    {
        _anim = this.gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        _time += Time.deltaTime;

        _anim.SetBool("change", true);

        if (_time >= _destroy_time)
        {
            _anim.SetBool("change", false);
            this.enabled = false;
        }
    }
}
