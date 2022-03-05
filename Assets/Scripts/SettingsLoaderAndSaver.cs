using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsLoaderAndSaver : MonoBehaviour
{
    public static bool postProOn = false;
    public static float mouseSens = 1;

    [SerializeField] Slider mouseSensSlider;
    [SerializeField] Toggle postToggle;

    private void Awake()
    {
        if (!File.Exists(Path.Combine(Application.dataPath, "settings.dave")))
        {
            using (var stream = File.Open(Application.dataPath + "/settings.dave", FileMode.Create))
            {
                using (var writer = new BinaryWriter(stream, Encoding.UTF8, false))
                {
                    writer.Write(true);
                    writer.Write(100f);
                }
            }
        }
        else
        {
            return;
        }
    }

    public void Start()
    {
        using(var stream = File.Open(Application.dataPath + "/settings.dave", FileMode.Open))
        {
            using(var reader = new BinaryReader(stream, Encoding.UTF8, false))
            {
                postProOn = reader.ReadBoolean();
                mouseSens = reader.ReadSingle();
            }
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
        using (var stream = File.Open(Application.dataPath + "/settings.dave", FileMode.Create))
        {
            using (var writer = new BinaryWriter(stream, Encoding.UTF8, false))
            {
                writer.Write(postToggle.isOn);
                writer.Write(mouseSensSlider.value);
            }
        }
    }
}
