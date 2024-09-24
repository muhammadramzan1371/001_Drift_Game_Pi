using UnityEngine;

public class MoveImageOnTilt : MonoBehaviour
{
    // Speed at which the image moves
    public float moveSpeed = 5f;

    // Update is called once per frame
    void Update()
    {
        // Get the current device orientation
        Vector3 tilt = Input.acceleration;

        // Check if the device is tilted to the left
        if (tilt.x < -0.5f)
        {
            MoveImage(-1); // Move image to the left
        }
        // Check if the device is tilted to the right
        else if (tilt.x > 0.5f)
        {
            MoveImage(1); // Move image to the right
        }
    }

    // Move the image based on direction
    void MoveImage(int direction)
    {
        // Calculate movement vector
        Vector3 moveDirection = Vector3.right * direction;

        // Move the image
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }
}
