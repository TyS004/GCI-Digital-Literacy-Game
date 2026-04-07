using UnityEngine;
using TMPro;

public class InfoToggle : MonoBehaviour
{
    public GameObject infoPanel;
    public TMP_Text arrowText;

    private bool isOpen = false;

    public void OnArrowClicked()
    {
        isOpen = !isOpen;
        infoPanel.SetActive(isOpen);
        arrowText.text = isOpen ? "▼" : "►";
        // this is a test
    }
}