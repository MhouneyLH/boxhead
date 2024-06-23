using TMPro;
using UnityEngine;

/// <summary>
/// Manages the game state and UI.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Controller")]
    [SerializeField] Spawner _spawner;

    [Header("UI")]
    [SerializeField] TMP_Text _scoreText;
    [SerializeField] TMP_Text _roundText;

    int currentScore = 0;
    int currentRound = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        UpdateScoreText();
        UpdateRoundText();

        _spawner.StartSpawning();
    }

    /// <summary>
    /// Adds the score to the current score and updates the UI.
    /// </summary>
    /// <param name="score">The score to add.</param>
    public void AddScore(int score)
    {
        currentScore += score;
        UpdateScoreText();
    }

    /// <summary>
    /// Increments the current round and updates the UI.
    /// </summary>
    public void NextRound()
    {
        currentRound++;
        UpdateRoundText();
    }

    /// <summary>
    /// Resets the game state.
    /// </summary>
    /// <remarks>
    /// This method is called when the player dies.
    public void ResetGame()
    {
        _spawner.Reset();

        currentScore = 0;
        currentRound = 0;
        UpdateScoreText();
        UpdateRoundText();

        _spawner.StartSpawning();
    }

    void UpdateScoreText()
    {
        _scoreText.text = currentScore.ToString();
    }

    void UpdateRoundText()
    {
        _roundText.text = "Round: " + currentRound;
    }
}
