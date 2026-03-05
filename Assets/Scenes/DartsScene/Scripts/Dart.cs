using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Dart : MonoBehaviour
{
    public Transform tip;
    [SerializeField] private LayerMask boardLayerMask;

    private Rigidbody rb;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab;
    private bool hasStuck = false;
    private bool isThrown = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        grab.selectEntered.AddListener(OnGrab);
        grab.selectExited.AddListener(OnRelease);
    }

    private void OnDestroy()
    {
        grab.selectEntered.RemoveListener(OnGrab);
        grab.selectExited.RemoveListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        transform.SetParent(null);
        rb.constraints = RigidbodyConstraints.None;
        rb.isKinematic = false;
        hasStuck = false;
        isThrown = false;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        isThrown = true;
        this.tag = "ThrownDart";
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasStuck || !isThrown) return;

        DartZone zone = collision.gameObject.GetComponent<DartZone>();
        if (zone == null)
        {
            isThrown = false;
            GameManager.Instance.RegisterThrow(0, zoneType.Missed);
            Debug.Log($"Dart missed — hit {collision.gameObject.name}");
            return;
        }

        Vector3 tipForward = tip.right;
        Vector3 surfaceNormal = collision.contacts[0].normal;
        float alignment = Vector3.Dot(tipForward, -surfaceNormal);
        if (alignment < 0.7f) return;

        hasStuck = true;
        isThrown = false;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezeAll;

        Vector3 rayOrigin = tip.position - tipForward * 0.05f;
        Vector3 tipOffset = tip.position - transform.position;

        if (Physics.Raycast(rayOrigin, tipForward, out RaycastHit hit, 0.15f, boardLayerMask))
            transform.position = hit.point - tipOffset;
        else
            transform.position = collision.contacts[0].point - tipOffset;

        transform.SetParent(zone.transform);
        GameManager.Instance.RegisterThrow(zone.score, zone.type);
        Debug.Log($"Dart stuck to {collision.gameObject.name}");
    }
}