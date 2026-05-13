using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InfoToggle : MonoBehaviour
{
    public GameObject infoPanel;
    public Button InfoButton;
    public Sprite LeftRegular;
    public Sprite LeftPressed;
    public Sprite RightRegular;
    public Sprite RightPressed;

    private bool isOpen = false;

    void Awake()
    {
        SetButton();
    }
    
    public void OnArrowClicked()
    {
        isOpen = !isOpen;
        infoPanel.SetActive(isOpen);
        SetButton();
    }

    private void SetButton()
    {
        SpriteState spriteState = InfoButton.spriteState;

        if (isOpen)
        {
            InfoButton.image.sprite = LeftRegular;
            spriteState.pressedSprite = LeftPressed;
        }
        else
        {
            InfoButton.image.sprite = RightRegular;
            spriteState.pressedSprite = RightPressed;
        }

        InfoButton.spriteState = spriteState;
    }
}