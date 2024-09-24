using System;
using UnityEngine;
using UnityEngine.Animations;

public class LookAtTargetSetter : MonoBehaviour
{
    public LookAtConstraint lookAtConstraint; // Reference to the LookAtConstraint component
    private CameraMovement _cameraMovement;


    private void Awake()
    {
        _cameraMovement = GetComponent<CameraMovement>();
    }

    public void AddSourceGameObject()
    {
        // Get the LookAtConstraint component if not assigned
        if (lookAtConstraint == null)
        {
            lookAtConstraint = GetComponent<LookAtConstraint>();
        }

        // Set the target for the LookAtConstraint
    }

    public void SetTarget(GameObject newTarget)
    {
        // Ensure the LookAtConstraint component is assigned
        if (lookAtConstraint != null)
        {
            // Clear any existing sources
            lookAtConstraint.RemoveSource(0);

            // Add a new source with the desired target
            ConstraintSource source = new ConstraintSource();
            source.sourceTransform = newTarget.transform;
            source.weight = 1f;
            lookAtConstraint.AddSource(source);

            // Apply the changes
            lookAtConstraint.constraintActive = true;
        }
        else
        {
            Debug.LogWarning("LookAtConstraint component is not assigned.");
        }
    }
}