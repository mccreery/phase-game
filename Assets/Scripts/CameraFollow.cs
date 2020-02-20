using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
	private Vector2 velocity;
	public Transform target;
	public float dampTime = 0.25f;

    private Vector2 smoothPosition;

    private new Camera camera;
    public Collider2D limits;

    private void Start()
    {
        camera = GetComponent<Camera>();
        smoothPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        smoothPosition = Vector2.SmoothDamp(smoothPosition, target.position, ref velocity, dampTime);
        Clamp();

        float pixelSize = GetPixelSize();
        transform.position = new Vector2(
            RoundToMultiple(smoothPosition.x, pixelSize),
            RoundToMultiple(smoothPosition.y, GetPixelSize()));
    }

    private void Clamp()
    {
        Vector2 cameraExtents = new Vector2(
            camera.orthographicSize * Screen.width / Screen.height,
            camera.orthographicSize);

        Bounds bounds = limits.bounds;
        bounds.extents -= (Vector3)cameraExtents;

        smoothPosition = new Vector2(
            Mathf.Clamp(smoothPosition.x, bounds.min.x, bounds.max.x),
            Mathf.Clamp(smoothPosition.y, bounds.min.y, bounds.max.y));
    }

    private float GetPixelSize()
    {
        float worldHeight = camera.orthographicSize * 2;
        float pixelHeight = camera.scaledPixelHeight;

        return worldHeight / pixelHeight;
    }

    private float RoundToMultiple(float x, float step)
    {
        return Mathf.Round(x / step) * step;
    }
}
