using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerInteractive_Mediation;

public class showAD : MonoBehaviour
{

    public void showBanner()
    {
        if (FindObjectOfType<Pi_AdsCall>())
        {
            FindObjectOfType<Pi_AdsCall>().showBanner1();
            FindObjectOfType<Pi_AdsCall>().showBanner2();
        }
    }

    public void loadInter()
    {
        if (FindObjectOfType<Pi_AdsCall>())
        {
            FindObjectOfType<Pi_AdsCall>().loadInterstitialAD();
        }
    }

    public void showInt()
    {
        if (FindObjectOfType<Pi_AdsCall>())
        {
            FindObjectOfType<Pi_AdsCall>().showInterstitialAD();
        }
    }

    public void showReward()
    {
        if (FindObjectOfType<Pi_AdsCall>())
        {
            FindObjectOfType<Pi_AdsCall>().showRewardADsBoth(rewardDone);
        }
    }

    public GameObject reward;

    void rewardDone()
    {
        reward.SetActive(true);
    }

    public void bigBanner()
    {
        if (FindObjectOfType<Pi_AdsCall>())
        {
            FindObjectOfType<Pi_AdsCall>().showBigBannerAD(GoogleMobileAds.Api.AdPosition.Center);
        }
    }
}
