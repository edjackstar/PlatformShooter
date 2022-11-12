using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimControl : MonoBehaviour
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
            animator.SetBool("IsRunning", true);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            animator.SetBool("IsRunning", false);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            animator.SetBool("Back", true);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            animator.SetBool("Back", false);
        }
    }
}
