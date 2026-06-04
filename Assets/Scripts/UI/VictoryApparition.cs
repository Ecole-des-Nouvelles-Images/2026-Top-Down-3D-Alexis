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
    
        void Start()
        {
            transform.DORotate(new Vector3(0, 0, _rotate), _duration).SetEase(_curve).SetUpdate(true);
            transform.DOScale(_scale, _duration).SetEase(_curve).SetUpdate(true);
            _button?.Select();
        }
    }
}
