using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RRModel : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float speedUp = 1.0f;
    public float Speed { get => speed; set => speed = value; }
    public float SpeedUp { get => speedUp; set => speedUp = value; }

    [SerializeField] private float timeStart = 3.0f;
    public float TimeStart { get { return timeStart; } }

    [Header("TSUNAMI CONFIG")]
    [SerializeField] private float[] tsunamiTime = new float[] { 15.0f, 40.0f, 25.0f };
    [SerializeField] private float[] lineStreet  = new float[] { 150.0f, 400.0f, 1000.0f };
    public float[] TsunamiTime { get { return tsunamiTime; } }
    public float[] LineStreet { get { return lineStreet; } }


    [Header("STREET")]
    [SerializeField] private float phase_1 = 400.0f;
    [SerializeField] private float phase_2 = 1000.0f;
    public float Phase_1 { get => phase_1; }
    public float Phase_2 { get => phase_2; }

}
