using System.Collections;
using Cinemachine;
using UnityEngine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance { get; private set; }
    private CinemachineVirtualCamera vcam;

    private float shakeTimer;
    private float shaleTimerTotal;
    private float startingInstensity;

    private void Awake()
    {
        Instance = this;
        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0)
            {
                // time over!
                CinemachineBasicMultiChannelPerlin channelPerlin = 
                    vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

               channelPerlin.m_AmplitudeGain = 
                    Mathf.Lerp(startingInstensity, 0f, 1 - (shakeTimer / shaleTimerTotal));
            }
        }
    }

    public void ShakeCamera(float intensity, float time)
    {
        
        CinemachineBasicMultiChannelPerlin channelPerlin = 
            vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        channelPerlin.m_AmplitudeGain = intensity;
        startingInstensity = intensity;
        shakeTimer = time;
        shaleTimerTotal = time;
    }
}
