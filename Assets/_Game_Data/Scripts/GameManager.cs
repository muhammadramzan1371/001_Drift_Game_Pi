using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameAnalyticsSDK;
using PlayerInteractive_Mediation;
using SickscoreGames.HUDNavigationSystem;
using UnityEngine;
public enum PlayerStatus
{
    ThirdPerson,CarDriving,BikeDriving
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public delegate void VehicleInteraction(PlayerStatus Status);

    public static event VehicleInteraction OnVehicleInteraction;


    public PlayerStatus TpsStatus = PlayerStatus.ThirdPerson;

    [Header("ThirdPerson Stuff")] [Space(5)]

    public GameObject TPSPlayer;

    [Space(5)] [Header("Car Stuff")] public Transform VehicleCamera;
    public Transform TpsCamera;
    public GameObject CurrentCar;
    public Transform TrafficSpawn;
    public Transform Weather;
    public HUDNavigationSystem hud;

    [Header("Mobile Stuff")] [Space(5)] public GameObject DefaultCar;
    public GameObject[] AllCarsOnVedio, AllShadows /*,AllBiCyclesOnVido,AllBikesOnVideo,AllHeliOnVedio*/;
    public Transform DefaultCarPosition;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        hud.PlayerCamera = TpsCamera.GetComponent<Camera>();
        hud.PlayerController = TPSPlayer.transform;
        TPSPlayer = LevelManager.instace?.TpsPlayer;
    }

    public async void GetInVehicle()
    {
        if (CurrentCar == null)
        {
            return;
        }

        if (!CurrentCar.GetComponent<VehicleProperties>().NotShowAdForSit)
        {
            ShowInter();
        }
        Time.timeScale = 1;
        UiManagerObject.instance.blankimage.SetActive(true);
        Invoke("offimage", 0.5f);
        UiManagerObject.instance.panels.CarControle.SetActive(true);
        UiManagerObject.instance.panels.TpsControle.SetActive(false);
        CurrentCar.GetComponent<Rigidbody>().angularDrag = 0.05f;
        if (CurrentCar.GetComponent<VehicleProperties>() == null)
        {
            return;
        }
        CurrentCar.GetComponent<VehicleProperties>().enabled = true;
        CurrentCar.GetComponent<DriftPhysics>().enabled = true;
        // CurrentCar.GetComponent<DriftPhysics>().Awakewhenicall();
        CurrentCar.GetComponent<VehicleProperties>().VehicleReadyForDrive();
        TPSPlayer.SetActive(false);
        TpsCamera.gameObject.SetActive(false);
        VehicleCamera.gameObject.SetActive(true);
        OnVehicleInteraction?.Invoke(PlayerStatus.CarDriving);
        await Task.Delay(50);
        TpsStatus = PlayerStatus.CarDriving;
        LevelManager.instace.vehicleCamera.SetTarget(CurrentCar);
        hud.PlayerCamera = VehicleCamera.GetComponentInChildren<Camera>();
        hud.PlayerController = CurrentCar.transform;
    }


    private void Update()
    {
        if (TpsStatus == PlayerStatus.CarDriving)
        {
            Weather.transform.position = VehicleCamera.transform.position;
            TrafficSpawn.position = VehicleCamera.position;
            TrafficSpawn.rotation = VehicleCamera.rotation;
        }
        else
        {
            Weather.transform.position = TpsCamera.transform.position;
            TrafficSpawn.position = TpsCamera.position;
            TrafficSpawn.rotation = TpsCamera.rotation;
        }
    }

    public void offimage()
    {
        UiManagerObject.instance.blankimage.SetActive(false);
    }

    public async void GetOutVehicle()
    {
        Time.timeScale = 1;
        UiManagerObject.instance.blankimage.SetActive(true);
        Logger.ShowLog("Here");
        UiManagerObject.instance.panels.CarControle.SetActive(false);
        UiManagerObject.instance.panels.TpsControle.SetActive(true);
        Invoke("offimage", 0.5f);
        TPSPlayer.SetActive(true);
        TpsCamera.gameObject.SetActive(true);
        VehicleCamera.gameObject.SetActive(false);
        CurrentCar.GetComponent<VehicleProperties>().GetOutVehicle();
        CurrentCar.GetComponent<VehicleProperties>().enabled = false;
        CurrentCar.GetComponent<DriftPhysics>().enabled = false;
        TPSPlayer.transform.position = CurrentCar.GetComponent<VehicleProperties>().TpsPosition.position;
        TPSPlayer.transform.eulerAngles = new Vector3(0, CurrentCar.GetComponent<VehicleProperties>().TpsPosition.rotation.y, 0);
        OnVehicleInteraction?.Invoke(PlayerStatus.ThirdPerson);
        await Task.Delay(50);
        TpsStatus = PlayerStatus.ThirdPerson;
        hud.PlayerCamera = TpsCamera.GetComponent<Camera>();
        hud.PlayerController = TPSPlayer.transform;
    }

    public void ShowInter()
    {
        if (FindObjectOfType<Pi_AdsCall>())
        {
            FindObjectOfType<Pi_AdsCall>().showInterstitialAD();
            PrefsManager.SetInterInt(1);
        }
        Invoke(nameof(LoadInter),2);
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

    public void StartEngein()
    {
        CurrentCar.GetComponent<RCC_CarControllerV3>().KillOrStartEngine();
    }



    public async void CarInstantiateNow(int lValue)
    {
        PrefsManager.SetCurrentCarOnVideo(lValue);
        PrefsManager.SetCurrentCarShadow(lValue);
        /*CarInstantiateDone();
        Showinter();
        Invoke("Loadinter", 2);*/
        Data.AdType = 25;
        if (FindObjectOfType<Pi_AdsCall>())
        {
            FindObjectOfType<Pi_AdsCall>().showRewardVideo(CarInstantiateDone);
        }

        await Task.Delay(1000);
        DefaultCar.GetComponent<CarShadow>().enabled = true;
        DefaultCar.GetComponent<CarShadow>().ombrePlane = AllShadows[lValue].transform;
        GameAnalytics.NewAdEvent(GAAdAction.RewardReceived, GAAdType.RewardedVideo, "Admob", "Get_Car_OnVideo_By_Mobile");
    }

    public void CarInstantiateDone()
    {
        DefaultCar = Instantiate(AllCarsOnVedio[PrefsManager.GetCurrentCarOnVideo()], DefaultCarPosition.position, DefaultCarPosition.rotation);
        DefaultCar.GetComponent<Rigidbody>().isKinematic = false;
        if (DefaultCar.GetComponent<VehicleProperties>())
        {
            DefaultCar.GetComponent<VehicleProperties>().NotShowAdForSit = true;
        }
        UiManagerObject.instance.Mobile.SetActive(false);
        UiManagerObject.instance.MobileOnBtn.SetActive(true);
    }
}
