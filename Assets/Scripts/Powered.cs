using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Powered : MonoBehaviour
{
    public float usage = 0;
    public abstract void OnOutage();
}
