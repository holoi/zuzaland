using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloInteractive.XR.HoloKit;

public class HoloKitManager : MonoBehaviour
{
    [SerializeField] GameObject m_Canvas;

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

    private void Update()
    {
        if (m_ScreenOrientation == Screen.orientation)
            return;

        m_ScreenOrientation = Screen.orientation;
        if (m_ScreenOrientation == ScreenOrientation.Portrait)
        {
            m_Canvas.SetActive(true);
            m_HoloKitCamera.RenderMode = HoloInteractive.XR.HoloKit.RenderMode.Mono;
        }
        else if (m_ScreenOrientation == ScreenOrientation.LandscapeLeft)
        {
            m_Canvas.SetActive(false);
            m_HoloKitCamera.RenderMode = HoloInteractive.XR.HoloKit.RenderMode.Stereo;
        }
    }
}
