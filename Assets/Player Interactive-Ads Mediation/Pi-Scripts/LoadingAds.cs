using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerInteractive_Mediation
{
    public class LoadingAds : MonoBehaviour
    {
        public Pi_AdsCall handler;

        public static Pi_AdsCall.AfterLoading Notify;

        //public bool MediumBanner;
        //public bool SmallBanner;
        public bool IsTimeScaled;
        private void OnEnable()
        {

            Invoke(nameof(ShowInt), .2f);

        }

        void ShowInt()
        {

            handler.showInterstitialAD();

            {
                Invoke(nameof(ShowNextScreen), .1f);
            }

        }



        void ShowNextScreen()
        {

            if (IsTimeScaled)
            {
                this.gameObject.SetActive(false);

            }
            else
            {
                Invoke(nameof(DisableLoading), .1f);
            }

            if (Notify != null)
            {
                Notify();

            }
        }
        void DisableLoading()
        {
            this.gameObject.SetActive(false);
        }
    }
}
