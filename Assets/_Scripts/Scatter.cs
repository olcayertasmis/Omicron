using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scatter : MonoBehaviour
{
    private Collider[] hitColliders;

    [SerializeField]
    float blastRadius, explosionPower;

    [SerializeField]
    LayerMask explosionLayer;

    private void OnCollisionEnter(Collision other)
    {
        ExplosionWork(other.contacts[0].point);
    }

    void ExplosionWork(Vector3 explosionPoint)
    {
        hitColliders = Physics.OverlapSphere(explosionPoint, blastRadius, explosionLayer);

        foreach (Collider hitCol in hitColliders)
        {
            if (hitCol.GetComponent<Rigidbody>() != null)
            {
                hitCol.GetComponent<Rigidbody>().AddExplosionForce(explosionPower, explosionPoint, blastRadius, 0.1f, ForceMode.Impulse);
            }
        }
    }

}
