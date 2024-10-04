using System;
using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour
{

	public Action CheckpointActivated;
	public bool IsFinalCheckPoint = false;


	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			Logger.ShowLog("Activate");
			if (CheckpointActivated != null)
			{
				CheckpointActivated();
				if (IsFinalCheckPoint)
				{
					UiManagerObject.instance.FinalCheckPointEffect.SetActive(true);
				}
				else
				{
					UiManagerObject.instance.CheckPointEffect.SetActive(true);
				}
				UiManagerObject.instance.CheckPointCollectSound.Play();
				Invoke(nameof(OffEffect),1.5f);
			}
		}
	}


	public void OffEffect()
	{
		UiManagerObject.instance.CheckPointEffect.SetActive(false);
		UiManagerObject.instance.FinalCheckPointEffect.SetActive(false);
	}
}
