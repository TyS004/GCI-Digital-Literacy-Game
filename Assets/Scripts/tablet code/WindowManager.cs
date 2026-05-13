using UnityEngine;
using UnityEngine.EventSystems;

public class WindowManager : MonoBehaviour
{
    public GameObject TabletWindow;
    public GameObject TabletTask;
    public CheckerResult CheckerResult;

    void Start()
    {
        TabletWindow.SetActive(false);
        TabletTask.SetActive(false);
    }

    public void MinimizeTabletWindow()
    {
        TabletWindow.SetActive(false);
        CheckerResult.Reset();
    }

    public void OpenTablet()
    {
        TabletWindow.SetActive(true);
        TabletTask.SetActive(true);
    }

    public void CloseTablet()
    {
        TabletWindow.SetActive(false);
        TabletTask.SetActive(false);
        CheckerResult.Reset();
    }
}