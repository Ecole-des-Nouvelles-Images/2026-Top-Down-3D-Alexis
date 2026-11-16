using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;
using UnityEngine.Serialization;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixerGroup;

    public void SwitchMasterValue(float value)
    {
        audioMixerGroup.SetFloat("Master", Mathf.Log10(value) * 20);
    }
    
    public void SwitchAmbianceValue(float value)
    {
        audioMixerGroup.SetFloat("Ambiance", Mathf.Log10(value) * 20);
    }
    
    public void SwitchMusicValue(float value)
    {
        audioMixerGroup.SetFloat("Music", Mathf.Log10(value) * 20);
    }
    
    public void SwitchSfxValue(float value)
    {
        audioMixerGroup.SetFloat("SFX", Mathf.Log10(value) * 20);
    }
}