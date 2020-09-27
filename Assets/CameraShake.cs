using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class CameraShake : MonoBehaviour
{
    // Shake Vars
    public float shakeDuration = 0.3f;
    public float shakeAmplitude = 1.2f;
    public float shakeFrequency = 2.0f;

    private float shakeElapsedTime = 0f;

    // Cinemachine Shake
    public CinemachineVirtualCamera virtualCam;
    private CinemachineBasicMultiChannelPerlin virtualCamNoise;

    void Start()
    {
        // Get virtual camera noise profile
        if (virtualCam != null)
        {
            virtualCamNoise = virtualCam.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        }
    }

    void Update()
    {
        //TODO: replace with personal trigger
        if (Input.GetKey(KeyCode.E))
        {
            shakeElapsedTime = shakeDuration;
        }

        // If camera shake effect is still playing
        if (shakeElapsedTime > 0)
        {
            // Set cinemachine camera noise parameters
            virtualCamNoise.m_AmplitudeGain = shakeAmplitude;
            virtualCamNoise.m_FrequencyGain = shakeFrequency;

            // Update shake timer
            shakeElapsedTime -= Time.deltaTime;
        }
        else
        {
            // If camera shake effect is over, reset variables
            virtualCamNoise.m_AmplitudeGain = 0f;
            shakeElapsedTime = 0f;
        }

    }
}
