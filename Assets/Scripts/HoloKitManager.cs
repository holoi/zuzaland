using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloKit;

public class HoloKitManager : MonoBehaviour
{
    [SerializeField] GameObject m_Canvas;

    [SerializeField] GameObject m_StereoCanvas;

    HoloKitCamera m_HoloKitCamera;

    ScreenOrientation m_ScreenOrientation = ScreenOrientation.Portrait;

    private void Start()
    {
        m_HoloKitCamera = FindObjectOfType<HoloKitCamera>(true);
        if (m_HoloKitCamera)
        {
            Debug.LogWarning("Cannot find HoloKitCamera component in the scene");
        }
    }

    public void SwitchScreenRenderMode()
    {
        if (m_HoloKitCamera.RenderMode == HoloKitRenderMode.Mono)
        {
            m_HoloKitCamera.RenderMode = HoloKitRenderMode.Stereo;
            m_Canvas.SetActive(false);
            m_StereoCanvas.SetActive(true);
        }
        else
        {
            m_HoloKitCamera.RenderMode = HoloKitRenderMode.Mono;
            m_Canvas.SetActive(true);
            m_StereoCanvas.SetActive(false);
        }
    }

    private void Update()
    {
        if (m_HoloKitCamera.RenderMode == HoloKitRenderMode.Mono && Screen.orientation != ScreenOrientation.Portrait)
        {
            Screen.orientation = ScreenOrientation.Portrait;
        }
    }

    //private void Update()
    //{
    //    if (m_ScreenOrientation == Screen.orientation)
    //        return;

    //    m_ScreenOrientation = Screen.orientation;
    //    if (m_ScreenOrientation == ScreenOrientation.Portrait)
    //    {
    //        m_Canvas.SetActive(true);
    //        m_HoloKitCamera.ScreenRenderMode = ScreenRenderMode.Mono;
    //    }
    //    else if (m_ScreenOrientation == ScreenOrientation.LandscapeLeft)
    //    {
    //        m_Canvas.SetActive(false);
    //        m_HoloKitCamera.ScreenRenderMode = ScreenRenderMode.Stereo;
    //    }
    //}
}
