using UnityEngine;
using System.Collections;

public class EffectAnimation : MonoBehaviour 
{
    private float   time        = 0;

	void Update () 
    {
        time += Time.deltaTime;
	    if (time > 0.5f)
        {
            this.gameObject.SetActive(true);
        }
	}
}
