using UnityEngine;
using System;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;

    [HideInInspector]
    public AudioSource source;
}

public class SoundManager : MonoBehaviour
{
    public Sound[] sounds;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }
    public void Play(string name)
    {   
        Debug.Log("we are now searching for" + name);
        Sound s = Array.Find(sounds, sound => sound.name == name);
        Debug.Log(s.name + " sound found\n");
        s.source.Play();
    }
}


// using UnityEngine;
// using System.Collections.Generic;

// public class SoundManager : MonoBehaviour
// {
//     [System.Serializable]
//     public class Sound
//     {
//         public string name;
//         public AudioClip clip;
//     }

//     public static SoundManager instance;

//     public List<Sound> sounds = new List<Sound>();
//     private Dictionary<string, AudioSource> soundDictionary = new Dictionary<string, AudioSource>();

//     private void Awake()
//     {
//         if (instance == null)
//         {
//             instance = this;
//             DontDestroyOnLoad(gameObject);
//         }
//         else if (instance != this)
//         {
//             Destroy(gameObject);
//             return  ;
//         }

//         foreach (var sound in sounds)
//         {
//             AudioSource source = gameObject.AddComponent<AudioSource>();
//             source.clip = sound.clip;
//             soundDictionary.Add(sound.name, source);
//         }
//     }

//     public void Play(string soundName)
//     {
//         if (soundDictionary.TryGetValue(soundName, out AudioSource source))
//         {
//             source.Play();
//         }
//         else
//         {
//             Debug.LogError("Sound not found in dictionary: " + soundName);
//         }
//     }
// }