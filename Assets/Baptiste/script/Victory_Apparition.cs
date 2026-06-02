using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Victory_Apparition : MonoBehaviour
{
    [SerializeField] private Vector3 _scale;
    [SerializeField] private float _duration;
    [SerializeField] private float _rotate;
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private Selectable _button;
    
    void Start()
    {
        transform.DORotate(new Vector3(0, 0, _rotate), _duration).SetEase(_curve);
        transform.DOScale(_scale, _duration).SetEase(_curve);
        _button?.Select();
    }
}
