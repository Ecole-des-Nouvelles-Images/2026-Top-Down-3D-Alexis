using System;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class transition2 : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    [Space(5)]
    [SerializeField] private Vector2 _startAnchorMin= new Vector2(0,0);
    [SerializeField] private Vector2 _startAnchorMax= new Vector2(1,0);
    [Space(5)]
    [SerializeField] private Vector2 _endAnchorMin=new Vector2(0,3);
    [SerializeField] private Vector2 _endAnchorMax=new Vector2(1,3);
    [Space (5)] 
    [SerializeField] private bool _isTransitioning= false;
    [SerializeField] private float _timer = 0;
    [SerializeField] private float _transitionDuration = 0.5f;
    [SerializeField] private string _nameScene;
    
    void Start()
    {
        RectTransform rect = transform.GetComponent<RectTransform>();
        rect.anchorMin = _startAnchorMin;
        rect.anchorMax = _startAnchorMax;
        rect.DOAnchorMin(_endAnchorMin, speed);
        rect.DOAnchorMax(_endAnchorMax, speed);
    }
    
    private void Update()
    {
        if (_isTransitioning = true)
        {
            _timer += Time.deltaTime;
            if (_timer >= _transitionDuration)
            {
                SceneManager.LoadScene(_nameScene);
            }
        }
       
    }
}