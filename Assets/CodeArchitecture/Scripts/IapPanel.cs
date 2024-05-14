using System.Collections;
using System.Collections.Generic;
using PlayerInteractive_Mediation;
using UnityEngine;

public class IapPanel : MonoBehaviour
{
 
    public void ShowRewardedAd(int AdType) 
    {

        Data.AdType = AdType;
        if (FindObjectOfType<Pi_AdsCall>())
        {
            FindObjectOfType<Pi_AdsCall>().showRewardADsBoth(GiverewaredNow);
        }
    }

    public void GiverewaredNow()
    {
        Data.RewardedAdWatched();
        Debug.Log("RewardGiven");
    }
    public void LoadInter()
    {
        if (FindObjectOfType<Pi_AdsCall>())
        {
            if (PrefsManager.GetInterInt() != 5)
            {
                FindObjectOfType<Pi_AdsCall>().loadInterstitialAD();
            }
        }

     
    }

    public void ShowInter()
    {
        LoadInter();
        if (FindObjectOfType<Pi_AdsCall>())
        {
            FindObjectOfType<Pi_AdsCall>().showInterstitialAD();
            PrefsManager.SetInterInt(1);
        }
        LoadInter();
    }
    
    public void HideMediumBanner()
    {
        if (FindObjectOfType<Pi_AdsCall>())
        {
            FindObjectOfType<Pi_AdsCall>().hideBigBanner();
        }
    }
    
}