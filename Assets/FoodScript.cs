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

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round( y), 0);
    }
}