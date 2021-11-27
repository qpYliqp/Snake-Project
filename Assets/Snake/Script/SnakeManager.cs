using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeManager : MonoBehaviour
{
    public GameObject snakeObject;
    private SnakeControler snake;

    [SerializeField]
    private float timerToTurn;
    private float valueOfTimerToTurn;

    public KeyCode keyUp;
    public KeyCode keyRight;
    public KeyCode keyDown;
    public KeyCode keyLeft;

    private KeyCode keyDebugAddPart = KeyCode.A;


    // Start is called before the first frame update
    void Start()
    {
        snake = snakeObject.GetComponent<SnakeControler>();
        valueOfTimerToTurn = 1.01f * (1 / (float)snake.GetSpeed());
        timerToTurn = valueOfTimerToTurn;
    }

    // Update is called once per frame
    void Update()
    {
        timerToTurn -= Time.deltaTime;

        if (timerToTurn <= 0.0f)
        {
            if (Input.GetKey(keyLeft))
            {
                snake.setDirection(directions.left);
                timerToTurn = valueOfTimerToTurn;
            }
            else if (Input.GetKey(keyRight))
            {
                snake.setDirection(directions.right);
                timerToTurn = valueOfTimerToTurn;
            }
            else if (Input.GetKey(keyDown))
            {
                snake.setDirection(directions.down);
                timerToTurn = valueOfTimerToTurn;
            }
            else if (Input.GetKey(keyUp))
            {
                snake.setDirection(directions.up);
                timerToTurn = valueOfTimerToTurn;
            }
        }

        if (Input.GetKeyDown(keyDebugAddPart))
        {
            snake.addPart();
        }
    }
}
