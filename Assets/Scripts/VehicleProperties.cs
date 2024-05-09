using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITS.AI;
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
    public Transform TpsPosition;
    public GameObject[] StuntEffect;
    public GameObject ConeEffect;
    public Rigidbody Rb;
    public RCC_CarControllerV3 controller;
    
    public bool Highbrake = false;
    
    [Header("SettIngWheel")]
    
    public bool isSetWheelsModel=false;

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
        }

    public GameObject AllAudioSource;
    void Start()
    {
        //   LightMaterial.DisableKeyword("_EMISSION");
      //  StuntEffect = UiManagerObject_EG.instance.StuntEffects;
      //  TriggerEffect = UiManagerObject_EG.instance.Triggereffect;

        AllAudioSource = transform.Find("All Audio Sources").gameObject;
    }
    // Update is called once per frame
    public async void VehicleReadyForDrive()
    {
        Playlights();
        if (!TrafficVehicle)
            GameManager.Instance.CurrentCar.GetComponent<CarShadow>().ombrePlane.gameObject.SetActive(true);
        ConeEffect.SetActive(false);
        if (controller.chassis)
        {
            controller.chassis.GetComponent<RCC_Chassis>().enabled = true;
        }
        controller.enabled = true;
        Rb.drag=0.05f;
        if (Rb)
        {
            Rb.constraints = RigidbodyConstraints.None;
            Rb.isKinematic = false;
            Rb.useGravity = true;
        }
     //   controller.KillOrStartEngine();
        transform.name = "PlayerCar";
        GetComponent<RCC_CameraConfig>().enabled = true;
        if (GetComponent<TSSimpleCar>())
        {
            /*if (controller.chassis)
            {
                if (controller.chassis.GetComponent<RCC_Chassis>().ColliderParent!=null)
                {
                    controller.chassis.GetComponent<RCC_Chassis>().ColliderParent?.SetActive(true);
                }
            }*/
            GetComponent<TSSimpleCar>().enabled = false;
            GetComponent<TSTrafficAI>().enabled = false;
            GetComponent<TSAntiRollBar>().enabled = false;
            GetComponent<TSAntiRollBar>().enabled = false;
           // GetComponent<ChangeWheelTrafficToPlayer>().ChangeToPlayer();
        }

        GetComponent<RCC_CameraConfig>()?.SetCameraSettingsNow();
        
        controller.FrontLeftWheelCollider.enabled = true;
        controller.FrontRightWheelCollider.enabled = true;
        controller.RearLeftWheelCollider.enabled = true;
        controller.RearRightWheelCollider.enabled = true;
        if ( GetComponent<CarShadow>())
        {
            GetComponent<CarShadow>().enabled = true;
        }
 

        await Task.Delay(2000);
        if (AllAudioSource!=null)
        {
            AllAudioSource.SetActive(true);
        }

       
    }
    //public bool IsRewarded=false;
    public bool TrafficVehicle=false;

    public async void GetOutVehicle()
    {
        Highbrake = true;
        HighForceBrake();
        Stoplights();
        ConeEffect.SetActive(true);
       // controller.KillOrStartEngine();
        if (controller.chassis)
        {
            controller.chassis.GetComponent<RCC_Chassis>().enabled = false;
        }
        controller.enabled = false;
        controller.FrontLeftWheelCollider.enabled = false;
        controller.FrontRightWheelCollider.enabled = false;
        controller.RearLeftWheelCollider.enabled = false;
        controller.RearRightWheelCollider.enabled = false;
        GetComponent<RCC_CameraConfig>().enabled = false;
        if (GetComponent<CarShadow>())
        {
            GetComponent<CarShadow>().enabled = false;
        }


        if (GetComponent<TSSimpleCar>())
        {
            /*if (controller.chassis)
            {
                if (controller.chassis.GetComponent<RCC_Chassis>().ColliderParent != null)
                {
                    controller.chassis.GetComponent<RCC_Chassis>().ColliderParent?.SetActive(false);
                }
            }*/

            GetComponent<TSSimpleCar>().enabled = true;
            GetComponent<TSTrafficAI>().enabled = true;
            GetComponent<TSAntiRollBar>().enabled = true;
            GetComponent<TSAntiRollBar>().enabled = true;
           // GetComponent<ChangeWheelTrafficToPlayer>().ChangeToAI();
            enabled = false;
        }
        else if (!TrafficVehicle)
        {
            GameManager.Instance.CurrentCar.GetComponent<CarShadow>().ombrePlane.gameObject.SetActive(false);
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
                Debug.Log("Grounded Car is in the Air");
                Rb.isKinematic = true;
                await Task.Delay(500);
                Rb.isKinematic = false;
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0f);
                Rb.angularDrag = 0.05f;
                Rb.drag = 5f;
                StartCoroutine(CheckisGrounded());
            }
            // Rb.constraints =  RigidbodyConstraints.FreezeRotationX | 
            //                   RigidbodyConstraints.FreezeRotationZ | 
            //                   RigidbodyConstraints.FreezePositionX | 
            //                   RigidbodyConstraints.FreezePositionZ;
        }
       
    }

    public IEnumerator CheckisGrounded()
    {
        Debug.Log("Grounded StartDebugging....");
        yield return new WaitUntil(() => Grounded);
        Debug.Log("Grounded is true "+Grounded);
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

    /*string AiCarTage = "AiCar";

    public void OnCollisionStay(Collision collision)
    {

        if (collision.gameObject.tag == AiCarTage)
        {
            if (collision.gameObject.GetComponentInParent<DamagManager>())
            {
               collision.gameObject.GetComponentInParent<DamagManager>().Damage(Time.timeScale / 1.2f);
            }
        }
    }*/


    public void Playlights()
    {
       // lights.SetActive(true);
      //  UiManagerObject.instance.fader.SetActive(true);
    }
    public void Stoplights()
    {
      //  lights.SetActive(false);
       // UiManagerObject.instance.fader.SetActive(false);
    }
}
