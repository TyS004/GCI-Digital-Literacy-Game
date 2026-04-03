using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.LightTransport;

public class ProfileImageManager : MonoBehaviour
{
    public Sprite DefaultSprite;
    private Sprite[] profileSprites;

    private void Awake()
    {
        string path = "Levels/ProfileImages";
        profileSprites = Resources.LoadAll<Sprite>(path);
    }

    public Sprite GetProfile(string email)
    {
        string domain = email.Contains("@") ? email.Split('@')[1].Split('.')[0].ToLower() : email.ToLower();
        
        foreach (Sprite sprite in profileSprites)
            if (sprite.name == domain)
                return sprite;
            
        return DefaultSprite;
    }
}