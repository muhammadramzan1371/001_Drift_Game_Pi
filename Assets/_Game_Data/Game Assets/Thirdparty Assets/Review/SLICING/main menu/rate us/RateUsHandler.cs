using System.Collections;
using System.Collections.Generic;
using Google.Play.Review;
using UnityEngine;
using UnityEngine.UI;

public class RateUsHandler : MonoBehaviour
{
  
    public GameObject Hand,RatePannel;
    public GameObject ReviewObject;
    public GameObject[] AllStars;
    // Start is called before the first frame update
    public void FillImage(float fillAmount) {
    
      for (int i = 0; i < fillAmount*5; i++)
      {
          AllStars[i].SetActive(true);
      }
      PlayerPrefs.SetInt("RateUsStatus", 1);
      //firebasecall.Instance.Event("RateUs_Stars_"+fillAmount*5);
        if (fillAmount >= 0.8f)
        {
            ReviewObject.SetActive(true);
            PrefsManager.SetProfileFill(1);
            UiManagerObject.instance.ShowGamePlay();
         
        //   firebasecall.Instance.Event("RateUs_Pannel_Opened");
            RatePannel.SetActive(false);
            PrefsManager.SetProfileFill(1);
            UiManagerObject.instance.ShowGamePlay();
        }
        else {
            LetterClick();
            RatePannel.SetActive(false);
            PrefsManager.SetProfileFill(1);
            UiManagerObject.instance.ShowGamePlay();
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
