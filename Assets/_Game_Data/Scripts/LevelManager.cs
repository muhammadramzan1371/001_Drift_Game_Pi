using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using  SickscoreGames.HUDNavigationSystem;
using UnityEngine.Rendering;

public class LevelManager : MonoBehaviour
{
    public GameObject[] Levels;
    public GameObject[] Players;
   // public GameObject[] Character;
    public int[] Reward;
    public PI_LevelProperties CurrentLevelProperties;
    public GameObject SelectedPlayer,TpsPlayer,FreeMode;
    public static LevelManager instace;
    public int CurrentLevel, coinsCounter;
    public HUDNavigationCanvas canvashud;
    public RCC_Camera vehicleCamera;
    public PlayerCamera_New Tpscamera;
    public OpenWorldManager OpenWorldManager;
    public DriftCanvasManager driftCanvasManagerNow;
    
    [Header("WheatherStuff")]
    public ParticleSystem snowParticleSystem;
    public Material daySkybox;
    public Material nightSkybox;
    public GameObject directionalLight_Night,DirectionLight_Day;
    
    private async void Awake()
    {
        instace = this;
        if (PrefsManager.GetGameMode() != "free")
        {
            if (PrefsManager.GetLevelMode() == 0)
            {
                SelectedPlayer = Players[PrefsManager.GetSelectedPlayerValue()];
               // Chracter = Character[PrefsManager.GetSelectedCracterValue()];
                CurrentLevel = PrefsManager.GetCurrentLevel() - 1;
                CurrentLevelProperties = Levels[CurrentLevel].GetComponent<PI_LevelProperties>();
                SelectedPlayer.transform.position = CurrentLevelProperties.CarPosition.position;
                SelectedPlayer.transform.rotation = CurrentLevelProperties.CarPosition.rotation;
                
                TpsPlayer.transform.position = CurrentLevelProperties.TpsPosition.position;
                TpsPlayer.transform.rotation = CurrentLevelProperties.TpsPosition.rotation;
                Tpscamera.transform.position = CurrentLevelProperties.TpsPosition.position;
                Tpscamera.transform.rotation = CurrentLevelProperties.TpsPosition.rotation;
                CurrentLevelProperties.gameObject.SetActive(true);
                await Task.Delay(500);
                Time.timeScale = 1; 
            }
        }
        else
        {
            Time.timeScale = 1; 
            FreeMode.SetActive(true);
            SelectedPlayer = Players[PrefsManager.GetSelectedPlayerValue()];
           // Chracter = Character[PrefsManager.GetSelectedCracterValue()];
            SetTransform(OpenWorldManager.TpsPosition, OpenWorldManager.CarPostiom);
        }
        SelectedPlayer.SetActive(true);
        TpsPlayer.SetActive(true);
        TpsPlayer.GetComponent<Rigidbody>().isKinematic = false;
        TpsPlayer.GetComponent<ThirdPersonUserControl>().enabled = true;
        SelectedPlayer.GetComponent<Rigidbody>().velocity = Vector3.zero;
        SelectedPlayer.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        SelectedPlayer.GetComponent<Rigidbody>().isKinematic = false;
        SelectedPlayer.GetComponent<VehicleProperties>().ConeEffect.SetActive(false);
        SelectedPlayer.GetComponent<VehicleProperties>().ConeEffect.transform.GetChild(0).gameObject.SetActive(false);
        SelectedPlayer.GetComponent<CarShadow>().enabled = true;
        SelectedPlayer.GetComponent<CarShadow>().ombrePlane.gameObject.SetActive(true);
    }
    
    
    public void SetTransform(Transform playerposition, Transform defulcar)
    {
        TpsPlayer.transform.position = playerposition.position;
        TpsPlayer.transform.rotation = playerposition.rotation;

        Tpscamera.transform.position = playerposition.position;
        Tpscamera.transform.rotation = playerposition.rotation;
        
        SelectedPlayer.transform.position = defulcar.position;
        SelectedPlayer.transform.rotation = defulcar.rotation;
    }

    public IEnumerator Start()
    {
        if (PrefsManager.GetGameMode() != "free")
        {
            yield return new WaitForSeconds(0.5f);
            UiManagerObject.instance.ShowObjective(CurrentLevelProperties.LevelStatment);
        }
    }

    public void DAy()
    {
        snowParticleSystem.Play();
        RenderSettings.skybox = nightSkybox;
        directionalLight_Night.SetActive(true);
        DirectionLight_Day.SetActive(false);
        snowParticleSystem.gameObject.SetActive(true);
    }
    
    public void Night()
    {
        RenderSettings.skybox = daySkybox;
        directionalLight_Night.SetActive(false);
        DirectionLight_Day.SetActive(true);
        snowParticleSystem.Stop();
        snowParticleSystem.gameObject.SetActive(false);
    }

    public void RestCar()
    {
        GameManager.Instance.CurrentCar.GetComponent<RCC_CarControllerV3>().RestCar();
    }

    public Material[] CarEffect;
    public float multiplaxer;
    private float offset;

    void Update()
    {
        offset += Time.deltaTime * multiplaxer;
        foreach (var VARIABLE in CarEffect)
        {
            VARIABLE.mainTextureOffset = new Vector2(0, offset);
        }
    }
}
