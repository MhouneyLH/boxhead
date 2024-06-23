using UnityEngine;

/// <summary>
/// Spawns the player and enemies.
/// </summary>
public class Spawner : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject enemyPrefab;

    [Header("Parents")]
    [SerializeField] Transform playerParent;
    [SerializeField] Transform enemyParent;

    const float SPAWN_BORDER_THRESHOLD_FACTOR = 0.8f;

    void Start()
    {
        SpawnPlayer();
        SpawnEnemy();
    }

    void SpawnPlayer()
    {
        Instantiate(playerPrefab, Vector3.zero, Quaternion.identity, playerParent);
    }

    void SpawnEnemy()
    {
        float width = Camera.main.orthographicSize * Camera.main.aspect * SPAWN_BORDER_THRESHOLD_FACTOR;
        float height = Camera.main.orthographicSize * SPAWN_BORDER_THRESHOLD_FACTOR;
        Vector3 randomPosition = new(Random.Range(-width, width), Random.Range(-height, height), 0.0f);

        Instantiate(enemyPrefab, randomPosition, Quaternion.identity, enemyParent);
    }
}
