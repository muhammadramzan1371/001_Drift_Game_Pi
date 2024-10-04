using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PI_LevelProperties : MonoBehaviour
{

    public Transform TpsPosition;
    public Transform CarPosition;
    public GameObject[] LevelObjective;
    public string LevelStatment;
    public float Time;
    public int currentobjective,LevelReward = 0;
    public bool freemode;
    
    
    public void Nextobjective()
    {
        currentobjective++;
        if (currentobjective >= LevelObjective.Length)
        {
            UiManagerObject.instance.ShowComplete();
        }
    }
    
    
    
    void OnEnable()
    {
        if (freemode)
        {
            reshuffle(LevelObjective);
        }

        if (LevelObjective.Length < 1)
        {
            return;
        }
        LevelObjective[currentobjective].SetActive(true);
    }

    
    void reshuffle(GameObject[] texts)
    {
        for (int t = 0; t < texts.Length; t++)
        {
            GameObject tmp = texts[t];
            int r = Random.Range(t, texts.Length);
            texts[t] = texts[r];
            texts[r] = tmp;
        }
    }
}
