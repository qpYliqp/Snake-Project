using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Note : MonoBehaviour
{
    public bool canBePressed;
    public KeyCode keyToPress;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(keyToPress)) {
            if (canBePressed) {
                gameObject.SetActive(false);

                if (Mathf.Abs(transform.position.y)>0.35)
                {
                    ScoreRythmeManager.instance.NoteHit("neutral");
                    Debug.Log("neutre");
                }
                else if(Mathf.Abs(transform.position.y)>0.25)
                {
                    ScoreRythmeManager.instance.NoteHit("good");
                    Debug.Log("good");

                }
                else if(Mathf.Abs(transform.position.y)>0.15)
                {
                    ScoreRythmeManager.instance.NoteHit("great");
                    Debug.Log("great");


                }
                else
                {
                    ScoreRythmeManager.instance.NoteHit("perfect");
                    Debug.Log("perfect");

                }
                //Destroy(this);
                Destroy(gameObject);
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Activator")
        {
            canBePressed = true;
        }

        if (collision.tag == "Missed")
        {
            //Il y a un trigger " Missed " juste en dessous du Trigger " Activator " qui siginifie qu'il est trop tard pour avoir des points
            canBePressed = false;
            gameObject.SetActive(false);
            Destroy(this);
            Destroy(gameObject);
            ScoreRythmeManager.instance.NoteMissed();

        }
    }


}
