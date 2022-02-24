using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monitor : Powered
{
    public Animator camFlipAnimator, staticAnimation;
    public bool camerasOpen, uiOpen;
    public Button camButton;

    public GameObject error, camUI;
    public int currentCam;
    public Camera currentCamera;

    public AudioSource flip;
    public override void OnOutage()
    {
        camButton.interactable = false;
        if (camerasOpen)
        {
            enabled = false;
        }
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void CameraButton()
    {
        camerasOpen = !camerasOpen;
        StartCoroutine(CameraOpen());
    }
    IEnumerator CameraOpen()
    {
        if(camerasOpen)
        {
            camFlipAnimator.SetTrigger("open");
            flip.Play();
            yield return new WaitForSeconds(0.15f);
            camFlipAnimator.SetBool("open", false);
            PowerManager.Instance.UsePower(this);
            yield return new WaitForSeconds(0.25f);
            camUI.SetActive(true);
            uiOpen = true;
        }
        else if(!camerasOpen)
        {
            camUI.SetActive(false);
            uiOpen = false;
            flip.Play();
            camFlipAnimator.SetTrigger("close");
            yield return new WaitForSeconds(0.15f);
            PowerManager.Instance.ReleasePower(this);
        }
    }
    public void ShowError()
    {
        staticAnimation.SetTrigger("transition");
    }
}
