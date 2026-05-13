using UnityEngine;
using UnityEditor;
public class Tex3DCreator : MonoBehaviour
{
    [MenuItem("Tools/Create Texture3D")]
    static void CreateTexture3D()
    {
        Texture2D[] slices = Selection.GetFiltered<Texture2D>(SelectionMode.Assets);

        int width = slices[0].width;
        int height = slices[0].height;
        int depth = slices.Length;

        Texture3D texture3D = new Texture3D(width, height, depth, TextureFormat.RFloat, false);

        Color[] colors = new Color[width * height * depth];

        for (int z = 0; z < depth; z++)
        {
            Color[] sliceColors = slices[z].GetPixels();

            for (int i = 0; i < sliceColors.Length; i++)
            {
                colors[z * width * height + i] = sliceColors[i];
            }
        }

        texture3D.SetPixels(colors);
        texture3D.Apply();

        AssetDatabase.CreateAsset(texture3D, "Assets/AlexisTaTa/Materials/Ocean/Texture3D.asset");
    }
}
