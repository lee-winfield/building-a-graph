using UnityEngine;
using static UnityEngine.Mathf;

public static class FunctionLibrary {
    public delegate Vector3 Function (float u, float v, float t);

    public enum FunctionName { Wave, MultiWave, Ripple, UVSphere, RotatingTwistingSphere };

    static Function[] functions = { Wave, MultiWave, Ripple, UVSphere, RotatingTwistingSphere };

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

    public static Vector3 UVSphere (float u, float v, float t) {
        float r = Cos(0.5f * PI * v);
        Vector3 p;
        p.x = r * Sin(PI * (u + t));
        p.y = Sin(PI * 0.5f * v);
        p.z = r * Cos(PI * (u + t));
        return p;
    }

    public static Vector3 RotatingTwistingSphere (float u, float v, float t) {
        float r = 0.9f + 0.1f * Sin(PI * (6f * u + 4f * v + t));
        float s = r * Cos(0.5f * PI * v);
        Vector3 p;
        p.x = s * Sin(PI * u);
        p.y = r * Sin(PI * 0.5f * v);
        p.z = s * Cos(PI * u);
        return p;
    }
}
