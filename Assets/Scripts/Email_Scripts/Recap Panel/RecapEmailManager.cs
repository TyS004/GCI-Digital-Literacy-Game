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
                
                SetButtonColor(InboxButtons[i], CorrectIndices.Contains(i));
            }
            else
            {
                InboxTexts[i].text = "";
                InboxImages[i].sprite = null;
            }
        }
    }
    
    private void SetButtonColor(Button button, bool isCorrect)
    {
        ColorBlock colors = button.colors;
        colors.normalColor = isCorrect ? EmailParameters.CorrectEmailNormalColor : EmailParameters.IncorrectEmailNormalColor;
        colors.highlightedColor = isCorrect ? EmailParameters.CorrectEmailHighlightedColor : EmailParameters.IncorrectEmailHighlightedColor;
        colors.pressedColor = isCorrect ? EmailParameters.CorrectEmailPressedColor : EmailParameters.IncorrectEmailPressedColor;
        colors.selectedColor = isCorrect ? EmailParameters.CorrectEmailSelectedColor : EmailParameters.IncorrectEmailSelectedColor;
        button.colors = colors;
    }
    
    private void ResetInboxStyling()
    {
        foreach (Button button in InboxButtons)
        {
            button.interactable = false;
            ColorBlock colors = button.colors;
            colors.normalColor = EmailParameters.DefaultEmailNormalColor;
            colors.highlightedColor = EmailParameters.DefaultEmailHighlightedColor;
            colors.pressedColor = EmailParameters.DefaultEmailPressedColor;
            colors.selectedColor = EmailParameters.DefaultEmailSelectedColor;
            button.colors = colors;
        }
    }
}
