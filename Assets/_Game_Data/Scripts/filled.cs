using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class filled : MonoBehaviour
{
	float stratpoint = 0;
	public float endpoint = 50;
	private Image image;


	private void Awake()
	{
		image = transform.GetChild(0).GetComponent<Image>();
	}
	
	
	void Update()
	{
		if (stratpoint < endpoint)
		{
			stratpoint += 60 * Time.deltaTime;
			image.fillAmount += 60 * Time.deltaTime / 100;
		}
	}
	

	public void OnDisable()
	{
		image.fillAmount = 0;
		stratpoint = 0;
	}


	public void SetEndpoint(int endpoint)
	{
		Logger.ShowLog("SetHere");
		stratpoint = 0;
		this.endpoint = endpoint;
		if (image != null)
			image.fillAmount = 0f;
	}
	
}
