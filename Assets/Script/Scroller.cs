using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    public float Tempo;
    public bool hasStarted;
    void Start()
    {
        Tempo = Tempo / 60;
    }

    // Update is called once per frame
    void Update()
    {
        if(hasStarted)
        {
            //Déplacement des notes
            transform.position -= new Vector3(0f, Tempo * Time.deltaTime, 0f);
        }
    }
}
