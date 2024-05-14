
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using System;
using System.Collections.Generic;
using GameAnalyticsSDK;
using UnityEngine;
using UnityEngine.Advertisements;
namespace PlayerInteractive_Mediation
{
    public class PlayerInteractive_Admob : Pi_AdsCall
    {
        [Space(20)]
        public string admobAppID;
        public string banner1;
        public string banner2;
        public string interstitialID;
        public string rewardedVideo;
        public string bigBannerID;
        public string rewardInterstitial;
        [Space(20)]
        public string UnityId;
        public string Unity_Interstitial_ID = "Interstitial_Android";
        public string Unity_RewardedVideo = "Rewarded_Android";
        [Space(20)]
        public AdPosition banner1_Position;
        public AdPosition banner2_Position;

        public static bool isSmallBannerLoadedFirst = false;
        public static bool isSmallBannerLoadedSecond = false;
        public static bool isMediumBannerLoaded = false;
        bool isAdmobInitialized = false;


        #region Small Banner ADs Variable
        [HideInInspector]
        public BannerView SmallBanner_L_Medium_Ecpm;
        [HideInInspector]
        public BannerView SmallBanner_R_Medium_Ecpm;

        public static bool Logs;

        #endregion

        #region Intersitial ADs Variable
        [HideInInspector]
        public InterstitialAd Interstitial_High_Ecpm;

        public delegate void InterstitialUnity();
        public static event InterstitialUnity Int_Unity;

        public static bool Interstitial_HighEcpm = true, UnityAds = false;
        #endregion

        #region RewardVideo ADs Variable
        private static RewardUserDelegate NotifyReward;

        [HideInInspector]
        public RewardedAd rewardBasedVideo;

        public delegate void RewardVideoUnity();
        public static event RewardVideoUnity RewardVideo_Unity;
        public static bool RewardVideo_High_Ecpm = true, UnityRewarded = false;
        #endregion

        #region Medium Banner ADs Variable

        [HideInInspector]
        public BannerView MediumBannerMediumEcpm;

        #endregion

        #region Rewared Interstitial ADs Variable

        [HideInInspector]
        public RewardedInterstitialAd rewardedInterstitialAd;
        [HideInInspector]
        public bool rewardedInterstitialHighECPMLoaded;

        #endregion


        private void Awake()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            DontDestroyOnLoad(this.gameObject);
            Logs = disbaleLogMode;

            if (testingMode)
            {
                admobAppID = "ca-app-pub-3940256099942544~3347511713";
                banner1 = "ca-app-pub-3940256099942544/6300978111";
                banner2 = "ca-app-pub-3940256099942544/6300978111";
                interstitialID = "ca-app-pub-3940256099942544/1033173712";
                rewardedVideo = "ca-app-pub-3940256099942544/5224354917";
                bigBannerID = "ca-app-pub-3940256099942544/6300978111";
                rewardInterstitial = "ca-app-pub-3940256099942544/5354046379";
                UnityId = "1234567";
                Unity_Interstitial_ID = "Interstitial_Android";
                Unity_RewardedVideo = "Rewarded_Android";
            }
            else
            {
#if UNITY_IOS
        ADMOB_ID = IosAndroid_ID;
          RequestConfiguration requestConfiguration = new RequestConfiguration.Builder()
       .SetSameAppKeyEnabled(false)
       .build();
        MobileAds.SetRequestConfiguration(requestConfiguration);
#endif
            }
        }
        private void Start()
        {
            InitAdmob();
            InitializeAds();
        }
        public void InitializeAds()
        {
            //Advertisement.Initialize(UnityId, testingMode, this);
        }
        public void OnInitializationComplete()
        {
            PlayerInteractive_Logger.Pi_LogEvent("PI_unity_advertisement_initialized_done");
        }
        public void OnInitializationFailed( string message)
        {
            Debug.Log($"PI_unity_advertisement_initialization_failed: {ToString()} - {message}");
        }
        private void InitAdmob()
        {
            PlayerInteractive_Logger.Pi_LogSender(Pi_AdmobEvents.Pi_Initializing);

            MobileAds.Initialize((initStatus) =>
            {
                Dictionary<string, AdapterStatus> map = initStatus.getAdapterStatusMap();
                foreach (KeyValuePair<string, AdapterStatus> keyValuePair in map)
                {
                    string className = keyValuePair.Key;
                    AdapterStatus status = keyValuePair.Value;
                    switch (status.InitializationState)
                    {
                        case AdapterState.NotReady:
                            break;
                        case AdapterState.Ready:
                            PlayerInteractive_Logger.Pi_LogSender(Pi_AdmobEvents.Pi_Initialized);
                            MediationAdapterConsent(className);
                            break;
                    }
                }
            });
#if UNITY_IOS
        MobileAds.SetiOSAppPauseOnBackground(true);    
#endif
        }
        void MediationAdapterConsent(string AdapterClassname)
        {
            if (AdapterClassname.Contains("ExampleClass"))
            {
                isAdmobInitialized = true;
                loadBanner1();
                loadBanner2();
                loadBigBannerAD();
                loadRewardInt();
                loadRewardVideoAD();
            }
            if (AdapterClassname.Contains("MobileAds"))
            {
                isAdmobInitialized = true;
                loadBanner1();
                loadBanner2();
                loadBigBannerAD();
                loadRewardInt();
                loadRewardVideoAD();
            }
        }


