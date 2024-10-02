using Cinemachine;
using UnityEngine;

public enum CameraVirtualType
{
    Start = 0,
    Main = 1,
}

public class CameraFollow : MonoBehaviour
{
    private const int ACTIVE_CAMERA_PRIORITY = 100;
    private const int UNACTIVE_CAMERA_PRIORITY = 0;

    [SerializeField] private CinemachineBrain cameraBrain;
    [SerializeField] private CameraVirtualType firstCamera;

    [Space]
    [SerializeField] CinemachineVirtualCamera virtualCameraStart;
    [SerializeField] CinemachineVirtualCamera virtualCameraMain;

    private void Awake()
    {
        EnableCamera(firstCamera);
    }

    public void EnableCamera(CameraVirtualType cameraType)
    {
        switch (cameraType)
        {
            case CameraVirtualType.Start:
                virtualCameraStart.Priority = ACTIVE_CAMERA_PRIORITY;
                virtualCameraMain.Priority = UNACTIVE_CAMERA_PRIORITY;
                break;
            case CameraVirtualType.Main:
                virtualCameraStart.Priority = UNACTIVE_CAMERA_PRIORITY;
                virtualCameraMain.Priority = ACTIVE_CAMERA_PRIORITY;
                break;
        }

    }
}
