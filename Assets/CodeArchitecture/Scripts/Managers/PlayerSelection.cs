using UnityEngine;
using System.Collections;
using System.Threading.Tasks;
using PlayerInteractive_Mediation;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
[System.Serializable]
public class Specification
{
    public int[] Values;
}

public class PlayerSelection : MonoBehaviour
{

    public Specification[] SpecificationValue;
    public GameObject[] sp_bar;
    public int[] Prices;
    public int selectedPlayerValue = 0;
    public GameObject[] selectedDogArray;

    public GameObject nextBtn,
        backBtn,
        menuCanvas,
        CarSlection,
        levelSelectionCanvas,
        lockSprite,
        LOADING,
        Play,
        notCash,
        successPannel,
        unlockPlayerButton,
        TestDriveButton,
        MainNextBack;

    public Text PriceText;
    bool isReadyForPurchase;
    public int ActivePlayervalue = 1;
    int coinValue;
    public static PlayerSelection instance;
    public Transform CarPos;
    public CameraRotate _CameraRotate;
    public Animator CarChangeAnimator;

    [Header("CarSoldCutSSceane")] public GameObject Timeline;
    public PlayableDirector Director;
    public GameObject CutSceanCamera;
    public CanvasGroup GrageUi;
    public GameObject MainCamera;
    public Image[] CarSprites;
    public Image CarIconImage;

    void Start()
    {
        instance = this;
        Time.timeScale = 1;
    }

    public void OnNextPressed()
    {
        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
        CarChangeEffect();
        if (selectedPlayerValue < selectedDogArray.Length - 1)
        {
            selectedPlayerValue++;
            ShowPlayerNow(selectedPlayerValue);
            for (int i = 0; i < CarSprites.Length; i++)
            {
                CarSprites[i].gameObject.SetActive(false);
                CarSprites[selectedPlayerValue].gameObject.SetActive(true);
            }
            ActivePlayervalue = selectedPlayerValue;
        }
    }

    public void OnBackPressed()
    {
        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
        CarChangeEffect();
        if (selectedPlayerValue > 0)
        {
            selectedPlayerValue--;
            ShowPlayerNow(selectedPlayerValue);
            for (int i = 0; i < CarSprites.Length; i++)
            {
                CarSprites[i].gameObject.SetActive(false);
                CarSprites[selectedPlayerValue].gameObject.SetActive(true);
            }
            ActivePlayervalue = selectedPlayerValue;
        }
    }

    public void CarChangeEffect()
    {
        CarChangeAnimator.Play("CarChangeAnim",0,0f);
        _CameraRotate.ChangeCameraViewForCar();
    }


    GameObject CurrentPlayer = null;
    

    public void ShowPlayerNow(int val)
    {
        ActivePlayervalue = val;
        PriceText.text = Prices[val] + "";
        if (PrefsManager.GetPlayerState(val) == 0 && val != 0)
        {
            PriceText.transform.parent.gameObject.SetActive(true);
            lockSprite.SetActive(true);
            Play.SetActive(false);
            unlockPlayerButton.SetActive(true);
            TestDriveButton.SetActive(true);
        }
        else
        {
            lockSprite.SetActive(false);
            PriceText.transform.parent.gameObject.SetActive(false);
            Play.SetActive(true);
            unlockPlayerButton.SetActive(false);
            TestDriveButton.SetActive(false);
        }

        for (int i = 0; i < selectedDogArray.Length; i++)
        {
            selectedDogArray[i].SetActive(false);
        }
        PrefsManager.SetSelectedPlayerValue(val);
        CurrentPlayer = selectedDogArray[val];
        CurrentPlayer.SetActive(true);
        for (int i = 0; i < sp_bar.Length; i++)
        {
            sp_bar[i].GetComponent<filled>().SetEndpoint(SpecificationValue[val].Values[i]);
        }

        if (ActivePlayervalue == selectedDogArray.Length - 1)
        {
            nextBtn.SetActive(false);
            backBtn.SetActive(true);
        }
        else if (ActivePlayervalue == 0)
        {
            nextBtn.SetActive(true);
            backBtn.SetActive(false);
        }
        else
        {
            nextBtn.SetActive(true);
            backBtn.SetActive(true);
        }
        selectedPlayerValue = ActivePlayervalue;
        CurrentPlayer.transform.position = CarPos.transform.position;
        CurrentPlayer.transform.rotation = CarPos.transform.rotation;
        _CameraRotate.SetCarMianPos();
    }

    public void TestDrive()
    {
        lockSprite.SetActive(false);
        PriceText.transform.parent.gameObject.SetActive(false);
        Play.SetActive(true);
        unlockPlayerButton.SetActive(false);
        TestDriveButton.SetActive(false);
        PrefsManager.SetSelectedPlayerValue(selectedPlayerValue);
        SelectDogPlay();
    }

    public void Ps_PlayEvent()
    {
        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
        GoToLevelSelect();
    }

    public void GoToLevelSelect()
    {
        for (int i = 0; i < selectedDogArray.Length; i++)
        {
            selectedDogArray[i].SetActive(false);
        }
        SelectDogPlay();
    }


    public void SelectDogPlay()
    {
        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
        fakeLoading.SetActive(true);
        levelSelectionCanvas.SetActive(true);
    }

    public void ForTutorialPlay()
    {
        levelSelectionCanvas.SetActive(true);
        menuCanvas.SetActive(false);
        Logger.ShowLog("Enable Here");
        LOADING.SetActive(true);
        PrefsManager.SetGameMode("challange");
        PrefsManager.SetCurrentLevel(1);
        PrefsManager.SetLevelMode(0);
    }

    public GameObject fakeLoading;
    
