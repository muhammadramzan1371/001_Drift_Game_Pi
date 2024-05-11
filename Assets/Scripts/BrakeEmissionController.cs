using System;
using UnityEngine;

public class BrakeEmissionController : MonoBehaviour
{
    public RCC_CarControllerV3 carController;
    public Material emissionMaterial; 
    
    private bool isBraking = false;
    

    void Update()
    {
        if (carController.enabled)
        {
            if (carController.brakeInput > 0)
            {
                isBraking = true;
                SetEmission(true);
            }
            else
            {
                if (isBraking)
                {
                    isBraking = false;
                    SetEmission(false);
                }
            }
        }
        else
        {
            return;
        }
    }

    void SetEmission(bool enable)
    {
       
        if (emissionMaterial != null)
        {
            if (enable)
            {
                emissionMaterial.EnableKeyword("_EMISSION");
                emissionMaterial.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;
            }
            else
            {
                emissionMaterial.DisableKeyword("_EMISSION");
                emissionMaterial.globalIlluminationFlags = MaterialGlobalIlluminationFlags.EmissiveIsBlack;
            }
        }
    }
}