using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watermelon;

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
    public CinemachineBrain CinemachineBrain { get { return cameraBrain; } }

    [Space]
    [SerializeField] CinemachineVirtualCamera[] virtualCameras;
    private static CameraFollow cameraController;
    private Transform player;
    private Action OnPlayGame;
    private bool isCameraMoving = false;

    private void Awake()
    {
        //CinemachineCore.CameraUpdatedEvent.AddListener(OnCameraUpdated);
        cameraController = this;
        EnableCamera(CameraVirtualType.Start);

    }

    public void Run(Transform player, Action OnPlayGame)
    {
        this.player = player;
        this.OnPlayGame = OnPlayGame;
        cameraController.cameraBrain.enabled = true;
        StartCoroutine(RunCamera());
    }

    private IEnumerator RunCamera()
    {
        yield return new WaitForSeconds(2.0f);
        EnableCamera(CameraVirtualType.ZoomOut);
        yield return new WaitForSeconds(2.0f);
        EnableCamera(CameraVirtualType.Main);
        virtualCameras[^1].Follow = player;
        virtualCameras[^1].LookAt = player;
        yield return new WaitForSeconds(2.0f);
        OnPlayGame?.Invoke();
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
        if (isCameraMoving)
            return;
        
        if (brain.ActiveVirtualCamera == virtualCameras[(int)CameraVirtualType.ZoomOut] && !brain.IsBlending)
        {
            EnableCamera(CameraVirtualType.Main);
        }
        if (brain.ActiveVirtualCamera == virtualCameras[(int)CameraVirtualType.Main] && !brain.IsBlending)
        {
            OnPlayGame?.Invoke();
            isCameraMoving = true;
        }
    }
}
