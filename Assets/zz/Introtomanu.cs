using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Introtomanu : MonoBehaviour
{
    public VideoPlayer Intro;
    
    void Start()
    {
        Intro.Play();
        StartCoroutine("changeScene");
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
    IEnumerator changeScene()
    {
        yield return new WaitForSeconds(5f);
        //AdmobAdsManager.Instance.LoadInterstitialAd();
        SceneManager.LoadSceneAsync(1);
    }
}
