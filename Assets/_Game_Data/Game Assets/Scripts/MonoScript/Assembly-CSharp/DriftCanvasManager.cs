using UnityEngine;
using UnityEngine.UI;

public class DriftCanvasManager : MonoBehaviour
{
	[Header("CONFIG")]
	public GameObject canvasWheelAll;
	public Image imgWheel;
	public Text textFactor;
	public Text textPoint;
	public Text textPointEnd;
	//public GameObject Effect;
	public Text totalCoins;
	public AudioSource audioCoin;
	public AudioSource audio4x;
	public GameObject FreeFlight;
	private Animator animPointEnd;
	private int StepDrawCoins;
	private float lastCoins;
	private float lastCoinsCount;
	public static int COINS = 0;
	//public AudioClip[] sounds;
	private void Awake()
	{
		canvasWheelAll.SetActive(false);
		textPoint.text = string.Empty;
		textFactor.text = string.Empty;
		textPointEnd.gameObject.SetActive(false);
//		Effect.gameObject.SetActive(false);
		COINS = PrefsManager.GetCoinsValue();
		totalCoins.text = Utils.FormatSpaceNumber(PrefsManager.GetCoinsValue());
		animPointEnd = textPointEnd.GetComponent<Animator>();
	//	imgWrongWay.SetActive(false);
		StopRace();
	}

	private void Update()
	{
		if (StepDrawCoins == 0)
		{
			return;
		}
		if (StepDrawCoins == 1)
		{
			lastCoinsCount += Time.deltaTime;
			if (lastCoinsCount > 1.3f)
			{
				lastCoinsCount = 0f;
				StepDrawCoins = 2;
				if (audioCoin.isActiveAndEnabled)
				{
					audioCoin.Play();
				}
			}
		}
		else if (StepDrawCoins == 2 && lastCoins < (float)COINS)
		{
			lastCoinsCount += Time.deltaTime * 1.8f;
			float f = Mathf.Lerp(lastCoins, COINS, lastCoinsCount);
			if (lastCoinsCount >= 1f)
			{
				f = COINS;
				StepDrawCoins = 0;
			}
			totalCoins.text = Utils.FormatSpaceNumber(Mathf.FloorToInt(f));
		}
	}

	public void UpdateWheel(float delaydrift)
	{
		imgWheel.fillAmount = Mathf.Clamp01(delaydrift);
		if (delaydrift >= 1f)
		{
			imgWheel.rectTransform.Rotate(0f, 0f, -500f * Time.deltaTime);
		}
	}
	
	public void UpdatePoint(float point)
	{
		if (point == 0f)
		{
			textPoint.text = string.Empty;
		}
		else 
		{
			textPoint.text = point.ToString("F0");
		}
	
	}

		public void UpdateFactor(int factor,int pointfactor)
		{
			if (factor == 0)
			{
				textFactor.text = string.Empty;
				return;
			}
			textFactor.text = "x"+ factor + "."+ pointfactor  ;
			if (factor > 1 && base.isActiveAndEnabled)
			{
				//textFactor.GetComponent<Animator>().Play(0);
				if (factor == 4 && audio4x.isActiveAndEnabled)
				{
					audio4x.Play();
				}
			}
		}

	public void UpdatePointEnd(bool ok)
	{
		textPointEnd.GetComponent<AutoDesactive>().SetAutoDesactivate(3f);
		textPointEnd.gameObject.SetActive(true);
		//Effect.SetActive(true);
		textPointEnd.text = textPoint.text;
		if (GetComponent<AudioSource>().isActiveAndEnabled)
		{
			if (ok)
			{
				animPointEnd.Play("PointGood");
				GetComponent<AudioSource>().Play();
			}
			else
			{
				animPointEnd.Play("PointBad");
			}
		}
	}

	public void UpdatePointFreeFlight(int point)
	{
		lastCoinsCount = 0f;
		lastCoins = PrefsManager.GetCoinsValue();
		COINS = PrefsManager.GetCoinsValue();
		int num = Mathf.FloorToInt((float)point * 1.5f);
		COINS += num;
		totalCoins.text = Utils.FormatSpaceNumber(Mathf.FloorToInt(lastCoins)) + " <color=yellow><size=24>+" + num + "</size></color>";
		StepDrawCoins = 1;
		PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue()+num);
		//PlayerPrefs.SetInt("COINSEARNED", COINS);
		//PlayerPrefs.Save();
	}

	private void UpdateCoinsTotal()
	{
		totalCoins.text = Utils.FormatSpaceNumber(Mathf.FloorToInt(COINS));
	}

	public void StopRace()
	{
		if ((bool)FreeFlight)
		{
			FreeFlight.SetActive(true);
		}
		UpdateCoinsTotal();
	}

	public void updateDrift(float point)
	{
		
	}


}
