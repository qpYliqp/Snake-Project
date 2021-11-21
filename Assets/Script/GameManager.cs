using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    
    public bool startPlaying;
    public Scroller Beat;

    public GameObject snakeObject;
    private SnakeControler snake;

    [SerializeField]
    private float timerToTurn;
    private float valueOfTimerToTurn;

    void snake_controller()
    {
        timerToTurn -= Time.deltaTime;

        if (timerToTurn <= 0.0f)
        {
            if (Input.GetKey("left"))
            {
                snake.setDirection(directions.left);
                timerToTurn = valueOfTimerToTurn;
            }
            else if (Input.GetKey("right"))
            {
                snake.setDirection(directions.right);
                timerToTurn = valueOfTimerToTurn;
            }
            else if (Input.GetKey("down"))
            {
                snake.setDirection(directions.down);
                timerToTurn = valueOfTimerToTurn;
            }
            else if (Input.GetKey("up"))
            {
                snake.setDirection(directions.up);
                timerToTurn = valueOfTimerToTurn;
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            snake.addPart();
        }
    
    }

    public static GameManager instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!startPlaying)
        {
            if (Input.anyKeyDown)
            {
                startPlaying = true;
                Beat.hasStarted = true;
                SoundManager.instance.PlayAMusic("redbone");


            }
        }


        snake_controller();
    }


}
