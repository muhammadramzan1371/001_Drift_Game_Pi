﻿using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using NiobiumStudios;
using PlayerInteractive_Mediation;

public class MainMenu : MonoBehaviour
{
	public GameObject ExitDialog,settingPannel;
    public Slider volume_value;
    public static MainMenu instance;
    public GameObject lMark, MMark, HMark;
    public GameObject arrow,tild,steering;
    private bool isStart=false;

    // Use this for initialization
    async void Start ()
	{
       

        instance = this;
		SoundManager.Instance.PlayAudio(SoundManager.Instance.menuSound);

        AudioListener.volume = PrefsManager.GetSound();
        volume_value.value = PrefsManager.GetSound();

        Time.timeScale = 1;
        

        isStart = true;
        currentNumberQuality = PrefsManager.GetGameQuality();
        QualityClick(currentNumberQuality);
     

        currentNumber = PrefsManager.GetControlls();
        
        SetControlls(currentNumber);

        ShowPrivacyDialog();
       // await Task.Delay(1000);
       // PlayerSelection.instance.SetstartPont();
        //UnlockModes();
    }

    private async void Awake()
    {
        await Task.Delay(500);
        PlayerSelection.instance.SetstartPont();
    }

    /// <summary>
    /// //////Handle PrivacyPolicy Dialog
    /// </summary>
    /// 
    public void ShowPrivacyDialog() {
      //  Debug.Log("Privacy Policy is "+PrefsManager.GetPrivacyPolicy());
        //if (PrefsManager.GetPrivacyPolicy() == 0)
        //{
        //    PrivacyDialog.SetActive(true);
        //}
        //else {
        //    PrivacyDialog.SetActive(false);
        //}
    }

	public void Event_Privacy()
	{
		SoundManager.Instance.PlayAudio(SoundManager.Instance.menuSound);
		//Debug.Log("Privacy Policy is " + PrefsManager.GetPrivacyPolicy());
		//if (PrefsManager.GetPrivacyPolicy() == 0)
		//{
		//	PrivacyDialog.SetActive(true);
		//	PrivacyDialog.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
		//	PrivacyDialog.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
		//}
		//else
		//{
		//	PrivacyDialog.SetActive(true);
		//	PrivacyDialog.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
		//	PrivacyDialog.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
		//}
	}
	public void Event_VisitWebsite()
	{
		SoundManager.Instance.PlayAudio(SoundManager.Instance.menuSound);
		//Application.OpenURL("https://gamefitstudio.blogspot.com/2019/06/the-gamefit-will-strictly-comply-with.html");

	}

		public void AcceptPolicy()
    {

        PrefsManager.SetPrivacyPolicy(1);
       
        //PrivacyDialog.SetActive(false);
        //if (PrefsManager.GetFirstTimeGame() == 0)
        //{
        //    dailyreward.SetActive(true);
        //    PrefsManager.SetFirstTimeGame(1);
        //} 

    }
    public void PlayOneShot()
    {
        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
    }



  



    /// <summary>
    /// /////////main menu 
    /// </summary>




    public void QuitBtnEvent ()
	{
		ExitDialog.SetActive (true);

		SoundManager.Instance.PlayOneShotSounds (SoundManager.Instance.click);

	}

	public void Yes(){
		Application.Quit ();
	}
	public void No(){
		ExitDialog.SetActive (false);
		SoundManager.Instance.PlayOneShotSounds (SoundManager.Instance.click);

	}

	public void MoreGamesEvent ()
	{
		SoundManager.Instance.PlayOneShotSounds (SoundManager.Instance.click);
		Application.OpenURL("https://play.google.com/store/apps/dev?id=8615786039404712360");
    }

	public void RateUsButtonEvent ()
	{
		SoundManager.Instance.PlayOneShotSounds (SoundManager.Instance.click);
		Application.OpenURL ("https://play.google.com/store/apps/details?id=" + Application.identifier);
    }

	public void MuteEvent ()
	{
		GameObject.FindGameObjectWithTag ("SoundManager").GetComponent<AudioSource> ().enabled = false;
		GameObject.FindGameObjectWithTag ("SoundManager").transform.GetChild (0).gameObject.GetComponent<AudioSource> ().enabled = false;
	
		PrefsManager.SetSound (0);
	}

	public void UnMuteEvent ()
	{
		GameObject.FindGameObjectWithTag ("SoundManager").GetComponent<AudioSource> ().enabled = true;
		GameObject.FindGameObjectWithTag ("SoundManager").transform.GetChild (0).gameObject.GetComponent<AudioSource> ().enabled = true;
		
		SoundManager.Instance.PlayOneShotSounds (SoundManager.Instance.click);
		PrefsManager.SetSound (1);
	}

	public void ShowSettingsPanel ()
	{
		settingPannel.SetActive (true);
        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
    }

