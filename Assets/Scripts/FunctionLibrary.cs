using UnityEngine;
using static UnityEngine.Mathf;

public static class FunctionLibrary {
    public delegate Vector3 Function (float u, float v, float t);

    public enum FunctionName { Wave, MultiWave, Ripple, UVSphere, RotatingTwistingSphere, Torus };

    private static readonly Function[] Functions = { Wave, MultiWave, Ripple, UVSphere, RotatingTwistingSphere, Torus };

    public static Function GetFunction (FunctionName name) {
        return Functions[(int)name];
    }

    public static FunctionName GetNextFunctionName(FunctionName name)
    {
        return ((int) name < Functions.Length - 1) ? name + 1 : 0;
    }

    public static FunctionName GetRandomFunctionName(FunctionName name)
    {
        var choice = (FunctionName) Random.Range(1, Functions.Length);
        return choice == name ? 0 : choice;
    }

    public static Vector3 Morph(float u, float v, float t, Function from, Function to, float progress)
    {
        return Vector3.Lerp(from(u, v, t), to(u, v, t), progress);
    }

    private static Vector3 Wave (float u, float v, float t) {
        Vector3 p;
        p.x = u;
        p.y = Sin(PI * (u + v + t));
        p.z = v;
        return p;
    }

    private static Vector3 MultiWave (float u, float v, float t) {
        Vector3 p;
        var y = Sin(PI * (u + t));
        y += 0.5f * Sin(2f * PI * (v + t));
        y += Sin(PI * (u + v + t * 0.25f));
        p.x = u;
        p.y = y * (1f / 2.5f);
        p.z = v;
        return p;
    }

    private static Vector3 Ripple (float u, float v, float t) {
        Vector3 p;
        var d = Sqrt(u * u + v * v);
        var y = Sin( PI * (4f * d - t));
        p.x = u;
        p.y = y / (1f + 10f * d);
        p.z = v;
        return p;
    }

    private static Vector3 UVSphere (float u, float v, float t) {
        var r = Cos(0.5f * PI * v);
        Vector3 p;
        p.x = r * Sin(PI * (u + t));
        p.y = Sin(PI * 0.5f * v);
        p.z = r * Cos(PI * (u + t));
        return p;
    }

    private static Vector3 RotatingTwistingSphere (float u, float v, float t) {
        var r = 0.9f + 0.1f * Sin(PI * (6f * u + 4f * v + t));
        var s = r * Cos(0.5f * PI * v);
        Vector3 p;
        p.x = s * Sin(PI * u);
        p.y = r * Sin(PI * 0.5f * v);
        p.z = s * Cos(PI * u);
        return p;
    }

    private static Vector3 Torus (float u, float v, float t) {
        var r1 = 0.7f + 0.1f * Sin(PI * (6f * u + 0.5f * t));
        var r2 = 0.15f + 0.05f * Sin(PI * (8f * u + 4f * v + 2f * t));
        var s = r1 + r2 * Cos(PI * v);
        Vector3 p;
        p.x = s * Sin(PI * u);
        p.y = r2 * Sin(PI * v);
        p.z = s * Cos(PI * u);
        return p;
    }
}
