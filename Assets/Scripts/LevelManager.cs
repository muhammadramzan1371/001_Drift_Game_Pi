using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using  SickscoreGames.HUDNavigationSystem;
public class LevelManager : MonoBehaviour
{
    public GameObject[] Levels;
    public GameObject[] Players;
    public int[] Reward; 
    public LevelProperties CurrentLevelProperties, FreeModeLevelProperties;
    public DrawMapPath mapPath;
    public GameObject SelectedPlayer,FreeMode,coinBar;
    public static LevelManager instace;
    public LineRenderer Line;
    public int CurrentLevel, coinsCounter;
   public HUDNavigationSystem system;
   public HUDNavigationCanvas canvashud;
   public RCC_Camera vehicleCamera;
   public PlayerCamera_New Tpscamera;
    void Awake()
    {
        instace = this;
       
        if (PrefsManager.GetGameMode() != "free")
        {
            if (PrefsManager.GetLevelMode() == 0)
            {
                SelectedPlayer = Players[PrefsManager.GetSelectedPlayerValue()];
                CurrentLevel = PrefsManager.GetCurrentLevel() - 1;
                CurrentLevelProperties = Levels[CurrentLevel].GetComponent<LevelProperties>();
                //CurrentLevelProperties.gameObject.SetActive(true);
                SelectedPlayer.transform.position = CurrentLevelProperties.PlayerPosition.position;
                SelectedPlayer.transform.rotation = CurrentLevelProperties.PlayerPosition.rotation;
               // mapPath = SelectedPlayer.GetComponent<VehicleProperties>().mapPath;
                CurrentLevelProperties.gameObject.SetActive(true);
                //rCC_Camera.TPSPitchAngle = 18f;
            }
        }
        else
        {

            Time.timeScale = 1; 
            FreeMode.SetActive(true);
            coinBar.GetComponentInChildren<Text>().text = "" + PrefsManager.GetCoinsValue();
            coinBar.SetActive(true);
            CurrentLevelProperties = FreeModeLevelProperties;
            SelectedPlayer = Players[PrefsManager.GetSelectedPlayerValue()];
            SelectedPlayer.transform.position = CurrentLevelProperties.PlayerPosition.position;
            SelectedPlayer.transform.rotation = CurrentLevelProperties.PlayerPosition.rotation;
        //    mapPath = SelectedPlayer.GetComponent<VehicleProperties>().mapPath;
        //     mapPath = SelectedPlayer.GetComponent<VehicleProperties>().mapPath;
            CurrentLevelProperties.gameObject.SetActive(true);
        }
        SelectedPlayer.SetActive(true);
        //aIContoller.player = SelectedPlayer.transform;
      //  rCC_Camera.playerCar = SelectedPlayer.GetComponent<RCC_CarControllerV3>();
        //canvasController.playerTransform = SelectedPlayer.transform;
        system.PlayerController = SelectedPlayer.transform;

    }

public IEnumerator Start(){
    if (PrefsManager.GetLevelMode()!=1)
    {
        yield return new WaitForSeconds(0.5f);
        UiManagerObject.instance.ShowObjective(CurrentLevelProperties.LevelStatment);
    }
  
}







    public void TaskCompleted()
    {

        CurrentLevelProperties.Nextobjective();

    }

}
