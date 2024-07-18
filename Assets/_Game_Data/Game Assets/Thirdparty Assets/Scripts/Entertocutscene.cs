using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Entertocutscene : MonoBehaviour
{
    public GameObject Timeline;
    public PlayableDirector Director;


    public GameObject Trigergameobject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Player")
        {
            Time.timeScale = 1f;
            Timeline.SetActive(true);
            Director.Play();
            Invoke("HideTimeline", (float)Director.duration - 0.9f);
            LevelManager.instace.Tpscamera.GetComponent<Camera>().farClipPlane = 30;
            UiManagerObject.instance.HideGamePlay();
        }
    }

    public void HideTimeline()
{
    Timeline.SetActive(false);
    LevelManager.instace.Tpscamera.GetComponent<Camera>().farClipPlane = getFar();
    UiManagerObject.instance.ShowGamePlay();
    LevelManager.instace.canvashud.gameObject.SetActive(true);
    Trigergameobject.SetActive(false);
}

private int  getFar()
{
    if (SystemInfo.systemMemorySize > 3500)
    {
        return 250;
    }
    else if (SystemInfo.systemMemorySize > 2500)
    {
        return 200;
    }
    else if (SystemInfo.systemMemorySize > 1200)
    {
        return 150;
    }
    else
    {
        return 100;
    }
}
    
    
    
    
    
    
    
    
    
    
}
