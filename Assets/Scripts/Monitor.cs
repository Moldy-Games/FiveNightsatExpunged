using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monitor : Powered
{
    public Animator camFlipAnimator;
    public bool camerasOpen;
    public Button camButton;

    public GameObject error, camUI;
    public int currentCam;
    public Camera currentCamera;
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
            camFlipAnimator.SetBool("open", true);
            yield return new WaitForSeconds(0.15f);
            camFlipAnimator.SetBool("open", false);
            PowerManager.Instance.UsePower(this);
            yield return new WaitForSeconds(0.25f);
            camUI.SetActive(true);
        }
        else if(!camerasOpen)
        {
            camUI.SetActive(false);
            camFlipAnimator.SetBool("close", true);
            yield return new WaitForSeconds(0.15f);
            camFlipAnimator.SetBool("close", false);
            PowerManager.Instance.ReleasePower(this);
        }
    }
    public IEnumerator ShowError()
    {
        error.SetActive(true);
        yield return new WaitForSeconds(Random.Range(0.5f, 1f));
        error.SetActive(false);
    }
}
