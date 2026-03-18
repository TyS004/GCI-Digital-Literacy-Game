using System.Collections.Generic;
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

    public void SetInbox(List<Email> inbox)
    {
        Inbox = inbox;
        Reset();
    }
    
    public void Reset()
    {
        currentIndex = 0;
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
    
    public void AcceptOrDeny()
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
}