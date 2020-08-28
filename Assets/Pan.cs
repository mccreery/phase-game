using UnityEngine;

public class Pan : MonoBehaviour
{
    public Vector2 startPosition;
    public Vector2 endPosition;
    public float speed = 1;

    private void Start()
    {
        transform.position = startPosition;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, endPosition, speed * Time.deltaTime);

        if (transform.position.Equals(endPosition))
        {
            transform.position = startPosition;
        }
    }
}
