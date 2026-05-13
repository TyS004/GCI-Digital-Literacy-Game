using UnityEngine;
using UnityEngine.UI;

public class InfoToggle : MonoBehaviour
{
    public GameObject infoPanel;
    public Button InfoButton;
    public Sprite LeftRegular;
    public Sprite LeftPressed;
    public Sprite RightRegular;
    public Sprite RightPressed;
    public DropdownManager DropdownManager;

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
        DropdownManager.OnInfoToggled(this);
    }

    public void Close()
    {
        isOpen = false;
        infoPanel.SetActive(false);
        SetButton();
    }

    public bool IsOpen()
    {
        return isOpen;
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