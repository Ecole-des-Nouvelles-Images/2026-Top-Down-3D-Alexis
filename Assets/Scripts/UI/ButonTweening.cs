using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    
    public class ButonTweening : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
    { 
        [SerializeField] private Transform _target;
        [SerializeField] private float size = 1.2f; 
        [SerializeField] private float speed = 0.3f; 
        [SerializeField] private AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        [SerializeField] private float rotate = 2;
        [SerializeField] private float pos = 1.2f;
        [SerializeField] private float rotate_shake = 5;
        [SerializeField] private float _endSize = 1;


        public void OnPointerEnter(PointerEventData eventData)
        { 
            Debug.Log("OnPointerEnter");
            transform.DOPause();
            transform.DORotate(new Vector3(0, 0, rotate), speed).SetUpdate(true);
            transform.DOScale(size, speed).SetEase(curve).SetUpdate(true);
        }

        public void OnClick(PointerEventData eventData)
        { 
            Debug.Log("click");
            transform.DOPause(); 

        }
        
        public void OnPointerExit(PointerEventData eventData) 
        { 
            Debug.Log("OnPointerExit"); 
            transform.DOPause(); 
            transform.DOScale(_endSize, speed).SetEase(curve).SetUpdate(true); 
            transform.DORotate(new Vector3(0, 0, 0), speed).SetUpdate(true);
        }

        public void OnSelect(BaseEventData eventData) 
        { 
            transform.DOPause(); 
            transform.DOScale(size, speed).SetEase(curve).SetUpdate(true); 
            transform.DORotate(new Vector3(0, 0, rotate), speed).SetUpdate(true);
        }

        public void OnDeselect(BaseEventData eventData)
        
        { 
            transform.DOPause(); 
            transform.DOScale(_endSize, speed).SetEase(curve).SetUpdate(true); 
            transform.DORotate(new Vector3(0, 0, 0), speed).SetUpdate(true); 
        }
        
        
    }
}
