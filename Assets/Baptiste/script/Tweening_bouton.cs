using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Baptiste.script
{
    
    public class Tweening_bouton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
    { 
        [SerializeField] private float size = 1.2f; 
        [SerializeField] private float speed = 0.3f; 
        [SerializeField] private AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        [SerializeField] private float rotate = 2;
        



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
            transform.DOScale(1, speed).SetEase(curve).SetUpdate(true); 
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
            transform.DOScale(1, speed).SetEase(curve).SetUpdate(true); 
            transform.DORotate(new Vector3(0, 0, 0), speed).SetUpdate(true); 
        }

        
        
    }
}
