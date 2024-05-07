using System.Collections;
using System.Collections.Generic;
/*using Google.Play.Review;*/
using UnityEngine;
using UnityEngine.UI;

public class RateUsHandler : MonoBehaviour
{
  
    public GameObject Hand,RatePannel;
    public GameObject ReviewObject;
    public GameObject[] AllStars;
    // Start is called before the first frame update
    public void FillImage(float fillAmount) {
    
        Time.timeScale = 1;
      for (int i = 0; i < fillAmount*5; i++)
      {
          AllStars[i].SetActive(true);
      }
      PlayerPrefs.SetInt("RateUsStatus", 1);
      if (fillAmount >= 0.8f)
        {
            Time.timeScale = 1;
            ReviewObject.SetActive(true);
          //  PrefsManager.SetRateUs(1);
          
            RatePannel.SetActive(false);
        }
        else {
            LetterClick();
            RatePannel.SetActive(false);
        }

        Hand.SetActive(false);

    }

    
    public void RateUs()
    {
        PlayerPrefs.SetInt("RateUsStatus", 1);
    }
    public void LetterClick()
    {
    //    SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
     //   PrefsManager.SetRateUs(1);
    }
}