        #region BannerCodeBlock
        public override bool checkSmallFirstBannerAD()
        {
            return isSmallBannerLoadedFirst;
        }
        public override void loadBanner1()
        {
            if (!PreferenceManager.GetAdsStatus() || checkSmallFirstBannerAD() || smallBanner_Status == AdsLoadingStatus.Loading)
            {
                return;
            }
            if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork | Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
            {
                this.SmallBanner_L_Medium_Ecpm = new BannerView(banner1, AdSize.Banner, banner1_Position);
                PlayerInteractive_Logging.Log("PI_FirstSmallBanner_M_Ecpm");
                BindSmallBannerFirstMediumEcpm();
                var request = new AdRequest();
                this.SmallBanner_L_Medium_Ecpm.LoadAd(request);
                this.SmallBanner_L_Medium_Ecpm.Hide();
            }
        }
        public override void hideBanner1()
        {
            if (this.SmallBanner_L_Medium_Ecpm != null)
            {
                this.SmallBanner_L_Medium_Ecpm.Hide();
                PlayerInteractive_Logging.Log("PI_Admob:smallBanner:Hide_M_Ecpm");
            }
        }
        public void ShowBanner()
        {
            showBanner1();
        }
        public override void showBanner1()
        {
            hideBanner1();

            try
            {
                if (!PreferenceManager.GetAdsStatus() || !isAdmobInitialized)
                {

                    return;
                }



                if (SmallBanner_L_Medium_Ecpm != null)
                {
                    PlayerInteractive_Logging.Log("PI_ >> FirstBanner_Medium_Ecpm_Show");
                    this.SmallBanner_L_Medium_Ecpm.Hide();

                    this.SmallBanner_L_Medium_Ecpm.Show();
                    this.SmallBanner_L_Medium_Ecpm.SetPosition(banner1_Position);
                }
                else
                {

                    loadBanner1();
                }




            }
            catch (Exception error)
            {
                PlayerInteractive_Logging.Log("PI_Small Banner Error: " + error);
            }
        }
        private void BindSmallBannerFirstMediumEcpm()
        {
            this.SmallBanner_L_Medium_Ecpm.OnBannerAdLoaded += () =>
            {
                PlayerInteractive_Logging.Log("PI_Banner view loaded an ad with response : "
                    + this.SmallBanner_L_Medium_Ecpm.GetResponseInfo());

                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {
                    smallBanner_Status = AdsLoadingStatus.Loaded;
                    PlayerInteractive_Logging.Log("PI_FirstSmallBanner_M_Loaded_Ecpm");
                    isSmallBannerLoadedFirst = true;
                });
            };

