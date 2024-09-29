using System.Threading.Tasks;
using Boxhead.Domain.Repositories;
using TMPro;
using UnityEngine;
using Zenject;

namespace Boxhead.Presentation.Game
{
    /// <summary>
    /// Manages the game state and UI.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [Header("Controller")]
        [SerializeField] private Spawner spawner;

        [Header("UI")]
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text roundText;

        public static GameManager Instance { get; private set; }
        public static Domain.Models.Game CurrentGame { get; set; }

        private IGameRepository _gameRepository;

        [Inject]
        public void Construct(IGameRepository gameRepository) => _gameRepository = gameRepository;

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
        public async Task NextRound()
        {
            CurrentGame.Data = CurrentGame.Data with { Round = CurrentGame.Data.Round + 1 };
            UpdateRoundText();

            // only save every 5th round
            if (CurrentGame.Data.Round % 5 != 0)
            {
                return;
            }

            var result = await _gameRepository.UpdateGame(CurrentGame);
            if (result.IsFailure)
            {
                Debug.LogError("Failed to update the game: " + result.Error);
            }
        }

        public void ResetGame() => CustomSceneManager.LoadStartMenuScene();
        private void UpdateScoreText() => scoreText.text = CurrentGame.Data.Score.ToString();
        private void UpdateRoundText() => roundText.text = "Round: " + CurrentGame.Data.Round.ToString();
    }
}