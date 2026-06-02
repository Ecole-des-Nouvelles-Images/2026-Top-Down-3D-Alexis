using DG.Tweening;
using UnityEngine;
public class logo_anim : MonoBehaviour
{
    [SerializeField] private float _rotation;
    [SerializeField] private float _duration;
    [SerializeField] private AnimationCurve _curve;
    void Start()
    {
        transform.DORotate(new Vector3(0, 0, _rotation), _duration).SetEase(_curve).SetLoops(-1, LoopType.Yoyo).SetUpdate(true);
    }

}
