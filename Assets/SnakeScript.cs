using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeScript : MonoBehaviour
{

    private Vector2 _direction = Vector2.up;
    private Vector2 input;

    private Vector3 unroundedPosition;
    private Vector3 originalPosition;

    private List<Transform> _segments;

    public Transform segmentPrefab;

    public float speed;

    public int intialSize = 4;

    public void Start()
    {
         Time.fixedDeltaTime = 0.1f;
         unroundedPosition = transform.position;
         _segments = new List<Transform>();
         ResetState();
    }

    private void Update()
    {
        // Only allow turning up or down while moving in the x-axis
        if (_direction.x != 0f)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
                input = Vector2.up;
            } else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
                input = Vector2.down;
            }
        }
        // Only allow turning left or right while moving in the y-axis
        else if (_direction.y != 0f)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
                input = Vector2.right;
            } else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
                input = Vector2.left;
            }
        }
    }

    private void FixedUpdate()
    {
        if (input != Vector2.zero) {
            _direction = input;
        }
    
        for( int i = _segments.Count - 1; i > 0;  i--){
            _segments[i].position = _segments[i - 1].position;
        }

        MoveOne();
    }

    private void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);

        segment.position = _segments[_segments.Count - 1].position;
        _segments.Add(segment);
    }


    private void ResetState()
    {
        Debug.Log("ResetState");

        for(int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }
        _segments.Clear();

        _segments.Add(this.transform);

        for ( int i = 1; i < intialSize; i++)
        {
            _segments.Add(Instantiate(this.segmentPrefab));
        }

        this.transform.position = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Print the name of the collided object in the console'

        Debug.Log("OnTriggerEnter2D: " + other.name);

        if (other.tag == "Food")
        {
            Grow();
        } else if (other.tag == "Obstacle")
        {
            ResetState();
        }

    }

    private void MoveOne()
    {
        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y,
            0.0f
        );
    }

}
