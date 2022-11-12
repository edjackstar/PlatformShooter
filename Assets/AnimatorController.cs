using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            animator.SetBool("IsRunning",true);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            animator.SetBool("IsRunning",false);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            animator.SetBool("Back", true);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            animator.SetBool("Back", false);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.SetBool("Left", true);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            animator.SetBool("Left", false);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            animator.SetBool("Right", true);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            animator.SetBool("Right", false);
        }
    }
}
