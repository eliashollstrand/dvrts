using UnityEngine;

public class Dart : MonoBehaviour
{
    private Rigidbody rb;
    private bool stuck = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (stuck) return;

        // Check if the dart hit a scoring zone
        DartZone zone = collision.collider.GetComponent<DartZone>();
        if (zone != null)
        {
            stuck = true;

            // Stop physics
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;

            // Move dart to contact point
            ContactPoint contact = collision.contacts[0];
            transform.position = contact.point;

            // Rotate dart to align with board surface
            transform.rotation = Quaternion.LookRotation(-contact.normal);

            // Parent dart to the zone so it sticks
            transform.SetParent(zone.transform);

            // Print score and zone name
            Debug.Log($"Hit {zone.gameObject.name} for {zone.score} points!");
        }
    }
}