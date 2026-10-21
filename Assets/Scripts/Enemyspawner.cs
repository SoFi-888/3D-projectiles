using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int maxEnemies = 4;
    public Vector2 areaX = new Vector2(-8f, 8f);
    public Vector2 areaZ = new Vector2(-8f, 8f);
    public float spawnY = 0.5f;
    public float respawnDelay = 1f;

    float timer;

    void Update()
    {
        if (FindObjectsByType<Enemy>(FindObjectsSortMode.None).Length >= maxEnemies) { timer = 0; return; }

        timer += Time.deltaTime;
        if (timer >= respawnDelay)
        {
            timer = 0;
            Vector3 pos = new Vector3(Random.Range(areaX.x, areaX.y), 20f, Random.Range(areaZ.x, areaZ.y));
            if (Physics.Raycast(pos, Vector3.down, out RaycastHit hit, 50f)) 
            pos.y = hit.point.y + spawnY;
            Instantiate(enemyPrefab, pos, Quaternion.identity);
        }
    }
}