using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Pulsate (scaling)")]
    public float pulseAmount = 0.25f;
    public float pulseSpeed = 4f;

    [Header("Movement (translation)")]
    public float moveSpeed = 2f;
    public Vector2 boundsX = new Vector2(-8f, 8f);
    public Vector2 boundsZ = new Vector2(-8f, 8f);

    Vector3 baseScale;
    Vector3 target;
    float phase;

    void Start()
    {
        baseScale = transform.localScale;
        phase = Random.value * Mathf.PI * 2f;
        PickTarget();
    }

    void Update()
    {
        float s = 1f + pulseAmount * Mathf.Sin(Time.time * pulseSpeed + phase);
        transform.localScale = baseScale * s;

        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        if ((transform.position - target).sqrMagnitude < 0.05f) PickTarget();
    }

    void PickTarget()
    {
        target = new Vector3(Random.Range(boundsX.x, boundsX.y), transform.position.y,
                             Random.Range(boundsZ.x, boundsZ.y));
    }

    public void Die()
    {
        ScoreManager.Instance?.AddKill();
        Destroy(gameObject);
    }
}