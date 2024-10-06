using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private Action OnFinish;
    public void Init(Action OnFinish)
    {
        this.OnFinish = OnFinish;
    }    

    private void OnTriggerEnter(Collider other)
    {
        if(other != null)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                OnFinish?.Invoke();
            }    
        }    
    }
}
