using UnityEngine;
using UnityEngine.UI;

public class StatsDisplay : MonoBehaviour
{
    public Text totalEmailsText;
    public Text accuracyText;
    public Text scoreText;
    public Text consecutiveCorrectText;
    
    private StatsManager statsManager;

    void Start()
    {
        statsManager = StatsManager.Instance;
        
        if (statsManager != null)
        {
            // Subscribe to events
            statsManager.OnStatsChanged += UpdateStatsDisplay;
            
            // Initial update
            UpdateStatsDisplay(statsManager.GetTotalEmails(), statsManager.GetCurrentScore(), statsManager.GetAccuracy());
        }
    }

    void OnDestroy()
    {
        if (statsManager != null)
        {
            statsManager.OnStatsChanged -= UpdateStatsDisplay;
        }
    }

    private void UpdateStatsDisplay(int totalEmails, int score, float accuracy)
    {
        if (totalEmailsText != null)
            totalEmailsText.text = $"Emails: {totalEmails}";
        
        if (accuracyText != null)
            accuracyText.text = $"Accuracy: {accuracy:F1}%";
        
        if (scoreText != null)
            scoreText.text = $"Score: {score}";
        
        if (consecutiveCorrectText != null && statsManager != null)
            consecutiveCorrectText.text = $"Streak: {statsManager.GetConsecutiveCorrect()}";
    }

    // Called by UI button to show detailed stats
    public void ShowDetailedStats()
    {
        if (statsManager != null)
        {
            Debug.Log(statsManager.GetStatsSummary());
        }
    }

    // Called by UI button to reset stats
    public void ResetStats()
    {
        if (statsManager != null)
        {
            statsManager.ResetStats();
        }
    }
}

