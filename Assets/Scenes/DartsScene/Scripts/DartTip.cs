using UnityEngine;

public class DartTip : MonoBehaviour
{
    private Rigidbody dartRigidbody;
    private bool hasStuck = false;

    private void Awake()
    {
        // Get the Rigidbody from the parent Dart object
        dartRigidbody = GetComponentInParent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit");
        // Already stuck, ignore further collisions
        if (hasStuck) return;

        // Only stick if we hit a dart zone
        if (collision.gameObject.GetComponent<DartZone>() == null) return;

        hasStuck = true;

        // Freeze the dart in place
        dartRigidbody.isKinematic = true;

        // Parent the dart to the board so it moves with it
        transform.parent.SetParent(collision.transform);
    }
}