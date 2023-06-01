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
        int maxAttempts = getTotalSpaces();
        bool positionIsFree = false;       
        Vector3 newPosition = Vector3.zero;

        while (!positionIsFree && maxAttempts > 0)
        {
            newPosition = randomRangeInBounds();
            if (checkPositionForOverlap(newPosition)){
                positionIsFree = true;
            } else {
                maxAttempts--;
            }
        }

        this.transform.position = newPosition;
    }

    private int getTotalSpaces(){
        Bounds bounds = this.gridArea.bounds;
        float width = bounds.size.x;
        float height = bounds.size.y;
        return Mathf.RoundToInt(width) * Mathf.RoundToInt(height);
    }

    private Vector3 randomRangeInBounds(){
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