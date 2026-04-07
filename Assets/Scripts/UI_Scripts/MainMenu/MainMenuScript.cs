using System;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public Canvas mainMenuPanel;
    public Canvas tablet;

    public void Start()
    {
        tablet.enabled = false;
    }

    public void OnStartClick()
    {
        mainMenuPanel.enabled = false;
        tablet.enabled = true;
    }
}
