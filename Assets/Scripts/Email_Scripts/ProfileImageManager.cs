using System.Collections.Generic;
using UnityEngine;

public class ProfileImageManager : MonoBehaviour
{ 
    public List<ProfileEntry> Profiles = new List<ProfileEntry>();
    public Sprite DefaultSprite;

    public Sprite GetProfile(string email)
    {
        foreach (var entry in Profiles)
        {
            if (entry.Email == email)
                return entry.ProfileSprite;
        }
        return DefaultSprite;
    }
}