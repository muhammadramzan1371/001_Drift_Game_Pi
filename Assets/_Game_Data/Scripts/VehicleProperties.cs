using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITS.AI;
using PlayerInteractive_Mediation;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum  CarNames
{
    Policecar1=0,
    Policecar2=1,
    Policecar3=2, 
    Policecar4=3, 
    Policecar5=4,
    Policecar6=5,
    Policecar7=6,
    Policecar8=7,
    Policecar9=8,
    Policecar10=9,
   
}
public class VehicleProperties : MonoBehaviour
{
    public Transform TpsPosition,DefaultCarPosition;
    public GameObject ConeEffect,Lights;
    public Rigidbody Rb;
    public RCC_CarControllerV3 controller;
    public bool Highbrake = false;
    //public bool IsOnVedio = false;
    
    [Header("SettIngWheel")]
    
    public bool isSetWheelsModel,NotShowAdForSit=false;

    private void OnDrawGizmos()
    {
        if (isSetWheelsModel)
        {
            isSetWheelsModel = false;
            GetComponent<RCC_CarControllerV3>().SetColliderToModel();
        }
    }
    
    public void HighForceBrake()
    {
        if (Highbrake=true)
        {
            Rb.velocity = Rb.velocity * 0.999f;
            Rb.angularVelocity = Rb.angularVelocity * 0.999f;
        }
    }
        private void Awake()
        {
            if (Rb==null)
            {
                Rb = GetComponent<Rigidbody>();
            }  if (controller==null)
            {
                controller = GetComponent<RCC_CarControllerV3>();
            }

            if (Lights!=null)
            {
                Lights.SetActive(false);
            } 

        }

    public GameObject AllAudioSource;
    void Start()
    {
        AllAudioSource = transform.Find("All Audio Sources").gameObject;
    }
    // Update is called once per frame
    public async void VehicleReadyForDrive()
    {
        if (!TrafficVehicle && FindObjectOfType<CarShadow>().ombrePlane != null)
        {
            GetComponent<CarShadow>().ombrePlane.gameObject.SetActive(true);
        }
        ConeEffect.SetActive(false);
        // if (controller.chassis)
        // {
        //     controller.chassis.GetComponent<RCC_Chassis>().enabled = true;
        // }

        if (Lights!=null)
        {
            Lights.SetActive(true);
        } 
        
        controller.enabled = true;
        Rb.drag = 0.2f;
        if (Rb)
        {
            Rb.constraints = RigidbodyConstraints.None;
            Rb.isKinematic = false;
            Rb.useGravity = true;
        }
        controller.StartEngine();
        transform.name = "PlayerCar";
        GetComponent<RCC_CameraConfig>().enabled = true;
        if (GetComponent<TSSimpleCar>())
        {
            // if (controller.chassis)
            // {
            //     if (controller.chassis.GetComponent<RCC_Chassis>().ColliderParent!=null)
            //     {
            //         controller.chassis.GetComponent<RCC_Chassis>().ColliderParent?.SetActive(true);
            //     }
            // }
            GetComponent<TSSimpleCar>().enabled = false;
            GetComponent<TSTrafficAI>().enabled = false;
            GetComponent<TSAntiRollBar>().enabled = false;
            GetComponent<TSAntiRollBar>().enabled = false;
            GetComponent<ChangeCarTrafficToPlayer>().ChangeToPlayer();
        }

        GetComponent<RCC_CameraConfig>()?.SetCameraSettingsNow();
        
        controller.FrontLeftWheelCollider.enabled = true;
        controller.FrontRightWheelCollider.enabled = true;
        controller.RearLeftWheelCollider.enabled = true;
        controller.RearRightWheelCollider.enabled = true;

        controller.FrontLeftWheelCollider.GenrateWheelParticals();
        controller.FrontRightWheelCollider.GenrateWheelParticals();
        controller.RearLeftWheelCollider.GenrateWheelParticals();
        controller.RearRightWheelCollider.GenrateWheelParticals();
        controller.FrontLeftWheelCollider.transform.GetChild(0).gameObject.SetActive(true);
        controller.FrontRightWheelCollider.transform.GetChild(0).gameObject.SetActive(true);
        controller.RearLeftWheelCollider.transform.GetChild(0).gameObject.SetActive(true);
        controller.RearRightWheelCollider.transform.GetChild(0).gameObject.SetActive(true);
        
        UiManagerObject.instance.PressButton();
        if ( GetComponent<CarShadow>())
        {
            GetComponent<CarShadow>().enabled = true;
        }

        if (IsOnInterSitail)
        {
            if (FindObjectOfType<Pi_AdsCall>())
            {
                FindObjectOfType<Pi_AdsCall>().showInterstitialAD();
                PrefsManager.SetInterInt(1);
            }
            await Task.Delay(2000);
            if (FindObjectOfType<Pi_AdsCall>())
            {
                if (PrefsManager.GetInterInt() != 5)
                {
                    FindObjectOfType<Pi_AdsCall>().loadInterstitialAD();
                }
            }
        }
        
        if (AllAudioSource!=null)
        {
            AllAudioSource.SetActive(true);
        }
    }
    //public bool IsRewarded=false;
    public bool TrafficVehicle=false;
    public bool IsOnInterSitail=false;
    
    
    
    
    
    

