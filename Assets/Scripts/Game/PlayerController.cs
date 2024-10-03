using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watermelon;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    //[SerializeField] private Rigidbody rb;
    //public Rigidbody GetRb() => rb;

    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float rotationSpeed = 10.0f;
    public float GetSpeed() => speed;

    public void Moving(Vector3 dir)
    {
        if (dir != Vector3.zero)
        {
            transform.position += dir * Time.deltaTime * speed;
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }
}
