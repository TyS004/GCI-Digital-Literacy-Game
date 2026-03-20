using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class EmailManager : MonoBehaviour
{
    public LevelManager LevelManager;
    public MainEmail MainEmail;
    public List<Text> InboxTexts;
    public List<Image> InboxImages;
    
    private List<Email> Inbox;
    private int currentIndex;
    private int correctAmount;
    private int incorrectAmount;

    public void SetInbox(List<Email> inbox)
    {
        Inbox = inbox;
        Reset();
    }
    
    public void Reset()
    {
        currentIndex = 0;
        correctAmount = 0;
        incorrectAmount = 0;
        ShuffleInbox();
        DisplayEmail(currentIndex);
        UpdateInbox(Inbox);
    }

    public void OnInboxEmailClick(int index)
    {
        DisplayEmail(index);
    }

    public List<Email> GetInbox()
    {
        return Inbox;
    }
    
    public void Accept()
    {
        AcceptOrDeny();
        if (HasDiscrepancies())
            incorrectAmount++;
        print("--------------------------------------");
        print("correct amount: " + correctAmount + "\nincorrect amount: " + incorrectAmount);
    }
    public void Deny()
    {
        AcceptOrDeny();
        if (HasDiscrepancies())
            correctAmount++;
        print("--------------------------------------");
        print("correct amount: " + correctAmount + "\nincorrect amount: " + incorrectAmount);
    }
    
    private void AcceptOrDeny()
    {
        Inbox.RemoveAt(currentIndex);

        if (currentIndex >= Inbox.Count)
            currentIndex--;

        if (Inbox.Count == 0)
        {
            LevelManager.LoadNextLevel();
        }

        DisplayEmail(currentIndex);
        UpdateInbox(Inbox);
    }

    private bool HasDiscrepancies()
    {
        bool hasDiscrepancies = MainEmail.GetDiscrepancies().Count > 0;
        return  hasDiscrepancies;
    }

    private void ShuffleInbox()
    {
        for (int i = 0; i < Inbox.Count; i++)
        {
            int randIndex = Random.Range(i, Inbox.Count);
            (Inbox[i], Inbox[randIndex]) = (Inbox[randIndex], Inbox[i]);
        }
    }

    public void DisplayEmail(int index)
    {
        if (Inbox.Count > 0)
        {
            Email email = Inbox[index];
            MainEmail.ChangeMainEmail(email);
            currentIndex = index;
        }
    }
    
    private void UpdateInbox(List<Email> inbox)
    {
        for (int i = 0; i < InboxTexts.Count; i++)
        {
            if (i < inbox.Count)
            {
                InboxTexts[i].text = inbox[i].GetFrom() + "\n" + inbox[i].GetSubject();
                InboxImages[i].sprite = inbox[i].GetProfileImageSprite();
            }
            else
            {
                InboxTexts[i].text = "";
                InboxImages[i].sprite = null;
            }
        }
    }

    public int GetCorrectAmount()
    {
        return correctAmount;
    }

    public int GetIncorrectAmount()
    {
        return incorrectAmount;
    }
}