using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whwathercontroler : MonoBehaviour
{
    // Start is called before the first frame update
 
       public ParticleSystem rainParticleSystem;
       public ParticleSystem snowParticleSystem;
       public float snowDelay = 120f; // 2 minutes delay
       public float rainDelay = 180f; // 3 minutes delay
       public float dayDelay = 180f; // 3 minutes delay
   
       private bool snowing = false;
       private bool raining = false;
       private bool isDay = false;
       private float lastTriggerTime = 0f;
   
       void Start()
       {
           // Initially, it's day time
           StartDay();
       }
   
       void Update()
       {
           // Check if no weather trigger in last 2 minutes
           if (Time.time - lastTriggerTime >= snowDelay && !snowing && !raining && !isDay)
           {
               StartSnow();
           }
   
           // Check if no weather trigger in last 3 minutes
           if (Time.time - lastTriggerTime >= rainDelay && snowing && !raining && !isDay)
           {
               StartRain();
           }
   
           // Check if no weather trigger in last 3 minutes after rain started
           if (Time.time - lastTriggerTime >= dayDelay && raining && !isDay)
           {
               StartDay();
           }
       }
   
       void OnTriggerEnter(Collider other)
       {
           if (other.CompareTag("Snow"))
           {
               StartSnow();
           }
           else if (other.CompareTag("Rain"))
           {
               StartRain();
           }
           else if (other.CompareTag("Day"))
           {
               StartDay();
           }
       }
   
       void StartSnow()
       {
           snowParticleSystem.Play();
           snowing = true;
           raining = false;
           isDay = false;
           lastTriggerTime = Time.time;
       }
   
       void StartRain()
       {
           rainParticleSystem.Play();
           snowParticleSystem.Stop();
           raining = true;
           snowing = false;
           isDay = false;
           lastTriggerTime = Time.time;
       }
   
       void StartDay()
       {
           rainParticleSystem.Stop();
           snowParticleSystem.Stop();
           raining = false;
           snowing = false;
           isDay = true;
           lastTriggerTime = Time.time;
       }
  
}
