using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamButton : MonoBehaviour
{
    public int camNumber;
    public RenderTexture cam;
    public RawImage camImage;
    public Monitor monitor;
    public Camera associatedCam;
    public void SwitchCam()
    {
        camImage.texture = cam;
        monitor.currentCam = camNumber;
        monitor.currentCamera = associatedCam;
    }
}
