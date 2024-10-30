using System;
using UnityEngine;


public class Score : MonoBehaviour
{
    [SerializeField] private int maxScore = 2;
    
    private int score;

    public event Action<int> OnScoreChanged;
    public event Action OnMaxScore;
   
    private void Awake()
    {
        OnScoreChanged?.Invoke(score);
    }

    public void PlusScore(int amount)
    {
        score += amount;
        OnScoreChanged?.Invoke(score);
        CheckMaxScore();
    }

    private void CheckMaxScore()
    {
        if (score >= maxScore)
        {
            OnMaxScore?.Invoke();
        }
    }
}