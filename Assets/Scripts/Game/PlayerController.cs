using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watermelon;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private FieldOfView fieldOfView;
    [SerializeField] private GameObject range;
    [SerializeField] private Transform containCat;
    //[SerializeField] private Rigidbody rb;
    //public Rigidbody GetRb() => rb;
    [SerializeField] private float rotationSpeed = 10.0f;

    private Stack<CatController> stackCats = new Stack<CatController>();

    private void Start()
    {
        range.SetActive(false);
        fieldOfView.Init(CathingCat);
    }

    private void CathingCat(CatController cat)
    {
        range.SetActive(fieldOfView.canSeePlayer);
        cat.Caught(ConquerCat);
    }
    
    private void ConquerCat(CatController cat)
    {
        cat.transform.SetParent(containCat);
        cat.transform.localPosition = Vector3.zero + new Vector3(0, (float)stackCats.Count * cat.HeightCat, 0);
        cat.transform.rotation = Quaternion.identity;
        stackCats.Push(cat);
    }    

    public void Moving(Vector3 dir, float speed)
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
