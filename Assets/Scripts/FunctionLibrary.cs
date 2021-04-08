using UnityEngine;
using static UnityEngine.Mathf;

public static class FunctionLibrary {
    public delegate Vector3 Function (float u, float v, float t);

    public enum FunctionName { Wave, MultiWave, Ripple };

    static Function[] functions = { Wave, MultiWave, Ripple };

    public static Function GetFunction (FunctionName name) {
        return functions[(int)name];
    }

    public static Vector3 Wave (float u, float v, float t) {
        Vector3 p;
        p.x = u;
        p.y = Sin(PI * (u + v + t));
        p.z = v;
        return p;
    }

    public static Vector3 MultiWave (float u, float v, float t) {
        Vector3 p;
        float y = Sin(PI * (u + t));
        y += 0.5f * Sin(2f * PI * (v + t));
        y += Sin(PI * (u + v + t * 0.25f));
        p.x = u;
        p.y = y * (1f / 2.5f);
        p.z = v;
        return p;
    }

    public static Vector3 Ripple (float u, float v, float t) {
        Vector3 p;
        float d = Sqrt(u * u + v * v);
        float y = Sin( PI * (4f * d - t));
        p.x = u;
        p.y = y / (1f + 10f * d);
        p.z = v;
        return p;
    }
}
