﻿//----------------------------------------------
//            Realistic Car Controller
//
// Copyright © 2015 BoneCracker Games
// http://www.bonecrackergames.com
//
//----------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using System.Collections;


[AddComponentMenu("BoneCracker Games/Realistic Car Controller/UI/Mobile/Mobile Buttons")]
public class RCC_MobileButtons : MonoBehaviour {

	public RCC_CarControllerV3[] carControllers;

	public RCC_UIController gasButton;
	public RCC_UIController brakeButton;
	public RCC_UIController leftButton;
	public RCC_UIController rightButton;
	public RCC_UISteeringWheelController steeringWheel;
	public RCC_UIController handbrakeButton;
	public RCC_UIController NOSButton;
	public GameObject gearButton;


	private float gasInput = 0f;
	private float brakeInput = 0f;
	private float leftInput = 0f;
	private float rightInput = 0f;
	private float steeringWheelInput = 0f;
	private float handbrakeInput = 0f;
	private float NOSInput = 1f;
	private float gyroInput = 0f;

	private Vector3 orgBrakeButtonPos;
	public Image NosImage;
	void Start()
	{

		if(RCC_Settings.Instance.controllerType != RCC_Settings.ControllerType.Mobile)
		{
			
			if(gasButton)
				gasButton.gameObject.SetActive(false);
			if(leftButton)
				leftButton.gameObject.SetActive(false);
			if(rightButton)
				rightButton.gameObject.SetActive(false);
			if(brakeButton)
				brakeButton.gameObject.SetActive(false);
			if(steeringWheel)
				steeringWheel.gameObject.SetActive(false);
			if(handbrakeButton)
				handbrakeButton.gameObject.SetActive(false);
			if(NOSButton)
				NOSButton.gameObject.SetActive(false);
			if(gearButton)
				gearButton.gameObject.SetActive(false);
			
			enabled = false;
			return;

		}

		orgBrakeButtonPos = brakeButton.transform.position;
		GetVehicles();

		NosImage = UiManagerObject.instance.NosFiller;
		UiManagerObject.instance.NosCountText.text = PrefsManager.GetNosCounter().ToString();
		if (PrefsManager.GetNosCounter()<=0)
		{
			UiManagerObject.instance.NosCountText.text = "";
			UiManagerObject.instance.NosButton.SetActive(true);
		}
	}

	public void GetVehicles()
	{
		carControllers = GameObject.FindObjectsOfType<RCC_CarControllerV3>();
	}


