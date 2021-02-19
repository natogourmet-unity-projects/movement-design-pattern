using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingController : MonoBehaviour, IThrowingHandler
{
    [Header("Throw")]
    [SerializeField] public float throwingStrenght;

    [SerializeField] public Transform throwingPos;

    [SerializeField] public GameObject throwableObject;

    public void OnThrow(float x, float y)
    {
        GameObject _go = Instantiate(throwableObject, throwingPos.position, Quaternion.identity);
        Vector3 _dir = new Vector3(x, y, 0);
        _go.GetComponent<Rigidbody>().velocity = _dir * throwingStrenght;
    }

}
