using UnityEngine;

namespace Items
{
    public class LandMineBlink : MonoBehaviour
    {
        [SerializeField] private Material glowMat;
        [ColorUsage(true, true)]
        [SerializeField] private Color emissionColor = Color.red;

        [SerializeField] private float intensity = 5f;
        [SerializeField] private float blinkInterval = 0.2f;

        private float timer;
        private bool isOn;

        void Update()
        {
            timer += Time.deltaTime;

            if (timer >= blinkInterval)
            {
                timer = 0f;

                isOn = !isOn;

                Color finalColor = isOn
                    ? emissionColor * intensity
                    : Color.black;

                glowMat.SetColor("_EmissionColor", finalColor);
            }
        }
    }
}
