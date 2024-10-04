using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
//using System.Security.Policy;

public class CheckpointController : MonoBehaviour
{
    
    public Checkpoint[] CheckpointsList;
    private Checkpoint CurrentCheckpoint;
    private int CheckpointId;

    
    void Start ()
    {
        if (CheckpointsList.Length==0) return;

        for (int index = 0; index < CheckpointsList.Length; index++)
            CheckpointsList[index].gameObject.SetActive(false);

        CheckpointId = 0;
        SetCurrentCheckpoint(CheckpointsList[CheckpointId]);
    }
   
  

    private void SetCurrentCheckpoint(Checkpoint checkpoint)
    {
        if (CurrentCheckpoint != null)
        {
            CurrentCheckpoint.gameObject.SetActive(false);
            CurrentCheckpoint.CheckpointActivated -= CheckpointActivated;
        }
        CurrentCheckpoint = checkpoint;
        CurrentCheckpoint.CheckpointActivated += CheckpointActivated;
        CurrentCheckpoint.gameObject.SetActive(true);
    }
    

    private void CheckpointActivated()
    {
        CheckpointId++;
        if (CheckpointId >= CheckpointsList.Length)
        {
            CurrentCheckpoint.gameObject.SetActive(false);
            CurrentCheckpoint.CheckpointActivated -= CheckpointActivated;
            UiManagerObject.instance.ShowComplete();
            return;
        }

        SetCurrentCheckpoint(CheckpointsList[CheckpointId]);
    }


}