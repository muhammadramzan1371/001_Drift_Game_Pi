using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CameraCulling : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera camera;
    float[] distances = new float[32];
    public float LargeDistance;
    public float MedDistance;
    public float SmallDistance;
    
    
    public float LowLargeDistance;
    public float LowMedDistance;
    public float LowSmallDistance;
    public float LowSmallCameraFar;
    
    public bool updatevalue;
    void Start()
    {
         camera = GetComponent<Camera>();


         if (SystemInfo.systemMemorySize <= 2560 || PrefsManager.GetGameQuality()==2)
         {
             distances[26] = LowLargeDistance;
             distances[27] = LowMedDistance;
             distances[28] = LowSmallDistance;
             camera.farClipPlane = LowSmallCameraFar;
         }
         else
         {
             distances[26] = LargeDistance;
             distances[27] = MedDistance;
             distances[28] = SmallDistance;
         }
         

        camera.layerCullDistances = distances;
    }



    public void Update()
    {
        if (updatevalue)
        {
            distances[26] = LargeDistance;
            distances[27] = MedDistance;
            distances[28] = SmallDistance;
            camera.layerCullDistances = distances;
            updatevalue = false;
        }
    }

}
