using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Emails : MonoBehaviour
{
    public FileReader FileReader;
    public EmailUI EmailUI;
    
    private List<Email> Inbox;
    private int CurrentIndex;

    public void Start()
    {
        Reset();
    }
    
    public void Reset()
    {
        CurrentIndex = 0;
        InitializeRandomInbox();
        DisplayEmail(CurrentIndex);
        EmailUI.UpdateInbox(Inbox);
    }

    public void OnInboxEmailClick(int index)
    {
        if (index >= Inbox.Count)
            return;
        DisplayEmail(index);
    }
    
    public void Accept()
    {
        AcceptOrDeny();
    }

    public void Deny()
    {
        AcceptOrDeny();
    }

    private void AcceptOrDeny()
    {
        if (Inbox.Count < 0)
        {
            return;
        }

        Inbox.RemoveAt(CurrentIndex);

        if (CurrentIndex >= Inbox.Count)
            CurrentIndex--;

        DisplayEmail(CurrentIndex);
        EmailUI.UpdateInbox(Inbox);
    }

    private void DisplayEmail(int index)
    {
        if (Inbox.Count > 0)
        {
            Email email = Inbox[index];
            EmailUI.DisplayEmail(email);
            CurrentIndex = index;
        }
    }

    private void InitializeRandomInbox()
    {
        Inbox = FileReader.Load();

        for (int i = 0; i < Inbox.Count; i++)
        {
            int randIndex = Random.Range(i, Inbox.Count);
            (Inbox[i], Inbox[randIndex]) = (Inbox[randIndex], Inbox[i]);
        }
    }
}