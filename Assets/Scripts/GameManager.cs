using TMPro;
using UnityEngine;

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

    public void AddScore(int score)
    {
        currentScore += score;
        scoreText.text = currentScore.ToString();
    }

    public void NextRound()
    {
        currentRound++;
        roundText.text = "Round: " + currentRound;
    }
}
