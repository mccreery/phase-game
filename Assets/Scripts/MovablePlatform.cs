using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePlatform : MonoBehaviour
{
    public bool moveVertical;
    public float maxPos;
    public float maxNeg;

    public float speed;
    private float moveSpeed;

    void Update()
    {
        if (moveVertical)
        {
            if (transform.position.y < maxNeg)
                moveSpeed = speed;
            if (transform.position.y > maxPos)
                moveSpeed = -speed;
                    
             transform.position = new Vector2(transform.position.x, transform.position.y + moveSpeed * Time.deltaTime);
        }
        else
        {
            if (transform.position.x < maxNeg)
                moveSpeed = speed;
            if (transform.position.x > maxPos)
                moveSpeed = -speed;
            
            transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
        }
    }
}
