
using UnityEngine;

namespace PlayerInteractive_Mediation
{
    public static class PlayerInteractive_Logging
    {
        static public void Log(object message)
        {
            if (!PlayerInteractive_Admob.Logs)
                UnityEngine.Debug.Log(message);
        }
    }
    public class PlayerInteractive_Logger : MonoBehaviour
    {
        public static void Pi_LogSender(Pi_AdmobEvents Log)
        {
            switch (Log)
            {
                case Pi_AdmobEvents.Pi_Initializing:

                    Pi_LogEvent("Pi_AdmobInitializing");
                    break;
                case Pi_AdmobEvents.Pi_Initialized:

                    Pi_LogEvent("Pi_AdmobInitialized");
                    break;
                case Pi_AdmobEvents.Pi_LoadInterstitial_High_Ecpm:
                    Pi_LogEvent("Pi_Admob_iAd_LoadRequest_H_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_LoadInterstitial_Medium_Ecpm:
                    Pi_LogEvent("Pi_Admob_iAd_LoadRequest_M_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_LoadIntersitiatial_Low_Ecpm:
                    Pi_LogEvent("Pi_Admob_iAd_LoadRequest_L_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_ShowInterstitial_High_Ecpm:
                    Pi_LogEvent("Pi_Admob_iAd_ShowCall_H_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_ShowInterstitial_Medium_Ecpm:
                    Pi_LogEvent("Pi_Admob_iAd_ShowCall_M_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_ShowIntersititial_Low_Ecpm:
                    Pi_LogEvent("Pi_Admob_iAd_ShowCall_L_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_Interstitial_WillDisplay_High_Ecpm:
                    Pi_LogEvent("Pi_Admob_iAd_WillDisplay_H_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_Interstitial_WillDisplay_Medium_Ecpm:
                    Pi_LogEvent("Pi_Admob_iAd_WillDisplay_M_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_Interstitial_WillDisplay_Low_Ecpm:
                    Pi_LogEvent("Pi_Admob_iAd_WillDisplay_L_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_Interstitial_Displayed_High_Ecpm:
                    Pi_LogEvent("Pi_Admob_iAd_Displayed_H_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_Interstilial_Displayed_Medium_Ecpm:
                    Pi_LogEvent("Pi_Admob_iAd_Displayed_M_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_Interstital_Displayed_Low_Ecpm:
                    Pi_LogEvent("Pi_Admob_iAd_Displayed_L_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_Interstitial_NoInventory_High_Ecpm:
                    Pi_LogEvent("Pi_Admob_iAd_NoInventery_H_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_Interstilial_NoInventory_Medium_Ecpm:
                    Pi_LogEvent("Pi_Admob_iAd_NoInventery_M_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_Interstitial_NoInventory_Low_Ecpm:
                    Pi_LogEvent("Pi_Admob_iAd_NoInventery_L_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_Interstitial_Closed_High_Ecpm:
                    Pi_LogEvent("Pi_Admob_iAd_Closed_H_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_Interstitial_Closed_Medium_Ecpm:
                    Pi_LogEvent("Pi_Admob_iAd_Closed_M_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_Interstitial_Closed_Low_Ecpm:
                    Pi_LogEvent("Pi_Admob_iAd_Closed_L_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_Interstitial_Loaded_High_Ecpm:
                    Pi_LogEvent("Pi_Admob_iAd_Loaded_H_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_Interstitial_Loaded_Medium_Ecpm:
                    Pi_LogEvent("Pi_Admob_iAd_Loaded_M_Ecpm");

                    break;
                case Pi_AdmobEvents.Pi_Interstitial_Loaded_Low_Ecpm:
                    Pi_LogEvent("Pi_Admob_iAd_Loaded_L_Ecpm");

                    break;
                case Pi_AdmobEvents.Pi_SmallBanner_Load_HighEcpm:
                    Pi_LogEvent("Pi_Admob_AB_LoadRequest_H_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_SmallBanner_Load_MediumEcpm:
                    Pi_LogEvent("Pi_Admob_AB_LoadRequest_M_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_SmallBanner_Load_LowEcpm:
                    Pi_LogEvent("Pi_Admob_AB_LoadRequest_L_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_SmallBanner_Show_HighEcpm:
                    Pi_LogEvent("Pi_Admob_AB_ShowCall_H_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_SmallBanner_Show_MediumEcpm:
                    Pi_LogEvent("Pi_Admob_AB_ShowCall_M_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_SmallBanner_Show_LowEcpm:
                    Pi_LogEvent("Pi_Admob_AB_ShowCall_L_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_SmallBanner_Loaded_HighEcpm:
                    Pi_LogEvent("Pi_Admob_AB_Loaded_H_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_SmallBannner_Loaded_MediumEcpm:
                    Pi_LogEvent("Pi_Admob_AB_Loaded_M_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_SmallBanner_Loaded_LowEcpm:
                    Pi_LogEvent("Pi_Admob_AB_Loaded_L_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_SmallBanner_NoInventory_HighEcpm:
                    Pi_LogEvent("Pi_Admob_AB_NoInventory_H_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_SmallBanner_NoInventory_MediumEcpm:
                    Pi_LogEvent("Pi_Admob_AB_NoInventory_M_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_SmallBanner_NoInventory_LowEcpm:
                    Pi_LogEvent("Pi_Admob_AB_NoInventory_L_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_SmallBanner_Displayed_HighEcpm:
                    Pi_LogEvent("Pi_Admob_AB_Displayed_H_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_SmallBanner_Displayed_MediumEcpm:
                    Pi_LogEvent("Pi_Admob_AB_Displayed_M_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_SmallBanner_Displayed_LowEcpm:
                    Pi_LogEvent("Pi_Admob_AB_Displayed_L_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_MediumBanner_Load_HighEcpm:
                    Pi_LogEvent("Pi_Admob_MB_LoadRequest_H_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_MediumBanner_Load_MediumEcpm:
                    Pi_LogEvent("Pi_Admob_MB_LoadRequest_M_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_MediumBanner_Load_LowEcpm:
                    Pi_LogEvent("Pi_Admob_MB_LoadRequest_L_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_MediumBanner_Show_HighEcpm:
                    Pi_LogEvent("Pi_Admob_MB_ShowCall_H_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_MediumBanner_Show_MediumEcpm:
                    Pi_LogEvent("Pi_Admob_MB_ShowCall_M_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_MediumBanner_Show_LowEcpm:
                    Pi_LogEvent("Pi_Admob_MB_ShowCall_L_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_MediumBanner_Loaded_HighEcpm:
                    Pi_LogEvent("Pi_Admob_MB_Loaded_H_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_MediumBanner_Loaded_MediumEcpm:
                    Pi_LogEvent("Pi_Admob_MB_Loaded_M_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_MediumBanner_Loaded_LowEcpm:
                    Pi_LogEvent("Pi_Admob_MB_Loaded_L_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_MediumBanner_NoInventory_HighEcpm:
                    Pi_LogEvent("Pi_Admob_MB_NoInventory_H_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_MediumBanner_NoInventory_MediumEcpm:
                    Pi_LogEvent("Pi_Admob_MB_NoInventory_M_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_MediumBanner_NoInventory_LowEcpm:
                    Pi_LogEvent("Pi_Admob_MB_NoInventory_L_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_MediumBanner_Displayed_HighEcpm:
                    Pi_LogEvent("Pi_Admob_MB_Displayed_H_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_MediumBanner_Displayed_MediumEcpm:
                    Pi_LogEvent("Pi_Admob_MB_Displayed_M_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_MediumBanner_Displayed_LowEcpm:
                    Pi_LogEvent("Pi_Admob_MB_Displayed_L_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_LoadRewardedVideo_High_Ecpm:
                    Pi_LogEvent("Pi_Admob_rAd_LoadRequest_H_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_LoadRewardedVideo_Medium_Ecpm:
                    Pi_LogEvent("Pi_Admob_rAd_LoadRequest_M_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_LoadRewardedVideo_Low_Ecpm:
                    Pi_LogEvent("Pi_Admob_rAd_LoadRequest_L_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_ShowRewardedVideo_High_Ecpm:
                    Pi_LogEvent("Pi_Admob_rAd_ShowCall_H_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_ShowRewardedVideo_Medium_Ecpm:
                    Pi_LogEvent("Pi_Admob_rAd_ShowCall_M_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_ShowRewardedVideo_Low_Ecpm:
                    Pi_LogEvent("Pi_Admob_rAd_ShowCall_L_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_RewardedVideo_WillDisplay_High_Ecpm:
                    Pi_LogEvent("Pi_Admob_rAd_WillDisplay_H_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_RewardedVideo_WillDisplay_Medium_Ecpm:
                    Pi_LogEvent("Pi_Admob_rAd_WillDisplay_M_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_RewardedVideo_WillDisplay_Low_Ecpm:
                    Pi_LogEvent("Pi_Admob_rAd_WillDisplay_L_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_RewardedVideo_Displayed_High_Ecpm:
                    Pi_LogEvent("Pi_Admob_rAd_Displayed_H_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_RewardedVideo_Displayed_Medium_Ecpm:
                    Pi_LogEvent("Pi_Admob_rAd_Displayed_M_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_RewardedVideo_Displayed_Low_Ecpm:
                    Pi_LogEvent("Pi_Admob_rAd_Displayed_L_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_RewardedVideo_NoInventory_High_Ecpm:
                    Pi_LogEvent("Pi_Admob_rAd_NoInventery_H_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_RewardedVideo_NoInventory_Medium_Ecpm:
                    Pi_LogEvent("Pi_Admob_rAd_NoInventery_M_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_RewardedVideo_NoInventory_Low_Ecpm:
                    Pi_LogEvent("Pi_Admob_rAd_NoInventery_L_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_RewardedVideo_Closed_High_Ecpm:
                    Pi_LogEvent("Pi_Admob_rAd_Closed_H_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_RewardedVideo_Closed_Medium_Ecpm:
                    Pi_LogEvent("Pi_Admob_rAd_Closed_M_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_RewardedVideo_Closed_Low_Ecpm:
                    Pi_LogEvent("Pi_Admob_rAd_Closed_L_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_RewardedVideo_Loaded_High_Ecpm:
                    Pi_LogEvent("Pi_Admob_rAd_Loaded_H_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_RewardedVideo_Loaded_Medium_Ecpm:
                    Pi_LogEvent("Pi_Admob_rAd_Loaded_M_Ecpm");

                    break;
                case Pi_AdmobEvents.Pi_RewardedVideo_Loaded_Low_Ecpm:
                    Pi_LogEvent("Pi_Admob_rAd_Loaded_L_Ecpm");

                    break;

                case Pi_AdmobEvents.Pi_LoadRewardedInterstitialAd_H_ECPM:
                    Pi_LogEvent("Pi_Admob_riAd_LoadRequest_H_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_LoadRewardedInterstitialAd_M_ECPM:
                    Pi_LogEvent("Pi_Admob_riAd_LoadRequest_M_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_LoadRewardedInterstitialAd_L_ECPM:
                    Pi_LogEvent("Pi_Admob_riAd_LoadRequest_L_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_ShowRewardedInterstitialAd_H_ECPM:
                    Pi_LogEvent("Pi_Admob_riAd_ShowCall_H_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_ShowRewardedInterstitialAd_M_ECPM:
                    Pi_LogEvent("Pi_Admob_riAd_ShowCall_M_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_ShowRewardedInterstitialAd_L_ECPM:
                    Pi_LogEvent("Pi_Admob_riAd_ShowCall_L_Ecpm");
                    break;

                case Pi_AdmobEvents.Pi_RewardedInterstitialAdDisplayed_H_ECPM:
                    Pi_LogEvent("Pi_Admob_riAd_Displayed_H_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_RewardedInterstitialAdDisplayed_M_ECPM:
                    Pi_LogEvent("Pi_Admob_riAd_Displayed_M_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_RewardedInterstitialAdDisplayed_L_ECPM:
                    Pi_LogEvent("Pi_Admob_riAd_Displayed_L_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_RewardedInterstitialNoInventory_H_ECPM:
                    Pi_LogEvent("Pi_Admob_riAd_NoInventery_H_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_RewardedInterstitialNoInventory_M_ECPM:
                    Pi_LogEvent("Pi_Admob_riAd_NoInventery_M_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_RewardedInterstitialNoInventory_L_ECPM:
                    Pi_LogEvent("Pi_Admob_riAd_NoInventery_L_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_RewardedInterstitialAdClosed_H_ECPM:
                    Pi_LogEvent("Pi_Admob_riAd_Closed_H_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_RewardedInterstitialAdClosed_M_ECPM:
                    Pi_LogEvent("Pi_Admob_riAd_Closed_M_Ecpm");
                    break;
                case Pi_AdmobEvents.Pi_RewardedInterstitialAdClosed_L_ECPM:
                    Pi_LogEvent("Pi_Admob_riAd_Closed_L_Ecpm");
                    break;



                default:
                    break;
            }
        }


        public static void Pi_LogEvent(string log)
        {
            PlayerInteractive_Logging.Log("Pi_ " + log);
            Firebase.Analytics.FirebaseAnalytics.LogEvent(log);
            //FB.LogAppEvent(eventName);
        }

        public static void Pi_MissionOrLevelStartedEventLog(string GameMode, int LevelNumber)
        {
            PlayerInteractive_Logging.Log("Pi_ LevelStarted_" + GameMode + "_Level_" + LevelNumber.ToString());

            Firebase.Analytics.FirebaseAnalytics.LogEvent("Pi_LevelStarted_" + GameMode + "_Level_" + LevelNumber.ToString());

        }
        public static void Pi_MissionOrLevelFailEventLog(string GameMode, int LevelNumber)
        {
            PlayerInteractive_Logging.Log("Pi_ LevelFail_" + GameMode + "_Level_Number_" + LevelNumber.ToString());

            Firebase.Analytics.FirebaseAnalytics.LogEvent("Pi_LevelFail_" + GameMode + "_Level_" + LevelNumber.ToString());

        }

        public static void Pi_MissionOrLevelCompletedEventLog(string GameMode, int LevelNumber)
        {
            PlayerInteractive_Logging.Log("Pi_ LevelComplete_" + GameMode + "_Level_" + LevelNumber);

            Firebase.Analytics.FirebaseAnalytics.LogEvent("Pi_LevelComplete_" + GameMode + "_Level_" + LevelNumber);


        }
    }

}
