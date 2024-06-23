using TMPro;
using UnityEngine;

/// <summary>
/// Manages the game state and UI.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text roundText;

    int currentScore = 0;
    int currentRound = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        AddScore(0);
        NextRound();
    }

    /// <summary>
    /// Adds the score to the current score and updates the UI.
    /// </summary>
    /// <param name="score">The score to add.</param>
    public void AddScore(int score)
    {
        currentScore += score;
        scoreText.text = currentScore.ToString();
    }

    /// <summary>
    /// Increments the current round and updates the UI.
    /// </summary>
    public void NextRound()
    {
        currentRound++;
        roundText.text = "Round: " + currentRound;
    }
}
