using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class Barre_de_vie : MonoBehaviour
{

    [SerializeField] private Transform _shaker;
    private Transform _shakerbase;
    [SerializeField]  float shakeDuration = 0.2f;
    [SerializeField]  float shakeAmount = 5f;
    [SerializeField]  float pos = 1f;
    [SerializeField]  float scale = 1.1f;

    void Start()
    {
        _shakerbase = _shaker;
    }
    
    public void Shake()
    {
        
        _shaker.DOShakePosition(shakeDuration, pos);
        _shaker.DOShakeRotation(shakeDuration, shakeAmount);
        _shaker.DOShakeScale(shakeDuration, scale);
    }

   
}
