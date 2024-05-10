using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Introtomanu : MonoBehaviour
{
    
    
    void Start()
    {
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
