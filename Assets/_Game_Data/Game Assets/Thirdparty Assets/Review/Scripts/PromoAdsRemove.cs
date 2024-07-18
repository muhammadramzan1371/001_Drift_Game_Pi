using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PromoAdsRemove : MonoBehaviour
{
	private static int tempCountForPromotionRateUs = 3;
	private int showCount = 3;
	public GameObject RateUsPanel,RateUs;
	 public bool isRateUs;

    public void SetCountToMax()
	{

		tempCountForPromotionRateUs = showCount;
	}

	void OnEnable()
	{

		if (isRateUs)
		{
			if (PlayerPrefs.GetInt("RateUsStatus")==0)
            {
	         if(RateUsPanel) 
	             RateUsPanel.SetActive(true);
            }

			//if (PrefsManager.GetCurrentLevel()==2 || PrefsManager.GetCurrentLevel()==27)
			{
				//RateUs.SetActive(true);
			}
		}
		
	}


}
