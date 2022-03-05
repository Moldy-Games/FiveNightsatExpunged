using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLight : Powered
{
    Light[] mainLight;
    public GameObject ui;
    public override void OnOutage()
    {
        for (int i = 0; i < mainLight.Length; i++)
        {
            mainLight[i].enabled = false;
        }
        enabled = false;
    }

    void Start()
    {
        mainLight = GetComponentsInChildren<Light>();
        PowerManager.Instance.UsePower(this);
    }
}
