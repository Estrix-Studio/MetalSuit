using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private float totalScore, currentScore;

    // Start is called before the first frame update
    private void Start()
    {
        totalScore = 0.0f;
        currentScore = 0.0f;
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void AddScore(float score)
    {
        currentScore += score;
    }

    public void ResetCurrentScore()
    {
        currentScore = 0.0f;
    }

    public void UpdateTotalScore()
    {
        totalScore += currentScore;
    }

    public float GetCurrentScore()
    {
        return currentScore;
    }

    public float GetTotalScore()
    {
        return totalScore;
    }
}