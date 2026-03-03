using UnityEngine;

public class DartCollision : MonoBehaviour
{
    public Transform tip; // assign the tip
    private Rigidbody rb;
    private bool hasStuck = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasStuck) return;

        DartZone zone = collision.gameObject.GetComponent<DartZone>();
        if (zone == null) return;

        // Check if tip hit first
        Vector3 dartDirection = rb.linearVelocity.normalized;
        Vector3 contactNormal = collision.contacts[0].normal;
        float dot = Vector3.Dot(dartDirection, contactNormal);
        Debug.Log($"Hit dot product: {dot}");

        if (dot < -0.7f)
        {
            hasStuck = true;

            // Freeze motion
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezeAll;

            // Move dart so tip touches board
            ContactPoint contact = collision.contacts[0];
            Vector3 tipDir = tip.right; // X+ is tip
            Vector3 tipOffset = tip.position - transform.position;
            transform.position = contact.point - tipOffset + tipDir * 0.001f; // tiny push for realistic stick

            // Parent to board
            transform.SetParent(zone.transform);

            Debug.Log($"Dart stuck to {collision.gameObject.name}");
        }
        else
        {
            Debug.Log("Glancing hit, bouncing");
        }
    }
}