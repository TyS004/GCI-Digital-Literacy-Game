using UnityEngine;

public class UISoundManager : MonoBehaviour
{
    public static UISoundManager instance;
    public AudioSource audioSource;

    private void Awake()
    {
        instance = this;
    }

    public void PlayClick()
    {
        audioSource.Play();
    }
}