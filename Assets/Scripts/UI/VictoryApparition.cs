using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class VictoryApparition : MonoBehaviour
    {
        [SerializeField] private Vector3 _scale;
        [SerializeField] private float _duration;
        [SerializeField] private float _rotate;
        [SerializeField] private AnimationCurve _curve;
        [SerializeField] private Selectable _button;
        [SerializeField] private GameObject _HUD;
        [SerializeField] private float _timeToAppear = 2f;
        [SerializeField] private GameObject _text;
        [SerializeField] private float _elapsed = 0f;


    
        void Start()
        {
            transform.DORotate(new Vector3(0, 0, _rotate), _duration).SetEase(_curve).SetUpdate(true);
            transform.DOScale(_scale, _duration).SetEase(_curve).SetUpdate(true);
            _button?.Select();
            _HUD.SetActive(false);
            AudioManager.Instance.PlaySound("Victory");
        }
        
        private void Update()
        {

            if (_elapsed > _timeToAppear)
            {
                _text.SetActive(true);
                Time.timeScale = 0;
            }

            else
            {
                _elapsed += Time.deltaTime ;
            }
        }
    }
}