            this.SmallBanner_L_Medium_Ecpm.OnBannerAdLoadFailed += (LoadAdError error) =>
            {
                Debug.LogError("PI_Banner view failed to load an ad with error : " + error);

                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {
                    smallBanner_Status = AdsLoadingStatus.NoInventory;
                    PlayerInteractive_Logging.Log("PI_FirstSmallBanner_M_Fail_Ecpm");
                    isSmallBannerLoadedFirst = false;
                });
            };

        }
        /// <summary>
        /// 2nd BannerCode
        /// </summary>

        private void BindSmallBannerSecondMediumEcpm()
        {
            this.SmallBanner_R_Medium_Ecpm.OnBannerAdLoaded += () =>
            {
                PlayerInteractive_Logging.Log("PI_Banner view loaded an ad with response : "
                    + this.SmallBanner_R_Medium_Ecpm.GetResponseInfo());


                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {

                    small2ndBanner_Status = AdsLoadingStatus.Loaded;
                    PlayerInteractive_Logging.Log("PI_2ndSmallBanner_M_Loaded_Ecpm");
                    isSmallBannerLoadedSecond = true;

                });
            };

            this.SmallBanner_R_Medium_Ecpm.OnBannerAdLoadFailed += (LoadAdError error) =>
            {
                PlayerInteractive_Logging.Log("PI_Banner view failed to load an ad with error : " + error);

                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {

                    small2ndBanner_Status = AdsLoadingStatus.NoInventory;


                    isSmallBannerLoadedSecond = false;


                    PlayerInteractive_Logging.Log("PI_2ndSmallBanner_M_Failed_Ecpm");

                });
            };

        }
        public override bool checkSmallSecondBannedAD()
        {
            return isSmallBannerLoadedSecond;
        }
        public override void loadBanner2()
        {
            if (!PreferenceManager.GetAdsStatus() || checkSmallSecondBannedAD() || small2ndBanner_Status == AdsLoadingStatus.Loading)
            {
                return;
            }
            if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork | Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
            {

                this.SmallBanner_R_Medium_Ecpm = new BannerView(banner2, AdSize.Banner, banner2_Position);


                PlayerInteractive_Logging.Log("PI_SecondSmallBanner_M_Ecpm");
                BindSmallBannerSecondMediumEcpm();
                var request = new AdRequest();
                this.SmallBanner_R_Medium_Ecpm.LoadAd(request);
                this.SmallBanner_R_Medium_Ecpm.Hide();

            }
        }
        public override void hideBanner2()
        {
            if (this.SmallBanner_R_Medium_Ecpm != null)
            {
                this.SmallBanner_R_Medium_Ecpm.Hide();
                PlayerInteractive_Logging.Log("PI_Admob:smallBanner:Hide_M_Ecpm");
            }
        }
        public override void showBanner2()
        {
            hideBanner2();
            try
            {
                if (!PreferenceManager.GetAdsStatus() || !isAdmobInitialized)
                {
                    return;
                }


                if (SmallBanner_R_Medium_Ecpm != null)
                {

                    this.SmallBanner_R_Medium_Ecpm.Hide();

                    this.SmallBanner_R_Medium_Ecpm.Show();
                    this.SmallBanner_R_Medium_Ecpm.SetPosition(banner2_Position);
                    PlayerInteractive_Logging.Log("PI_SecondBanner_Medium__Ecpm_Show");
                }
                else
                {
                    loadBanner2();
                }


            }
            catch (Exception error)
            {
                PlayerInteractive_Logging.Log("PI_Small Banner Error: " + error);
            }
        }

        #endregion

        #region MediumBannerCodeBlocks
        public override bool checkBigBannerAD()
        {
            return isMediumBannerLoaded;
        }

        public override void loadBigBannerAD()
        {
            if (!PreferenceManager.GetAdsStatus() || checkBigBannerAD() || bigBanner_Status == AdsLoadingStatus.Loading)
            {
                return;
            }
            if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork | Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
            {

                PlayerInteractive_Logger.Pi_LogSender(Pi_AdmobEvents.Pi_MediumBanner_Load_MediumEcpm);
                this.MediumBannerMediumEcpm = new BannerView(bigBannerID, AdSize.MediumRectangle, AdPosition.BottomLeft);
                BindMediumBannerEvents_M_Ecpm();
                var request = new AdRequest();
                this.MediumBannerMediumEcpm.LoadAd(request);
                this.MediumBannerMediumEcpm.Hide();

            }

        }
        public override void showBigBannerAD(AdPosition pos)
        {
            try
            {
                if (!PreferenceManager.GetAdsStatus() || !isAdmobInitialized)
                {
                    return;
                }


                PlayerInteractive_Logger.Pi_LogSender(Pi_AdmobEvents.Pi_MediumBanner_Show_MediumEcpm);
                if (MediumBannerMediumEcpm != null)
                {

                    this.MediumBannerMediumEcpm.Hide();
                    this.MediumBannerMediumEcpm.Show();
                    this.MediumBannerMediumEcpm.SetPosition(pos);
                }

            }
            catch (Exception e)
            {

                PlayerInteractive_Logging.Log("PI_Medium Banner Error: " + e);
            }
        }
        public override void hideBigBanner()
        {

            if (this.MediumBannerMediumEcpm != null)
            {
                PlayerInteractive_Logging.Log("PI_Admob:mediumBanner:Hide_M_Ecpm");
                this.MediumBannerMediumEcpm.Hide();
            }

        }

        #endregion

        #region MediumBannerCallBack Handlers

        //MediumBanner2
        private void BindMediumBannerEvents_M_Ecpm()
        {
            this.MediumBannerMediumEcpm.OnBannerAdLoaded += () =>
            {
                PlayerInteractive_Logging.Log("PI_Banner view loaded an ad with response : "
                    + this.MediumBannerMediumEcpm.GetResponseInfo());

                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {

                    bigBanner_Status = AdsLoadingStatus.Loaded;
                    PlayerInteractive_Logger.Pi_LogSender(Pi_AdmobEvents.Pi_MediumBanner_Loaded_MediumEcpm);
                    loadBigBannerAD();
                    isMediumBannerLoaded = true;

                });
            };

            this.MediumBannerMediumEcpm.OnBannerAdLoadFailed += (LoadAdError error) =>
            {
                PlayerInteractive_Logging.Log("PI_Banner view failed to load an ad with error : " + error);

                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {
                    bigBanner_Status = AdsLoadingStatus.NotLoaded;
                    PlayerInteractive_Logger.Pi_LogSender(Pi_AdmobEvents.Pi_MediumBanner_NoInventory_MediumEcpm);

                    isMediumBannerLoaded = false;
                });
            };

            this.MediumBannerMediumEcpm.OnAdPaid += (AdValue adValue) =>
            {
                PlayerInteractive_Logging.Log(String.Format("PI_Banner view paid {0} {1}.",
                    adValue.Value,
                    adValue.CurrencyCode));
            };

            this.MediumBannerMediumEcpm.OnAdImpressionRecorded += () =>
            {
                PlayerInteractive_Logging.Log("PI_Banner view recorded an impression.");
            };

            this.MediumBannerMediumEcpm.OnAdClicked += () =>
            {
                PlayerInteractive_Logging.Log("PI_Banner view was clicked.");
            };

            this.MediumBannerMediumEcpm.OnAdFullScreenContentOpened += () =>
            {
                PlayerInteractive_Logging.Log("PI_Banner view full screen content opened.");

                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {

                    PlayerInteractive_Logger.Pi_LogSender(Pi_AdmobEvents.Pi_MediumBanner_Displayed_MediumEcpm);

                });
            };

            this.MediumBannerMediumEcpm.OnAdFullScreenContentClosed += () =>
            {
                PlayerInteractive_Logging.Log("PI_Banner view full screen content closed.");

                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {

                });
            };
        }


        #endregion

        #region IntersititialCodeBlock
        public override bool checkInterstitialAD()
        {
            if (this.Interstitial_High_Ecpm != null)
            {

                return this.Interstitial_High_Ecpm.CanShowAd();

            }
            else
            {
                return false;

            }
        }

        public override void showInterstitialAD()
        {
            if (!PreferenceManager.GetAdsStatus() || !isAdmobInitialized)
            {
                return;
            }

            if (Interstitial_HighEcpm)
            {
                if (this.Interstitial_High_Ecpm != null)
                {
                    if (this.Interstitial_High_Ecpm.CanShowAd())
                    {
                        if (Pi_appOpenHandler.Instance)
                            Pi_appOpenHandler.Instance.AdShowing = true;

                        PlayerInteractive_Logger.Pi_LogSender(Pi_AdmobEvents.Pi_Interstitial_WillDisplay_High_Ecpm);
                        GameAnalytics.NewAdEvent(GAAdAction.Show,GAAdType.Interstitial,"Admob","Admob_Interstitial");
                        this.Interstitial_High_Ecpm.Show();

                    }
                }
            }
            else if (UnityAds)
            {
                if (Pi_appOpenHandler.Instance)
                    Pi_appOpenHandler.Instance.AdShowing = true;

                PlayerInteractive_Logger.Pi_LogEvent("PI_unity_interstitial_loaded");
                GameAnalytics.NewAdEvent(GAAdAction.Show,GAAdType.Interstitial,"Unity","Unity_Interstitial");
             //   Advertisement.Show(Unity_Interstitial_ID, this);
            }
        }
        public override void loadInterstitialAD()
        {

            if (!isAdmobInitialized || checkInterstitialAD() /*|| interstitial_Status == AdsLoadingStatus.Loading*/ || !PreferenceManager.GetAdsStatus())
            {

                return;
            }
            if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork | Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
            {
                if (Interstitial_HighEcpm)
                {
                    Int_Unity += loadInterstitialAD;
                    PlayerInteractive_Logger.Pi_LogSender(Pi_AdmobEvents.Pi_LoadInterstitial_High_Ecpm);
                    var request = new AdRequest();
                    interstitial_Status = AdsLoadingStatus.Loading;

                    InterstitialAd.Load(interstitialID, request, (InterstitialAd ad, LoadAdError error) =>
                    {
                        if (error != null)
                        {
                            PlayerInteractive_Logger.Pi_LogEvent("PI_Interstitial ad failed to load an ad with error : " + error);

                            return;
                        }

                        if (ad == null)
                        {
                            PlayerInteractive_Logger.Pi_LogEvent("PI_Unexpected error: Interstitial load event fired with null ad and null error.");
                            return;
                        }

                        PlayerInteractive_Logger.Pi_LogEvent("PI_Interstitial ad loaded with response : " + ad.GetResponseInfo());
                        this.Interstitial_High_Ecpm = ad;

                        BindIntersititialHighEcpmEvents();
                    });
                }
                else if (UnityAds)
                {
                    PlayerInteractive_Logger.Pi_LogEvent("PI_Load_Unity_Int");
                  //  Advertisement.Load(Unity_Interstitial_ID, this);
                }
            }
        }

        #endregion

        #region IntersititialEventCallBacks
        //HighEcpmEvents
        private void BindIntersititialHighEcpmEvents()
        {
            this.Interstitial_High_Ecpm.OnAdPaid += (AdValue adValue) =>
            {
                PlayerInteractive_Logger.Pi_LogEvent(String.Format("PI_Interstitial ad paid {0} {1}.",
                    adValue.Value,
                    adValue.CurrencyCode));
            };
            this.Interstitial_High_Ecpm.OnAdImpressionRecorded += () =>
            {
                PlayerInteractive_Logger.Pi_LogEvent("PI_Interstitial ad recorded an impression.");

                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {
                    if (Interstitial_HighEcpm)
                    {
                        interstitial_Status = AdsLoadingStatus.Loaded;

                        PlayerInteractive_Logger.Pi_LogSender(Pi_AdmobEvents.Pi_Interstitial_Loaded_High_Ecpm);
                        Int_Unity -= loadInterstitialAD;
                        Interstitial_HighEcpm = true;
                        UnityAds = false;
                    }

                });
            };
            this.Interstitial_High_Ecpm.OnAdClicked += () =>
            {
                PlayerInteractive_Logger.Pi_LogEvent("PI_Interstitial ad was clicked.");
            };
            this.Interstitial_High_Ecpm.OnAdFullScreenContentOpened += () =>
            {
                PlayerInteractive_Logger.Pi_LogEvent("PI_Interstitial ad full screen content opened.");

                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {
                    interstitial_Status = AdsLoadingStatus.NotLoaded;

                    if (Interstitial_HighEcpm)
                    {
                        PlayerInteractive_Logger.Pi_LogSender(Pi_AdmobEvents.Pi_Interstitial_Displayed_High_Ecpm);
                        Int_Unity -= loadInterstitialAD;
                    }
                });
            };
            this.Interstitial_High_Ecpm.OnAdFullScreenContentClosed += () =>
            {
                PlayerInteractive_Logger.Pi_LogEvent("PI_Interstitial ad full screen content closed.");

                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {
                    PlayerInteractive_Logger.Pi_LogSender(Pi_AdmobEvents.Pi_Interstitial_Closed_High_Ecpm);
                    interstitial_Status = AdsLoadingStatus.NotLoaded;
                    if (Interstitial_HighEcpm)
                    {
                        this.Interstitial_High_Ecpm.Destroy();
                        Int_Unity -= loadInterstitialAD;
                        Interstitial_HighEcpm = true;
                        UnityAds = false;
                    }
                });
            };
            this.Interstitial_High_Ecpm.OnAdFullScreenContentFailed += (AdError error) =>
            {
                PlayerInteractive_Logger.Pi_LogEvent("PI_Interstitial ad failed to open full screen content with error : "
                    + error);

                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {
                    if (Interstitial_HighEcpm)
                    {
                        interstitial_Status = AdsLoadingStatus.NoInventory;
                        PlayerInteractive_Logger.Pi_LogSender(Pi_AdmobEvents.Pi_Interstitial_NoInventory_High_Ecpm);
                        Interstitial_HighEcpm = false;
                        UnityAds = true;
                        if (Int_Unity != null)
                            Int_Unity();
                    }
                });
            };
        }


        #endregion

        #region RewardedVideoCodeBlock
        public override void loadRewardVideoAD()
        {
            if (!isAdmobInitialized || checkRewardAD() || rewardADs_Status == AdsLoadingStatus.Loading)
            {
                return;
            }
            if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork | Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
            {
                if (RewardVideo_High_Ecpm)
                {
                    RewardVideo_Unity += loadRewardVideoAD;
                    PlayerInteractive_Logger.Pi_LogSender(Pi_AdmobEvents.Pi_LoadRewardedVideo_High_Ecpm);
                    var request = new AdRequest();
                    rewardADs_Status = AdsLoadingStatus.Loading;
                    RewardedAd.Load(rewardedVideo, request, (RewardedAd ad, LoadAdError error) =>
                    {
                        if (error != null)
                        {
                            RewardVideo_High_Ecpm = false;

                            UnityRewarded = true;
                            if (RewardVideo_Unity != null)
                            {
                                RewardVideo_Unity();
                            }
                            PlayerInteractive_Logging.Log("PI_Rewarded ad failed to load an ad with error : " + error);
                            return;
                        }

                        if (ad == null)
                        {
                            PlayerInteractive_Logging.Log("PI_Unexpected error: Rewarded load event fired with null ad and null error.");
                            return;
                        }

                        PlayerInteractive_Logging.Log("PI_Rewarded ad loaded with response : " + ad.GetResponseInfo());
                        this.rewardBasedVideo = ad;
                        BindRewardedEvents_H_Ecpm();
                    });
                }
                else if (UnityRewarded)
                {
                    //Advertisement.Load(Unity_RewardedVideo);
                }
            }
        }
        public override bool checkRewardAD()
        {
            if (this.rewardBasedVideo != null)
                return this.rewardBasedVideo.CanShowAd();
            else
                return false;
        }
        public override void showRewardVideo(RewardUserDelegate _delegate)
        {
            if (RewardVideo_High_Ecpm)
            {
                NotifyReward = _delegate;
                PlayerInteractive_Logger.Pi_LogSender(Pi_AdmobEvents.Pi_ShowRewardedVideo_High_Ecpm);

                if (rewardBasedVideo != null)
                {
                    //Debug.Log(" First Statment pass ");
                    if (!this.rewardBasedVideo.CanShowAd())
                    {
                        return;
                    }
                    // Debug.Log(" Second Statment pass ");

                    if (Pi_appOpenHandler.Instance)
                        Pi_appOpenHandler.Instance.AdShowing = true;

                    PlayerInteractive_Logger.Pi_LogSender(Pi_AdmobEvents.Pi_RewardedVideo_WillDisplay_High_Ecpm);

                    this.rewardBasedVideo.Show((Reward reward) =>
                    {

                        PlayerInteractive_Logging.Log(String.Format("PI_Rewarded ad granted a reward: {0} {1}",
                                                reward.Amount,
                                                reward.Type));
                    });
                }
                else if (UnityRewarded)
                {

                    if (Pi_appOpenHandler.Instance)
                        Pi_appOpenHandler.Instance.AdShowing = true;

                    Debug.Log("PI_Unity Rewarded Second Statment pass ");
                    NotifyReward = _delegate;

                    //Advertisement.Show(Unity_RewardedVideo, this);
                }
            }
            else if (UnityRewarded)
            {
                if (Pi_appOpenHandler.Instance)
                    Pi_appOpenHandler.Instance.AdShowing = true;

                NotifyReward = _delegate;

              //  Advertisement.Show(Unity_RewardedVideo, this);
            }
        }

        #endregion

        #region RewardedVideoEvents
        //***** Rewarded Events *****//
        private void BindRewardedEvents_H_Ecpm()
        {
            rewardBasedVideo.OnAdPaid += (AdValue adValue) =>
            {

            };

            rewardBasedVideo.OnAdImpressionRecorded += () =>
            {
                PlayerInteractive_Logging.Log("PI_Rewarded ad recorded an impression.");

                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {
                    if (RewardVideo_High_Ecpm)
                    {
                        RewardVideo_Unity -= loadRewardVideoAD;
                        rewardADs_Status = AdsLoadingStatus.Loaded;
                        UnityRewarded = false;
                        PlayerInteractive_Logger.Pi_LogSender(Pi_AdmobEvents.Pi_RewardedVideo_Loaded_High_Ecpm);

                    }
                });
            };

            rewardBasedVideo.OnAdClicked += () =>
            {
                PlayerInteractive_Logging.Log("PI_Rewarded ad was clicked.");
            };

            rewardBasedVideo.OnAdFullScreenContentOpened += () =>
            {
                PlayerInteractive_Logging.Log("PI_Rewarded ad full screen content opened.");

                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {
                    if (RewardVideo_High_Ecpm)
                    {
                        RewardVideo_Unity -= loadRewardVideoAD;
                        rewardADs_Status = AdsLoadingStatus.NotLoaded;

                        if (NotifyReward != null)
                            NotifyReward();

                        PlayerInteractive_Logger.Pi_LogSender(Pi_AdmobEvents.Pi_RewardedVideo_Displayed_High_Ecpm);
                    }
                });
            };

            rewardBasedVideo.OnAdFullScreenContentClosed += () =>
            {
                PlayerInteractive_Logging.Log("PI_Rewarded ad full screen content closed.");

                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {
                    if (RewardVideo_High_Ecpm)
                    {
                        RewardVideo_Unity -= loadRewardVideoAD;
                        rewardADs_Status = AdsLoadingStatus.NotLoaded;
                        PlayerInteractive_Logging.Log("PI_ Admob:rad:Closed_H_Ecpm");
                        PlayerInteractive_Logger.Pi_LogSender(Pi_AdmobEvents.Pi_RewardedVideo_Closed_High_Ecpm);
                        loadRewardVideoAD();
                    }
                });
            };

            rewardBasedVideo.OnAdFullScreenContentFailed += (AdError error) =>
            {
                PlayerInteractive_Logging.Log("PI_Rewarded ad failed to open full screen content with error : " + error);

                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {
                    if (RewardVideo_High_Ecpm)
                    {
                        rewardADs_Status = AdsLoadingStatus.NoInventory;
                        PlayerInteractive_Logger.Pi_LogSender(Pi_AdmobEvents.Pi_RewardedVideo_NoInventory_High_Ecpm);
                        RewardVideo_High_Ecpm = false;



                        UnityRewarded = true;
                        if (RewardVideo_Unity != null)
                        {
                            RewardVideo_Unity();
                        }
                    }
                });
            };
        }

        #endregion

        #region RewardedInterstial
        public override void loadRewardInt()
        {
            if (!isAdmobInitialized || checkRewardIntAD() || rewardInterstitial_Status == AdsLoadingStatus.Loading)
            {
                return;
            }

            if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork || Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
            {
                PlayerInteractive_Logger.Pi_LogSender(Pi_AdmobEvents.Pi_LoadRewardedInterstitialAd_H_ECPM);
                var request = new AdRequest();
                rewardInterstitial_Status = AdsLoadingStatus.Loading;

                RewardedInterstitialAd.Load(rewardInterstitial, request, (RewardedInterstitialAd ad, LoadAdError error) =>
                {
                    if (error != null)
                    {
                        PlayerInteractive_Logging.Log("PI_Rewarded interstitial ad failed to load an ad with error : " + error);
                        return;
                    }

                    if (ad == null)
                    {
                        PlayerInteractive_Logging.Log("PI_Unexpected error: Rewarded interstitial load event fired with null ad and null error.");
                        return;
                    }

                    PlayerInteractive_Logging.Log("PI_Rewarded interstitial ad loaded with response : " + ad.GetResponseInfo());
                    rewardedInterstitialAd = ad;
                    RegisterEventHandlers(ad);
                });
            }
        }

        public override void showRewardInt(RewardUserDelegate _delegate)
        {
            NotifyReward = _delegate;
            PlayerInteractive_Logger.Pi_LogSender(Pi_AdmobEvents.Pi_ShowRewardedInterstitialAd_H_ECPM);

            if (this.rewardedInterstitialAd != null)
            {
                if (rewardedInterstitialHighECPMLoaded)
                {
                    if (Pi_appOpenHandler.Instance)
                        Pi_appOpenHandler.Instance.AdShowing = true;

                    this.rewardedInterstitialAd.Show(userEarnedRewardCallback);
                }
            }
        }

        private void userEarnedRewardCallback(Reward reward)
        {

        }


        public override bool checkRewardIntAD()
        {
            if (this.rewardedInterstitialAd != null)
            {
                if (rewardedInterstitialHighECPMLoaded)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region RewardedInterstitialCallbackHandler

        ///////// Rewarded Interstitial High ECPM Callbacks //////////
        protected void RegisterEventHandlers(RewardedInterstitialAd ad)
        {
            rewardedInterstitialHighECPMLoaded = true;

            ad.OnAdPaid += (AdValue adValue) =>
            {

            };
            ad.OnAdImpressionRecorded += () =>
            {
                PlayerInteractive_Logging.Log("PI_Rewarded interstitial ad recorded an impression.");
            };
            ad.OnAdClicked += () =>
            {
                PlayerInteractive_Logging.Log("PI_Rewarded interstitial ad was clicked.");
            };
            ad.OnAdFullScreenContentOpened += () =>
            {
                PlayerInteractive_Logging.Log("PI_Rewarded interstitial ad has presented.");
                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {
                    {
                        rewardInterstitial_Status = AdsLoadingStatus.NotLoaded;

                        PlayerInteractive_Logger.Pi_LogSender(Pi_AdmobEvents.Pi_RewardedInterstitialAdDisplayed_H_ECPM);
                    }
                });
            };
            ad.OnAdFullScreenContentClosed += () =>
            {
                PlayerInteractive_Logging.Log("PI_Rewarded interstitial ad has dismissed presentation.");
                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {
                    {
                        rewardedInterstitialHighECPMLoaded = false;
                        rewardInterstitial_Status = AdsLoadingStatus.NotLoaded;
                        PlayerInteractive_Logger.Pi_LogSender(Pi_AdmobEvents.Pi_RewardedInterstitialAdClosed_H_ECPM);
                        NotifyReward();
                        loadRewardInt();
                    }
                });
            };
            ad.OnAdFullScreenContentFailed += (AdError error) =>
            {
                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {

                    {
                        rewardInterstitial_Status = AdsLoadingStatus.NotLoaded;
                        PlayerInteractive_Logging.Log("PI_Admob:riad:FailedToShow:HCPM");
                    }
                });
            };
        }

        #endregion

        #region UnityCallBack
        public void OnUnityAdsAdLoaded(string adUnitId)
        {
            if (adUnitId == Unity_Interstitial_ID)
            {
                interstitial_Status = AdsLoadingStatus.Loaded;
                UnityAds = true;
                Interstitial_HighEcpm = false;


            }
            else if (adUnitId == Unity_RewardedVideo)
            {
                rewardADs_Status = AdsLoadingStatus.Loaded;
                RewardVideo_High_Ecpm = false;
                UnityRewarded = true;
            }
        }

        public void OnUnityAdsFailedToLoad(string adUnitId, string message)
        {
            Debug.Log($"PI_Error loading Ad Unit: {adUnitId} - {ToString()} - {message}");
            if (adUnitId == Unity_Interstitial_ID)
            {
                interstitial_Status = AdsLoadingStatus.Loaded;
                UnityAds = true;
                Interstitial_HighEcpm = false;
            }
            else if (adUnitId == Unity_RewardedVideo)
            {
                rewardADs_Status = AdsLoadingStatus.Loaded;
                RewardVideo_High_Ecpm = false;
                UnityRewarded = true;

            }
            // Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
        }

        public void OnUnityAdsShowFailure(string adUnitId,  string message)
        {
            Debug.Log($"PI_Error showing Ad Unit {adUnitId}: {ToString()} - {message}");
            if (adUnitId == Unity_Interstitial_ID)
            {
                interstitial_Status = AdsLoadingStatus.Loaded;
                UnityAds = false;
                Interstitial_HighEcpm = true;


            }
            else if (adUnitId == Unity_RewardedVideo)
            {
                rewardADs_Status = AdsLoadingStatus.Loaded;
                RewardVideo_High_Ecpm = true;

                UnityRewarded = false;

            }
            // Optionally execute code if the Ad Unit fails to show, such as loading another ad.
        }

        public void OnUnityAdsShowStart(string adUnitId) { }
        public void OnUnityAdsShowClick(string adUnitId) { }
        public void OnUnityAdsShowComplete(string adUnitId)
        {


            //  ADmobInterstial = true;
            if (adUnitId == Unity_Interstitial_ID)
            {
                interstitial_Status = AdsLoadingStatus.NotLoaded;
                Interstitial_HighEcpm = true;

                UnityAds = false;

            }
            else if (adUnitId.Equals(Unity_RewardedVideo))
            {

                // Grant a reward.
                if (adUnitId == Unity_RewardedVideo)
                {
                    RewardVideo_High_Ecpm = true;

                    UnityRewarded = false;
                    NotifyReward();
                }
                // Load another ad:
                //Advertisement.Load(Unity_RewardedVideo, this);
            }
        }

        #endregion
    }
}
