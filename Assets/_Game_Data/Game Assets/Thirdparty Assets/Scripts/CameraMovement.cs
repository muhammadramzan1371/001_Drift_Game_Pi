using System;
using UnityEngine;
using System.Collections;
using System.Threading.Tasks;

public class CameraMovement : MonoBehaviour
{
    private Vector3 originalPosition;
    public PlayerCamera_New TpsCamera;
    public GameObject[] TpsPlayer;
    public SlectionManger slc_;
    private void Awake()
    {
       TpsCamera = GetComponent<PlayerCamera_New>();
      // slc_.TpsPlayervalue = TpsPlayervalue;
      TpsPlayer[slc_.TpsPlayervalue].GetComponent<ThirdPersonUserControl>().enabled = true;
    }

  
    public void UpdateCamera()
    {
        TpsCamera.target.GetComponent<Rigidbody>().isKinematic = true;
        TpsCamera.enabled = false; 
        GetComponent<CameraTransition>().targetTransform = TpsCamera.target.GetComponent<TpsTriger>().Hight;
        GetComponent<CameraTransition>().Pos();
        UiManagerObject.instance.fadeimage.SetActive(true);
        Invoke("nextChracter",3.5f);
        GetComponent<LookAtTargetSetter>().SetTarget(TpsCamera.target.gameObject);
    }


    public async void nextChracter()
    {
        GetComponent<CameraTransition>().duration = 3.5f;
        TpsPlayer[slc_.TpsPlayervalue].SetActive(true);
        TpsPlayer[slc_.TpsPlayervalue].GetComponent<Rigidbody>().isKinematic = false;
        TpsCamera.target = TpsPlayer[slc_.TpsPlayervalue].transform;
        GameManager.Instance.TPSPlayer= TpsPlayer[slc_.TpsPlayervalue];
        GetComponent<LookAtTargetSetter>().SetTarget(TpsPlayer[slc_.TpsPlayervalue]);
        GetComponent<CameraTransition>().Pos();
        
        GetComponent<CameraTransition>().targetTransform= TpsCamera.target;
        TpsCamera.cameraDistance = TpsCamera.target.GetComponent<TpsTriger>().cameraDistance;
        TpsCamera.targetOffset.y= TpsCamera.target.GetComponent<TpsTriger>().targetOffset.y ;
        TpsCamera.enabled = true;
        UiManagerObject.instance.panels.TpsControle.GetComponent<CanvasGroup>().alpha = 1;
        
       LevelManager.instace.Tpscamera.transform.position = TpsCamera.target.transform.position;
       LevelManager.instace.Tpscamera.transform.rotation = TpsCamera.target.transform.rotation;
        foreach (var tps in TpsPlayer)
        {
            tps.GetComponent<ThirdPersonUserControl>().enabled = false;
        }
        TpsPlayer[slc_.TpsPlayervalue].GetComponent<ThirdPersonUserControl>().enabled = true;
        await Task.Delay(500);
        UiManagerObject.instance.fadeimage.SetActive(false);
    }
}
