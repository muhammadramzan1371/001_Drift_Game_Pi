using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControler : MonoBehaviour
{
   public Animator DoorContrer;
   public Animator Door1Contrer;
   public bool Door1 = false;
 

   private void OnTriggerEnter(Collider other)
   {
       
      if (other.gameObject.tag=="Player")
      {
          if (Door1)
          {
              DoorContrer.enabled = true;
              DoorContrer.Play("DoorOpen");
          }
          else
          {
              Door1Contrer.enabled = true;
              Door1Contrer.Play("Open");
          }
       
       
      }
   }
   private void OnTriggerExit(Collider other)
   {
       if (other.gameObject.tag == "Player")
       {
           if (Door1)
           {
               DoorContrer.Play("DoorClose");
           }
           else
           {
               Door1Contrer.Play("Close");
           }
       }
   }
}