    public void BackfromSettingPannel()
    {
        settingPannel.SetActive(false);
        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
    }

    public void ShowModeSelection() {
     
        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
       // UnlockModes();
    }

    public void SettingVolume() {
        AudioListener.volume = volume_value.value;
        PrefsManager.SetSound(volume_value.value);
       // Debug.Log("Audio listener Volume " + AudioListener.volume);
    }


    public void ClickSound() {
        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);

    }



    int currentNumberQuality = 0;
    public void QualityClick (int number)
    {

        currentNumberQuality += number;

        if (currentNumberQuality < 0)
            currentNumberQuality = 2;
        if (currentNumberQuality > 2)
            currentNumberQuality = 0;
        SetGraphicQuality(currentNumberQuality);
    }


    public void SetGraphicQuality(int value) {
        PrefsManager.SetGameQuality(value);
        switch (value) { 
        
        
        
           case 0:
               lMark.SetActive(false);
               MMark.SetActive(false);
               HMark.SetActive(true);
               QualitySettings.SetQualityLevel(0); 
               break;
           
           case  1:
                lMark.SetActive(false);
                MMark.SetActive(true);
                HMark.SetActive(false);
                QualitySettings.SetQualityLevel(1);
                break;

            case 2:
                lMark.SetActive(true);
                MMark.SetActive(false);
                HMark.SetActive(false);
                QualitySettings.SetQualityLevel(2);
               
                break;
        }
       
     
        if (!isStart)
            ClickSound();
    }

    public void CloseAds(GameObject objectHide) {
        objectHide.SetActive(false);
        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
    }


    public void RewardedToUser()
    {

        PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue() + 50);

        Invoke("offitagain", 2f);
    }


    public void offitagain()
    {
     
    }


    public void ButtonSound() {

        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);

    }


    int currentNumber=0;
    public void ControlClick(int number) {

        currentNumber += number;

        if (currentNumber < 0)
            currentNumber = 2;
        if (currentNumber > 2)
            currentNumber = 0;
        SetControlls(currentNumber);
    }


    public void SetControlls(int value)
    {
        if (!isStart)
            ClickSound();
        if (value == 0)
        {
            arrow.SetActive(true);
            steering.SetActive(false);
            tild.SetActive(false);
        }
        if (value == 1)
        {
            arrow.SetActive(false);
            steering.SetActive(true);
            tild.SetActive(false);
        }

        if (value == 2)
        {
            arrow.SetActive(false);
            steering.SetActive(false);
            tild.SetActive(true);
        }

        isStart = false;
        PrefsManager.SetControlls(value);

    }

  
    

  

    private void OnEnable()
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

        DailyRewards.onClaimPrize += Claim;
     
    }

    private void OnDisable()
    {
        DailyRewards.onClaimPrize -= Claim;
    }


    public void Claim(int Day)
    {
        Debug.Log("Day=" + Day);
        if (Day == 1)
        {

            PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue() + 1000);
        }
        if (Day == 2)
        {
            PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue() + 2000);
        }
        if (Day == 3)
        {
            PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue() + 5000);
          //  PrefsManager.SetPlayerState(5,1);
        }
        if (Day == 4)
        {
            PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue() + 10000);
            //PrefsManager.SetUnlockCoinMode(1);
        }
        if (Day == 5)
        {
            PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue() + 15000);
           // PrefsManager.SetPlayerState(4,1);
        }
        if (Day == 6)
        {
            PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue() + 25000);

        }
        if (Day == 7)
        {
            PrefsManager.SetPlayerState(3,1);
            //PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue() + 30000);
        }
    }



//    public void FreeMode()
//    {
//        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
//        Modes.SetActive(false);
//        PrefsManager.SetGameMode("free");
//        DogSelectionManager.instance.RedirectToDogSelection();
//        //loading.SetActive(true);
//        //SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
//        // loading.GetComponentInChildren<bl_SceneLoader>().LoadLevel("GameplayFinal");
//
//    }
    public void FreeMode(int number)
    {
        PrefsManager.SetGameMode("Free");
        PlayerSelection.instance.RedirectToDogSelection();
        
//        PrefsManager.SetWeather(number);
//        Play();

    }



    public void ChallangeMode(int modselect)
    {

        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
        ModeSelected(modselect);
        


    }

    public void ModeSelected(int modselect)
    {
      //  SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
     
        PrefsManager.SetGameMode("challange");
        PrefsManager.SetLevelMode(modselect);
        if (modselect == 1)
        {
           
            PlayerSelection.instance.ChrackterSlection.SetActive(true);
            PlayerSelection.instance.menuCanvas.SetActive(false);
            PlayerSelection.instance.SelectDogPlay();
        }
        else { 
        PlayerSelection.instance.RedirectToDogSelection();

        }

    }

}
