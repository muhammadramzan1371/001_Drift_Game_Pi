using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlayerInteractive_Mediation;
using UnityEngine;
using UnityEngine.UI;

public class Countdowntimer : MonoBehaviour
{
    private float currentime = 0f;
    private float startingtime = 10f;
    private bool singlecall = false;


    [SerializeField] private Text countdountext;

    void OnEnable()
    {
        currentime = startingtime;
        singlecall = false;
        LoadInter();
    }

    private async void Update()
    {
        currentime -= 1 * Time.deltaTime;
        countdountext.text = currentime.ToString("0 0");

        if (currentime <= 0 && !singlecall)
        {
            singlecall = true;
            currentime = 0;
            UiManagerObject.instance.AdPanelOff();
            UiManagerObject.instance.AdBrakepanel.SetActive(true);
            await Task.Delay(1500);
            UiManagerObject.instance.AdBrakepanel.SetActive(false);
            UiManagerObject.instance.OnAnyPannelAppear();
            ShowInter();
            Invoke("ShowPanel", 90f);
        }
    }


    public void ShowPanel()
    {
        currentime += 10;
        UiManagerObject.instance.AdPanelOnFirstTime();
    }


    public void ShowInter()
    {
        if (FindObjectOfType<Pi_AdsCall>())
        {
            FindObjectOfType<Pi_AdsCall>().showInterstitialAD();
            PrefsManager.SetInterInt(1);
        }

        Invoke(nameof(LoadInter), 2);
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

}