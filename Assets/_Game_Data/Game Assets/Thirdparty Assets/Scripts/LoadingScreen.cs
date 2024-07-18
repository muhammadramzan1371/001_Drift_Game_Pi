using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using GoogleMobileAds.Api;
using PlayerInteractive_Mediation;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider loadingSlider;
    public GameObject AdBreakPanel;
    void OnEnable()
    {
        loadingSlider.value = 0;
        if (FindObjectOfType<Pi_AdsCall>())
        {
            FindObjectOfType<Pi_AdsCall>().showBigBannerAD(AdPosition.BottomLeft);

            if (PrefsManager.GetInterInt()!=5)
            {
                FindObjectOfType<Pi_AdsCall>().loadInterstitialAD();
            }
        }

        oneTime = false;Single=false;
    }

    private bool oneTime = false,Single=false;
    
    // Update is called once per frame
    async void Update()
    {
        if (loadingSlider.value<1)
        {
            loadingSlider.value += 0.23f * Time.deltaTime;
            if (loadingSlider.value >= 0.7f && !oneTime)
            {
                AdBreakPanel.SetActive(true);
                await Task.Delay(1000);
                if (FindObjectOfType<Pi_AdsCall>())
                {
                    FindObjectOfType<Pi_AdsCall>().hideBigBanner();
                    FindObjectOfType<Pi_AdsCall>().showInterstitialAD();
                    PrefsManager.SetInterInt(1);
                    oneTime = true;
                }
                AdBreakPanel.SetActive(false);
            }
        }
        else
        {
            if (!Single)
            {
                Single = true;
                gameObject.SetActive(false);
            }
        }
    }
}
