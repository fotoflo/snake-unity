using UnityEngine;

public class FoodScript : MonoBehaviour
{

    public BoxCollider2D gridArea;
    // Start is called before the first frame update
    void Start()
    {
        RandomizePosition();
    }

    // Update is called once per frame
    private void RandomizePosition()
    {
        Bounds bounds = this.gridArea.bounds;

        bool positionIsFree = false;
        Vector3 newPosition = Vector3.zero;

        float width = bounds.size.x;
        float height = bounds.size.y;
        int totalSpaces = Mathf.RoundToInt(width) * Mathf.RoundToInt(height);
        
        int maxAttempts = totalSpaces;

        while (!positionIsFree && maxAttempts > 0)
        {
            newPosition = randomRange();

            // The second parameter is the radius of the circle used for overlap testing. Adjust as needed.
            LayerMask mask = LayerMask.GetMask("Player", "Obstacle");
            if (checkPositionForOverlap(newPosition)){
                positionIsFree = true;
            } else {
                maxAttempts--;
            }
        }

        // Only update the position when a free position is found
        this.transform.position = newPosition;
    }

    private Vector3 randomRange(){
        Bounds bounds = this.gridArea.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector3(Mathf.Round(x), Mathf.Round(y), 0);
    }

    private bool checkPositionForOverlap(Vector3 position)
    {
        // The second parameter is the radius of the circle used for overlap testing. Adjust as needed.
        LayerMask mask = LayerMask.GetMask("Player", "Obstacle");
        return Physics2D.OverlapCircle(position, 0.5f, mask) == null;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        { 
        RandomizePosition();
        }
    }
}