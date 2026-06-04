using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UI
{
    public class AudioSlider : MonoBehaviour
    {
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private Slider _masterSlider;
        [SerializeField] private Slider _musicSlider ;
        [SerializeField] private Slider _ambianceSlider;
        [SerializeField] private Slider _SFXSlider;


        private void Start()
        {
            SetMasterVolume();
            SetMusicVolume();
            SetAmbianceVolume();
            SetSFXVolume();
        }
    
    
        public void SetMasterVolume()
        {
            float volume = _masterSlider.value;
            audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20); 
        }

        public void SetMusicVolume()
        {
            float volume = _musicSlider.value;
            audioMixer.SetFloat("Musique", Mathf.Log10(volume) * 20);
        }

        public void SetAmbianceVolume()
        {
            float volume = _ambianceSlider.value;
            audioMixer.SetFloat("Ambiance", Mathf.Log10(volume) * 20);
        }

        public void SetSFXVolume()
        {
            float volume = _SFXSlider.value;
            audioMixer.SetFloat("VFX", Mathf.Log10(volume) * 20);
        }
    }
}
