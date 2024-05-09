using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCarTrafficToPlayer : MonoBehaviour
{

    public GameObject TrafficCarCol,VehicleCarCol,VehicleCarWheel;
    public GameObject TrafficAiBody, VehicleCarBody;

    public void ChangeToTrafficAi()
    {
        TrafficCarCol.SetActive(true);
        TrafficAiBody.SetActive(true);
        VehicleCarBody.SetActive(false);
        VehicleCarCol.SetActive(false);
        VehicleCarWheel.SetActive(false);
    }


    public void ChangeToPlayer()
    {
        TrafficCarCol.SetActive(false);
        TrafficAiBody.SetActive(false);
        VehicleCarBody.SetActive(true);
        VehicleCarCol.SetActive(true);
        VehicleCarWheel.SetActive(true);
    }
}
