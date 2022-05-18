using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeFloor : MonoBehaviour
{
    Rigidbody rb;

    bool rotate;

    Vector3 m_EulerAngleVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Invoke("StartRotate", 1f);
        m_EulerAngleVelocity = new Vector3(0, 15f, 0);
    }

    void StartRotate()
    {
        rotate = true;
    }

    void FixedUpdate()
    {
        if (rotate)
        {
            Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.fixedDeltaTime);
            rb.MoveRotation(deltaRotation * rb.rotation);
        }
    }
}
