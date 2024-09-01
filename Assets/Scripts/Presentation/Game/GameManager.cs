using Boxhead.Domain.Models;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Boxhead.Presentation.Game
{
    /// <summary>
    /// Manages the game state and UI.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [Header("Controller")]
        [SerializeField] Spawner spawner;

        [Header("UI")]
        [SerializeField] TMP_Text scoreText;
        [SerializeField] TMP_Text roundText;

        public static GameManager Instance { get; private set; }

        private GameData _gameData = new(0, 0);

        private const string START_MENU_SCENE_NAME = "StartMenuScene";

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            UpdateScoreText();
            UpdateRoundText();

            spawner.StartSpawning();
        }

        /// <summary>
        /// Adds the score to the current score and updates the UI.
        /// </summary>
        /// <param name="score">The score to add.</param>
        public void AddScore(int score)
        {
            _gameData = _gameData with { Score = _gameData.Score + score };
            UpdateScoreText();
        }

        /// <summary>
        /// Increments the current round and updates the UI.
        /// </summary>
        public void NextRound()
        {
            _gameData = _gameData with { Round = _gameData.Round + 1 };
            UpdateRoundText();
        }

        public void ResetGame() => SceneManager.LoadSceneAsync(START_MENU_SCENE_NAME);
        private void UpdateScoreText() => scoreText.text = _gameData.Score.ToString();
        private void UpdateRoundText() => roundText.text = "Round: " + _gameData.Round.ToString();
    }
}