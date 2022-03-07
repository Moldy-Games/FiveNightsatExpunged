using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : Powered, Usable
{
    public float doorHeight, doorSpeed, baseHeight;
    public bool doorInUse = false, doorOpen = true;

    public AudioSource doorSound;

    public Transform door;
    IEnumerator OpenDoor()
    {
        PowerManager.Instance.ReleasePower(this);
        doorSound.Play();
        for (float amount = 0; amount < doorHeight; amount += doorSpeed * Time.deltaTime)
        {
            door.position = new Vector3(door.position.x, amount + baseHeight, door.position.z);
            yield return null;
        }
        door.position = new Vector3(door.position.x, doorHeight, door.position.z);
        doorInUse = false;
        doorOpen = true;
    }
    IEnumerator CloseDoor()
    {
        PowerManager.Instance.UsePower(this);
        doorSound.Play();
        for (float amount = doorHeight; amount > 0; amount -= doorSpeed * Time.deltaTime)
        {
            door.position = new Vector3(door.position.x, amount + baseHeight, door.position.z);
            yield return null;
        }
        door.position = new Vector3(door.position.x, baseHeight, door.position.z);
        doorInUse = false;
        doorOpen = false;
    }

    public void Use(RaycastHit hit)
    {
        if (!doorInUse && enabled)
        {
            doorInUse = true;
            if (doorOpen)
            {
                StartCoroutine(CloseDoor());
            }
            else
            {
                StartCoroutine(OpenDoor());
            }
        }
    }

    public override void OnOutage()
    {
        if(!doorOpen)
        {
            StartCoroutine(OpenDoor());
        }
        enabled = false;
    }
}
