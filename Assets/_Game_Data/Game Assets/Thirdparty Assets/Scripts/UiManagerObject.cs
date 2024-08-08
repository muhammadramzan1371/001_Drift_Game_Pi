using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameAnalyticsSDK;
using PlayerInteractive_Mediation;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManagerObject : MonoBehaviour
{
    [Serializable]
    public class Panels
    {
        //public GameObject tachometer;
        public GameObject CarControle;
        public GameObject TpsControle;
        public GameObject  RateUsPanel;
    }
    public Panels panels;
    // Start is called before the first frame update
    public GameObject ObjectivePannel, Pause, Fail, Complete, Loading,OutOfFuel,error,fadeimage;
    public Text ObjectiveText,NosCountText;
    public static UiManagerObject instance;
    public int TotalLevels;
    public Image fuelBar;
    public GameObject blankimage;
    public Image NosFiller;
    public GameObject NosButton;
    public Button Driftbutton;
    void Awake()
    {

        instance = this;
        SoundManager.Instance.PlayAudio(SoundManager.Instance.BgSound);
        Time.timeScale = 1f;
        
    }
    
    void Start()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, PrefsManager.GetGameMode(),PrefsManager.GetCurrentLevel());
      
    }
    void OnEnable()
    {
       
        if (FindObjectOfType<Pi_AdsCall>())
        {
            FindObjectOfType<Pi_AdsCall>().showBanner1();
            FindObjectOfType<Pi_AdsCall>().showBanner2();
            FindObjectOfType<Pi_AdsCall>().hideBigBanner();
            if (PrefsManager.GetInterInt()!=5)
            {
                FindObjectOfType<Pi_AdsCall>().loadInterstitialAD();
            }
        }
        if (PrefsManager.GetProfileFill()==0)
        {
          //  Invoke("OnRateusPanel",40f);
        }
        else
        {
          //  panels.RateUsPanel.SetActive(false);
        }

       
    }
    public void OnRateusPanel()
    {
        panels.RateUsPanel.SetActive(true);
        GameManager.Instance.CurrentCar.GetComponent<Rigidbody>().velocity = Vector3.zero;
        GameManager.Instance.CurrentCar.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        HideGamePlay();
    }
    public void ShowObjective(string statment)
    {
        ObjectiveText.text = statment;
        //if (LevelManager.instace.FreeMode)
        if (PrefsManager.GetGameMode() == "free")
        {
           
            ObjectivePannel.SetActive(false);
            SetTimeScale(1);

           
        }
         else if(PrefsManager.GetLevelMode() == 0){
        ObjectivePannel.SetActive(true);
        SetTimeScale(0);
        HideGamePlay();
        }
        else if(PrefsManager.GetLevelMode() ==1){
        ObjectivePannel.SetActive(false);
            //MiniMap.GetComponent<CanvasGroup>().alpha = 0;

        }
        
        
    }


    public  void PressButton()
    {
        Driftbutton.onClick.Invoke();
    }

    public void FillFuelbar(float fillAmount) {
        fuelBar.fillAmount = fillAmount;
        if (fillAmount <= 0) {
            OutOfFuel.SetActive(true);
        }
    }

    public void FillFuelTank() {
        if (PrefsManager.GetCoinsValue() > 1000)
            PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue() - 1000);
        else { 
            error.SetActive(true);
            Invoke("OffError",4f);
        }
     //   LevelManager.instace.SelectedPlayer.GetComponent<RCC_CarControllerV3>().FillFullTank();
        OutOfFuel.SetActive(false);
    }


    public void OffError() {
        error.SetActive(false);

    }

   public void FullTankWithVideo() { 
        //LevelManager.instace.SelectedPlayer.GetComponent<RCC_CarControllerV3>().FillFullTank();
        OutOfFuel.SetActive(false);

    }

   public void HideGamePlay()
   {
       LevelManager.instace.canvashud.gameObject.SetActive(false);
      Logger.ShowLog("Here");
       panels. CarControle.SetActive(false);
       panels.TpsControle.SetActive(false);
     
   }

   public void ShowGamePlay()
   {
      
       if (GameManager.Instance.TpsStatus == PlayerStatus.BikeDriving)
       {
           panels.CarControle.SetActive(false);
           panels.TpsControle.SetActive(false);
       }

       if (GameManager.Instance.TpsStatus == PlayerStatus.CarDriving)
       {
           panels. CarControle.SetActive(true);
           panels. TpsControle.SetActive(false);
       }

       if (GameManager.Instance.TpsStatus == PlayerStatus.ThirdPerson)
       {
           panels. CarControle.SetActive(false);
           panels. TpsControle.SetActive(true);
       }
       LevelManager.instace.canvashud.gameObject.SetActive(true);
   }

    public void HideObjectivePannel()
    {
       // AdmobAdsManager.Instance.LoadInterstitialAd();
        ObjectivePannel.SetActive(false);
        SetTimeScale(1);
        ShowGamePlay();
       
    }

    public void ShowPause()
    {
        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
      //  AdmobAdsManager.Instance.ShowInt(ShowPauseNow,true);
      ShowPauseNow();
    }

    public async void ShowPauseNow()
    {
        if (FindObjectOfType<Pi_AdsCall>())
        {
            FindObjectOfType<Pi_AdsCall>().showInterstitialAD();
            PrefsManager.SetInterInt(1);
        }
        Pause.SetActive(true);
        SetTimeScale(0);
        HideGamePlay();
        await Task.Delay(2000);
        if (FindObjectOfType<Pi_AdsCall>())
        {
            if (PrefsManager.GetInterInt() != 5)
            {
                FindObjectOfType<Pi_AdsCall>().loadInterstitialAD();
            }
        }
        GameManager.Instance.CurrentCar.GetComponent<Rigidbody>().velocity = Vector3.zero;
        GameManager.Instance.CurrentCar.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    public void Resume()
    {
        SetTimeScale(1);
        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
        Pause.SetActive(false);
        ShowGamePlay();
        if (FindObjectOfType<Pi_AdsCall>())
        {
            if (PrefsManager.GetInterInt() != 5)
            {
                FindObjectOfType<Pi_AdsCall>().loadInterstitialAD();
            }
        }
    }

    public void Restart()
    {
        SetTimeScale(1);
        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
        Loading.SetActive(true);
        Loading.GetComponentInChildren<bl_SceneLoader>().LoadLevel("GamePlay");
        if (PrefsManager.GetInterInt()!=5)
        {
            FindObjectOfType<Pi_AdsCall>().loadInterstitialAD();
        }
        Invoke(nameof(showInterAd),5f);
    }

    public void Home()
    {
        SetTimeScale(1);
        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
        Loading.SetActive(true);
        Loading.GetComponentInChildren<bl_SceneLoader>().LoadLevel("MenuScene");
        if (PrefsManager.GetInterInt()!=5)
        {
            FindObjectOfType<Pi_AdsCall>().loadInterstitialAD();
        }
        Invoke(nameof(showInterAd),5f);
    }
    public GameObject AdBrakepanel;
    public async void showInterAd()
    {
        AdBrakepanel.SetActive(true);
        await Task.Delay(1000);
        if (FindObjectOfType<Pi_AdsCall>())
        {
            FindObjectOfType<Pi_AdsCall>().showInterstitialAD();
			
            PrefsManager.SetInterInt(1);
        }
        AdBrakepanel.SetActive(false);
        SetTimeScale(1);
    }

    public void LevelCompleteNow()
    {
        Complete.SetActive(true);
        // SetTimeScale(0);
        HideGamePlay();
        if (PrefsManager.GetGameMode() != "free")
        {

            if (PrefsManager.GetLevelMode() == 0)
            {
               Logger.ShowLog("FirstMode"+PrefsManager.GetCurrentLevel()+" "+PrefsManager.GetLevelLocking());
                if (PrefsManager.GetCurrentLevel() >= PrefsManager.GetLevelLocking())
                {
                    PrefsManager.SetLevelLocking(PrefsManager.GetLevelLocking() + 1);

                }
               Logger.ShowLog("FirstMode"+PrefsManager.GetCurrentLevel()+" "+PrefsManager.GetLevelLocking());
            }
            else if (PrefsManager.GetLevelMode() == 1)
            {
               Logger.ShowLog("SnowMode"+PrefsManager.GetCurrentLevel()+" "+PrefsManager.GetSnowLevelLocking());
                if ((PrefsManager.GetCurrentLevel()) >= PrefsManager.GetSnowLevelLocking())
                {
                    PrefsManager.SetSnowLevelLocking(PrefsManager.GetSnowLevelLocking() + 1);

                }
               Logger.ShowLog("SnowMode"+PrefsManager.GetCurrentLevel()+" "+PrefsManager.GetSnowLevelLocking());
            }
          //  GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, PrefsManager.GetGameMode(), PrefsManager.GetCurrentLevel());
            //  Data.SendCompleteEvent(PrefsManager.GetCurrentLevel());
           // Admob_LogHelper.MissionOrLevelCompletedEventLog(PrefsManager.GetGameMode(),PrefsManager.GetCurrentLevel());

        }
        else { 
            //Data.SendCompleteEvent(20);

         //   Admob_LogHelper.MissionOrLevelCompletedEventLog("Free",0);

        }

    }

    public void ShowComplete()
    {
   //   AdmobAdsManager.Instance.ShowInt(LevelCompleteNow,true);
      LevelCompleteNow();
        SoundManager.Instance?.PlayAudio(SoundManager.Instance.LevelComplete);

    }

    public void ShowFail()
    {
       
        SoundManager.Instance.PlayAudio(SoundManager.Instance.levelFail);
      //  Data.SendFailEvent(PrefsManager.GetCurrentLevel());
          //AdmobAdsManager.Instance.ShowInt(ShowLevelFailNow,true);
          ShowLevelFailNow();
    }

    public void ShowLevelFailNow()
    {
        
        Fail.SetActive(true);
        SetTimeScale(0);
        HideGamePlay();
      //  GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, PrefsManager.GetGameMode(), PrefsManager.GetCurrentLevel());
     //  Admob_LogHelper.MissionOrLevelFailEventLog(PrefsManager.GetGameMode(),PrefsManager.GetCurrentLevel());
    }

    public void Next()
    {
     
        PrefsManager.SetCurrentLevel(PrefsManager.GetCurrentLevel()+1);
        Loading.SetActive(true);
        if (PrefsManager.GetCurrentLevel() >= TotalLevels)
        { 
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex-1);

        }
        else
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }

    }
    public void SetTimeScale(float timescale)
    {
        Time.timeScale = timescale;
    }


}
