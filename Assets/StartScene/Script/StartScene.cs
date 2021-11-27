using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class StartScene : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Canva")]
    [SerializeField] private GameObject go_ScreenPlay;
    [SerializeField] private GameObject go_ScreenSelect;
    [SerializeField] private int i_ScreenIndex;

    [Header("Fonctionnalités")]
    [SerializeField] private Button btn_Play;

    private void Awake()
    {
        i_ScreenIndex = 1;
        go_ScreenPlay.SetActive(true);
        go_ScreenSelect.SetActive(false);
    }

    void ScreenDisplay()
    {
        switch (i_ScreenIndex)
        {
            default: break;
            case 1:
                go_ScreenPlay.SetActive(true);
                go_ScreenSelect.SetActive(false);
                break;
            case 2:
                go_ScreenPlay.SetActive(false);
                go_ScreenSelect.SetActive(true);
                break;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
