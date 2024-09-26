using TMPro;
using UnityEngine;

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
        public static Domain.Models.Game CurrentGame { get; set; }

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
            CurrentGame.Data = CurrentGame.Data with { Score = CurrentGame.Data.Score + score };
            UpdateScoreText();
        }

        /// <summary>
        /// Increments the current round and updates the UI.
        /// </summary>
        public void NextRound()
        {
            CurrentGame.Data = CurrentGame.Data with { Round = CurrentGame.Data.Round + 1 };
            UpdateRoundText();
        }

        public void ResetGame() => CustomSceneManager.LoadStartMenuScene();
        private void UpdateScoreText() => scoreText.text = CurrentGame.Data.Score.ToString();
        private void UpdateRoundText() => roundText.text = "Round: " + CurrentGame.Data.Round.ToString();
    }
}