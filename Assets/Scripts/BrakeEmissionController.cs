using System;
using UnityEngine;

public class BrakeEmissionController : MonoBehaviour
{
    public RCC_CarControllerV3 carController; // Reference to the RCC car controller
    public Material emissionMaterial; // Reference to the material with emission property
    public float emissionIntensity = 1.0f; // Intensity of the emission when braking

    private bool isBraking = false;


 

    void Update()
    {
        if (carController.enabled)
        {
            // Check if the brake input is pressed
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
        // Ensure the material has an emission property
        if (emissionMaterial != null)
        {
            if (enable)
            {
                emissionMaterial.EnableKeyword("_EMISSION");
                emissionMaterial.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;
             //   emissionMaterial.SetColor("_EmissionColor", Color.red * emissionIntensity);
            }
            else
            {
                emissionMaterial.DisableKeyword("_EMISSION");
                emissionMaterial.globalIlluminationFlags = MaterialGlobalIlluminationFlags.EmissiveIsBlack;
            }
        }
    }
}