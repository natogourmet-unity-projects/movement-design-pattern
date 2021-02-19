using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private void Start()
    {
        EventManager.instance.onLeverTrigger += OpenDoor;
    }

    public void OpenDoor()
    {
        transform.position += Vector3.up * 5;
    }
}