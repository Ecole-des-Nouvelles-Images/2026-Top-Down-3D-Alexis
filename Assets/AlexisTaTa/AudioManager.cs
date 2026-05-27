using UnityEngine;
using System.Collections.Generic;
public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public class SoundEffect
    {
        public string name;
        
        public AudioClip clip;
        
        [Range(0f, 1f)] public float volume = 1f;
        
        [Range(0f, 1f)] public float pitch = 1f;
        
    }
    
    [SerializeField] private SoundEffect[] soundEffects;
    [SerializeField] private int poolSize = 5;
    
    private List<AudioSource> audioSources = new List<AudioSource>();
    
    private Dictionary<string, AudioClip> soundDictionary = new Dictionary<string, AudioClip>();
    
    private float globalSfxVolume = 1f;
    
    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        for (int i = 0; i < poolSize; i++)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            audioSources.Add(source);
        }

        foreach (SoundEffect sound in soundEffects)
        {
            if (sound.clip != null)
            {
                soundDictionary[sound.name] = sound.clip;
            }
        }
    }

    private AudioSource GetAvailableAudioSource()
    {
        foreach (AudioSource source in audioSources)
        {
            if (!source.isPlaying)
            {
                return source;
            }
        }
        return audioSources.Count > 0 ? audioSources[0] : null;
    }

    public void PlaySound(string soundName)
    {
        if (!soundDictionary.TryGetValue(soundName, out AudioClip clip))
        {
            Debug.LogWarning($"No audio sound found for {soundName}");
            return;
        }
        
        AudioSource source = GetAvailableAudioSource();
        if (source != null)
        {
            float soundVolume = 1f;
            foreach (SoundEffect sound in soundEffects)
            {
                if (sound.name == soundName)
                {
                    break;
                }
            }
            source.clip = clip;
            source.volume = soundVolume;
            source.Play();
        }
    }
    
    
}
