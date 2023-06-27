using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightFlickering : MonoBehaviour
{
    [SerializeField]
    private float _lightNoiseSpeed = 5f;
    [Range(-5, 0)]
    [SerializeField]
    private float _minLightIntensity = -0.3f;
    [Range(0, 5)]
    [SerializeField]
    private float _maxLightIntensity = 0.3f;
    private float _startIntensity;
    private Light _light;
    private float _randomOffset = 0f;

    void Start()
    {
        _light = GetComponent<Light>();
        _startIntensity = _light.intensity;
        _randomOffset = Random.value;
    }

    void Update()
    {
        float noise = Mathf.PerlinNoise(_randomOffset + Time.timeSinceLevelLoad * _lightNoiseSpeed, 0f);
        float currentIntensity = _startIntensity + Mathf.Lerp(_minLightIntensity, _maxLightIntensity, noise);
        _light.intensity = _startIntensity + Random.Range(_minLightIntensity, _maxLightIntensity);
    }
}
