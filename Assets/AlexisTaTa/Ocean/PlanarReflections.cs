using UnityEngine;

public class PlanarReflections : MonoBehaviour
{
    [SerializeField] private Camera ReflectionCamera;
    [SerializeField] private RenderTexture ReflectionRenderTexture;

    private void LateUpdate()
    {
        ReflectionCamera.transform.position = new Vector3(
            Camera.main.transform.position.x,
            2f * transform.position.y - Camera.main.transform.position.y,
            Camera.main.transform.position.z
        );

        ReflectionCamera.transform.rotation = Quaternion.Euler(
            -Camera.main.transform.eulerAngles.x,
            Camera.main.transform.eulerAngles.y,
            0f
        );

        Vector3 normal = Vector3.up;
        Vector3 pos = transform.position + Vector3.up * 1f;

        Matrix4x4 m = ReflectionCamera.worldToCameraMatrix;

        Vector3 cpos = m.MultiplyPoint(pos);
        Vector3 cnormal = m.MultiplyVector(normal).normalized;

        Vector4 clipPlane = new Vector4(
            cnormal.x,
            cnormal.y,
            cnormal.z,
            -Vector3.Dot(cpos, cnormal)
        );

        ReflectionCamera.projectionMatrix =
            Camera.main.CalculateObliqueMatrix(clipPlane);
    }
}