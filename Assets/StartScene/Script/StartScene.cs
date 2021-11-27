using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StartScene : MonoBehaviour
{
    // Start is called before the first frame update

    private static StartScene instance;

    [Header("Canva")]
    [SerializeField] private GameObject go_ScreenPlay;
    [SerializeField] private GameObject go_ScreenSelect;
    [SerializeField] private int i_ScreenIndex;

    [Header("Fonctionnalités")]
    [SerializeField] private Button btn_Play;
    [SerializeField] private Button btn_Snake;
    [SerializeField] private Button btn_Rythm;
    [SerializeField] private GameObject go_Snake;
    private int test = 1;

    [Header("Public")]
    public bool b_Snake;
    public bool b_Rythm;

    private void Awake()
    {
        instance = this;
        b_Snake = false;
        b_Rythm = false;
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

    public void Play()
    {
        Debug.Log("la fonction ptn");
        i_ScreenIndex = 2;

    }

    public void Return()
    {
        i_ScreenIndex--;
    }
    void Start()
    {
        btn_Play.Select();
    }

    // Update is called once per frame
    void Update()
    {
        ScreenDisplay();

        if(Input.GetKey(KeyCode.A) && i_ScreenIndex == 1)
        {
            Play();
        }
        

    }

}
