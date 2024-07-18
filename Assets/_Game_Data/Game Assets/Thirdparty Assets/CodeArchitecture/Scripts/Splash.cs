using System.Collections;
using System.Collections.Generic;
using PlayerInteractive_Mediation;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Splash : MonoBehaviour
{
    public bl_SceneLoader bl_SceneLoader;


    void Awake() 
    {
        GameAnalyticsSDK.GameAnalytics.Initialize();
    }

    public void loadappOpenAd()
    {
        if (FindObjectOfType<Pi_appOpenHandler>())
        {
            FindObjectOfType<Pi_appOpenHandler>().LoadAppOpenAd();
        }
    }
    void Start()
    {
        StartCoroutine("changeScene");
        Invoke("loadappOpenAd",6);
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
    IEnumerator changeScene()
    {
        yield return new WaitForSeconds(0.4f);
        if (FindObjectOfType<Pi_AdsCall>())
        {
            Debug.Log("Find Ads Called Done loading");
            FindObjectOfType<Pi_AdsCall>().loadInterstitialAD();
            PrefsManager.SetInterInt(5);
        }
        bl_SceneLoader.LoadLevel("MenuScene");
    }
}
