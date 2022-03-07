using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class CameraMovement : MonoBehaviour
{
    public float cameraSpeed;
    private float yaw;

    private void Awake()
    {
        cameraSpeed = PlayerPrefs.GetFloat("MouseSens");
    }
    void Update()
    {
        if(!FindObjectOfType<Monitor>().camerasOpen && !GameManager.Instance.GameOver)
        {
            yaw = Mathf.Clamp(yaw + cameraSpeed * Input.GetAxis("Mouse X"), -65f, 65f);
            Vector3 direction = new Vector3(0, yaw, 0);
            Camera.main.transform.rotation = Quaternion.Euler(direction);
        }
    }
}
