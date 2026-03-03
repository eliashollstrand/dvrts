using UnityEngine;

public class DebugCollisionParent : MonoBehaviour
{
    void OnCollisionEnter(Collision c)  => Debug.Log($"[Dart] CollisionEnter: {c.gameObject.name}");
    void OnCollisionStay(Collision c)   => Debug.Log($"[Dart] CollisionStay: {c.gameObject.name}");
    void OnTriggerEnter(Collider c)     => Debug.Log($"[Dart] TriggerEnter: {c.gameObject.name}");
    void OnTriggerStay(Collider c)      => Debug.Log($"[Dart] TriggerStay: {c.gameObject.name}");
}