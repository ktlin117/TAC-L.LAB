using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private const float Y_ANGLE_MIN = 0.0f;
    private const float Y_ANGLE_MAX = 50.0f;

    public Transform lookAt;
    public Transform camTransform;
    public float distance = 5.0f, maxDist = 15.0f, minDist = 1.0f;

    private float currentX = 0.0f;
    private float currentY = 45.0f;
    private float sensitivityX = 9.0f;
    private float sensitivityY = 6.0f;

    public bool invertedControls;

    private void Start()
    {
        camTransform = transform;
    }

    private void Update()
    {
        currentX += Input.GetAxis("Mouse X") * sensitivityX;

        if (invertedControls)
            currentY += Input.GetAxis("Mouse Y") * sensitivityY;
        else
            currentY -= Input.GetAxis("Mouse Y") * sensitivityY;

        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);

        float d = Input.GetAxis("Mouse ScrollWheel") * 5;
        float newDist = distance - d;
        if (newDist >= minDist && newDist <= maxDist) {
            distance = newDist;
        }
        else {
            d = newDist = distance;
        }
    }

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = lookAt.position + rotation * dir;
        camTransform.LookAt(lookAt.position);
    }

    public float getCameraX() {
        return currentX;
    }
}
