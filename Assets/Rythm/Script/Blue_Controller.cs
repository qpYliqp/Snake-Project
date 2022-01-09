using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue_Controller : MonoBehaviour
{

    private Animator animator;

    public KeyCode keyToPress;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            animator.Play("Blue_Animator");
        }
        if (Input.GetKeyUp(keyToPress))
        {
            animator.Play("Blue");

        }

    }
}
