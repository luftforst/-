using UnityEngine;
using System.Collections;

public class Utils : MonoBehaviour
{
    static public void ArrayActive(GameObject[] objs, bool b)
    {
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i] != null)
            {
                objs[i].SetActive(b);
            }
        }
    }

}
