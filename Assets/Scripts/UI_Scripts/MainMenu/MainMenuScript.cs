using System;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject EmailPanel;
    public GameObject tablet;

    public void Start()
    {
        tablet.SetActive(false);
        mainMenuPanel.SetActive(true);
        EmailPanel.SetActive(false);
    }

    public void OnStartClick()
    {
        mainMenuPanel.SetActive(false);
        tablet.SetActive(false);
        EmailPanel.SetActive(true);
    }
}
