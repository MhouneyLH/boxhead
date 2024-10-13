using System;
using TMPro;
using UnityEngine;

namespace Boxhead.Presentation.LoadGame
{
    public class GameListItemComponent : MonoBehaviour
    {
        [SerializeField] private TMP_Text createdAtText;
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text roundText;

        private Domain.Models.Game _game;

        public event Action<Domain.Models.Game> OnSelect = delegate { };
        public event Action<Domain.Models.Game> OnDelete = delegate { };

        public void Initialize(Domain.Models.Game game)
        {
            _game = game;
            createdAtText.text = $"Erstellt am: {_game.CreatedAt:dd.MM.yyyy}";
            scoreText.text = $"Score: {_game.Data.Score}";
            roundText.text = $"Runde: {_game.Data.Round.RoundNumber}";
        }

        public void Select() => OnSelect.Invoke(_game);

        public void Delete()
        {
            OnDelete.Invoke(_game);
            Destroy(gameObject);
        }
    }
}