using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using  SickscoreGames.HUDNavigationSystem;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

public class SlectionManger : MonoBehaviour
{

    public int TpsPlayervalue = 0;
    public GameObject[] Buttons,Players;
    public string [] name ;

    public Text NameText;

    public int[] hp, def, max, atc, elmast;
    public Text HpTexts, defTexts, maxTexts, atcTexts, elmastTexts;
    public int randeomSpidervalue = 0;

    public Canvas RccCanvas;
    public GameObject camera;
    private void Start()
    {
        UpdateValues();
    }

    private void OnEnable()
    {
        RccCanvas.renderMode = RenderMode.ScreenSpaceCamera;
    }
    private void OnDisable()
    {
        RccCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
    }

    private void UpdateValues()
    {
        randeomSpidervalue = Random.Range(0, hp.Length); 
        randeomSpidervalue = Random.Range(0, def.Length); 
        randeomSpidervalue = Random.Range(0, max.Length); 
        randeomSpidervalue = Random.Range(0, atc.Length); 
        randeomSpidervalue = Random.Range(0, elmast.Length); 
        HpTexts.text= hp[randeomSpidervalue].ToString();
        defTexts.text= def[randeomSpidervalue].ToString();
        maxTexts.text= max[randeomSpidervalue].ToString();
        atcTexts.text= atc[randeomSpidervalue].ToString();
        elmastTexts.text= elmast[randeomSpidervalue].ToString();
    }

    public void ChracterSlection(int Val)
    {
        TpsPlayervalue = Val;
          NameText.text=name[TpsPlayervalue].ToString();
        foreach (var btn in Buttons)
        {
            btn.transform.GetChild(0).gameObject.SetActive(false);
        }
        foreach (var tps in Players)
        {
            tps.gameObject.SetActive(false);
        }
        Buttons[TpsPlayervalue].transform.GetChild(0).gameObject.SetActive(true);
        Players[TpsPlayervalue].gameObject.SetActive(true);
        UpdateValues();
    }
    public void MoreGamesEvent ()
    {
        SoundManager.Instance.PlayOneShotSounds (SoundManager.Instance.click);
        Application.OpenURL("https://play.google.com/store/apps/dev?id=8615786039404712360");
    }

    public void Offpanel()
    {
        transform.gameObject.SetActive(false);
        camera.gameObject.SetActive(false);
        UiManagerObject.instance.panels.TpsControle.GetComponent<CanvasGroup>().alpha = 0;
        LevelManager.instace.Tpscamera.GetComponent<CameraMovement>().UpdateCamera();
    }
}
