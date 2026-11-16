using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    
    public class ButonTweening : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
    { 
        [Header("DoTween")]
        [SerializeField] private float size = 1.2f; 
        [SerializeField] private float speed = 0.3f; 
        [SerializeField] private AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        [SerializeField] private float rotate = 2;
        [SerializeField] private float _endSize = 1;

        [Header("Sound")] [SerializeField] private List<AudioClip> audioClips;
        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        { 
            if (_audioSource != null)
            {
                foreach (AudioClip clip in audioClips)
                {
                    if (clip.name != "ButtonOnSelect")
                    {
                        _audioSource.clip = clip;
                    }
                }
                _audioSource.Play();
            }
            transform.DOPause();
            transform.DORotate(new Vector3(0, 0, rotate), speed).SetUpdate(true);
            transform.DOScale(size, speed).SetEase(curve).SetUpdate(true);
        }

        public void OnClick(PointerEventData eventData)
        { 
            if (_audioSource != null)
            {
                foreach (AudioClip clip in audioClips)
                {
                    if (clip.name != "ButtonOnClick")
                    {
                        _audioSource.clip = clip;
                    }
                }
                _audioSource.Play();
            }
            transform.DOPause(); 

        }
        
        public void OnPointerExit(PointerEventData eventData) 
        { 
            if (_audioSource != null)
            {
                foreach (AudioClip clip in audioClips)
                {
                    if (clip.name != "ButtonOnReturn")
                    {
                        _audioSource.clip = clip;
                    }
                }
                _audioSource.Play();
            }
            transform.DOPause(); 
            transform.DOScale(_endSize, speed).SetEase(curve).SetUpdate(true); 
            transform.DORotate(new Vector3(0, 0, 0), speed).SetUpdate(true);
        }

        public void OnSelect(BaseEventData eventData) 
        {
            if (_audioSource != null)
            {
                foreach (AudioClip clip in audioClips)
                {
                    if (clip.name != "ButtonOnSelect")
                    {
                        _audioSource.clip = clip;
                    }
                }
                _audioSource.Play();
            }

            transform.DOPause(); 
            transform.DOScale(size, speed).SetEase(curve).SetUpdate(true); 
            transform.DORotate(new Vector3(0, 0, rotate), speed).SetUpdate(true);
        }

        public void OnDeselect(BaseEventData eventData)
        
        { 
            if (_audioSource != null)
            {
                foreach (AudioClip clip in audioClips)
                {
                    if (clip.name != "ButtonOnReturn")
                    {
                        _audioSource.clip = clip;
                    }
                }
                _audioSource.Play();
            }
            transform.DOPause(); 
            transform.DOScale(_endSize, speed).SetEase(curve).SetUpdate(true); 
            transform.DORotate(new Vector3(0, 0, 0), speed).SetUpdate(true); 
        }
        
        
    }
}
