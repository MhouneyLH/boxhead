using System;
using System.Collections;
using Boxhead.Domain.Models;
using Boxhead.Presentation.Game.Enemy;
using Boxhead.Presentation.Game.Player;
using UnityEngine;
using Random = UnityEngine.Random;

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

        public event Action OnRoundFinished = delegate { };

        private Round _currentRound;

        private const float BETWEEN_ROUNDS_DELAY_IN_S = 1.0f;
        private const float SPAWN_ENEMY_DELAY_IN_S = 0.2f;
        private const float SPAWN_BORDER_THRESHOLD_FACTOR = 0.8f;

        public void Initialize(Round round) => _currentRound = round;

        public void StartFirstSpawn()
        {
            SpawnPlayer();
            StartCoroutine(SpawnEnemies());
        }

        public void NextRound(Round round)
        {
            _currentRound = round;
            StartCoroutine(SpawnEnemies());
        }

        public void Reset()
        {
            StopAllCoroutines();
            DespawnEnemies();
            DespawnPlayers();

            _currentRound = Round.CreateFirstRound();
        }

        private IEnumerator SpawnEnemies()
        {
            yield return new WaitForSeconds(BETWEEN_ROUNDS_DELAY_IN_S);
            for (int i = 0; i < _currentRound.EnemyCount; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(SPAWN_ENEMY_DELAY_IN_S);
            }

            yield return new WaitUntil(() => enemyParent.childCount == 0);
            OnRoundFinished.Invoke();
        }

        private void SpawnPlayer()
        {
            GameObject createdPlayer = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity, playerParent);
            var playerComponent = createdPlayer.GetComponent<PlayerComponent>();
            playerComponent.Initialize(_currentRound.Player);
        }

        private void SpawnEnemy()
        {
            float width = Camera.main.orthographicSize * Camera.main.aspect * SPAWN_BORDER_THRESHOLD_FACTOR;
            float height = Camera.main.orthographicSize * SPAWN_BORDER_THRESHOLD_FACTOR;
            Vector3 randomPosition = new(Random.Range(-width, width), Random.Range(-height, height), 0.0f);

            GameObject createdEnemy = Instantiate(enemyPrefab, randomPosition, Quaternion.identity, enemyParent);
            var enemyComponent = createdEnemy.GetComponent<EnemyComponent>();
            enemyComponent.Initialize(_currentRound.EnemyConfiguration);
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