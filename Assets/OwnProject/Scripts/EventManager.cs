using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{

    #region Singleton
    public static EventManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    public event Action onLeverTrigger;
    public void LeverTrigger()
    {
        if (onLeverTrigger != null) onLeverTrigger();
    }
}