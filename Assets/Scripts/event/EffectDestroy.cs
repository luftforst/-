using UnityEngine;
using System.Collections;

public class EffectDestroy : MonoBehaviour 
{
    private float   time                = 0;

    public float    destroy_time;

    void Update() 
    {
	    time += Time.deltaTime;

        if (time >= destroy_time)
            Destroy(this.gameObject);
	}
}
