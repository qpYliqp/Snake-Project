using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    public bool hasStarted;


    // Update is called once per frame
    void Update()
    {
        if(hasStarted)
        {
            //D�placement des notes
            transform.position -= new Vector3(0f, RythmManager.instance.f_tempo  * Time.deltaTime, 0f);
        }
    }
}
