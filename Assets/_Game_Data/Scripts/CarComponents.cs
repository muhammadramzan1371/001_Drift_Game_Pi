/*
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CarComponents : MonoBehaviour
{
	[Serializable]
	public class CameraViewSetting
	{
		public List<Transform> cameraViews;

		public float distance = 5f;

		public float height = 1f;

		public float Angle = 20f;
	}

	public Transform handleTrigger;

	//public Transform door;

	//public Transform sitPoint;

	public Transform driver,TpsPosition;

	public AudioClip[] deathSoundClips;

	public CameraViewSetting cameraViewSetting;
	public RCC_CarControllerV3 CarController;
	public GameObject AllAudioSource;
	public Rigidbody Rigidbody;
	public GameObject ConeEffect;
	
	[HideInInspector]
	public bool driving = true;
	private void Awake()
	{
		if (Rigidbody==null)
		{
			Rigidbody = GetComponent<Rigidbody>();
		}
		
		if (CarController==null)
		{
			CarController = GetComponent<RCC_CarControllerV3>();
		}
	}
	
	private void Start()
	{
		AllAudioSource = transform.Find("All Audio Sources").gameObject;
	}

	

	public bool TrafficCarAi=false;
	public async void GetInCarForDrive()
	{
		GameManager.Instance.Currentvehicle.GetComponent<TriggerManager>().TrafficCar = false;
		StopCoroutine(CheckisGrounded());
		if (CarController.chassis)
		{
			CarController.chassis.GetComponent<RCC_Chassis>().enabled = true;
		}
		CarController.enabled = true;
		CarController.KillOrStartEngine();
		transform.name = "CurrentPlayer";
		GetComponent<RCC_CameraConfig>()?.SetCameraSettingsNew();
		if (!TrafficCarAi)
		{
			ConeEffect.SetActive(false);
		}
		if (AllAudioSource != null)
		{
			AllAudioSource.SetActive(false);
		}
		else
		{
			AllAudioSource = transform.Find("All Audio Sources").gameObject;
			AllAudioSource?.SetActive(false);
		}
		Rigidbody.drag=0.05f;
		if (Rigidbody)
		{
			Rigidbody.constraints = RigidbodyConstraints.None;
			Rigidbody.isKinematic = false;
			Rigidbody.useGravity = true;
		}
		GetComponent<RCC_CameraConfig>().enabled = true;
		
		if (GetComponent<TSSimpleCar>())
		{
			if (CarController.chassis)
			{
				if (CarController.chassis.GetComponent<RCC_Chassis>().ColliderParent!=null)
				{
					CarController.chassis.GetComponent<RCC_Chassis>().ColliderParent?.SetActive(true);
				}
			}
			GetComponent<TSSimpleCar>().enabled = false;
			GetComponent<TSAntiRollBar>().enabled = false;
			GetComponent<TSAntiRollBar>().enabled = false;
			GetComponent<ChangeCarTrafficToPlayer>().ChangeToPlayer();
		}
		
		CarController.FrontLeftWheelCollider.enabled = true;
		CarController.FrontRightWheelCollider.enabled = true;
		CarController.RearLeftWheelCollider.enabled = true;
		CarController.RearRightWheelCollider.enabled = true;
		if ( GetComponent<CarShadow>())
		{
			GetComponent<CarShadow>().enabled = true;
			GetComponent<CarShadow>().ombrePlane.gameObject.SetActive(true);
		}
		await Task.Delay(2000);
		if (AllAudioSource != null)
		{
			AllAudioSource.SetActive(true);
		}
	}



	public async void GetOutVehicle()
	{
		if ( GetComponent<CarShadow>())
		{
			GetComponent<CarShadow>().enabled = false;
			GetComponent<CarShadow>().ombrePlane.gameObject.SetActive(false);
		}
		GameManager.Instance.Currentvehicle.GetComponent<TriggerManager>().TrafficCar = true;
		CarController.KillOrStartEngine();
		if (AllAudioSource != null)
		{
			AllAudioSource.SetActive(false);
		}
		else
		{
			AllAudioSource = transform.Find("All Audio Sources").gameObject;
			AllAudioSource?.SetActive(false);
		}
		if (CarController.chassis)
		{
			CarController.chassis.GetComponent<RCC_Chassis>().enabled = false;
		}
		CarController.enabled = false;
		CarController.FrontLeftWheelCollider.enabled = false;
		CarController.FrontRightWheelCollider.enabled = false;
		CarController.RearLeftWheelCollider.enabled = false;
		CarController.RearRightWheelCollider.enabled = false;
		GetComponent<RCC_CameraConfig>().enabled = false;
		if (GetComponent<TSSimpleCar>())
		{
			if (CarController.chassis)
			{
				if (CarController.chassis.GetComponent<RCC_Chassis>().ColliderParent != null)
				{
					CarController.chassis.GetComponent<RCC_Chassis>().ColliderParent?.SetActive(false);
				}
			}
			GetComponent<TSSimpleCar>().enabled = true;
			GetComponent<TSAntiRollBar>().enabled = true;
			GetComponent<TSAntiRollBar>().enabled = true;
			GetComponent<ChangeCarTrafficToPlayer>().ChangeToTrafficAi();
			enabled = false;
		}
		else if (!TrafficCarAi)
		{
			ConeEffect.SetActive(true);
			if (Grounded)
			{
				Rigidbody.angularDrag = 0;
				Rigidbody.drag = 50f;
				Rigidbody.isKinematic = true;
				enabled = false;
			}
			else
			{
				Debug.Log("Grounded Car is in the Air");
				Rigidbody.isKinematic = true;
				await Task.Delay(500);
				Rigidbody.isKinematic = false;
				transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0f);
				Rigidbody.angularDrag = 0.05f;
				Rigidbody.drag = 5f;
				StartCoroutine(CheckisGrounded());
			}
		}
	}


	public IEnumerator CheckisGrounded()
	{
		Debug.Log("Grounded StartDebugging....");
		yield return new WaitUntil(() => Grounded);
		Debug.Log("Grounded is true "+Grounded);
		yield return new WaitForSeconds(1f);
		Rigidbody.isKinematic = true;
		enabled = false;
	}
	
	public bool Grounded = false;
	public float groundCheckDistance=1.1f;
	RaycastHit hit;
	public LayerMask LayerMask;


    private void Update()
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


        if (!driver)
        {
            return;
        }
        if (driving)
        {
            //driver.position = sitPoint.position;
            //driver.rotation = sitPoint.rotation;
            return;
        }
        driver.position = handleTrigger.position;
        driver.rotation = handleTrigger.rotation;
        Component[] componentsInChildren = driver.GetComponentsInChildren(typeof(Rigidbody));
        Component[] array = componentsInChildren;
        for (int i = 0; i < array.Length; i++)
        {
            Rigidbody rigidbody = (Rigidbody)array[i];
            rigidbody.isKinematic = false;
        }
        Component[] componentsInChildren2 = driver.GetComponentsInChildren(typeof(Collider));
        Component[] array2 = componentsInChildren2;
        for (int j = 0; j < array2.Length; j++)
        {
            Collider collider = (Collider)array2[j];
            collider.enabled = true;
        }
        driver.GetComponent<AudioSource>().clip = deathSoundClips[UnityEngine.Random.Range(0, deathSoundClips.Length)];
        driver.GetComponent<AudioSource>().Play();
        UnityEngine.Object.Destroy(driver.gameObject, 10f);
        driver.parent = null;
        driver = null;
    }
}
*/
