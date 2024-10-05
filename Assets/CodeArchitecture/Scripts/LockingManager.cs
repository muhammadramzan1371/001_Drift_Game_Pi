using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LockingManager : MonoBehaviour
{
	public int ID;
	private int LockingValue = 0;


	void Start()
	{
		if (PrefsManager.GetLevelMode() == 0)
			LockingValue = PrefsManager.GetLevelLocking();
		else if (PrefsManager.GetLevelMode() == 1)
			LockingValue = PrefsManager.GetSnowLevelLocking();




		if (LockingValue > ID)
		{
			transform.GetChild(0).gameObject.SetActive(false);
			transform.GetChild(2).gameObject.SetActive(true);
			transform.GetChild(1).gameObject.SetActive(false);
			gameObject.GetComponent<Button>().interactable = true;
			GetComponent<Button>().enabled = true;
		}
		else if (LockingValue == ID)
		{
			transform.GetChild(2).gameObject.SetActive(false);
			transform.GetChild(1).gameObject.SetActive(true);
			gameObject.GetComponent<Button>().interactable = true;
			GetComponent<Button>().enabled = true;
		}
		else
		{
			GetComponent<Button>().enabled = false;
		}
	}


}
