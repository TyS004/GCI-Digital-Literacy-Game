using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class RecapEmailManager : MonoBehaviour
{
    public EmailManager EmailManager;
    public RecapMainEmail RecapMainEmail;
    
    public List<Text> InboxTexts;
    public List<Image> InboxImages;
    public List<Button> InboxButtons;
    public List<Button> InboxButtonImages;
    public Sprite GreySprite;
    
    private List<Email> Inbox;
    private List<int> CorrectIndices;
    private int currentIndex;

    public void SetInbox()
    {
        Inbox = EmailManager.GetOriginalInbox();
        CorrectIndices = EmailManager.GetCorrectIndices();
        currentIndex = 0;
        ResetInboxStyling();
        DisplayEmail(currentIndex);
        UpdateInbox(Inbox);
    }
    

    public void OnInboxEmailClick(int index)
    {
        DisplayEmail(index);
    }
    
    public void DisplayEmail(int index)
    {
        if (Inbox.Count > 0)
        {
            Email email = Inbox[index];
            RecapMainEmail.ChangeMainEmail(email);
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
                InboxButtons[i].interactable = true;
            
                SetImageColor(InboxButtonImages[i].image, CorrectIndices.Contains(i));
            }
            else
            {
                InboxTexts[i].text = "";
                InboxImages[i].sprite = GreySprite;
                InboxButtonImages[i].image.color = Color.white;
            }
        }
    }
    
    private void SetImageColor(Image image, bool isCorrect)
    {
        image.color = isCorrect ? EmailParameters.CorrectEmailNormalColor : EmailParameters.IncorrectEmailNormalColor;
    }

    private void ResetInboxStyling()
    {
        foreach (Button button in InboxButtons)
        {
            button.interactable = false;
        }
        foreach (Button button in InboxButtonImages)
        {
            button.image.color = Color.white;
        }
    }
}
