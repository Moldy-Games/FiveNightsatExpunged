using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLight : Powered
{
    Light mainLight;
    public override void OnOutage()
    {
        mainLight.enabled = false;
        enabled = false;
    }

    void Start()
    {
        mainLight = GetComponent<Light>();
        PowerManager.Instance.UsePower(this);
    }
}
