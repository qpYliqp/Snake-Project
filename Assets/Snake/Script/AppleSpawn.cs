using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleSpawn : MonoBehaviour
{
    [SerializeField]
    private bool isInContact;
    [SerializeField]
    private bool isOkayToBePickedUp;

    private void Start()
    {
        isOkayToBePickedUp = false;
        isInContact = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isOkayToBePickedUp)
        {
            isInContact = true;
            Debug.Log("Spawn must change");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isOkayToBePickedUp)
        {
            isInContact = false;
            Debug.Log("Spawn changed");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isOkayToBePickedUp)
        {
            isInContact = true;
            Debug.Log("Spawn must change");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!isOkayToBePickedUp)
        {
            isInContact = false;
            Debug.Log("Spawn changed");
        }
    }

    public bool IsInContact()
    {
        return isInContact;
    }

    public bool IsOkayToBePickedUp()
    {
        return isOkayToBePickedUp;
    }

    public void OkayToBePickedUp()
    {
        isOkayToBePickedUp = true;
    }
}
