using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Sound
{
    public string name;
    public AudioClip clip;
}

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource audioSource;
    public List<Sound> soundDatabase;

    public void PlaySound(string name)
    {
        audioSource.clip = FetchByName(name).clip;
        audioSource.Play();
    }

    public Sound FetchByName(string name)
    {
        Sound fetchedSound = new Sound();

        foreach(Sound sound in soundDatabase)
        {
            if (sound.name == name)
            {
                fetchedSound = sound;
                return fetchedSound;
            }
        }
        return fetchedSound;
    }
}