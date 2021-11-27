using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{


    private static Scenes instance = null;

    public static Scenes Instance
    {
        get
        {
            return instance;
        }
    }


    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(gameObject);
    }

    public void Load (int index)
    {
        SceneManager.LoadScene(index);

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
