using System.Collections;
using UnityEngine;

namespace Boxhead.Presentation.Game
{
    /// <summary>
    /// Spawns the player and enemies.
    /// </summary>
    public class Spawner : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private GameObject enemyPrefab;

        [Header("Parents")]
        [SerializeField] private Transform playerParent;
        [SerializeField] private Transform enemyParent;

        private int _enemiesToSpawnCount = 1;

        private const float SPAWN_INTERVAL_IN_S = 10.0f;
        private const float SPAWN_BORDER_THRESHOLD_FACTOR = 0.8f;

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
            _enemiesToSpawnCount = 1;
        }

        private void SpawnPlayer() => Instantiate(playerPrefab, Vector3.zero, Quaternion.identity, playerParent);

        private IEnumerator SpawnEnemies()
        {
            var nextRoundTask = GameManager.Instance.NextRound();
            while (!nextRoundTask.IsCompleted)
            {
                yield return null;
            }

            for (int i = 0; i < _enemiesToSpawnCount; i++)
            {
                SpawnEnemy();
            }

            yield return new WaitForSeconds(SPAWN_INTERVAL_IN_S);

            _enemiesToSpawnCount *= 2;
            StartCoroutine(SpawnEnemies());
        }

        private void SpawnEnemy()
        {
            float width = Camera.main.orthographicSize * Camera.main.aspect * SPAWN_BORDER_THRESHOLD_FACTOR;
            float height = Camera.main.orthographicSize * SPAWN_BORDER_THRESHOLD_FACTOR;
            Vector3 randomPosition = new(Random.Range(-width, width), Random.Range(-height, height), 0.0f);

            Instantiate(enemyPrefab, randomPosition, Quaternion.identity, enemyParent);
        }

        private void DespawnEnemies()
        {
            foreach (Transform child in enemyParent)
            {
                Destroy(child.gameObject);
            }
        }

        private void DespawnPlayers()
        {
            foreach (Transform child in playerParent)
            {
                Destroy(child.gameObject);
            }
        }
    }
}