    public void SetstartPont()
    {
        ShowPlayerNow(PrefsManager.GetLastJeepUnlock());
        selectedPlayerValue = PrefsManager.GetLastJeepUnlock();
        for (int i = 0; i < CarSprites.Length; i++)
        {
            CarSprites[i].gameObject.SetActive(false);
            CarSprites[selectedPlayerValue].gameObject.SetActive(true);
        }
        CurrentPlayer.transform.position = CarPos.transform.position;
        CurrentPlayer.transform.rotation = CarPos.transform.rotation;
        _CameraRotate.SetCarMianPos();
        OffCamScript();
    }



    public void RedirectToCarSelection()
    {
        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
        fakeLoading.SetActive(true);
        menuCanvas.SetActive(false);
        CarSlection.SetActive(true);
        ShowPlayerNow(PrefsManager.GetLastJeepUnlock());
        MainNextBack.SetActive(true);
        selectedPlayerValue = PrefsManager.GetLastJeepUnlock();
        for (int i = 0; i < CarSprites.Length; i++)
        {
            CarSprites[i].gameObject.SetActive(false);
            CarSprites[selectedPlayerValue].gameObject.SetActive(true);
        }
        CurrentPlayer.transform.position = CarPos.transform.position;
        CurrentPlayer.transform.rotation = CarPos.transform.rotation;
        OnCamScript();
        _CameraRotate.SetCarMianPos();
    }

    
    public void BackToMainCanvas()
    {
        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
        CarSlection.SetActive(false);
        menuCanvas.SetActive(true);
        Logger.ShowLog("Enable Here");
        SetstartPont();
    }

    private void OffAnimator()
    {
        MainCamera.GetComponent<Animator>().enabled = false;
    }

    private void OnAnimator()
    {
        MainCamera.GetComponent<Animator>().enabled = true;
    }
    
    
    private void OffCamScript()
    {
        MainCamera.GetComponent<CameraRotate>().enabled = false;
    }

    private void OnCamScript()
    {
        MainCamera.GetComponent<CameraRotate>().enabled = true;
    }

    public void BackFromLevelScreen()
    {
        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
        levelSelectionCanvas.SetActive(false);
        CarSlection.SetActive(true);
        ShowPlayerNow(PrefsManager.GetSelectedPlayerValue());
        MainNextBack.SetActive(true);
        selectedPlayerValue = PrefsManager.GetSelectedPlayerValue();
    }

    public void SetLevelValue(int lValue)
    {
        LoadGamePlay();
        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
        PrefsManager.SetCurrentLevel(lValue);
    }

    public void LoadGamePlay()
    {
        LOADING.SetActive(true);
        LOADING.GetComponentInChildren<bl_SceneLoader>().LoadLevel("GamePlay");
        if (PrefsManager.GetInterInt() != 5)
        {
            FindObjectOfType<Pi_AdsCall>().loadInterstitialAD();
        }
        Invoke(nameof(showInterAd), 7f);
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


    public void UnlockSelectedDog()
    {
        UnlockDog(Prices[ActivePlayervalue]);
    }

    public void UnlockDog(int dogVal)
    {
        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
        if (PrefsManager.GetCoinsValue() >= dogVal)
        {
            PrefsManager.SetPlayerState(ActivePlayervalue, 1);
            PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue() - dogVal);
            Success_purchase();
            PrefsManager.SetLastJeepUnlock(ActivePlayervalue);
            Play.SetActive(true);
            unlockPlayerButton.SetActive(false);
            lockSprite.SetActive(false);
            TestDriveButton.SetActive(false);
        }
        else
        {
            notCash.SetActive(true);
        }
    }

    public void OffNoCash()
    {
        notCash.SetActive(false);
    }

    public void Offsuccess()
    {
        successPannel.SetActive(false);
    }

    public void Success_purchase()
    {
        Time.timeScale = 1f;
        GrageUi.alpha = 0;
        CutSceanCamera.SetActive(true);
        MainCamera.SetActive(false);
        Timeline.SetActive(true);
        Director.Play();
        Invoke("HideTimeline", (float)Director.duration - 0.9f);

    }



    public async void PlayCutScne()
    {
        Time.timeScale = 1f;
        if (FindObjectOfType<Pi_AdsCall>())
        {
            if (PrefsManager.GetInterInt() != 5)
            {
                FindObjectOfType<Pi_AdsCall>().loadInterstitialAD();
            }
        }

        if (FindObjectOfType<Pi_AdsCall>())
        {
            FindObjectOfType<Pi_AdsCall>().showInterstitialAD();
            PrefsManager.SetInterInt(1);
        }

        GrageUi.alpha = 0;
        CutSceanCamera.SetActive(true);
        MainCamera.SetActive(false);
        Timeline.SetActive(true);
        Director.Play();
        Invoke("StopCutScene", (float)Director.duration - 0.9f);
        await Task.Delay(2000);
        if (FindObjectOfType<Pi_AdsCall>())
        {
            if (PrefsManager.GetInterInt() != 5)
            {
                FindObjectOfType<Pi_AdsCall>().loadInterstitialAD();
            }
        }
    }


    public void StopCutScene()
    {
        GrageUi.alpha = 1;
        CutSceanCamera.SetActive(false);
        MainCamera.SetActive(true);
        Timeline.SetActive(false);
    }

    public void HideTimeline()
    {
        GrageUi.alpha = 1;
        CutSceanCamera.SetActive(false);
        MainCamera.SetActive(true);
        Timeline.SetActive(false);
        isReadyForPurchase = true;
        successPannel.SetActive(true);
        lockSprite.SetActive(false);
        unlockPlayerButton.SetActive(false);
        Play.SetActive(true);
        Invoke("Offsuccess", 3f);
        PriceText.transform.parent.gameObject.SetActive(false);
    }

}
