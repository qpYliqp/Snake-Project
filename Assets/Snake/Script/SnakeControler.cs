using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class SnakeControler : MonoBehaviour
{
    private List<Transform> AllParts = new List<Transform>();
    public Transform HeadPrefab;
    public Transform BodyPrefab;
    public Transform TailPrefab;
    public Vector2 direction = Vector2.right;
    public int initialSize = 3;

    public GameObject parentOfPart;

    public KeyCode keyUp;
    public KeyCode keyRight;
    public KeyCode keyDown;
    public KeyCode keyLeft;

    public KeyCode keyUpArrow;
    public KeyCode keyRightArrow;
    public KeyCode keyDownArrow;
    public KeyCode keyLeftArrow;

    public float speed = 10.0f;

    public float sizeOfParts = 3.0f;

    private Vector3 sauvPos;
    private Quaternion sauvRot;

    private float X;
    private float Y;

    public float timerBeforeTurn;

    public GameObject SnakeManager;

    private void Start()
    {
        sauvPos = transform.position;
        sauvRot = transform.rotation;
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
            // Only allow turning up or down while moving in the x-axis
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
            // Only allow turning left or right while moving in the y-axis
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
        // Set each segment's position to be the same as the one it follows. We
        // must do this in reverse order so the position is set to the previous
        // position, otherwise they will all be stacked on top of each other.
        for (int i = AllParts.Count - 1; i > 0; i--)
        {
            AllParts[i].position = AllParts[i - 1].position;
        }

        // Move the snake in the direction it is facing
        // Round the values to ensure it aligns to the grid
        // float x = Mathf.Round(this.transform.position.x) + this.direction.x / 100.0f * speed;
        // float y = Mathf.Round(this.transform.position.y) + this.direction.y / 100.0f * speed;
        float x = this.transform.position.x + this.direction.x / 100.0f * speed;
        float y = this.transform.position.y + this.direction.y / 100.0f * speed;
        //X = X + this.direction.x / 100.0f * speed;
        //Y = Y + this.direction.y / 100.0f * speed;

        this.transform.position = new Vector2(x, y);
    }

    public void Grow()
    {
        for (int i = 0; i < 5; i++)
        {
            Transform segInv = Instantiate(this.BodyPrefab);
            segInv.position = AllParts[AllParts.Count - 5].position;
            segInv.parent = parentOfPart.transform;
            segInv.gameObject.GetComponent<SpriteRenderer>().enabled = false;

            AllParts.Insert(AllParts.Count - 5, segInv);
        }

        Transform segment = Instantiate(this.BodyPrefab);
        segment.position = AllParts[AllParts.Count - 5].position;
        segment.parent = parentOfPart.transform;

        AllParts.Insert(AllParts.Count - 5, segment);
    }

    public void GrowUntagged()
    {
        for (int i = 0; i < 5; i++)
        {
            Transform segInv = Instantiate(this.BodyPrefab);
            segInv.position = AllParts[AllParts.Count - 5].position;
            segInv.parent = parentOfPart.transform;
            segInv.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            segInv.tag = "Untagged";

            AllParts.Insert(AllParts.Count - 5, segInv);
        }

        Transform segment = Instantiate(this.BodyPrefab);
        segment.position = AllParts[AllParts.Count - 5].position;
        segment.parent = parentOfPart.transform;
        segment.tag = "Untagged";

        AllParts.Insert(AllParts.Count - 5, segment);
    }

    public void ResetState()
    {
        SnakeManager.GetComponent<SnakeManager>().SupprAllApple();

        this.direction = Vector2.right;
        transform.position = sauvPos;
        transform.rotation = sauvRot;

        // Start at 1 to skip destroying the head
        for (int i = 1; i < AllParts.Count; i++)
        {
            Destroy(AllParts[i].gameObject);
        }

        // Clear the list but add back this as the head
        AllParts.Clear();
        AllParts.Add(this.transform);

        for (int i = 0; i < 4; i++)
        {
            Transform tailInv = Instantiate(this.TailPrefab);
            tailInv.position = this.transform.position;// + new Vector3(-direction.x * sizeOfParts, -direction.y * sizeOfParts, parentOfPart.transform.position.z);
            tailInv.parent = parentOfPart.transform;
            tailInv.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            tailInv.tag = "Untagged";

            AllParts.Add(tailInv);
        }

        Transform tail = Instantiate(this.TailPrefab);
        tail.position = this.transform.position;// + new Vector3(-direction.x * sizeOfParts, -direction.y * sizeOfParts, parentOfPart.transform.position.z);
        tail.parent = parentOfPart.transform;
        tail.tag = "Untagged";

        AllParts.Add(tail);

        GrowUntagged();
        GrowUntagged();

        SnakeManager.GetComponent<SnakeManager>().AddApple();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food-Snake")
        {
            Grow();
            Destroy(other.gameObject);
            SnakeManager.GetComponent<SnakeManager>().AddApple();
        }
        else if (other.tag == "Wall-Snake")
        {
            ResetState();
        }
    }
}
