using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour, IExplosionHandler
{
    public void OnExplosion()
    {
        EventManager.instance.LeverTrigger();
    }
}