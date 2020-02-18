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
        transform.position = Vector2.SmoothDamp(transform.position, target.position, ref velocity, dampTime);
        Clamp();
    }

    private void Clamp()
    {
        Vector2 cameraExtents = new Vector2(
            camera.orthographicSize * Screen.width / Screen.height,
            camera.orthographicSize);

        Bounds bounds = limits.bounds;
        bounds.extents -= (Vector3)cameraExtents;

        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x, bounds.min.x, bounds.max.x),
            Mathf.Clamp(transform.position.y, bounds.min.y, bounds.max.y));
    }
}
