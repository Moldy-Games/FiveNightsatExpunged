using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monitor : Powered
{
    public Animator camFlipAnimator, staticAnimation;
    public bool camerasOpen, uiOpen, inTrans;
    public Button camButton;

    public GameObject error, camUI;
    public int currentCam;
    public Camera currentCamera;

    public AudioSource flip;

    public List<GameObject> cameras = new List<GameObject>();
    public override void OnOutage()
    {
        camButton.interactable = false;
        if (camerasOpen)
        {
            camerasOpen = false;
            StartCoroutine(CameraOpen());
            enabled = false;
        }
    }
    void Start()
    {
        
    }
    void Update()
    {
        foreach (GameObject cam in cameras)
        {
            cam.SetActive(cam == currentCamera.gameObject);
        }
    }
    public void CameraButton()
    {
        if(!inTrans)
        {
            camerasOpen = !camerasOpen;
            StartCoroutine(CameraOpen());
        }
    }
    public IEnumerator CameraOpen()
    {
        if(camerasOpen)
        {
            GameManager.Instance.brob.SetActive(false);
            camFlipAnimator.SetTrigger("open");
            inTrans = true;
            flip.Play();
            PowerManager.Instance.UsePower(this);
            yield return new WaitForSeconds(0.25f);
            camUI.SetActive(true);
            inTrans = false;
            uiOpen = true;
        }
        else if(!camerasOpen)
        {
            GameManager.Instance.brobChance = Random.Range(1, 5000);
            PowerManager.Instance.ReleasePower(this);
            inTrans = true;
            camUI.SetActive(false);
            uiOpen = false;
            flip.Play();
            camFlipAnimator.SetTrigger("close");
            yield return new WaitForSeconds(0.25f);
            inTrans = false;
        }
    }
    public void ShowError()
    {
        staticAnimation.SetTrigger("transition");
    }
}
