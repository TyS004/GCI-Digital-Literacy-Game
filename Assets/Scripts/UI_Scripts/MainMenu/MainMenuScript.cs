using System;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject EmailPanel;

    public void Start()
    {
        mainMenuPanel.SetActive(true);
        EmailPanel.SetActive(false);
    }

    public void OnStartClick()
    {
        mainMenuPanel.SetActive(false);
        EmailPanel.SetActive(true);
    }
}
