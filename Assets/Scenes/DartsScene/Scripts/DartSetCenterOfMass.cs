using UnityEngine;

public class DartSetCenterOfMass : MonoBehaviour
{
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, 0.02f, 0);
    }
}
