using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Baptiste.script
{
    public class Transition : MonoBehaviour
    {
        [SerializeField] private float speed = 4f;
        [SerializeField] private AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        [Space(5)]
        [SerializeField] private Vector2 _startAnchorMin= new Vector2(0,0);
        [SerializeField] private Vector2 _startAnchorMax= new Vector2(1,0);
        [Space(5)]
        [SerializeField] private Vector2 _endAnchorMin=new Vector2(0,3);
        [SerializeField] private Vector2 _endAnchorMax=new Vector2(1,3);
        [Space (5)] 
        [SerializeField] private bool _panelActive= false;
        [SerializeField] private float _timer = 0;
        [SerializeField] private float _transitionDuration = 0.5f;
        [SerializeField] private string _nameScene;
    
        void Start()
        {
            RectTransform rect = transform.GetComponent<RectTransform>();
            rect.anchorMin = _startAnchorMin;
            rect.anchorMax = _startAnchorMax;
            rect.DOAnchorMin(_endAnchorMin, speed).SetEase(curve).SetUpdate(true);
            rect.DOAnchorMax(_endAnchorMax, speed).SetEase(curve).SetUpdate(true);
            
        }

        public void Jump()
        {
            transform.DOPause();
            RectTransform rect = transform.GetComponent<RectTransform>();
            rect.DOAnchorMin(_endAnchorMin, speed).SetEase(curve).SetUpdate(true);
            rect.DOAnchorMax(_endAnchorMax, speed).SetEase(curve).SetUpdate(true);
        }

        public void Back()
        {
            transform.DOPause();
            RectTransform rect = transform.GetComponent<RectTransform>();
            rect.DOAnchorMin(_startAnchorMin, speed).SetEase(curve).SetUpdate(true);
            rect.DOAnchorMax(_startAnchorMax, speed).SetEase(curve).SetUpdate(true);
        }
        
    }
}