using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Forwater : MonoBehaviour
{
   private async void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.tag == "Player")
      {
         LevelManager.instace.vehicleCamera.GetComponent<RCC_Camera>().enabled = false;
         LevelManager.instace.Tpscamera.GetComponent<PlayerCamera_New>().enabled = false;
         UiManagerObject.instance.HideGamePlay();
         await Task.Delay(2000);
         UiManagerObject.instance.Restart();

      }
   }
}
