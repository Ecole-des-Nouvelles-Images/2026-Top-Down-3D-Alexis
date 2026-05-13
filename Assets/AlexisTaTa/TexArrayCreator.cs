using UnityEngine;
using UnityEditor;
public class TexArrayCreator : MonoBehaviour
{
    [MenuItem("Tools/Create Texture2DArray")]
    static void CreateArray()
    {
        // Sélectionne tes 32 textures dans le Project
        Object[] textures = Selection.objects;

        if (textures.Length == 0)
        {
            Debug.LogError("Sélectionne les textures.");
            return;
        }

        Texture2D first = textures[0] as Texture2D;

        Texture2DArray array = new Texture2DArray(
            first.width,
            first.height,
            textures.Length,
            first.format,
            true
        );

        array.wrapMode = TextureWrapMode.Repeat;
        array.filterMode = FilterMode.Bilinear;

        for (int i = 0; i < textures.Length; i++)
        {
            Texture2D tex = textures[i] as Texture2D;

            Graphics.CopyTexture(tex, 0, 0, array, i, 0);
        }

        AssetDatabase.CreateAsset(
            array,
            "Assets/AlexisTaTa/Materials/Ocean/TextureArray.asset"
        );

        AssetDatabase.SaveAssets();

        Debug.Log("Texture2DArray créé !");
    }
}
