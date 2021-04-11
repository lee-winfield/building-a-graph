using System;
using TMPro;
using UnityEngine;

public class FrameRateCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI display = default;

    private void Update()
    {
        var frameDuration = Time.unscaledDeltaTime;
        display.SetText("FPS\n{0:0}\n000\n000", 1f / frameDuration);
    }
}