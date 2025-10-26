using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Email : MonoBehaviour
{
    public Text EmailText;

    private List<string> emails;
    private List<int> emailQueue;
    private string defaultEmail = "From: someone@email.com\nSubject: Hello\nBody: This is an email body.";
    private int currentEmailIndex = -1;

    void Start() 
    {
        Reset();
    }

    public void Reset()
    {
        emails = EmailReader.Load(defaultEmail);
        InitializeRandomEmailQueue();
        DisplayRandomEmail();
    }

    public void TestingButton()
    {
        DisplayRandomEmail();
    }
    
    public void MarkEmailAsScam()
    {
        if (currentEmailIndex >= 0 && StatsManager.Instance != null)
        {
            StatsManager.Instance.SetCurrentEmail(emails[currentEmailIndex], true);
            StatsManager.Instance.ProcessEmailDecision(true);
        }
    }
    
    public void MarkEmailAsLegitimate()
    {
        if (currentEmailIndex >= 0 && StatsManager.Instance != null)
        {
            StatsManager.Instance.SetCurrentEmail(emails[currentEmailIndex], false);
            StatsManager.Instance.ProcessEmailDecision(false);
        }
    }

    private void DisplayRandomEmail()
    {
        if (emailQueue.Count > 0)
        {
            int index = emailQueue[0];
            currentEmailIndex = index;
            EmailText.text = emails[index];
            
            // Notify stats manager about the current email
            if (StatsManager.Instance != null)
            {
                StatsManager.Instance.SetCurrentEmail(emails[index], false);
            }
            
            emailQueue.RemoveAt(0);
        }
        else
        {
            // No more emails
            EmailText.text = "No more emails in queue. Great job!";
            currentEmailIndex = -1;
        }
    }

    private void InitializeRandomEmailQueue()
    {
        emailQueue = new List<int>();
        
        List<int> indexPool = new List<int>();
        for (int i = 0; i < emails.Count; i++)
            indexPool.Add(i);

        for (int i = 0; i < emails.Count; i++)
        {
            int randIndex = Random.Range(0, indexPool.Count);
            emailQueue.Add(indexPool[randIndex]);
            indexPool.RemoveAt(randIndex);
        }

    }
    
}
