using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PostProcessingObjecrt : MonoBehaviour
{
    private void Start()
    {
        var stream = File.Open(Application.dataPath + "/settings.dave", FileMode.Open);
        var reader = new BinaryReader(stream, System.Text.Encoding.UTF8, false);
        gameObject.SetActive(reader.ReadBoolean());
    }
}
