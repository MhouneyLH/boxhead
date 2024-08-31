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

        int _currentScore = 0;
        int _currentRound = 0;

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
            _currentScore += score;
            UpdateScoreText();
        }

        /// <summary>
        /// Increments the current round and updates the UI.
        /// </summary>
        public void NextRound()
        {
            _currentRound++;
            UpdateRoundText();
        }

        /// <summary>
        /// Resets the game state.
        /// </summary>
        /// <remarks>
        /// This method is called when the player dies.
        /// </remarks>
        public void ResetGame()
        {
            // todo: I don't know if this state reset is needed at all
            spawner.Reset();

            _currentScore = 0;
            _currentRound = 0;
            UpdateScoreText();
            UpdateRoundText();

            SceneManager.LoadSceneAsync(START_MENU_SCENE_NAME);
        }

        private void UpdateScoreText() => scoreText.text = _currentScore.ToString();
        private void UpdateRoundText() => roundText.text = "Round: " + _currentRound;
    }
}