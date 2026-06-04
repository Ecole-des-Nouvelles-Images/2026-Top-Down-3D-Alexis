using UnityEditor;
using UnityEngine;

namespace AlexisVeVer.Editor
{
    public static class FindInvalidTransforms
    {
        [MenuItem("Tools/Debug/Deep Scan Scene For Invalid Values")]
        public static void Scan()
        {
            int count = 0;

            foreach (var go in Resources.FindObjectsOfTypeAll<GameObject>())
            {
                if (!go.scene.IsValid()) continue; // ignore assets/prefabs hors scène

                var t = go.transform;

                CheckVector(go, "position", t.position, ref count);
                CheckVector(go, "localPosition", t.localPosition, ref count);
                CheckVector(go, "localScale", t.localScale, ref count);
                CheckQuat(go, "rotation", t.rotation, ref count);
                CheckQuat(go, "localRotation", t.localRotation, ref count);

                foreach (var r in go.GetComponents<Renderer>())
                {
                    CheckVector(go, $"Renderer bounds center ({r.GetType().Name})", r.bounds.center, ref count);
                    CheckVector(go, $"Renderer bounds size ({r.GetType().Name})", r.bounds.size, ref count);
                }

                foreach (var ps in go.GetComponents<ParticleSystem>())
                {
                    var main = ps.main;
                    if (!float.IsFinite(main.startSize.constant))
                        Debug.LogError($"Invalid ParticleSystem startSize on {go.name}", go);
                }
            }

            Debug.Log($"Deep scan finished. Problems found: {count}");
        }

        static void CheckVector(GameObject go, string label, Vector3 v, ref int count)
        {
            if (!float.IsFinite(v.x) || !float.IsFinite(v.y) || !float.IsFinite(v.z) ||
                Mathf.Abs(v.x) > 1000000 || Mathf.Abs(v.y) > 1000000 || Mathf.Abs(v.z) > 1000000)
            {
                count++;
                Debug.Log(
                    $"INVALID VALUE #{count}\n" +
                    $"Object: {GetPath(go)}\n" +
                    $"Field: {label}\n" +
                    $"Value: {v}\n" +
                    $"Scene: {go.scene.name}",
                    go
                );
            }
        }

        static void CheckQuat(GameObject go, string label, Quaternion q, ref int count)
        {
            if (!float.IsFinite(q.x) || !float.IsFinite(q.y) ||
                !float.IsFinite(q.z) || !float.IsFinite(q.w))
            {
                count++;
                Debug.Log(
                    $"INVALID VALUE #{count}\n" +
                    $"Object: {GetPath(go)}\n" +
                    $"Field: {label}\n" +
                    $"Value: {q}\n" +
                    $"Scene: {go.scene.name}",
                    go
                );
            }
        }

        static string GetPath(GameObject go)
        {
            string path = go.name;
            var t = go.transform;
            while (t.parent != null)
            {
                t = t.parent;
                path = t.name + "/" + path;
            }

            return path;
        }
    }
}