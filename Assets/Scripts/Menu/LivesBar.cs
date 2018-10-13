using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesBar : MonoBehaviour {

    private Transform[] lives = new Transform[3];
    private int i;

    private void Awake()
    {
        for (i = 0; i < lives.Length; i++)
        {
            lives[i] = transform.GetChild(i);
        }

        i = 0;
        lives[0].gameObject.SetActive(true);
        
        
    }

    public void AddLive()
    {
        if (i != 2)
        {
            i++;
        }
        lives[i].gameObject.SetActive(true);
    }

    public void SubLive()
    {
        if (i != -1)
        {
            lives[i].gameObject.SetActive(false);
            i--;
        }
    }
}
