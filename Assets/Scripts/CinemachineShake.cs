using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance { get; private set; }

    private CinemachineVirtualCamera _vcam;
    private float _shakeTimer;

    private void Awake()
    {
        Instance = this;
        _vcam = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin mcPerlin = _vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        mcPerlin.m_AmplitudeGain = intensity;
        _shakeTimer = time;
    }


    private void Update()
    {
        _shakeTimer -= Time.deltaTime;

        if (_shakeTimer < 0)
        {
            CinemachineBasicMultiChannelPerlin mcPerlin = _vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            mcPerlin.m_AmplitudeGain = 0;
        }
    }
}
