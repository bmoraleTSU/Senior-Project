using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{
    private float rotationSpeed = 1f;

    private void Update()
    {
        float horizontalInput = NewBehaviourScript.instance.joystick.Horizontal;
        //float verticalInput = NewBehaviourScript.instance.joystick.Vertical;

        // Calculate the rotation angles based on joystick input
        //float rotationX = verticalInput * rotationSpeed;
        float rotationY = horizontalInput * rotationSpeed;

        // Apply rotation to the object
        transform.Rotate(0f, rotationY, 0f, Space.Self);
    }
}






