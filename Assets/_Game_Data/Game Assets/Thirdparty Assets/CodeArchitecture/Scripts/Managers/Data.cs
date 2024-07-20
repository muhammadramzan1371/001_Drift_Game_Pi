using UnityEngine;
using System.Collections;
using NiobiumStudios;
using System.Collections.Generic;
using GameAnalyticsSDK;

public class Data : MonoBehaviour {
    public delegate void RemoveAds();
    public static event RemoveAds OnRemoveAds;


    public delegate void UnlockAllPlayers();
    public static event UnlockAllPlayers OnUnlockAllPlayers;

	public delegate void UnlockPlayers();
	public static event UnlockPlayers OnUnlockPlayersCheck;

	public delegate void UnlockAllMission();
    public static event UnlockAllMission OnUnlockAllMission;



    public static int AdType;
    // Use this for initialization
    void Start () {
		DontDestroyOnLoad (gameObject);
	}
	public static void CoinsAddition(int coins)
	{
		//GameObject.Find ("Canvas").GetComponent<ShowAds>().EnableRewarded();
		Logger.ShowLog (coins + " Coins Added");
	}
	public static void GoldAddition(int coins)
	{
		Logger.ShowLog (coins + " Gold Added");
	}
	public static void UnlockAllPurchased ()
	{
		
		RemoveAdsPurchased ();
		Logger.ShowLog ("Unlock All Purchased");
	}
	public static void RemoveAdsPurchased ()
	{
		PlayerPrefs.SetInt ("removeads", 1);
		PlayerPrefs.Save ();
		
		Logger.ShowLog ("Remove Ads Purchased");
        OnRemoveAds();
    }
    public static void UnlockAllMissiionsPurchased()
    {
        //LevelSelectionScript1.instance.UnlockAllLevels();

        Logger.ShowLog("Unlock All Levels");
        PrefsManager.SetLevelLocking(20);
        PlayerPrefs.SetFloat("UnlockAllLevels", 1f);
        OnUnlockAllMission.Invoke();

    }
    public static void UnlockAllVehiclesPurchased()
    {

        print("Data:unlock vehicle");

        PrefsManager.UnLockAllVehicle(5);

        PlayerPrefs.SetFloat("UnlockAllVehicles", 1f);
        OnUnlockAllPlayers();
        OnUnlockPlayersCheck?.Invoke();
    }




    public static void RewardEconomyPakage()
    {

        RemoveAdsPurchased();
        UnlockAllMissiionsPurchased();
        UnlockAllVehiclesPurchased();
       // PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue() + 50000);
        PlayerPrefs.SetFloat("unlockEconomyPackage", 1);
        Logger.ShowLog("Reward economy pakage  ");
        OnUnlockPlayersCheck?.Invoke();
    }



    public static void RewardedAdWatched()
    {
        Logger.ShowLog("Reward Received");
        if (AdType == 0)
        {
            PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue() + 500);
            GameAnalytics.NewAdEvent(GAAdAction.RewardReceived,GAAdType.RewardedVideo,"Admob","Get_500_Coins");
        }

        if (AdType==1)
        {
	        /*if (FindObjectOfType<SpiningManager>())
	        {
		        FindObjectOfType<SpiningManager>().SpinNow();
	        }*/
        }
     
        if (AdType == 2) { 
		// MainMenuScript.instance.AddCashOnRewardedVideo();
		PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue()+1000);
		GameAnalytics.NewAdEvent(GAAdAction.RewardReceived,GAAdType.RewardedVideo,"Admob","Get_1000_Coins");
		}
	
            if (AdType == 3)
        {
	        if (FindObjectOfType<MoneyCounterAuto>())
	        {
		        
	        }
            FindObjectOfType<MoneyCounterAdd>().DoubleReward();
            GameAnalytics.NewAdEvent(GAAdAction.RewardReceived,GAAdType.RewardedVideo,"Admob","Get_2X_Coins");

        }
		if (AdType == 4)
        {
            UiManagerObject.instance.FullTankWithVideo();
            GameAnalytics.NewAdEvent(GAAdAction.RewardReceived,GAAdType.RewardedVideo,"Admob","Get_FullTank");
        }

        if (AdType == 5)
        {
	        PlayerSelection.instance.TestDrive();
	        GameAnalytics.NewAdEvent(GAAdAction.RewardReceived,GAAdType.RewardedVideo,"Admob","Get_TestDrive");
           // FindObjectOfType<MoneyCounterAdd>().DoubleReward();
            //VehicleSelectionScript.instance.AddCashOnRewardedVideo();
        }
        else if (AdType == 6)
        {
            FindObjectOfType<StoreScript>().WatchStatus();
            GameAnalytics.NewAdEvent(GAAdAction.RewardReceived,GAAdType.RewardedVideo,"Admob","Get_Watch Status");
        }
        else if (AdType == 7)
        {
         

        }
        if (AdType == 8)
        {
          //  FindObjectOfType<TimeController>().TimeReward();
        }
        
        else if (AdType == 9)
        {

           // LevelManger.instance.UpdateHits();
        }
        else if (AdType == 10)
        {
	        PrefsManager.SetNosCounter(2);
	        FindObjectOfType<RCC_MobileButtons>().FillNos();
	        GameAnalytics.NewAdEvent(GAAdAction.RewardReceived, GAAdType.RewardedVideo, "Admob", "Fill_Nos_With_Video");
          //  LevelManger.instance.UpdateHumanHits();
        }
        else if (AdType == 14)
        {
            FindObjectOfType<DailyRewardsInterface>().Close2xReward();
            GameAnalytics.NewAdEvent(GAAdAction.RewardReceived,GAAdType.RewardedVideo,"Admob","Get_Close 2X");
        }
        else if (AdType == 15)
        {
          //  FindObjectOfType<TimeController>().TimeRewardFreeMode();
        }
        else if (AdType == 16)
        {
	        GameAnalytics.NewAdEvent(GAAdAction.RewardReceived,GAAdType.RewardedVideo,"Admob","Get_500_Coins");
            PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue() + 500);
            FindObjectOfType<PlayerSelection>().OffNoCash();
        }

        //GameObject.Find ("Canvas").GetComponent<ShowAds_GP>().EnableRewarded();
    }

    //public static void SendCompleteEvent(int Level)
    //{
    //    Dictionary<string, object> param = new Dictionary<string, object>();
    //    param.Add("LevelNumber ", Level);
    //    AppMetrica.Instance.ReportEvent("LevelComplete", param);
    //}

    //public static void SendFailEvent(int Level)
    //{
    //    Dictionary<string, object> param = new Dictionary<string, object>();
    //    param.Add("LevelNumber ", Level);
    //    AppMetrica.Instance.ReportEvent("LevelFail", param);
    //}


}