    public async void GetOutVehicle()
    {
        Highbrake = true;
        HighForceBrake();
        ConeEffect.SetActive(true);
        controller.KillEngine();
        // if (controller.chassis)
        // {
        //     controller.chassis.GetComponent<RCC_Chassis>().enabled = false;
        // }
        controller.enabled = false;
        controller.FrontLeftWheelCollider.enabled = false;
        controller.FrontRightWheelCollider.enabled = false;
        controller.RearLeftWheelCollider.enabled = false;
        controller.RearRightWheelCollider.enabled = false;
        
        
        if (Lights!=null)
        {
            Lights.SetActive(false);
        } 
        
        
        controller.FrontLeftWheelCollider.transform.GetChild(0).gameObject.SetActive(false);
        controller.FrontRightWheelCollider.transform.GetChild(0).gameObject.SetActive(false);
        controller.RearLeftWheelCollider.transform.GetChild(0).gameObject.SetActive(false);
        controller.RearRightWheelCollider.transform.GetChild(0).gameObject.SetActive(false);
        
        
        GetComponent<RCC_CameraConfig>().enabled = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero; 
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        
        
        if (GetComponent<CarShadow>())
        {
            GetComponent<CarShadow>().enabled = false;
        }


        if (GetComponent<TSSimpleCar>())
        {
            if (controller.chassis)
            {
                if (controller.chassis.GetComponent<RCC_Chassis>().ColliderParent != null)
                {
                    controller.chassis.GetComponent<RCC_Chassis>().ColliderParent?.SetActive(false);
                }
            }

            GetComponent<TSSimpleCar>().enabled = true;
            GetComponent<TSTrafficAI>().enabled = true;
            GetComponent<TSAntiRollBar>().enabled = true;
            GetComponent<TSAntiRollBar>().enabled = true;
            GetComponent<ChangeCarTrafficToPlayer>().ChangeToTrafficAi();
            enabled = false;
        }
        else if (!TrafficVehicle)
        {
          GetComponent<CarShadow>().ombrePlane.gameObject.SetActive(false);
            if (AllAudioSource != null)
            {
                AllAudioSource.SetActive(false);
            }
            else
            {
                AllAudioSource = transform.Find("All Audio Sources").gameObject;
                AllAudioSource?.SetActive(false);
            }

            if (Grounded)
            {
                Rb.angularDrag = 0;
                Rb.drag = 50f;
                enabled = false;
            }
            else
            {
                Logger.ShowLog("Grounded Car is in the Air");
                Rb.isKinematic = true;
                await Task.Delay(500);
                Rb.isKinematic = false;
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0f);
                Rb.angularDrag = 0.05f;
                Rb.drag = 5f;
                StartCoroutine(CheckisGrounded());
            }
        }
        UiManagerObject.instance.PressButton();
    }

    public IEnumerator CheckisGrounded()
    {
        Logger.ShowLog("Grounded StartDebugging....");
        yield return new WaitUntil(() => Grounded);
        Logger.ShowLog("Grounded is true "+Grounded);
        yield return new WaitForSeconds(1f);
        Rb.isKinematic = true;
        enabled = false;
    }
    

    public bool Grounded = false;
    public float groundCheckDistance=1.1f;
  //  public float BufferCheckDistance = 0.1f;
  RaycastHit hit;

  public LayerMask LayerMask;
  //groundCheckDistance = 1.1f;
  public void Update()
  {

      Debug.DrawRay(transform.position, -transform.up * groundCheckDistance, Color.green);
      if (Physics.Raycast(transform.position, -transform.up, out hit, groundCheckDistance, LayerMask))
      {
          Grounded = true;
      }
      else
      {
          Grounded = false;
      }
  }

  string AiCarTag = "TrafficCar";
  string StuntTag = "Stunt";
  string FailTag = "Fail";
  string CallManagerTrue = "Call";


  
  

    public IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
         
            Invoke("OffCoinsEffect", 2f);
            PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue() + 10);
            other.gameObject.SetActive(false);
            
            yield return new WaitForSeconds(5);
            other.gameObject.SetActive(true);
            // StartCoroutine(ConfettiEffect());
        }
    }
}
