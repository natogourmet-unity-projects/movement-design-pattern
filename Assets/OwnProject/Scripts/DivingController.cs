using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivingController : MonoBehaviour
{
    [SerializeField] public int timeUnderWater;
    [SerializeField] private bool underWater;

    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            underWater = true;
            StartCoroutine("DrowningCountdown");
        }

    }

    private void OnTriggerExit(UnityEngine.Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            underWater = false;
            StopCoroutine("DrowningCountdown");
        }
    }

    IEnumerator DrowningCountdown()
    {
        int count = 0;
        while (count < timeUnderWater)
        {
            print(count);
            yield return new WaitForSeconds(1);
            count++;
        }
        OnDrowning();
    }

    public void OnDrowning()
    {
        print("YIKES, U DROWNED");
    }
}