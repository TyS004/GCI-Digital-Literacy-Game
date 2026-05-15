using System;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject EmailPanel;

    public void Start()
    {
        Show();
    }

    public void OnStartClick()
    {
        Hide();
    }

    public void Show()
    {
        mainMenuPanel.SetActive(true);
        EmailPanel.SetActive(false);
    }

    public void Hide()
    {
        mainMenuPanel.SetActive(false);
        EmailPanel.SetActive(true);
    }
}
