using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UI
{
    public class AudioSlider : MonoBehaviour
    {
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private Slider _masterSlider;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _ambianceSlider;
        [SerializeField] private Slider _SFXSlider;


        private void Start()
        {
            if (PlayerPrefs.HasKey("Master"))
            {
                LoadVolume();
            }
            else
            {
                SetMasterVolume();
                SetMusicVolume();
                SetAmbianceVolume();
                SetSFXVolume();
            }
        }
    
    
        public void SetMasterVolume()
        {
            float volume = _masterSlider.value;
            audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("Master", volume);
        }

        public void SetMusicVolume()
        {
            float volume = _musicSlider.value;
            audioMixer.SetFloat("Musique", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("Musique", volume);
        }

        public void SetAmbianceVolume()
        {
            float volume = _ambianceSlider.value;
            audioMixer.SetFloat("Ambiance", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("Ambiance", volume);
        }

        public void SetSFXVolume()
        {
            float volume = _SFXSlider.value;
            audioMixer.SetFloat("VFX", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("VFX", volume);
        }

        private void LoadVolume()
        {
            _musicSlider.value = PlayerPrefs.GetFloat("MasterVolume");
            _masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
            _ambianceSlider.value = PlayerPrefs.GetFloat("AmbianceVolume");
            _SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        
            SetMasterVolume();
            SetMusicVolume();
            SetAmbianceVolume();
            SetSFXVolume();
        }
    }
}
