using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsLoaderAndSaver : MonoBehaviour
{
    public bool postProOn = false;
    public float mouseSens = 1;

    [SerializeField] Slider mouseSensSlider;
    [SerializeField] Toggle postToggle;

    public void Start()
    {
        mouseSens = PlayerPrefs.GetFloat("MouseSens");
        if(PlayerPrefs.GetInt("PostProcessing") == 0)
        {
            postProOn = false;
        }
        else
        {
            postProOn = true;
        }
        mouseSensSlider.value = mouseSens;
        postToggle.isOn = postProOn;
    }

    private void Update()
    {
        postProOn = postToggle.isOn;
        mouseSens = mouseSensSlider.value;
    }

    public void SaveValues()
    {
        if(postToggle.isOn)
        {
            PlayerPrefs.SetInt("PostProcessing", 1);
        }
        else
        {
            PlayerPrefs.SetInt("PostProcessing", 0);
        }
        PlayerPrefs.SetFloat("MouseSens", mouseSens);
    }
}
