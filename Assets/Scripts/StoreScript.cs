using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class StoreScript : MonoBehaviour
{
    public int[] numberOfvideo;
    public int[] reward;
    public int[] InAppreward;

    public Text[] watchvideoCount;
   // public Text[] TotalWatchVideoText;
    public Text[] rewardText;
    public Text money, cashtext;
    public GameObject erroPanel;
    int CurrentVideoNo, nextvideoNo;
    public static StoreScript instance;
    void Start()
    {
        money.text = PrefsManager.GetCoinsValue() + "";
        nextvideoNo = 1;
        instance = this;
        for (int i = 0; i < numberOfvideo.Length; i++)
        {
            watchvideoCount[i].text = PlayerPrefs.GetInt("GetWatchVideo" + nextvideoNo) + " / " + numberOfvideo[i];
            //TotalWatchVideoText[i].text = " / " + numberOfvideo[i];
            nextvideoNo++;
        }
        for (int i = 0; i <= reward.Length - 1; i++)
        {
            rewardText[i].text = reward[i] + "";
        }
    }


    public void WatchStatus()
    {
        int count = PlayerPrefs.GetInt("GetWatchVideo" + CurrentVideoNo);
        count += 1;
        PlayerPrefs.SetInt("GetWatchVideo" + CurrentVideoNo, count);
        print("Watch Status Called");

        if (PlayerPrefs.GetInt("GetWatchVideo" + CurrentVideoNo) == numberOfvideo[CurrentVideoNo])
        {
            int money = PrefsManager.GetCoinsValue();
            money += reward[CurrentVideoNo];
            PrefsManager.SetCoinsValue(money);

            PlayerPrefs.SetInt("GetWatchVideo" + CurrentVideoNo, 0);
        }
        watchvideoCount[CurrentVideoNo].text = PlayerPrefs.GetInt("GetWatchVideo" + CurrentVideoNo)  +" / " + numberOfvideo[CurrentVideoNo];
        money.text = PrefsManager.GetCoinsValue() + "";
        cashtext.text = PrefsManager.GetCoinsValue() + "";
    }


    public void InAppBuutonClick(int id)
    {
        InApp(id, InAppreward[id]);
    }

    public void RemoveAds()
    {
       // MyIAPManager_GF.instance.RemoveAds();
    }
    public void UnlockAllVehicles()
    {
       // MyIAPManager_GF.instance.UnlockAllVehicles();
    }
    public void UnlockAllVehiclesAndLevels()
    {
      //  MyIAPManager_GF.instance.UnlockAllVehicles();

    }
    public void InApp(int id, int coins)
    {
       // MyIAPManager_GF.instance.CashInApp(id, coins);
    }
    public void UpdateCashText(int newCoins)
    {
        int cash = PrefsManager.GetCoinsValue();
        cash += newCoins;
        PrefsManager.SetCoinsValue(cash);


        money.text = cash + "";
    }

    private void Update()
    {
        money.text = PrefsManager.GetCoinsValue() + "";
    }
    public void ShowRewardedAd(int no)
    {
        CurrentVideoNo = no;
        Data.AdType = 6;
     //  AdCalls.instance.ShowRewarded();
       //AdmobAdsManager.Instance.ShowRewardVideo();
    }

  

}
