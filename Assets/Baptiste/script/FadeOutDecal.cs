using UnityEngine;
using UnityEngine.Rendering.Universal; 

public class FadeOutDecal : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 2f;
    private DecalProjector decalProjector;

    private void Start()
    {
        decalProjector = GetComponent<DecalProjector>();
        StartCoroutine(FadeOut());
    }

    private System.Collections.IEnumerator FadeOut()
    {
        float elapsed = 0f;
        float initialOpacity = decalProjector.fadeFactor;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            decalProjector.fadeFactor = Mathf.Lerp(initialOpacity, 0, elapsed / fadeDuration);
            yield return null;
        }
        
        decalProjector.fadeFactor = 0;
        gameObject.SetActive(false);
    }
}
