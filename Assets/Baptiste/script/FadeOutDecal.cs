using UnityEngine;
using UnityEngine.Rendering.Universal; 

public class FadeOutDecal : MonoBehaviour
{
    [SerializeField] private float _fadeDuration = 2f;
    private DecalProjector _decalProjector;

    private void Awake()
    {
        _decalProjector = GetComponent<DecalProjector>();
    }
    
    private void Update()
    {
        float elapsed = 0f;

        if (elapsed < _fadeDuration)
        {
            elapsed += Time.deltaTime;
            _decalProjector.fadeFactor -= elapsed / _fadeDuration;
        }
        
        if (_decalProjector.fadeFactor <= 0)
        {
            Destroy(gameObject);
        }
    }
}
