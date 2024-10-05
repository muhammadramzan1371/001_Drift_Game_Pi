using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpsTriger : MonoBehaviour
{
   public float cameraDistance;
   public Vector3 targetOffset;
   public Transform Hight;
   

   private void OnTriggerStay(Collider other)
   {
      //this forcar
      if (other.gameObject.tag == "Carhandle")
      {
         if (GetComponent<ThirdPersonUserControl>().enabled)
         {
            GameManager.Instance.CurrentCar = other.GetComponentInParent<RCC_CarControllerV3>().gameObject;
            GameControl.manager.getInVehicle.SetActive(true);
         }
      }
   }

   private void OnTriggerExit(Collider other)
   {
      //this forcar
      if (other.gameObject.tag == "Carhandle")
      {
         GameControl.manager.getInVehicle.SetActive(false);
         Logger.ShowLog("Car Handle");
      }
   }
}
   