using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using PlayerInteractive_Mediation;
using UnityEngine;

public class RectBannerHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnEnable() 
    {
        if (FindObjectOfType<Pi_AdsCall>())
        {
            FindObjectOfType<Pi_AdsCall>().showBigBannerAD(AdPosition.BottomLeft);
            FindObjectOfType<Pi_AdsCall>().hideBanner1();
        }
    }

    public void OnDisable() 
    {
        if (FindObjectOfType<Pi_AdsCall>())
        {
            FindObjectOfType<Pi_AdsCall>().hideBigBanner();
            FindObjectOfType<Pi_AdsCall>().showBanner1();
        }
    }
}
