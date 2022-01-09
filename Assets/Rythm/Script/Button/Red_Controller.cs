using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red_Controller : MonoBehaviour
{
    private Animator animator;

    public KeyCode keyToPress;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Anim()
    {
        animator.Play("Red_Animation");
        animator.speed = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            animator.Play("Red_Animator");
            animator.speed = 3;
        }
        if (Input.GetKeyUp(keyToPress))
        {
            animator.Play("Red");

        }

    }
}
