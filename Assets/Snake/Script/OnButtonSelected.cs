using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnButtonSelected : MonoBehaviour, ISelectHandler
{
    public GameObject gameManager;
    public int gamemodeID;

    public void OnSelect(BaseEventData eventData)
    {
        gameManager.GetComponent<SnakeManager>().OverlayGamemode(gamemodeID);
        //throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<SnakeManager>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
