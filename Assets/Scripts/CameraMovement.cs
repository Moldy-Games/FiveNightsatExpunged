using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CameraMovement : MonoBehaviour
{
    public float cameraSpeed;
    private float rotationValue, rotValueClamped;

    private void Start()
    {
        using(var stream = File.Open(Application.dataPath + "/settings.dave", FileMode.Open))
        {
            using(var reader = new BinaryReader(stream, System.Text.Encoding.UTF8, false))
            {
                cameraSpeed = reader.ReadSingle();
            }
        }
    }
    void Update()
    {
        if(!FindObjectOfType<Monitor>().camerasOpen && !GameManager.Instance.GameOver)
        {
            rotationValue += cameraSpeed * Input.GetAxis("Mouse X") * Time.deltaTime;
            rotValueClamped = Mathf.Clamp(rotationValue, -65f, 65f);
            transform.eulerAngles = new Vector3(0, rotValueClamped, 0);
        }
    }
}
