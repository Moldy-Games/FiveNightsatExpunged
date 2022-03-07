using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PostProcessingObjecrt : MonoBehaviour
{
    private void Start()
    {
        if(PlayerPrefs.GetInt("PostProcessing") == 1)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
