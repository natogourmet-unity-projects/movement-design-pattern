using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] public float explosionTime;
    [SerializeField] public float explosionRadius;
    [SerializeField] private bool waitingForExplosion;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    private void OnCollisionEnter(UnityEngine.Collision other)
    {
        if (!waitingForExplosion) StartCoroutine(WaitToExplode());
    }

    IEnumerator WaitToExplode()
    {
        waitingForExplosion = true;
        yield return new WaitForSeconds(explosionTime);
        Explode();
    }

    private void Explode()
    {
        Collider[] _cols = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider _col in _cols)
        {
            IExplosionHandler _eh = _col.GetComponent<IExplosionHandler>();
            if (_eh != null) _eh.OnExplosion();
        }
        Destroy(gameObject);
    }


}