using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IapPanel : MonoBehaviour
{
 


    public void ShowAdUnity() {

       // AdCalls.instance.ShowInterstitial();
    
    }

   public void ShowAdAdmob() {
        //AdCalls.instance.ShowInterstitial();
    }

    public void ShowRewardedAd(int AdType) {

        Data.AdType = AdType;
      //  AdmobAdsManager.Instance.ShowRewardVideo();
        //AdCalls.instance.ShowRewarded();
    }

    public void RemoveAd() {
        //MyIAPManager.instance.RemoveAds();
    }
    public void UnlockAllVehicles()
    {
      //  MyIAPManager.instance.UnlockAll();
    }
    public void UnlockAllLevels()
    {
       // MyIAPManager.instance.UnlockAllMissions();
    }
    public void UnlockFullGame()
    {
       // MyIAPManager.instance.unlockEconomyPakage();
    }
    public void ShowDebugger() {

       // MaxSdk.ShowMediationDebugger();


    }
}
