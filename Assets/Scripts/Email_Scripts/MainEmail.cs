using UnityEngine;
using UnityEngine.UI;

public class MainEmail : MonoBehaviour
{
    public Text EmailText;
    public Image SenderImage;

    public void ChangeMainEmail(Email email)
    {
        EmailText.text = email.GetFullText();
        SenderImage.sprite = email.GetProfileImageSprite();
    }
    
}
