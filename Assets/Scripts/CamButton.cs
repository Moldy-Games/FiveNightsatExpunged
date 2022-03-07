using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CamButton : MonoBehaviour
{
    public int camNumber;
    public RenderTexture cam;
    public RawImage camImage;
    public Monitor monitor;
    public Camera associatedCam;
    public Animator staticThingy;
    public TMP_Text camText;
    public string camLocation;
    public void SwitchCam()
    {
        camImage.texture = cam;
        monitor.currentCam = camNumber;
        monitor.currentCamera = associatedCam;
        camText.text = $"Cam {camNumber} - {camLocation}";
        staticThingy.SetTrigger("cameraSwitch"); 
    }
}
