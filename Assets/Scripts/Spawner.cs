using System.Collections;
using UnityEngine;

namespace Boxhead
{
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

        int enemiesToSpawnCount = 1;

        const float SPAWN_INTERVAL_IN_S = 10.0f;
        const float SPAWN_BORDER_THRESHOLD_FACTOR = 0.8f;

        public void StartSpawning()
        {
            SpawnPlayer();
            StartCoroutine(SpawnEnemies());
        }

        public void Reset()
        {
            StopAllCoroutines();
            DespawnEnemies();
            DespawnPlayers();
            enemiesToSpawnCount = 1;
        }

        void SpawnPlayer()
        {
            Instantiate(playerPrefab, Vector3.zero, Quaternion.identity, playerParent);
        }

        IEnumerator SpawnEnemies()
        {
            GameManager.Instance.NextRound();

            for (int i = 0; i < enemiesToSpawnCount; i++)
            {
                SpawnEnemy();
            }

            yield return new WaitForSeconds(SPAWN_INTERVAL_IN_S);

            enemiesToSpawnCount *= 2;
            StartCoroutine(SpawnEnemies());
        }

        void SpawnEnemy()
        {
            float width = Camera.main.orthographicSize * Camera.main.aspect * SPAWN_BORDER_THRESHOLD_FACTOR;
            float height = Camera.main.orthographicSize * SPAWN_BORDER_THRESHOLD_FACTOR;
            Vector3 randomPosition = new(Random.Range(-width, width), Random.Range(-height, height), 0.0f);

            Instantiate(enemyPrefab, randomPosition, Quaternion.identity, enemyParent);
        }

        void DespawnEnemies()
        {
            foreach (Transform child in enemyParent)
            {
                Destroy(child.gameObject);
            }
        }

        void DespawnPlayers()
        {
            foreach (Transform child in playerParent)
            {
                Destroy(child.gameObject);
            }
        }
    }
}