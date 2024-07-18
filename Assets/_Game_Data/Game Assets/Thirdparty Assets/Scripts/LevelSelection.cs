using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlayerInteractive_Mediation;
using UnityEngine;using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour {
	public GameObject[] Levels;
	public GameObject[] LevelContent;


	public GameObject Modes, errorOnMode, Desertmode, snowmode,loading;
	public Text alertobject;
	// Use this for initialization
	void Start () {



		//Levels[PrefsManager.GetLevelMode()].SetActive(true);
		

	}

	private void OnEnable()
	{
		

		Data.OnUnlockAllMission += UnlockAllLevels;
		// UnlockModes();
		Modes.SetActive(true);
		Levels[0].SetActive(false);
 
    }


	public void UnlockAllLevels() {

		for (int i = 0; i < LevelContent.Length; i++)
		{
			LevelContent[i].transform.GetChild(1).gameObject.SetActive(false);
			LevelContent[i].GetComponent<Button>().interactable = true;

		}
		//UnlockModes();
	}


   

	public void OnDisable()
	{


		Data.OnUnlockAllMission -= UnlockAllLevels;
	  
	}

	public void FreeMode()
	{
		SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
	//AdmobAdsManager.Instance.ShowInt(LoadGameNow,false);
	LoadGameNow();

	}

	public void LoadGameNow()
	{
		Modes.SetActive(false);
		PrefsManager.SetGameMode("free");
		loading.SetActive(true);
		//SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
		loading.GetComponentInChildren<bl_SceneLoader>().LoadLevel("GamePlay");
		if (PrefsManager.GetInterInt()!=5)
		{
			FindObjectOfType<Pi_AdsCall>().loadInterstitialAD();
		}
		Invoke(nameof(showInterAd),5f);
	}
	public GameObject AdBrakepanel;
	public async void showInterAd()
	{
		AdBrakepanel.SetActive(true);
		await Task.Delay(1000);
		if (FindObjectOfType<Pi_AdsCall>())
		{
			FindObjectOfType<Pi_AdsCall>().showInterstitialAD();
			
			PrefsManager.SetInterInt(1);
		}
		AdBrakepanel.SetActive(false);
	}
	public void ChallangeMode(int modselect)
	{
        //AdmobAdsManager.Instance.LoadInterstitialAd();
		SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
		if (modselect == 1)
		{
			ModeSelected(modselect);
			//if (PrefsManager.GetLevelLocking() > 10)
			//{
			//    ModeSelected(modselect);
			//}
			//else
			//{
			//    errorOnMode.SetActive(true);
			//    alertobject.text = "Please clear Mountain mode First.";
			//}
		}
		if (modselect == 2)
		{
			
            ModeSelected(modselect);
            //if (PrefsManager.GetLevelLocking() > 20)
            //{
            //	ModeSelected(modselect);
            //}
            //else
            //{
            //	errorOnMode.SetActive(true);

            //	alertobject.text = "Plese clear desert mode First.";
            //}
        }

		if (modselect == 0)
		{
			ModeSelected(modselect);
		}


	}

	public void ModeSelected(int modselect)
	{
		SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
		Modes.SetActive(false);
		PrefsManager.SetGameMode("challange");
		PrefsManager.SetLevelMode(modselect);


		Levels[0].SetActive(false);
		Levels[1].SetActive(false);
		Levels[PrefsManager.GetLevelMode()].SetActive(true);

	}

	public void UnlockModes()
	{

		if (PrefsManager.GetLevelLocking() > 10)
		{
			Desertmode.transform.GetChild(0).gameObject.SetActive(false);
		}
		if (PrefsManager.GetLevelLocking() > 20)
		{
			snowmode.transform.GetChild(0).gameObject.SetActive(false);
		}

	}




}
