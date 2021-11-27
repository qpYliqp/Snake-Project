using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RythmManager : MonoBehaviour
{
    
    public bool startPlaying;
    public Scroller Beat;

    

    public static RythmManager instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!startPlaying)
        {
            if (Input.anyKeyDown)
            {
                startPlaying = true;
                Beat.hasStarted = true;
                SoundManager.instance.PlayAMusic("redbone");


            }
        }


    }


}
