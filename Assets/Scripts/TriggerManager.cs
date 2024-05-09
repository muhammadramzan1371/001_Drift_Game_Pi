/*
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class TriggerManager : MonoBehaviour
{

    public int point;

    public Text text;

    public ParticleSystem GoldEffectForcar;
    public bool TrafficCar = false;


    // Start is called before the first frame update
    void Start()
    {
       // text = GameUI.Instance.CheckpointCountText;
       // point=GameUI.Instance.point;
      //  text.text = point.ToString() + " / " + Save.save.TotalCheckPoint;
    }

    // Update is called once per frame
    void Update()
    {
      //  text.text = point.ToString() + " / " + Save.save.TotalCheckPoint;
    }

    private bool isSingle = false;

    private async void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fail" && !isSingle)
        {
            GameUI.Instance.ShowLevelFailed();
            isSingle = true;
        }
        if (other.gameObject.tag=="GoldBrick")
        {
            if (!TrafficCar)
            {
                Save.save.Gold++;
                Save.save.UpdateGold();
                Save.save.plus_Gold(transform);
                GoldEffectForcar.gameObject.SetActive(true);
                GoldEffectForcar.Play();
                other.gameObject.SetActive(false);
                await Task.Delay(10000);
                other.gameObject.SetActive(true);
            }
            // UnityEngine.Object.Destroy(base.gameObject);
        }
        if (other.gameObject.tag == "Finish" && !isSingle)
        {
            GameUI.Instance.ShowLevelComplete();
            isSingle = true;
        }
        if (base.gameObject.tag == "GoldTrigger" && other.gameObject.tag == "checkpoint")
        {
            point++;
            if (Save.save)
            {
                if (other.gameObject.name.Contains("CheckPoint"))
                {
                    if (Save.save.CheckPointEffect != null)
                    {
                        Save.save.CheckPointEffect.gameObject.SetActive(true);
                        Save.save.CheckPointEffect.Play();
                    }
                }
                else
                {
                    if (Save.save.coinEffect != null)
                    {
                        Save.save.coinEffect.gameObject.SetActive(true);
                        Save.save.coinEffect.Play();
                    }
                }
            }

            // if (text)
            // {
            //  text.text = point.ToString() + " / " + Save.save.TotalCheckPoint;
            // }
        }
    }




    public void Trig()
    {
        BoxCollider component = base.gameObject.GetComponent<BoxCollider>();
        Vector3 size = base.gameObject.GetComponent<BoxCollider>().size;
        component.size = new Vector3(2f, size.y, 10f);
        BoxCollider component2 = base.gameObject.GetComponent<BoxCollider>();
        Vector3 center = base.gameObject.GetComponent<BoxCollider>().center;
        float x = center.x;
        Vector3 center2 = base.gameObject.GetComponent<BoxCollider>().center;
        component2.center = new Vector3(x, center2.y, 5f);
    }

    public void TrigOff()
    {
        BoxCollider component = base.gameObject.GetComponent<BoxCollider>();
        Vector3 size = base.gameObject.GetComponent<BoxCollider>().size;
        component.size = new Vector3(0.7f, size.y, 0.7f);
        BoxCollider component2 = base.gameObject.GetComponent<BoxCollider>();
        Vector3 center = base.gameObject.GetComponent<BoxCollider>().center;
        float x = center.x;
        Vector3 center2 = base.gameObject.GetComponent<BoxCollider>().center;
        component2.center = new Vector3(x, center2.y, 0f);
    }

}
*/
