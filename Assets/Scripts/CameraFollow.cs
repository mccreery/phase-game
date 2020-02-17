using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
	private Vector2 velocity;
	public Transform target;
	public float dampTime = 0.25f;

    private new Camera camera;
    public Collider2D limits;

    private void Start()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
		Vector2 delta = target.position - camera.ViewportToWorldPoint(new Vector2(0.5f, 0.4f));
		Vector2 destination = (Vector2)transform.position + delta;
        Vector2 position = Vector2.SmoothDamp(transform.position, destination, ref velocity, dampTime);

        Vector2 cameraExtents = new Vector2(
            camera.orthographicSize * Screen.width / Screen.height,
            camera.orthographicSize);

        Bounds bounds = limits.bounds;
        bounds.extents -= (Vector3)cameraExtents;

        position.x = Mathf.Clamp(position.x, bounds.min.x, bounds.max.x);
        position.y = Mathf.Clamp(position.y, bounds.min.y, bounds.max.y);
        transform.position = position;
    }
}
