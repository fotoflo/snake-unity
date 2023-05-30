using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeScript : MonoBehaviour
{

    private Vector2 _direction = Vector2.up;
    private Vector3 unroundedPosition;

    private Vector3 originalPosition;

    private List<Transform> _segments;

    public Transform segmentPrefab;

    public float speed;

    public void Start()
    {
         Time.fixedDeltaTime = 0.02f;
         unroundedPosition = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            _direction = Vector2.left;
            MoveOne();
        } else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            _direction = Vector2.right;
            MoveOne();
        } else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            _direction = Vector2.up;
            MoveOne();
        } else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            _direction = Vector2.down;
            MoveOne();
        }

    }

    private void FixedUpdate()
    {
        MoveOne();
    }

    private void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);

        _segments.Add(segment);
        segment.position = _segments[_segments.Count - 1].position;
    }


    private void ResetGame()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Print the name of the collided object in the console
        if (other.tag == "Food")
        {
            Grow();
        }

        if (other.tag == "Obsticle")
        {
            ResetGame();
        }

    }

    private void MoveOne()
    {
        unroundedPosition += new Vector3(_direction.x * speed, _direction.y * speed, 0);
        transform.position = new Vector3(Mathf.Round(unroundedPosition.x), Mathf.Round(unroundedPosition.y), 0);
    }

}
