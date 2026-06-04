using UnityEngine;
using DG.Tweening;

public class Scale_Indicator : MonoBehaviour
{
    
    [SerializeField] AnimationCurve _curve;
    [SerializeField] float _scale;
    [SerializeField] float _duration;
    [SerializeField] Material _fade;
    
    
    private void Start()
    {
        transform.DOScale(_scale,_duration).SetEase(_curve).SetLoops(-1, LoopType.Yoyo).SetUpdate(true);
        _fade.DOFade(1, _duration).SetEase(_curve).SetLoops(-1, LoopType.Yoyo).SetUpdate(true);
    }
    
}
