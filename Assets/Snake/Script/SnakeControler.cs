using System.Collections.Generic;
using UnityEngine;

public class SnakeControler : MonoBehaviour
{
    [Header("Prefabs")]
    public Transform BodyPrefab;
    public Transform TailPrefab;

    [Header("Snake")]
    [SerializeField]
    private List<Transform> AllParts = new List<Transform>();
    [SerializeField]
    private Vector2 direction = Vector2.right;
    [SerializeField]
    private GameObject parentOfPart;
    public float speed = 10.0f;
    [SerializeField]
    private float timerBeforeTurn;

    [Header("Touches / Inputs")]
    public KeyCode keyUp;
    public KeyCode keyRight;
    public KeyCode keyDown;
    public KeyCode keyLeft;

    public KeyCode keyUpArrow;
    public KeyCode keyRightArrow;
    public KeyCode keyDownArrow;
    public KeyCode keyLeftArrow;

    [Header("Game Manager")]
    [SerializeField]
    private GameObject SnakeManager;

    [SerializeField]
    private int nombreDuShlagh = 4;

    private void Start()
    {
        SnakeManager = FindObjectOfType<SnakeManager>().gameObject;
        parentOfPart = GameObject.Find("SnakeHolder");

        ResetState();

        ResetTimerBeforeTurn();
    }

    private void ResetTimerBeforeTurn()
    {
        timerBeforeTurn = speed * 0.04f;
    }

    private void Update()
    {
        if (timerBeforeTurn <= 0)
        {
            if (this.direction.x != 0f)
            {
                if (Input.GetKeyDown(keyUp) || Input.GetKeyDown(keyUpArrow))
                {
                    switch (direction.x)
                    {
                        case 1:
                            transform.Rotate(new Vector3(0, 0, 90));
                            break;
                        case -1:
                            transform.Rotate(new Vector3(0, 0, 270));
                            break;
                    }
                    this.direction = Vector2.up;
                    ResetTimerBeforeTurn();
                }
                else if (Input.GetKeyDown(keyDown) || Input.GetKeyDown(keyDownArrow))
                {
                    switch (direction.x)
                    {
                        case 1:
                            transform.Rotate(new Vector3(0, 0, 270));
                            break;
                        case -1:
                            transform.Rotate(new Vector3(0, 0, 90));
                            break;
                    }
                    this.direction = Vector2.down;
                    ResetTimerBeforeTurn();
                }
            }
            else if (this.direction.y != 0f)
            {
                if (Input.GetKeyDown(keyRight) || Input.GetKeyDown(keyRightArrow))
                {
                    switch (direction.y)
                    {
                        case 1:
                            transform.Rotate(new Vector3(0, 0, -90));
                            break;
                        case -1:
                            transform.Rotate(new Vector3(0, 0, -270));
                            break;
                    }
                    this.direction = Vector2.right;
                    ResetTimerBeforeTurn();
                }
                else if (Input.GetKeyDown(keyLeft) || Input.GetKeyDown(keyLeftArrow))
                {
                    switch (direction.y)
                    {
                        case 1:
                            transform.Rotate(new Vector3(0, 0, -270));
                            break;
                        case -1:
                            transform.Rotate(new Vector3(0, 0, -90));
                            break;
                    }
                    this.direction = Vector2.left;
                    ResetTimerBeforeTurn();
                }
            }
        }
        else
        {
            timerBeforeTurn -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Grow();
        }
    }

    private void FixedUpdate()
    {
        for (int i = AllParts.Count - 1; i > 0; i--)
        {
            AllParts[i].position = AllParts[i - 1].position;
        }
        float x = this.transform.position.x + this.direction.x / 100.0f * speed;
        float y = this.transform.position.y + this.direction.y / 100.0f * speed;

        this.transform.position = new Vector2(x, y);
    }

    public void Grow()
    {
        for (int i = 0; i < nombreDuShlagh; i++)
        {
            Transform segInv = Instantiate(this.BodyPrefab);
            segInv.position = AllParts[AllParts.Count - nombreDuShlagh].position;
            segInv.parent = parentOfPart.transform;
            segInv.gameObject.GetComponent<SpriteRenderer>().enabled = false;

            AllParts.Insert(AllParts.Count - nombreDuShlagh, segInv);
        }

        Transform segment = Instantiate(this.BodyPrefab);
        segment.position = AllParts[AllParts.Count - nombreDuShlagh].position;
        segment.parent = parentOfPart.transform;

        AllParts.Insert(AllParts.Count - nombreDuShlagh, segment);
    }

    public void GrowUntagged()
    {
        for (int i = 0; i < nombreDuShlagh; i++)
        {
            Transform segInv = Instantiate(this.BodyPrefab);
            segInv.position = AllParts[AllParts.Count - nombreDuShlagh].position;
            segInv.parent = parentOfPart.transform;
            segInv.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            segInv.tag = "Untagged";

            AllParts.Insert(AllParts.Count - nombreDuShlagh, segInv);
        }

        Transform segment = Instantiate(this.BodyPrefab);
        segment.position = AllParts[AllParts.Count - nombreDuShlagh].position;
        segment.parent = parentOfPart.transform;
        segment.tag = "Untagged";

        AllParts.Insert(AllParts.Count - nombreDuShlagh, segment);
    }

    public void ResetState()
    {

        this.direction = Vector2.right;

        for (int i = 1; i < AllParts.Count; i++)
        {
            Destroy(AllParts[i].gameObject);
        }

        AllParts.Clear();
        AllParts.Add(this.transform);

        for (int i = 0; i < nombreDuShlagh - 1; i++)
        {
            Transform tailInv = Instantiate(this.TailPrefab);
            tailInv.position = this.transform.position;
            tailInv.parent = parentOfPart.transform;
            tailInv.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            tailInv.tag = "Untagged";

            AllParts.Add(tailInv);
        }

        Transform tail = Instantiate(this.TailPrefab);
        tail.position = this.transform.position;
        tail.parent = parentOfPart.transform;
        tail.tag = "Untagged";

        AllParts.Add(tail);

        GrowUntagged();
        GrowUntagged();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Food-Snake":
                Grow();
                SnakeManager.GetComponent<SnakeManager>().SnakeEatApple(other.transform);
                break;
            case "Wall-Snake":
                if (!SnakeManager.GetComponent<SnakeManager>().isHarmlessWallsGM())
                    Death();
                else
                {
                    if (direction == Vector2.right || direction == Vector2.left)
                        this.transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
                    else if (direction == Vector2.up || direction == Vector2.down)
                        this.transform.position = new Vector3(transform.position.x, -transform.position.y, transform.position.z);
                }
                break;
            case "Snake-Snake":
                if (!SnakeManager.GetComponent<SnakeManager>().isHarmlessSnakeGM())
                    Death();
                break;
        }
    }

    public void Death()
    {
        for (int i = 0; i < parentOfPart.transform.childCount; i++)
        {
            Destroy(parentOfPart.transform.GetChild(i).gameObject);
        }
        SnakeManager.GetComponent<SnakeManager>().Defeat();
    }
}
