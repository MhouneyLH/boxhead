using System.Threading.Tasks;
using Boxhead.Domain.Models;
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

            spawner.Initialize(Round.CreateFirstRound());
            spawner.StartFirstSpawn();
        }

        private void OnEnable() => spawner.OnRoundFinished += OnRoundFinished;
        private void OnDisable() => spawner.OnRoundFinished -= OnRoundFinished;

        private async void OnRoundFinished()
        {
            var round = await NextRound();
            spawner.NextRound(round);
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
        private async Task<Round> NextRound()
        {
            CurrentGame.Data = CurrentGame.Data.NextRound();
            UpdateRoundText();

            var result = await _gameRepository.UpdateGame(CurrentGame);
            if (result.IsFailure)
            {
                Debug.LogError("Failed to update the game: " + result.Error);
            }

            return CurrentGame.Data.Round;
        }

        public void ResetGame() => CustomSceneManager.LoadStartMenuScene();
        private void UpdateScoreText() => scoreText.text = CurrentGame.Data.Score.ToString();
        private void UpdateRoundText() => roundText.text = "Round: " + CurrentGame.Data.Round.RoundNumber.ToString();
    }
}