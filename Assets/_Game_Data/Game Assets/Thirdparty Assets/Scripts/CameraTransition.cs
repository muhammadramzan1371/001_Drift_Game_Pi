using UnityEngine;
using System.Collections;

public class CameraTransition : MonoBehaviour
{
    public Transform targetTransform; // The transform you want to move the camera to
    public float duration = 2.0f; // The duration of the transition

    public void Pos()
    {
        StartCoroutine(SmoothTransition());
    }


    private IEnumerator SmoothTransition()
    {
        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;
        Vector3 targetPosition = targetTransform.position;
        Quaternion targetRotation = targetTransform.rotation;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            yield return null;
        }
        transform.position = targetPosition;
        transform.rotation = targetRotation;
    }
}