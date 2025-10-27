using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmailUI : MonoBehaviour
{
    //public List<Email> Inbox;
    public MainEmail MainEmail;
    
    public List<Text> InboxTexts;
    public List<Image> InboxImages;

    public void ClearMainEmail()
    {
        
    }
    public void HighlightInboxEmail(int index)
    {
        
    }

    public void UnhighlightInboxEmail(int index)
    {
        
    }
    
    public void HideInboxEmail(int index)
    {
        if (index < InboxTexts.Count && index < InboxImages.Count)
        {
            InboxTexts[index].text = "";
            InboxImages[index].sprite = null; // optional: use a placeholder sprite
        }
    }
    
    public void DisplayEmail(Email email)
    {
        MainEmail.ChangeMainEmail(email);
    }

    public void UpdateInbox(List<Email> inbox)
    {
        for (int i = 0; i < InboxTexts.Count; i++)
        {
            if (i < inbox.Count && inbox[i].GetFullText() != "" &&  inbox[i].GetProfileImageSprite() != null)
            {
                InboxTexts[i].text = inbox[i].GetFullText();
                InboxImages[i].sprite = inbox[i].GetProfileImageSprite();
            }

            else
            {
                InboxTexts[i].text = "";
                InboxImages[i].sprite = null; // empty sprite
            }
        }
    }
    


}
