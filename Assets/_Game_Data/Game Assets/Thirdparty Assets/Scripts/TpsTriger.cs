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
           
            GameControl.manager.IdButton.SetActive(false);
            GameManager.Instance.CurrentCar = other.GetComponentInParent<RCC_CarControllerV3>().gameObject;
            if ( GameManager.Instance.CurrentCar.GetComponentInParent<VehicleProperties>().IsOnVedio)
            {
               GameControl.manager.getInVehicleOnvedo.SetActive(true);
               GameControl.manager.getInVehicle.SetActive(false);
            }
            else
            {
               GameControl.manager.getInVehicle.SetActive(true);
               GameControl.manager.getInVehicleOnvedo.SetActive(false);
            }
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
            GameControl.manager.IdButton.SetActive(false);
            GameManager.Instance.CurrentCar = other.GetComponentInParent<RCC_CarControllerV3>().gameObject;
            if ( GameManager.Instance.CurrentCar.GetComponentInParent<VehicleProperties>().IsOnVedio)
            {
               GameControl.manager.getInVehicleOnvedo.SetActive(true);
               GameControl.manager.getInVehicle.SetActive(false);
            }
            else
            {
               GameControl.manager.getInVehicle.SetActive(true);
               GameControl.manager.getInVehicleOnvedo.SetActive(false);
            }
         }
      }
   }

   private void OnTriggerExit(Collider other)
   {
      //this forcar
      if (other.gameObject.tag == "Carhandle")
      {
         GameControl.manager.IdButton.SetActive(true);
         if ( GameManager.Instance.CurrentCar.GetComponentInParent<VehicleProperties>().IsOnVedio)
         {
            GameControl.manager.getInVehicleOnvedo.SetActive(false);
         }
         else
         {
            GameControl.manager.getInVehicle.SetActive(false);
         }
         Logger.ShowLog("Car Handle");
      }
   }
}
   