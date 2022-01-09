using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Green_Controller : MonoBehaviour
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
        animator.Play("Green_Animation");
        animator.speed = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            animator.Play("Green_Animator");
            animator.speed = 3;
        }
        if (Input.GetKeyUp(keyToPress))
        {
            animator.Play("Green");

        }

    }
}
