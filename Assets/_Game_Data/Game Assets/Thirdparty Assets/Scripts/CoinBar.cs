using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinBar : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<Text>().text = "$" + PrefsManager.GetCoinsValue();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = "$" + PrefsManager.GetCoinsValue();
    }
}
