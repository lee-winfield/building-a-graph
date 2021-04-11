using System;
using TMPro;
using UnityEngine;

public class FrameRateCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI display = default;
    [SerializeField, Range(0.1f, 2f)] private float sampleDuration = 1f;
    
    private int _frames;
    private float _duration;
    private void Update()
    {
        var frameDuration = Time.unscaledDeltaTime;
        _frames++;
        _duration += frameDuration;
        if (_duration >= sampleDuration)
        {
            display.SetText("FPS\n{0:0}\n000\n000", _frames / _duration);
            _frames = 0;
            _duration = 0f;
        }
    }
}