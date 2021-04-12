using System;
using UnityEngine;

public class GPUGraph : MonoBehaviour
{
    
    [SerializeField, Range(10, 200)] private int resolution = 10;

    [SerializeField] private FunctionLibrary.FunctionName function;

    [SerializeField, Min(0f)] private float functionDuration = 1f, transitionDuration = 1f;

    private enum TransitionMode { Cycle, Random }

    [SerializeField] private TransitionMode transitionMode = TransitionMode.Cycle;

    private float _duration;

    private bool _transitioning;
    private FunctionLibrary.FunctionName _transitionFunction;

    private ComputeBuffer positionsBuffer;

    private void OnEnable()
    {
        positionsBuffer = new ComputeBuffer(resolution * resolution, 3 * 4);
    }

    private void OnDisable()
    {
        positionsBuffer.Release();
        positionsBuffer = null;
    }

    private void Update ()
    {
        _duration += Time.deltaTime;
        
        if (_transitioning)
        {
            if (_duration >= transitionDuration)
            {
                _duration -= transitionDuration;
                _transitioning = false;                
            }
        }
        else if (_duration >= functionDuration)
        {
            _duration -= functionDuration;
            _transitioning = true;
            _transitionFunction = function;
            PickNextFunction();
        }
        
    }

    private void PickNextFunction()
    {
        function = transitionMode == TransitionMode.Cycle
            ? FunctionLibrary.GetNextFunctionName(function)
            : FunctionLibrary.GetRandomFunctionName(function);
    }
}
