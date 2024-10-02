using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watermelon;

public class RRController : MonoBehaviour
{
    [SerializeField] private GameUIController gameUIController;
    [SerializeField] private CameraFollow cameraFollow;
    [SerializeField] private Transform player;

    private void Start()
    {
        cameraFollow.EnableCamera(CameraVirtualType.Main);
    }

    private void Update()
    {
        Debug.Log(Joystick.Instance.FormatInput);
    }
}
