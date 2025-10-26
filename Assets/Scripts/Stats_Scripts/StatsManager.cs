using System;
using UnityEngine;
using System.Collections.Generic;

public class StatsManager : MonoBehaviour
{
    private static StatsManager instance;
    public static StatsManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<StatsManager>();
                if (instance == null)
                {
                    GameObject go = new GameObject("StatsManager");
                    instance = go.AddComponent<StatsManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return instance;
        }
    }

    // Stats tracking
    private int totalEmailsProcessed = 0;
    private int correctClassifications = 0;
    private int incorrectClassifications = 0;
    private int scamsCorrectlyFlagged = 0;
    private int legitimateEmailsCorrectlyFlagged = 0;
    private int scamsIncorrectlyFlagged = 0; // Marked as legitimate but were scams
    private int legitimateIncorrectlyFlagged = 0; // Marked as scam but were legitimate
    private float sessionStartTime = 0f;
    private float gameSessionTime = 0f;
    
    // Store current email info for tracking
    private string currentEmailSource = "";
    private bool currentEmailIsScam = false;

    // Events for UI updates
    public event Action<int, int, float> OnStatsChanged;
    public event Action<float> OnSessionTimeChanged;

    // Score system
    private int basePointsPerCorrect = 100;
    private int bonusPointsPerPerfectSession = 50;
    private int currentScore = 0;
    private int consecutiveCorrect = 0;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        sessionStartTime = Time.time;
        UpdateUI();
    }

    void Update()
    {
        if (sessionStartTime > 0)
        {
            gameSessionTime = Time.time - sessionStartTime;
            OnSessionTimeChanged?.Invoke(gameSessionTime);
        }
    }

    public void SetCurrentEmail(string emailSource, bool isScam)
    {
        currentEmailSource = emailSource;
        currentEmailIsScam = isScam;
    }

    public void ProcessEmailDecision(bool flaggedAsScam)
    {
        totalEmailsProcessed++;

        // Determine if the decision was correct
        bool wasCorrect = (flaggedAsScam == currentEmailIsScam);

        if (wasCorrect)
        {
            correctClassifications++;
            consecutiveCorrect++;

            if (currentEmailIsScam)
            {
                scamsCorrectlyFlagged++;
            }
            else
            {
                legitimateEmailsCorrectlyFlagged++;
            }

            // Calculate score with consecutive bonus
            int scoreIncrease = basePointsPerCorrect;
            if (consecutiveCorrect > 3)
            {
                scoreIncrease += (consecutiveCorrect - 3) * 10; // Bonus for streaks
            }
            currentScore += scoreIncrease;
        }
        else
        {
            incorrectClassifications++;
            consecutiveCorrect = 0;

            if (currentEmailIsScam && !flaggedAsScam)
            {
                scamsIncorrectlyFlagged++; // Failed to flag a scam
            }
            else if (!currentEmailIsScam && flaggedAsScam)
            {
                legitimateIncorrectlyFlagged++; // Flagged legitimate as scam
            }

            // Penalty for incorrect classification
            currentScore = Mathf.Max(0, currentScore - 50);
        }

        UpdateUI();
    }

    public void ResetStats()
    {
        totalEmailsProcessed = 0;
        correctClassifications = 0;
        incorrectClassifications = 0;
        scamsCorrectlyFlagged = 0;
        legitimateEmailsCorrectlyFlagged = 0;
        scamsIncorrectlyFlagged = 0;
        legitimateIncorrectlyFlagged = 0;
        gameSessionTime = 0f;
        sessionStartTime = Time.time;
        currentScore = 0;
        consecutiveCorrect = 0;
        
        UpdateUI();
    }

    private void UpdateUI()
    {
        float accuracyRate = totalEmailsProcessed > 0 ? (float)correctClassifications / totalEmailsProcessed * 100f : 0f;
        OnStatsChanged?.Invoke(totalEmailsProcessed, currentScore, accuracyRate);
    }

    // Public getters for stats
    public int GetTotalEmails() => totalEmailsProcessed;
    public int GetCorrectClassifications() => correctClassifications;
    public int GetIncorrectClassifications() => incorrectClassifications;
    public float GetAccuracy() => totalEmailsProcessed > 0 ? (float)correctClassifications / totalEmailsProcessed * 100f : 0f;
    public int GetScamsCorrectlyFlagged() => scamsCorrectlyFlagged;
    public int GetLegitimateEmailsCorrectlyFlagged() => legitimateEmailsCorrectlyFlagged;
    public int GetScamsIncorrectlyFlagged() => scamsIncorrectlyFlagged;
    public int GetLegitimateIncorrectlyFlagged() => legitimateIncorrectlyFlagged;
    public float GetSessionTime() => gameSessionTime;
    public int GetCurrentScore() => currentScore;
    public int GetConsecutiveCorrect() => consecutiveCorrect;

    // Get formatted time string
    public string GetFormattedTime()
    {
        int minutes = Mathf.FloorToInt(gameSessionTime / 60f);
        int seconds = Mathf.FloorToInt(gameSessionTime % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Get detailed stats summary
    public string GetStatsSummary()
    {
        string summary = "=== Email Scam Detection Stats ===\n\n";
        summary += $"Total Emails Processed: {totalEmailsProcessed}\n";
        summary += $"Correct: {correctClassifications} | Incorrect: {incorrectClassifications}\n";
        summary += $"Accuracy: {GetAccuracy():F1}%\n\n";
        summary += $"Scams Caught: {scamsCorrectlyFlagged}\n";
        summary += $"Scams Missed: {scamsIncorrectlyFlagged}\n";
        summary += $"Legitimate Emails Accepted: {legitimateEmailsCorrectlyFlagged}\n";
        summary += $"False Positives: {legitimateIncorrectlyFlagged}\n\n";
        summary += $"Current Score: {currentScore}\n";
        summary += $"Consecutive Correct: {consecutiveCorrect}\n";
        summary += $"Session Time: {GetFormattedTime()}\n";
        
        return summary;
    }
}

