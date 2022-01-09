using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yellow_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;

    public KeyCode keyToPress;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Anim()
    {
        animator.Play("Yellow_Animation");
        animator.speed = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            animator.Play("Yellow_Animator");
            animator.speed = 3;
        }
        if (Input.GetKeyUp(keyToPress))
        {
            animator.Play("Yellow");

        }

    }
}
