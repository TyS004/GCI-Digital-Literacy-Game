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
        // dont check for domain, either reformat the levsl syntax to have a "Profile" section at the top of each email that takes in teh file name of the profile image
        // or have it look for the name+domain in one string that we then set the names of the profile image files to
        // probably the first one is better and easier
        string domain = email.Contains("@") ? email.Split('@')[1].Split('.')[0].ToLower() : email.ToLower();
        
        foreach (Sprite sprite in profileSprites)
            if (sprite.name == domain)
                return sprite;
            
        return DefaultSprite;
    }
}