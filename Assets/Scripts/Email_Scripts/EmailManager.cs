using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class EmailManager : MonoBehaviour
{
    public LevelManager LevelManager;
    public RecapPanelManager RecapPanelManager;
    public MainEmail MainEmail;
    public List<Text> InboxTexts;
    public List<Image> InboxImages;
    public List<Button> InboxButtons;
    
    private List<Email> Inbox;
    private List<Email> OriginalInbox;
    private List<int> CorrectIndices;
    private int currentIndex;
    private int correctAmount;
    private int incorrectAmount;

    public void SetInbox(List<Email> inbox)
    {
        Inbox = new List<Email>(inbox);
        Reset();
    }

    public void Reset()
    {
        currentIndex = 0;
        correctAmount = 0;
        incorrectAmount = 0;
        CorrectIndices = new List<int>();
        
        ShuffleInbox();
        ResetInboxStyling();
        DisplayEmail(currentIndex);
        UpdateInbox(Inbox);
        
        OriginalInbox = new List<Email>(Inbox);
    }

    public void OnInboxEmailClick(int index)
    {
        DisplayEmail(index);
    }
    
    public void AcceptOrDeny(bool accepted)
    {
        bool correct = accepted ? !HasDiscrepancies() : HasDiscrepancies();

        if (correct)
        {
            correctAmount++;
            CorrectIndices.Add(GetOriginalEmailIndex());
            print("correct");
        }
        else
        {
            incorrectAmount++;
            print("incorrect");
        }
        
        Inbox.RemoveAt(currentIndex);
        if (currentIndex >= Inbox.Count)
            currentIndex--;

        if (Inbox.Count == 0)
        {
            LevelManager.IncreaseTotalCorrect(correctAmount);
            LevelManager.IncreaseTotalIncorrect(incorrectAmount);
            RecapPanelManager.Show();
            return;
        }

        DisplayEmail(currentIndex);
        UpdateInbox(Inbox);
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

    private int GetOriginalEmailIndex()
    {
        Email current = Inbox[currentIndex];
        for (int i = 0; i < OriginalInbox.Count; i++)
            if (ReferenceEquals(OriginalInbox[i], current))
                return i;
        Debug.Log("Could not match email to original inbox");
        return -1;
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
    
    private void UpdateInbox(List<Email> inbox)
    {
        for (int i = 0; i < InboxTexts.Count; i++)
        {
            if (i < inbox.Count)
            {
                InboxTexts[i].text = inbox[i].GetFrom() + "\n" + inbox[i].GetSubject();
                InboxImages[i].sprite = inbox[i].GetProfileImageSprite();
                InboxButtons[i].interactable = true;
                // show profile image
            }
            else
            {
                InboxTexts[i].text = "";
                InboxImages[i].sprite = null;
                InboxButtons[i].interactable = false;
            }
        }
    }
    
    private void ResetInboxStyling()
    {
        foreach (Button button in InboxButtons)
        {
            // hide profile images
            button.interactable = false;
            ColorBlock colors = button.colors;
            colors.normalColor = EmailParameters.DefaultEmailNormalColor;
            colors.highlightedColor = EmailParameters.DefaultEmailHighlightedColor;
            colors.pressedColor = EmailParameters.DefaultEmailPressedColor;
            colors.selectedColor = EmailParameters.DefaultEmailSelectedColor;
            button.colors = colors;
        }
    }
    
    public List<Email> GetInbox()
    {
        return Inbox;
    }

    public int GetCorrectAmount()
    {
        return correctAmount;
    }

    public int GetIncorrectAmount()
    {
        return incorrectAmount;
    }

    public List<Email> GetOriginalInbox()
    {
        return OriginalInbox;
    }

    public List<int> GetCorrectIndices()
    {
        return CorrectIndices;
    }
    
    
}