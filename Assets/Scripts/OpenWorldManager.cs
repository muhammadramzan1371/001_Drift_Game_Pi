using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class OpenWorldManager : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform CarPostiom;
    public Transform defulBikePostion;
    public Transform TpsPosition;

    public LevelProperties[] AllMission;

    public LevelProperties CurrentMissionProperties;
    public static OpenWorldManager Instance;
    public bool isCutScene;

    public GameObject Timeline;

    public PlayableDirector Director;
    public int TotalMisson;
    public bool isSetPosition = false;
    private void Awake()
    {
        Instance = this;
    }


   // public string GetCurrentMissionStatmentk()
   // {
       // return AllMission[PrefsManager.GetCurrentMission()].GetComponent<LevelProperties>().LevelStatment;
   // }


    public void Start()
    {
        if (isCutScene)
        {
            Time.timeScale = 1f;
            Timeline.SetActive(true);
            Director.Play();
            Invoke("HideTimeline", (float)Director.duration - 0.9f);
          //  LevelManager.instace.Tpscamera.GetComponent<Camera>().farClipPlane = 10f;
            UiManagerObject.instance.controls.SetActive(false);
          //  LevelManager.instace.canvashud.gameObject.SetActive(false);
        }

    }

    

    public bool missionon = false;

    /*
    public void AcceptMissionformission()
    {
        foreach (var Mission in AllMission)
            {
                Mission.gameObject.SetActive(false);
            }
            CurrentMissionProperties = AllMission[PrefsManager.GetCurrentMission()];
            CurrentMissionProperties.gameObject.SetActive(true);
            missionon = true;
            LevelManager.instace.SetTransform
                (CurrentMissionProperties.TpsPlayer, 
                    CurrentMissionProperties.DefultBike, 
                    CurrentMissionProperties.CarPosition,CurrentMissionProperties.isSetPosition);
          
            setcarok();
    }
    */

    /*public void setcarok()
    {
        if (GameManager.Instance.CurrentCar!=null)
        { 
            GameManager.Instance.CurrentCar.GetComponent<Rigidbody>().velocity=Vector3.zero; 
            GameManager.Instance.CurrentCar.GetComponent<Rigidbody>().angularVelocity=Vector3.zero; 
        }
        else 
        {
            return;
        }
    }*/


    public void HideTimeline()
    {
        Timeline.SetActive(false);
       LevelManager.instace.Tpscamera.GetComponent<Camera>().farClipPlane = getFar();
        UiManagerObject.instance.controls.SetActive(true);
        LevelManager.instace.canvashud.gameObject.SetActive(true);
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



















    


