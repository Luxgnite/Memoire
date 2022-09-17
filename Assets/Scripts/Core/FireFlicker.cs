using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
[ExecuteAlways]
public class FireFlicker : MonoBehaviour
{
    private float fadeIn, fadeOut;
    private Light myLight;
    private Vector3 lightPos;

    public float positionScrollSpeed = 2f;
    public float intensityScrollSpeed = 1f;
    public float intensityBase = 1f;
    public float positionJumpScale = 1f;
    public float intensityJumpScale = 0.1f;
    public float fadeInTime = 5f;
    public float fadeOutTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        myLight = GetComponent<Light>();
        lightPos = myLight.transform.localPosition;
        fadeIn = 0;
        fadeOut = 0;
    }

    private Vector3 PositionDelta(float positionScrollSpeed, float scale)
    {
        float x = Mathf.PerlinNoise(Time.time * positionScrollSpeed, 1f + Time.time * positionScrollSpeed) - 0.5f;
        float y = Mathf.PerlinNoise(2f + Time.time * positionScrollSpeed, 3f + Time.time * positionScrollSpeed) - 0.5f;
        float z = Mathf.PerlinNoise(4f + Time.time * positionScrollSpeed, 5f + Time.time * positionScrollSpeed) - 0.5f;
        return new Vector3(x, y, z) * scale;
    }

    private float NewIntensity(float intensityBase, float intensityJumpScale, float intensityScrollSpeed)
    {
        return (intensityBase + (intensityJumpScale * Mathf.PerlinNoise(Time.time * intensityScrollSpeed, 1f + Time.time * intensityScrollSpeed)));
    }


    // Update is called once per frame
    private void Update()
    {
        myLight.intensity = NewIntensity(intensityBase, intensityJumpScale, intensityScrollSpeed);
        transform.localPosition = lightPos + PositionDelta(positionScrollSpeed, positionJumpScale);
    }
}
