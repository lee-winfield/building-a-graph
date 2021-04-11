using UnityEngine;

public class Graph : MonoBehaviour {

    [SerializeField] private Transform pointPrefab = default;

    [SerializeField, Range(10, 100)] private int resolution = 10;

    [SerializeField] private FunctionLibrary.FunctionName function = default;

    [SerializeField, Min(0f)] private float functionDuration = 1f;

    private enum TransitionMode { Cycle, Random }

    [SerializeField] private TransitionMode transitionMode = TransitionMode.Cycle;

    private Transform[] _points;
    private float _duration;

    private void Awake () {
        var step = 2f / resolution;
        var scale = Vector3.one * step;
        _points = new Transform[resolution * resolution];
        for (var i = 0; i < _points.Length; i++) {
            var point = Instantiate(pointPrefab, transform, false);
            point.localScale = scale;
            _points[i] = point;
        }
    }

    private void Update ()
    {
        _duration += Time.deltaTime;
        if (_duration >= functionDuration)
        {
            _duration -= functionDuration;
            PickNextFunction();
        }
        UpdateFunction();
    }

    private void PickNextFunction()
    {
        function = transitionMode == TransitionMode.Cycle
            ? FunctionLibrary.GetNextFunctionName(function)
            : FunctionLibrary.GetRandomFunctionName(function);
    }

    private void UpdateFunction()
    {
        var f = FunctionLibrary.GetFunction(function);
        var time = Time.time;
        var step = 2f / resolution;
        for (int i = 0, x = 0, z = 0; i < _points.Length; i++, x++)
        {
            if (x == resolution)
            {
                x = 0;
                z++;
            }

            var point = _points[i];
            var position = point.localPosition;
            var u = (x + 0.5f) * step - 1f;
            var v = (z + 0.5f) * step - 1f;
            point.localPosition = f(u, v, time);
        }
    }
}
