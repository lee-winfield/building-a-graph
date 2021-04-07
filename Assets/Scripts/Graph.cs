using UnityEngine;

public class Graph : MonoBehaviour {

    [SerializeField]
    Transform pointPrefab = default;

    [SerializeField, Range(10, 100)]
    int resolution = 10;

    [SerializeField]
    FunctionLibrary.FunctionName function = default;

    Transform[] points;

    void Awake () {
        float step = 2f / resolution;
        var scale = Vector3.one * step;
        var position = Vector3.zero;
        points = new Transform[resolution];
        for (int i = 0; i < points.Length; i++) {
            Transform point = Instantiate(pointPrefab);
            position.x = (i + 0.5f) * step - 1f;
            point.localPosition = position;
            point.localScale = scale;
            point.SetParent(transform, false);
            points[i] = point;
        }
    }

    void Update () {
        float time = Time.time;
        FunctionLibrary.Function f = FunctionLibrary.GetFunction(function);
        for (int i = 0; i < points.Length; i++) {
            Transform point = points[i];
            Vector3 position = point.localPosition;
            position.y = f(position.x, time);
            point.localPosition = position;
        }
    }
}
