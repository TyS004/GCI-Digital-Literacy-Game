using UnityEngine;

public class DropdownManager : MonoBehaviour
{
    private InfoToggle currentOpen;

    public void OnInfoToggled(InfoToggle toggle)
    {
        if (currentOpen != null && currentOpen != toggle)
            currentOpen.Close();

        currentOpen = toggle.IsOpen() ? toggle : null;
    }
}