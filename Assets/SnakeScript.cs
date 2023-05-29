using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeScript : MonoBehaviour
{

    private Vector2 _direction = Vector2.up;
    private Vector3 unroundedPosition;
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

    void OnCollisionEnter(Collision collision)
    {
        // Print the name of the collided object in the console
        Debug.Log("Collided with " + collision.gameObject.name);
    }

    private void MoveOne()
    {
        unroundedPosition += new Vector3(_direction.x * speed, _direction.y * speed, 0);
        transform.position = new Vector3(Mathf.Round(unroundedPosition.x), Mathf.Round(unroundedPosition.y), 0);
    }

}
