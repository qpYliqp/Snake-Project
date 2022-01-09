using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    public bool hasStarted;
    private bool b_end;
    private bool b_rewind;
    public Note[] tab_note;


    // Update is called once per frame
    void Update()
    {
        ScrollerPosition();
    }

    void ScrollerPosition()
    {
        if (hasStarted)
        {
            //Déplacement du scroller qui contient les notes
            transform.position -= new Vector3(0f, RythmManager.instance.f_tempo * Time.deltaTime, 0f);
        }
        if (b_rewind)

        {   //Déplacement du scroller pour revenir à la position 0 au cas où le joueur veut rejouer la même musique ( "Retry" )
            transform.position += new Vector3(0f, RythmManager.instance.f_tempo * Time.deltaTime * 7, 0f);
        }
        if (transform.position.y >= 0)
        {
            //Lorsque que le scroller revient à 0, on arrête le déplacement du scroller.
            b_rewind = false;

            if (b_end)
            {
                //Puis on relance la musique
                transform.position = new Vector2(0, 0);
                endtrue();
                b_end = false;
                RythmManager.instance.wait();
            }
        }
    }
    public void Rewind()
    {
        Debug.Log("Rewind");
        foreach (Note no in tab_note)
        {
            no.b_end = true;
            no.SetVisible();
            Debug.Log("visible");
        }
        Debug.Log("Fin visible");
        b_rewind = true;
        b_end = true;
    }

    void endtrue()
    {
        foreach (Note no in tab_note)
        {
            no.b_end = false; ;
        }

    }
}
