using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class DriftPhysics : MonoBehaviour
{
	private Rigidbody thisRigidbody;
	private Transform thisTransform;
	public float Speed;
	public float Speed01;
	private RCC_CarControllerV3 RCCController;
	[Header("DRIFT")]
	private float maxspeedDelayDrift = 0.055f;
	private float accspeedDelayDrift = 0.1f;
	private int sensDrift = -1;
	public int driftPointTotal;
	public bool isDrifting;
	public bool isDriftScoring;
	public float delayDrift;
	public float speedDelayDrift;
	public float driftPoint;
	public int driftFactor;
	public float waitDriftFailed;
	public bool isWrongWay;
	public DriftCanvasManager driftCanvasManager;
	
	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action m_GetCarDamageFunc;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action m_GetDriftPointFunc;
	private Vector3 currentVel;
	private Vector3 currentVelAngle;
	public float MaxSpeed = 0;
	public void Awakewhenicall()
	{
		driftCanvasManager.gameObject.SetActive(true);
		thisRigidbody = GetComponent<Rigidbody>();
		RCCController = GetComponent<RCC_CarControllerV3>();
		thisTransform = base.transform;
		{
			RCCController.antiRollFrontHorizontal /= 1.5f;
			RCCController.antiRollRearHorizontal /= 1.5f;
			RCCController.applyCounterSteering = true;
		}
		speedDelayDrift = maxspeedDelayDrift;
	}

	public float[] TargetValues = { 1, 200, 1200, 3200, 9200, 12000 };
	public float[] TargetValuesDevided = { 1, 20, 120, 320, 920, 1200};
	private int DevidedNumber=0;
	private void Update()
	{
		Speed = Vector3.Dot(thisRigidbody.velocity, thisTransform.forward);
		Speed01 = Mathf.Clamp01(Mathf.Abs(Speed / 30f));
		if (!thisRigidbody.useGravity)
		{
			thisRigidbody.AddForce(Vector3.down * 1f, ForceMode.Acceleration);
		}
		if (isDrifting)
		{
			speedDelayDrift += Time.deltaTime*  accspeedDelayDrift * (float)sensDrift;
			speedDelayDrift = Mathf.Clamp(speedDelayDrift, (0f - maxspeedDelayDrift) * 0.48f, maxspeedDelayDrift * 1.1f);
			delayDrift += speedDelayDrift * Time.deltaTime *45f;
			driftCanvasManager.UpdateWheel(delayDrift);
			isDriftScoring = false;
			if (delayDrift <= 0f)
			{
				delayDrift = 0f;
				DriftEnd(true);
			}
			else if (delayDrift >= 1f)
			{
				isDriftScoring = true;
				if (delayDrift >= 1.2f)
				{
					delayDrift = 1.2f;
					speedDelayDrift = 0f;
				}
			}
			if (isDriftScoring && !isWrongWay)
			{
				
				driftPoint += Time.deltaTime * (float)driftFactor  *50;
				driftCanvasManager.UpdatePoint(driftPoint);
				DevidedNumber = (int)(driftPoint / TargetValuesDevided[driftFactor]);
				switch (driftFactor)
				{
				case 0:
					driftFactor++;
					break;
				case 1:
					if (driftPoint > 200f)
					{
						driftFactor++;
						//driftCanvasManager.GetComponent<AudioSource>().PlayOneShot(driftCanvasManager.sounds[0]);
						
					}
					break;
				case 2:
					if (driftPoint > 1200f)
					{
						driftFactor++;
						//driftCanvasManager.GetComponent<AudioSource>().PlayOneShot(driftCanvasManager.sounds[1]);
						//driftCanvasManager.UpdateFactor(driftFactor,(int)(driftPoint/320f));
					}
					break;
				case 3:
					if (driftPoint > 3200f)
					{
						driftFactor++;
						//driftCanvasManager.GetComponent<AudioSource>().PlayOneShot(driftCanvasManager.sounds[2]);
						//driftCanvasManager.textFactor.GetComponent<Animator>().Play(0);
						//driftCanvasManager.UpdateFactor(driftFactor,(int)(driftPoint/920f));
					}
					break;
				case 4:
					if (driftPoint > 9200)
					{
						driftFactor++;
					//	driftCanvasManager.GetComponent<AudioSource>().PlayOneShot(driftCanvasManager.sounds[3]);
					//	if (Mathf.Abs(driftPoint % 1.0f - 0.1f) < Mathf.Epsilon) 
						//{
						//	driftCanvasManager.textFactor.GetComponent<Animator>().Play(0);
					//	}
						//driftCanvasManager.textFactor.GetComponent<Animator>().Play(0);
						//driftCanvasManager.UpdateFactor(driftFactor,(int)(driftPoint/1200f));
					}
					break;
				}
				driftCanvasManager.UpdateFactor(driftFactor,DevidedNumber);
				

			}
		}
		if (waitDriftFailed > 0f)
		{
			waitDriftFailed -= Time.deltaTime;
		}		
	}
	private void DriftEnd(bool isOK)
	{
		driftCanvasManager.UpdatePointEnd(isOK);
		if (!isOK)
		{
			waitDriftFailed = 1f;
		}
		else
		{
			driftPointTotal += Mathf.RoundToInt(driftPoint);
			driftCanvasManager.UpdatePointFreeFlight(Mathf.RoundToInt(driftPoint));
		}
		driftPoint = 0f;
		driftCanvasManager.UpdatePoint(0f);
		driftFactor = 0;
		driftCanvasManager.UpdateFactor(0,0);
		speedDelayDrift = maxspeedDelayDrift;
		driftCanvasManager.canvasWheelAll.SetActive(false);
		isDrifting = false;
	}

	public void FixedUpdate()
	{
		currentVel = thisRigidbody.velocity;
		currentVelAngle = thisRigidbody.angularVelocity;
	}

	private void OnCollisionEnter(Collision check)
	{
		if (check.gameObject.CompareTag("RigidKine") || check.gameObject.CompareTag("TrafficCar"))
		{
			try
			{
				check.rigidbody.isKinematic = false;
				if (thisRigidbody!=null)
				{
					thisRigidbody.velocity = currentVel;
					thisRigidbody.angularVelocity = currentVelAngle;

				}
			}
			catch (Exception e)
			{
			//	GameAnalytics.NewErrorEvent(GAErrorSeverity.Error,"Error at OcCollision "+e.ToString());
				throw;
			}
		
		}
   		if (waitDriftFailed <= 0f && !check.gameObject.CompareTag("RigidKine") && !check.gameObject.CompareTag("DriftOK"))
		{
			DriftEnd(false);
		}
	}

	public void UpdateDriftStatus(bool isdrift)
	{
		if (isdrift)
		{
			if (!isDrifting && waitDriftFailed <= 0f)
			{
				isDrifting = true;
				driftCanvasManager.canvasWheelAll.SetActive(true);
			}
			sensDrift = 1;
		}
		else
		{
			sensDrift = -1;
		}
	}
}
