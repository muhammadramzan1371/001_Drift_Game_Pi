using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

namespace PlayerInteractive_Mediation
{
    public enum AdsLoadingStatus
    {
        NotLoaded,
        Loading,
        Loaded,
        NoInventory
    }
    public abstract class Pi_AdsCall : MonoBehaviour
    {
        [Space(20)]
        [Header("                            Please Note : AD IDs are Autofill when TestADs is on")]
        public bool testingMode;
        public bool disbaleLogMode = true;
      
        public delegate void RewardUserDelegate();
        public delegate void AfterLoading();

        public static AdsLoadingStatus rewardADs_Status = AdsLoadingStatus.NotLoaded;
        public static AdsLoadingStatus rewardInterstitial_Status = AdsLoadingStatus.NotLoaded;
        public static AdsLoadingStatus interstitial_Status = AdsLoadingStatus.NotLoaded;
        public static AdsLoadingStatus smallBanner_Status = AdsLoadingStatus.NotLoaded;
        public static AdsLoadingStatus small2ndBanner_Status = AdsLoadingStatus.NotLoaded;
        public static AdsLoadingStatus bigBanner_Status = AdsLoadingStatus.NotLoaded;

        public abstract bool checkSmallFirstBannerAD();
        public abstract void loadBanner1();
        public abstract void showBanner1();
        public abstract void hideBanner1();
        public abstract bool checkSmallSecondBannedAD();
        public abstract void loadBanner2();
        public abstract void showBanner2();
        public abstract void hideBanner2();
        public abstract bool checkInterstitialAD();
        public abstract void showInterstitialAD();
        public abstract void loadInterstitialAD();
        //2nd Banner
        public abstract bool checkRewardAD();
        public abstract void loadRewardVideoAD();
        public abstract void showRewardVideo(RewardUserDelegate _delegate);
        public abstract bool checkRewardIntAD();
        public abstract void loadRewardInt();
        public abstract void showRewardInt(RewardUserDelegate _delegate);
        public abstract bool checkBigBannerAD();
        public abstract void loadBigBannerAD();
        public abstract void showBigBannerAD(AdPosition pos);
        public abstract void hideBigBanner();
        
        int a = 0;

        public void ShowInt(AfterLoading afterLoading)
        {
            LoadingAds.Notify = afterLoading;
        }

        public void showRewardADsBoth(RewardUserDelegate _delegate)
        {
            if (a == 0)
            {
                showRewardVideo(_delegate);
                a = 1;
            }
            else if (a == 1)
            {
                showRewardInt(_delegate);
                a = 0;
            }
        }
        public void ShowRewardVideo()
        {
            showRewardADsBoth(GiveReward);
        }
        public void GiveReward()
        {
            Debug.Log("RewardGiven");
        }
    }
}

