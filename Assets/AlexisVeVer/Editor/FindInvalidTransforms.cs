using UnityEditor;
using UnityEngine;

namespace AlexisVeVer.Editor
{
    public static class FindInvalidTransforms
    {
        [MenuItem("Tools/Debug/Find Invalid Transforms")]
        public static void Find()
        {
            foreach (var t in Object.FindObjectsByType<Transform>(FindObjectsSortMode.None))
            {
                if (!IsValid(t.position) || !IsValid(t.rotation) || !IsValid(t.localScale))
                {
                    Debug.Log($"Invalid Transform: {t.name}", t);
                }
            }
        }

        static bool IsValid(Vector3 v) =>
            float.IsFinite(v.x) && float.IsFinite(v.y) && float.IsFinite(v.z) &&
            Mathf.Abs(v.x) < 100000 && Mathf.Abs(v.y) < 100000 && Mathf.Abs(v.z) < 100000;

        static bool IsValid(Quaternion q) =>
            float.IsFinite(q.x) && float.IsFinite(q.y) &&
            float.IsFinite(q.z) && float.IsFinite(q.w);
    }
}