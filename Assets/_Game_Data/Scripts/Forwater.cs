using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Forwater : MonoBehaviour
{
   private async void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.tag == "Player" || GameManager.Instance.CurrentCar.GetComponent<VehicleProperties>().TrafficVehicle)
      {
         transform.gameObject.SetActive(false);
         UiManagerObject.instance.HideGamePlay();
         if (GameManager.Instance.TpsStatus == PlayerStatus.ThirdPerson)
         {
            LevelManager.instace.Tpscamera.GetComponent<Camera>().enabled = false;
            await Task.Delay(2000);
            UiManagerObject.instance.ShowFail();
         }
         if (GameManager.Instance.TpsStatus == PlayerStatus.CarDriving)
         {
            LevelManager.instace.vehicleCamera.GetComponent<RCC_Camera>().enabled = false;
            await Task.Delay(2000);
            UiManagerObject.instance.ShowFail();
         }
      }
   }
   
   
   
   
   // private async void ondead()
   // {
   //    LevelManager.instace.Tpscamera.GetComponent<PlayerCamera_New>().enabled = false;   
   //    await Task.Delay(3500);
   //    UiManagerObject.instance.Restart();
   // }
}
