using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PowerManager : MonoBehaviour
{
    public static PowerManager Instance { get; private set; }

    float powerDrain;
    int objectsUsing;
    public float power { get; private set; }

    protected Powered[] poweredObjects;
    public TMP_Text powerText;
    public Slider usageMeter;

    public bool powerOut;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        power = 100;
        poweredObjects = FindObjectsOfType<Powered>();
    }
    private void Update()
    {
        power -= powerDrain * Time.deltaTime;
        if(power <= 0)
        {
            powerOut = true;
            power = 0;
            foreach(var poweredItem in poweredObjects)
            {
                poweredItem.OnOutage();
            }
            enabled = false;
        }
        powerText.text = $"Power: {power.ToString("0")}%";
        usageMeter.value = objectsUsing;
    }
    public void UsePower(Powered poweredObject)
    {
        powerDrain += poweredObject.usage;
        objectsUsing++;
    }
    public void ReleasePower(Powered poweredObject)
    {
        powerDrain -= poweredObject.usage;
        objectsUsing--;
    }
}
