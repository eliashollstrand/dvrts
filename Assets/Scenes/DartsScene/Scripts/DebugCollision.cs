using UnityEngine;

public class DebugCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision c)  => Debug.Log($"[Tip] CollisionEnter: {c.gameObject.name}");
    void OnCollisionStay(Collision c)   => Debug.Log($"[Tip] CollisionStay: {c.gameObject.name}");
    void OnTriggerEnter(Collider c)     => Debug.Log($"[Tip] TriggerEnter: {c.gameObject.name}");
    void OnTriggerStay(Collider c)      => Debug.Log($"[Tip] TriggerStay: {c.gameObject.name}");
}