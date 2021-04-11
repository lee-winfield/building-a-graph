using System;
using TMPro;
using UnityEngine;

public class FrameRateCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI display = default;
    [SerializeField, Range(0.1f, 2f)] private float sampleDuration = 1f;
    
    private int _frames;
    private float _duration, _bestDuration = float.MaxValue, _worstDuration;
    private void Update()
    {
        var frameDuration = Time.unscaledDeltaTime;
        _frames++;
        _duration += frameDuration;
        if (frameDuration < _bestDuration)
        {
            _bestDuration = frameDuration;
        }

        if (frameDuration > _worstDuration)
        {
            _worstDuration = frameDuration;
        }

        if (_duration < sampleDuration) return;
        display.SetText("FPS\n{0:0}\n{0:0}\n{0:0}", _frames/_duration, 1f/_bestDuration, 1f/_worstDuration);
        _frames = 0;
        _duration = 0f;
    }
}