	void Update()
	{
		if(RCC_Settings.Instance.useSteeringWheelForSteering)
		{
			if(RCC_Settings.Instance.useAccelerometerForSteering) 
				RCC_Settings.Instance.useAccelerometerForSteering = false;
			if(!steeringWheel.gameObject.activeInHierarchy)
			{
				steeringWheel.gameObject.SetActive(true);
				brakeButton.transform.position = orgBrakeButtonPos;
			}
			if(leftButton.gameObject.activeInHierarchy)
				leftButton.gameObject.SetActive(false);
			if(rightButton.gameObject.activeInHierarchy)
				rightButton.gameObject.SetActive(false);
		}

		if(RCC_Settings.Instance.useAccelerometerForSteering)
		{
			if(RCC_Settings.Instance.useSteeringWheelForSteering)
				RCC_Settings.Instance.useSteeringWheelForSteering = false;
			brakeButton.transform.position = leftButton.transform.position;
			if(steeringWheel.gameObject.activeInHierarchy)
				steeringWheel.gameObject.SetActive(false);
			if(leftButton.gameObject.activeInHierarchy)
				leftButton.gameObject.SetActive(false);
			if(rightButton.gameObject.activeInHierarchy)
				rightButton.gameObject.SetActive(false);
		}

		if(!RCC_Settings.Instance.useAccelerometerForSteering && !RCC_Settings.Instance.useSteeringWheelForSteering)
		{
			if(steeringWheel && steeringWheel.gameObject.activeInHierarchy)
				steeringWheel.gameObject.SetActive(false);
			if(!leftButton.gameObject.activeInHierarchy){
				brakeButton.transform.position = orgBrakeButtonPos;
				leftButton.gameObject.SetActive(true);
			}
			if(!rightButton.gameObject.activeInHierarchy)
				rightButton.gameObject.SetActive(true);
		}

		gasInput = GetInput(gasButton);
		brakeInput = GetInput(brakeButton);
		leftInput = GetInput(leftButton);
		rightInput = GetInput(rightButton);
		
		if(steeringWheel)
			steeringWheelInput = steeringWheel.input;
		
		if(RCC_Settings.Instance.useAccelerometerForSteering)
			gyroInput = Input.acceleration.x * RCC_Settings.Instance.gyroSensitivity;
		else
			gyroInput = 0f;
		
		handbrakeInput = GetInput(handbrakeButton);
		NOSInput = Mathf.Clamp(GetInput(NOSButton) * 2.5f, 1f, 2.5f);

		for (int i = 0; i < carControllers.Length; i++) 
		{
			if(carControllers[i].canControl && !carControllers[i].AIController)
			{
				carControllers[i].gasInput = gasInput;
				carControllers[i].brakeInput = brakeInput;
				carControllers[i].steerInput = -leftInput + rightInput + steeringWheelInput + gyroInput;
				carControllers[i].handbrakeInput = handbrakeInput;
				carControllers[i].boostInput = NOSInput;
				
				if (Nos)
				{
					carControllers[i].boostInput = 2.5f;
					carControllers[i].gasInput = 1;
					
					if (NosImage.fillAmount <= 0.05f)
					{
						Nos = false;
						carControllers[i].boostInput = 1f;
						carControllers[i].gasInput = 0;
						Logger.ShowLog("Nos Refilled");
						for (int j = 0; j < carControllers.Length; j++)
						{
							if (carControllers[j].canControl && !carControllers[j].AIController)
							{
								carControllers[j].NoS = 100;
							}
						}
						
						if (PrefsManager.GetNosCounter()>1)
						{
							PrefsManager.SetNosCounter(PrefsManager.GetNosCounter()-1);
							UiManagerObject.instance.NosCountText.text = PrefsManager.GetNosCounter().ToString();
						}
						else
						{
							UiManagerObject.instance.NosCountText.text ="";
							UiManagerObject.instance.NosButton.SetActive(true);
						}
						
					}
				}
				else
				{
					carControllers[i].boostInput =1f;
					//	Logger.ShowLog("0");
				}

			}

		}

	}
	
	private bool Nos = false;
	public void setNos(bool input)
	{
		Nos = input;
		

	}
	
	public void FillNos()
	{
		UiManagerObject.instance.NosButton.SetActive(false);
		
		UiManagerObject.instance.NosCountText.text = PrefsManager.GetNosCounter().ToString();
		for (int i = 0; i < carControllers.Length; i++)
		{
			if (carControllers[i].canControl && !carControllers[i].AIController)
			{
				carControllers[i].NoS = 100;
			}
		}
	}

	float GetInput(RCC_UIController button)
	{

		if(button == null)
			return 0f;

		return(button.input);

	}

	public void ChangeCamera () {

	//	if(GameObject.FindObjectOfType<RCC_Camera_Old>())
		//	GameObject.FindObjectOfType<RCC_Camera_Old>().ChangeCamera();

	}
public GameObject Tile,arrow,staring;
	public void ChangeController(int index)
	{

		switch(index)
		{

		case 0:
			RCC_Settings.Instance.useAccelerometerForSteering = false;
			RCC_Settings.Instance.useSteeringWheelForSteering = false;
			Tile.SetActive(false);
			arrow.SetActive(true);
			staring.SetActive(false);
			break;
		case 1:
			RCC_Settings.Instance.useAccelerometerForSteering = true;
			RCC_Settings.Instance.useSteeringWheelForSteering = false;
			Tile.SetActive(false);
			arrow.SetActive(false);
			staring.SetActive(true);
			break;
		case 2:
			RCC_Settings.Instance.useAccelerometerForSteering = false;
			RCC_Settings.Instance.useSteeringWheelForSteering = true;
			Tile.SetActive(true);
			arrow.SetActive(false);
			staring.SetActive(false);
			break;

		}

	}

}
