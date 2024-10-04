using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Pill : MonoBehaviour
{
    [SerializeField] private TMP_Text speed;
    public void SetUp(float speedUp)
    {
        speed.text = "+ " + speedUp.ToString();
    }    
}
