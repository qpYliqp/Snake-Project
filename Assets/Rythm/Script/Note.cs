using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Note : MonoBehaviour
{
    public bool canBePressed;
    public bool b_end;
    public KeyCode keyToPress;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Détecte la précision d'appui sur la touche
        if (Input.GetKeyDown(keyToPress)) {
            if (canBePressed) {
                gameObject.SetActive(false);

                if (Mathf.Abs(transform.position.y)>0.35)
                {
                    RythmManager.instance.NoteHit("neutral");
                    Debug.Log("neutre");
                }
                else if(Mathf.Abs(transform.position.y)>0.25)
                {
                    RythmManager.instance.NoteHit("good");
                    Debug.Log("good");

                }
                else if(Mathf.Abs(transform.position.y)>0.15)
                {
                    RythmManager.instance.NoteHit("great");
                    Debug.Log("great");


                }
                else
                {
                    RythmManager.instance.NoteHit("perfect");
                    Debug.Log("perfect");

                }   
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!b_end)
        {
            if (collision.tag == "Activator")
            {
                canBePressed = true;
            }

            if (collision.tag == "Missed")
            {
                //Il y a un trigger " Missed " juste en dessous du Trigger " Activator " qui siginifie qu'il est trop tard pour avoir des points
                canBePressed = false;
                gameObject.SetActive(false);
                gameObject.SetActive(false);
                RythmManager.instance.NoteMissed();

            }
        }
    }

    public void SetVisible()
    {

        gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Activator")
        {
            canBePressed = false;

        }
    }


}
