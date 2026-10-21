using UnityEngine;

public class CannonController : MonoBehaviour
{
    [Header("Movement (translation + rotation)")]
    public float moveSpeed = 5f;
    public float turnSpeed = 90f;
    public Vector2 boundsX = new Vector2(-8f, 8f);
    public Vector2 boundsZ = new Vector2(-8f, 8f);

    [Header("Shooting")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float launchSpeed = 20f;
    public float fireCooldown = 0.25f;

    [Header("Optional: angled / arc launch")]
    public bool useArcLaunch = false;
    [Range(0f, 80f)] public float launchAngle = 20f;

    float nextFireTime;

    void Update()
    {
        Vector2 input = GameInput.Move();
        Vector3 move = new Vector3(input.x, 0f, input.y) * moveSpeed * Time.deltaTime;
        transform.position += move;

        transform.Rotate(0f, GameInput.Turn() * turnSpeed * Time.deltaTime, 0f, Space.World);

        // Keep on the platform
        Vector3 p = transform.position;
        p.x = Mathf.Clamp(p.x, boundsX.x, boundsX.y);
        p.z = Mathf.Clamp(p.z, boundsZ.x, boundsZ.y);
        transform.position = p;

        // --- Shoot ---
        if (GameInput.FirePressed() && Time.time >= nextFireTime)
        {
            Fire();
            nextFireTime = Time.time + fireCooldown;
        }
    }

    void Fire()
    {
        if (projectilePrefab == null || firePoint == null) return;

        GameObject bullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        Vector3 dir = firePoint.forward;
        if (useArcLaunch)
        {
            dir = Quaternion.AngleAxis(-launchAngle, firePoint.right) * dir;
        }

        rb.linearVelocity = dir.normalized * launchSpeed;
    }
}