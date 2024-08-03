using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpsTriger : MonoBehaviour
{
   public float cameraDistance;
   public Vector3 targetOffset;
   public Transform Hight;
   public void OnTriggerEnter(Collider other)
   {
      //this forcar
      if (other.gameObject.tag == "Carhandle")
      {
         if (GetComponent<ThirdPersonUserControl>().enabled)
         {
            GameControl.manager.getInVehicle.SetActive(true);
            GameControl.manager.IdButton.SetActive(false);
            GameManager.Instance.CurrentCar = other.GetComponentInParent<RCC_CarControllerV3>().gameObject;
         }
      }
   }

   private void OnTriggerStay(Collider other)
   {
      //this forcar
      if (other.gameObject.tag == "Carhandle")
      {
         if (GetComponent<ThirdPersonUserControl>().enabled)
         {
            GameControl.manager.getInVehicle.SetActive(true);
            GameControl.manager.IdButton.SetActive(false);
            GameManager.Instance.CurrentCar = other.GetComponentInParent<RCC_CarControllerV3>().gameObject;
         }
      }
   }

   private void OnTriggerExit(Collider other)
   {
      //this forcar
      if (other.gameObject.tag == "Carhandle")
      {
         GameControl.manager.getInVehicle.SetActive(false);
         GameControl.manager.IdButton.SetActive(true);
         Logger.ShowLog("Car Handle");
      }
   }
}
   