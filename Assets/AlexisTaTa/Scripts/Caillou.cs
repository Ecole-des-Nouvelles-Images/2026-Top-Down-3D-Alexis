using UnityEngine;

public class Caillou : MonoBehaviour
{
    public GameObject rockPrefab;

    [Header("Zone")]
    public Vector2 areaSize = new Vector2(20, 20);

    [Header("Quantité")]
    public int count = 100;

    [Header("Scale aléatoire")]
    public Vector2 randomScale = new Vector2(0.8f, 1.5f);

    [Header("Rotation")]
    public bool randomYRotation = true;

    void Start()
    {
        for (int i = 0; i < count; i++)
        {
            // Position aléatoire
            Vector3 randomPos = new Vector3(
                Random.Range(-areaSize.x / 2, areaSize.x / 2),
                0,
                Random.Range(-areaSize.y / 2, areaSize.y / 2)
            );

            GameObject rock = Instantiate(
                rockPrefab,
                transform.position + randomPos,
                Quaternion.identity,
                transform
            );

            // Rotation aléatoire
            if (randomYRotation)
            {
                rock.transform.rotation = Quaternion.Euler(
                    0,
                    Random.Range(0f, 360f),
                    0
                );
            }

            // Taille aléatoire
            float scale = Random.Range(randomScale.x, randomScale.y);
            rock.transform.localScale = Vector3.one * scale;
        }
    }
}
