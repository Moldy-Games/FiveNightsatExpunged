using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    Camera cam;
    Monitor monitor;
    private void Start()
    {
        monitor = FindObjectOfType<Monitor>();
    }
    void Update()
    {
        if(!monitor.uiOpen)
        {
            cam = Camera.main;
        }
        else if(monitor.uiOpen)
        {
            cam = monitor.currentCamera;
        }
        transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
        Vector3 eulerAngles = transform.eulerAngles;
        eulerAngles.x = 0;
        transform.eulerAngles = eulerAngles;
    }
}
