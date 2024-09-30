using System;
using UnityEngine;
using UnityEngine.UI;

public class CharcterSlectionManger : MonoBehaviour
{

    public static CharcterSlectionManger Caller;
    public GameObject[] sp_bar;
    public Specification[] SpecificationValue;
    public GameObject[] gameObjects; // Array of game objects to be sold
    public Button[] Chracterbutton; // Array of buttons to purchase game objects
    public GameObject noCoinsPanel;  // Panel to display when there are no coins
    public GameObject Sucecfully;  // Panel to display when there are no coins
    public int[] gameObjectPrices;   // Prices for each game object

    public int coins;
    public Text PriceText;
    public GameObject Play, unlockPlayerButton,TestDriveButton;

    public Transform StartPos;
    
    public Sprite[] ChrakterName;
    public Sprite[] Profileimages;

    public Image namebar;
    public Image[] ProfileImage;
    private void Awake()
    {
        Caller = this;
    }

    void Start()
    {
        coins = PrefsManager.GetCoinsValue();
        Onclick(selectedPlayerValue);
    }



    public void Onclick(int Value)
    {
         SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
         selectedPlayerValue=Value;
         ShowPlayerNow(selectedPlayerValue);
    } 
    
    
    
    
    
    
    
    public void OnNextPressed()
    {
        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
      
        if (selectedPlayerValue < gameObjects.Length - 1)
        {
            selectedPlayerValue++;
            
            ShowPlayerNow(selectedPlayerValue);
          //  purchaseButtons[selectedPlayerValue].gameObject.SetActive(true);
           // CarIconImage.sprite = CarSprites[selectedPlayerValue];
        }

    }
   public int selectedPlayerValue = 0;
    public void OnBackPressed()
    {
        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
        if (selectedPlayerValue > 0)
        {
            selectedPlayerValue--;
            ShowPlayerNow(selectedPlayerValue);
           // purchaseButtons[selectedPlayerValue].gameObject.SetActive(true);
          //  CarIconImage.sprite = CarSprites[selectedPlayerValue];
        }

    }




    public GameObject CurrentPlayer;
    
    public void ShowPlayerNow(int val)
    {
        
        ActivePlayervalue = val;
        PriceText.text = gameObjectPrices[val] + "";
        if (PrefsManager.GetCracterState(val) == 0 && val != 0)
        {
            PriceText.transform.parent.gameObject.SetActive(true);
            for (int i = 0; i <Chracterbutton.Length; i++)
            {
                if (PrefsManager.GetCracterState(i) == 1)
                {
                    Chracterbutton[i].transform.GetChild(1).gameObject.SetActive(false);
                }
            }
            Play.SetActive(false);
            unlockPlayerButton.SetActive(true);
            TestDriveButton.SetActive(true);
        }
        else
        {
           
            if (PrefsManager.GetCracterState(val) == 1)
            {
                Chracterbutton[val].transform.GetChild(1).gameObject.SetActive(false);
            }
            
            PriceText.transform.parent.gameObject.SetActive(false);
            Play.SetActive(true);
            unlockPlayerButton.SetActive(false);
            TestDriveButton.SetActive(false);

        }
        for (int i = 0; i < sp_bar.Length; i++)
        {
            sp_bar[i].GetComponent<filled>().SetEndpoint(SpecificationValue[val].Values[i]);
        }
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].SetActive(false);
        }
        PrefsManager.SetSelectedCracterValue(val);
        CurrentPlayer = gameObjects[val];
        CurrentPlayer.SetActive(true);
        /*for (int i = 0; i < Chracterbutton.Length; i++)
        {
            Chracterbutton[i].transform.GetChild(1).gameObject.SetActive(true);
        }*/
        CurrentPlayer.transform.position = StartPos.transform.position;
        CurrentPlayer.transform.rotation = StartPos.transform.rotation;
        selectedPlayerValue = ActivePlayervalue;
        foreach (var Button in Chracterbutton)
        {
            Button.transform.GetChild(0).gameObject.SetActive(false);
        }
        Chracterbutton[selectedPlayerValue].transform.GetChild(0).gameObject.SetActive(true);
        namebar.sprite = ChrakterName[selectedPlayerValue];
        foreach (var Proimage in ProfileImage)
        {
            Proimage.sprite = Profileimages[selectedPlayerValue];
        }
        _CameraRotate.SetChracterPos();
    }

    public CameraRotate _CameraRotate;
    public int ActivePlayervalue;
    
    public void UnlockSelectedDog()
    {
        UnlockDog(gameObjectPrices[ActivePlayervalue]);
    }
    public void UnlockDog(int dogVal)
    {
        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
        if (PrefsManager.GetCoinsValue() >= dogVal)
        {
            PrefsManager.SetCrackterState(ActivePlayervalue, 1);
            PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue() - dogVal);
            Chracterbutton[ActivePlayervalue].transform.GetChild(1).gameObject.SetActive(false);
            Sucecfully.SetActive(true);
            PrefsManager.SetLastcharcterUnlock(ActivePlayervalue);
            Play.SetActive(true);
            unlockPlayerButton.SetActive(false);
            TestDriveButton.SetActive(false);
           Invoke("HideNoCoinsPanel",2f);
        }
        else
        {
            noCoinsPanel.SetActive(true);
        }
    }
    public void TestChracter()
    {
        
     //   lockSprite.SetActive(false);
        PriceText.transform.parent.gameObject.SetActive(false);
        Play.SetActive(true);
        unlockPlayerButton.SetActive(false);
        TestDriveButton.SetActive(false);
        PrefsManager.SetSelectedCracterValue(selectedPlayerValue);
    }
    
    public void SelectDogPlay()
    {
        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
        PlayerSelection.instance.fakeLoading.SetActive(true);
        PlayerSelection.instance.CarSlection.SetActive(true);
    }
    void HideNoCoinsPanel()
    {
        Sucecfully.SetActive(false);
      //  noCoinsPanel.SetActive(false);
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        PlayerPrefs.SetInt("Coins", coins);
    }
}
