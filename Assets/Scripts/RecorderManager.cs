using System.Collections;
using System.Collections.Generic;
using NatML.VideoKit;
using UnityEngine;

public class RecorderManager : MonoBehaviour
{
    [SerializeField] GameObject m_StartBtn;

    [SerializeField] GameObject m_StopBtn;

    VideoKitRecorder m_VideoKitRecorder;

    private void Start()
    {
        m_StartBtn.SetActive(true);
        m_StopBtn.SetActive(false);

        m_VideoKitRecorder = GetComponent<VideoKitRecorder>();
    }

    public void StartRecording()
    {
        Debug.Log($"VideoKit: {m_VideoKitRecorder.status}");
        if (m_VideoKitRecorder.status == VideoKitRecorder.Status.Idle)
        {
            m_StartBtn.SetActive(false);
            m_StopBtn.SetActive(true);
            Screen.autorotateToLandscapeLeft = false;
            m_VideoKitRecorder.StartRecording();
        }   
    }

    public void StopRecording()
    {
        if (m_VideoKitRecorder.status == VideoKitRecorder.Status.Recording)
        {
            m_StartBtn.SetActive(true);
            m_StopBtn.SetActive(false);
            Screen.autorotateToLandscapeLeft = true;
            m_VideoKitRecorder.StopRecording();
        }   
    }
}
