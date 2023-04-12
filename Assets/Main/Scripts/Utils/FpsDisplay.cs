using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FpsDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _fpsText;
    [SerializeField] private float _hudRefreshRate = 0.5f;

    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _hudRefreshRate)
        {
            _timer = 0f;
            _fpsText.text = "FPS: " + (1f / Time.unscaledDeltaTime).ToString("0");
        }
    }
}
