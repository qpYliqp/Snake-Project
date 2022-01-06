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
    public int initialSize = 20;

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

    private void Start()
    {
        ResetState();
    }

    private void Update()
    {
        // Only allow turning up or down while moving in the x-axis
        if (this.direction.x != 0f)
        {
            if (Input.GetKeyDown(keyUp) || Input.GetKeyDown(keyUpArrow))
            {
                this.direction = Vector2.up;
            }
            else if (Input.GetKeyDown(keyDown) || Input.GetKeyDown(keyDownArrow))
            {
                this.direction = Vector2.down;
            }
        }
        // Only allow turning left or right while moving in the y-axis
        else if (this.direction.y != 0f)
        {
            if (Input.GetKeyDown(keyRight) || Input.GetKeyDown(keyRightArrow))
            {
                this.direction = Vector2.right;
            }
            else if (Input.GetKeyDown(keyLeft) || Input.GetKeyDown(keyLeftArrow))
            {
                this.direction = Vector2.left;
            }
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
            /*
            Vector2 position = AllParts[i - 1].position;
            if (AllParts[i - 1].rotation.z == 0)
                position.y -= this.BodyPrefab.gameObject.GetComponent<BoxCollider2D>().size.y;
            if (AllParts[i - 1].rotation.z == 90)
                position.x += this.BodyPrefab.gameObject.GetComponent<BoxCollider2D>().size.x;
            if (AllParts[i - 1].rotation.z == 180)
                position.y += this.BodyPrefab.gameObject.GetComponent<BoxCollider2D>().size.y;
            if (AllParts[i - 1].rotation.z == 270)
                position.x -= this.BodyPrefab.gameObject.GetComponent<BoxCollider2D>().size.x;
            */
            
            AllParts[i].position = AllParts[i - 1].position + new Vector3(10, 0, 0);
        }

        // Move the snake in the direction it is facing
        // Round the values to ensure it aligns to the grid
        // float x = Mathf.Round(this.transform.position.x) + this.direction.x / 100.0f * speed;
        // float y = Mathf.Round(this.transform.position.y) + this.direction.y / 100.0f * speed;
        float x = this.transform.position.x + this.direction.x / 100.0f * speed;
        float y = this.transform.position.y + this.direction.y / 100.0f * speed;

        this.transform.position = new Vector2(x, y);
    }

    public void Grow()
    {
        Transform segment = Instantiate(this.BodyPrefab);
        segment.position = AllParts[AllParts.Count - 1].position;
        segment.parent = parentOfPart.transform;

        AllParts.Add(segment);
    }

    public void ResetState()
    {
        this.direction = Vector2.right;
        this.transform.position = Vector3.zero;

        // Start at 1 to skip destroying the head
        for (int i = 1; i < AllParts.Count; i++)
        {
            Destroy(AllParts[i].gameObject);
        }

        // Clear the list but add back this as the head
        AllParts.Clear();
        AllParts.Add(this.transform);

        // -1 since the head is already in the list
        for (int i = 0; i < this.initialSize - 1; i++)
        {
            Grow();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            Grow();
        }
        else if (other.tag == "Obstacle")
        {
            ResetState();
        }
    }

}
