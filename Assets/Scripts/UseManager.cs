using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Usable
{
    void Use(RaycastHit hit);
}
public class UseManager : MonoBehaviour
{
    public static UseManager Instance { get; private set; }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 20))
            {
                Usable temp = hit.collider.GetComponent<Usable>();
                if(temp != null)
                {
                    temp.Use(hit);
                }
            }
        }
    }
}
