using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeControler : MonoBehaviour
{
    [SerializeField]
    private float x;
    [SerializeField]
    private float y;

    [SerializeField]
    private int speed;


    [SerializeField]
    private List<GameObject> snake;

    public GameObject snakePartPrefab;

    // Start is called before the first frame update
    void Start()
    {
        GameObject snakeHead = Instantiate(snakePartPrefab, new Vector2(x, y), Quaternion.identity);
        snakeHead.name = "Head";
        snakeHead.transform.parent = gameObject.transform;
        snakeHead.GetComponent<PartControler>().SetVariables(directions.right, x, y);
        snakeHead.GetComponent<PartControler>().SetSpeed(speed);

        snake = new List<GameObject>();
        snake.Add(snakeHead);
    }

    // Update is called once per frame
    void Update()
    {
        x = snake[snake.Count - 1].GetComponent<PartControler>().GetX();
        y = snake[snake.Count - 1].GetComponent<PartControler>().GetY();
        switch (snake[snake.Count - 1].GetComponent<PartControler>().GetDirection())
        {
            case directions.up:
                y -= 1.0f;
                break;
            case directions.right:
                x -= 1.0f;
                break;
            case directions.down:
                y += 1.0f;
                break;
            case directions.left:
                x += 1.0f;
                break;
            default:
                break;
        }
    }

    public void setDirection(directions _newDir)
    {
        if (snake[0].GetComponent<PartControler>().GetDirection() - 2 != _newDir && snake[0].GetComponent<PartControler>().GetDirection() + 2 != _newDir)
        {
            snake[0].GetComponent<PartControler>().SetDirection(_newDir);
            StartCoroutine(WaitAndTurn());
        }
    }

    public void addPart()
    {
        GameObject snakeHead = Instantiate(snakePartPrefab, new Vector2(x, y), Quaternion.identity);
        snakeHead.name = "BodyPart";
        snakeHead.transform.parent = gameObject.transform;
        snakeHead.GetComponent<PartControler>().SetVariables(snake[snake.Count - 1].GetComponent<PartControler>().GetDirection(), (float)x, (float)y);
        snakeHead.GetComponent<PartControler>().SetSpeed(speed);

        snake.Add(snakeHead);
    }

    private IEnumerator WaitAndTurn()
    {
        GameObject snakeTurn = Instantiate(snakePartPrefab, new Vector2(0, 0), Quaternion.identity);
        snakeTurn.name = "Turn";
        snakeTurn.transform.parent = gameObject.transform;
        snakeTurn.GetComponent<PartControler>().SetVariables(directions.up, snake[0].GetComponent<PartControler>().GetX(), snake[0].GetComponent<PartControler>().GetY());
        snakeTurn.GetComponent<PartControler>().SetSpeed(0);

        int i = 1;
        while (i < snake.Count)
        {
            yield return new WaitForSeconds(1.0f / (float)speed);
            directions dirPrec = snake[i - 1].GetComponent<PartControler>().GetDirection();
            snake[i].GetComponent<PartControler>().SetDirection(dirPrec);
            switch (dirPrec)
            {
                case directions.up:
                    snake[i].GetComponent<PartControler>().SetX(snake[i - 1].GetComponent<PartControler>().GetX());
                    break;
                case directions.down:
                    snake[i].GetComponent<PartControler>().SetX(snake[i - 1].GetComponent<PartControler>().GetX());
                    break;
                case directions.left:
                    snake[i].GetComponent<PartControler>().SetY(snake[i - 1].GetComponent<PartControler>().GetY());
                    break;
                case directions.right:
                    snake[i].GetComponent<PartControler>().SetY(snake[i - 1].GetComponent<PartControler>().GetY());
                    break;
            }
            i += 1;
        }

        Destroy(snakeTurn);
    }

    public int GetSpeed()
    {
        return speed;
    }
}

public enum directions
{
    up,
    right,
    down,
    left
}