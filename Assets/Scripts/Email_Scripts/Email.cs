using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//david was here branch
public class Email : MonoBehaviour
{
    public Text EmailText;

    private List<string> emails;
    private List<int> emailQueue;
    private string defaultEmail = "From: someone@email.com\nSubject: Hello\nBody: This is an email body.";

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

    private void DisplayRandomEmail()
    {
        if (emailQueue.Count > 0)
        {
            int index = emailQueue[0];
            EmailText.text = emails[index];
            emailQueue.RemoveAt(0);
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
