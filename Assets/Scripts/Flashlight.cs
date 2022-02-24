using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : Powered
{
    Light flashlight;
    AudioSource clicking;
    public bool flashlightEnabled;
    public override void OnOutage()
    {
        flashlight.enabled = false;
        enabled = false;
    }
    void Start()
    {
        flashlight = GetComponent<Light>();
        clicking = GetComponent<AudioSource>();
    }
    void Update()
    {
        if(enabled)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                flashlight.enabled = true;
                clicking.Play();
                PowerManager.Instance.UsePower(this);
                flashlightEnabled = true;
            }
            else if (Input.GetKeyUp(KeyCode.Z))
            {
                flashlight.enabled = false;
                clicking.Play();
                PowerManager.Instance.ReleasePower(this);
                flashlightEnabled = false;
            }
        }
    }
}
