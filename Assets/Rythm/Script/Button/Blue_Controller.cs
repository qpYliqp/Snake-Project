using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue_Controller : MonoBehaviour
{

    private Animator animator;

    public KeyCode keyToPress;
    public KeyCode keyToPress2;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Anim()
    {
        animator.Play("Blue_Animation");
        animator.speed = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPress) || Input.GetKeyDown(keyToPress2))
        {
            animator.Play("Blue_Animator");
            animator.speed = 3;
        }
        if (Input.GetKeyUp(keyToPress) || Input.GetKeyUp(keyToPress2))
        {
            animator.Play("Blue");

        }

    }
}
