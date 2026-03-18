using UnityEngine;

public class LevelCompleteScript : MonoBehaviour
{
    private int minScoreToPass = 7;
    private int maxScore = 5;
    private int currentScore = 6;

    public void setminScoreToPass(int i)
    {
        minScoreToPass = i;
    }

    public void setmaxScore(int i)
    {
        maxScore = i;
    }

    public void setCurrentScore(int i)
    {
        currentScore = i;
    }

    public void increaseScore()
    {
        currentScore++;
    }

    public int returncurrentScore()
    {
        return currentScore;
    }

    public int returnminScore()
    {
        return minScoreToPass;
    }

    public int returnmaxScore()
    {
        return maxScore;
    }

    public bool CheckIfPass()
    {
        if (currentScore >= minScoreToPass)
        {
            return true;
        }
        return false;
    }
    void Update()
    {
        
    }
}
