using UnityEngine;
using DG.Tweening;

public class Fade_Out_Material : MonoBehaviour
{
    [SerializeField] Material _material;
    [SerializeField] AnimationCurve _curve;
    void Start()
    {
        _material.DOFade(0.0f, 2f).SetEase(_curve);
    }
}
