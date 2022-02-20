using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float cameraSpeed;
    private float rotationValue, rotValueClamped;
    void Update()
    {
        rotationValue += cameraSpeed * Input.GetAxis("Mouse X");
        rotValueClamped = Mathf.Clamp(rotationValue, -50f, 50f);
        transform.eulerAngles = new Vector3(0, rotValueClamped, 0);
    }
}
