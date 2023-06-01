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
        Debug.Log("RandomizePosition");
        Bounds bounds = this.gridArea.bounds;

        bool positionIsFree = false;
        Vector3 newPosition = Vector3.zero;

        while (!positionIsFree)
        {
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = Random.Range(bounds.min.y, bounds.max.y);

            newPosition = new Vector3(Mathf.Round(x), Mathf.Round(y), 0);

            // The second parameter is the radius of the circle used for overlap testing. Adjust as needed.
            LayerMask mask = LayerMask.GetMask("Player", "Obstacle");
            if (Physics2D.OverlapCircle(newPosition, 0.5f, mask) == null){
                positionIsFree = true;
            }
        }

        // Only update the position when a free position is found
        this.transform.position = newPosition;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        { 
        RandomizePosition();
        }
    }
}