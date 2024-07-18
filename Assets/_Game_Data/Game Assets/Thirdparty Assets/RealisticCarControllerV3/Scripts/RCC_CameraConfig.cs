//----------------------------------------------
//            Realistic Car Controller
//
// Copyright © 2015 BoneCracker Games
// http://www.bonecrackergames.com
//
//----------------------------------------------

using System;
using UnityEngine;
using System.Collections;

[AddComponentMenu("BoneCracker Games/Realistic Car Controller/Camera/Auto Camera Config")]
public class RCC_CameraConfig : MonoBehaviour {

    public bool automatic = true;
    private Bounds combinedBounds;
   // public Transform Target;

    public float distance = 25f;
    public float height = 25f;
    public float TpsPichAngle = 7;

    void Awake(){

		

    }





    public void Start()
    {
        LevelManager.instace.vehicleCamera.TPSDistance =distance;
        LevelManager.instace.vehicleCamera.TPSHeight=height;
        LevelManager.instace.vehicleCamera.TPSPitchAngle=TpsPichAngle;
      //  LevelManager_EG.instace.VehicleCamera.rotateView = new Vector3(height,0f,0f);
        // if (Target)
        // {
        //     LevelManager_EG.instace.VehicleCamera.playerCar = Target;
        // }else{
        LevelManager.instace.vehicleCamera.SetTarget(this.gameObject);
      //  }
    }

    public void SetCameraSettings () 
    {

        LevelManager.instace.vehicleCamera.TPSDistance = distance;
        LevelManager.instace.vehicleCamera.TPSHeight = height;
        LevelManager.instace.vehicleCamera.TPSPitchAngle=TpsPichAngle;
        // RCC_Camera cam = GameObject.FindObjectOfType<RCC_Camera>();
        //  
        // if(!cam)
        // 	return;
        // 	
        // cam.distance = distance;
        // cam.height = height;

    }
    
    public void SetCameraSettingsNow () 
    {
        LevelManager.instace.vehicleCamera.TPSDistance = distance;
        LevelManager.instace.vehicleCamera.TPSHeight = height;
        LevelManager.instace.vehicleCamera.TPSPitchAngle=TpsPichAngle;
        LevelManager.instace.vehicleCamera.SetTarget(this.gameObject);
    }

    public static float MaxBoundsExtent(Transform obj){
        // get the maximum bounds extent of object, including all child renderers,
        // but excluding particles and trails, for FOV zooming effect.

        var renderers = obj.GetComponentsInChildren<Renderer>();

        Bounds bounds = new Bounds();
        bool initBounds = false;
        foreach (Renderer r in renderers)
        {
            if (!((r is TrailRenderer)  || (r is ParticleSystemRenderer)))
            {
                if (!initBounds)
                {
                    initBounds = true;
                    bounds = r.bounds;
                }
                else
                {
                    bounds.Encapsulate(r.bounds);
                }
            }
        }
        float max = Mathf.Max(bounds.extents.x, bounds.extents.y, bounds.extents.z);
        return max;
    }

}