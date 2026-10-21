using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [Tooltip("Seconds before a missed projectile is cleaned up. Set to 0 to keep it forever.")]
    public float lifetime = 15f;

    void Start()
    {
        if (lifetime > 0f) Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        Enemy enemy = collision.collider.GetComponentInParent<Enemy>();
        if (enemy != null)
        {
            enemy.Die();
            Destroy(gameObject);
        }
    }
}