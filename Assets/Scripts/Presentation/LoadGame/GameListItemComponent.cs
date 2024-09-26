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
            createdAtText.text = _game.CreatedAt.ToString();
            scoreText.text = _game.Data.Score.ToString();
            roundText.text = _game.Data.Round.ToString();
        }

        public void Select() => OnSelect.Invoke(_game);

        public void Delete()
        {
            OnDelete.Invoke(_game);
            Destroy(this.gameObject);
        }
    }
}