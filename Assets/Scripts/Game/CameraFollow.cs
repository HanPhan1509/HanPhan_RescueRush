using Cinemachine;
using System;
using UnityEngine;

public enum CameraVirtualType
{
    Start = 0,
    ZoomOut = 1,
    Main = 2,
}

public class CameraFollow : MonoBehaviour
{
    private const int ACTIVE_CAMERA_PRIORITY = 100;
    private const int UNACTIVE_CAMERA_PRIORITY = 0;

    [SerializeField] private CinemachineBrain cameraBrain;
    [SerializeField] private CameraVirtualType firstCamera;
    public CinemachineBrain CinemachineBrain { get { return cameraBrain; } }

    [Space]
    [SerializeField] CinemachineVirtualCamera[] virtualCameras;
    private Action OnPlayGame;

    private void Awake()
    {
        CinemachineCore.CameraUpdatedEvent.AddListener(OnCameraUpdated);
        EnableCamera(firstCamera);
    }

    public void Run(Action OnPlayGame)
    {
        this.OnPlayGame = OnPlayGame;
        EnableCamera(CameraVirtualType.ZoomOut);
    }

    public void EnableCamera(CameraVirtualType cameraType)
    {
        for (int i = 0; i < virtualCameras.Length; i++)
        {
            virtualCameras[i].Priority = ((i == (int)cameraType) ? ACTIVE_CAMERA_PRIORITY : UNACTIVE_CAMERA_PRIORITY);
        }
    }

    void OnCameraUpdated(CinemachineBrain brain)
    {
        Debug.Log(brain.ActiveVirtualCamera);
        if (brain.ActiveVirtualCamera == virtualCameras[(int)CameraVirtualType.ZoomOut] && !brain.IsBlending)
        {
            EnableCamera(CameraVirtualType.Main);
        }
        if (brain.ActiveVirtualCamera == virtualCameras[(int)CameraVirtualType.Main] && !brain.IsBlending)
        {
            OnPlayGame?.Invoke();
        }
    }
}
