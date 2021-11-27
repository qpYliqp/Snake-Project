using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartControler : MonoBehaviour
{
    private directions dir;
    private float x;
    private float y;

    private int speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (dir)
        {
            case directions.up:
                y += Time.deltaTime * speed;
                break;
            case directions.right:
                x += Time.deltaTime * speed;
                break;
            case directions.down:
                y -= Time.deltaTime * speed;
                break;
            case directions.left:
                x -= Time.deltaTime * speed;
                break;
            default:
                break;
        }

        transform.position = new Vector2((float)x, (float)y);
    }

    public void SetVariables(directions _dir, float _x, float _y)
    {
        dir = _dir;
        x = _x;
        y = _y;
        transform.position = new Vector2(x, y);
    }

    public void SetPosition(float _x, float _y)
    {
        x = _x;
        y = _y;
    }

    public void SetX(float _x)
    {
        x = _x;
    }

    public void SetY(float _y)
    {
        y = _y;
    }

    public directions GetDirection()
    {
        return dir;
    }

    public void SetDirection(directions _dir)
    {
        dir = _dir;
    }

    public float GetX()
    {
        return x;
    }

    public float GetY()
    {
        return y;
    }

    public void SetSpeed(int _speed)
    {
        speed = _speed;
    }
}
