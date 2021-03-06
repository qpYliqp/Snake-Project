using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{

    public Animator animator;

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
          animator.Play("color");
        }
        if (Input.GetKeyUp(keyToPress))
        {
           animator.Play("color_animation");
        }
        
    }
}
