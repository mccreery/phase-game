using UnityEngine;

public class Reset : MonoBehaviour
{
    public GameObject player;
    public Vector2 resetPosition;

    public EnergyMeter meter;
    public new Camera camera;

    private void Start()
    {
        DoReset();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Reset"))
        {
            DoReset();
        }
    }

    private void DoReset()
    {
        player.transform.position = resetPosition;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        meter.Energy = meter.maxEnergy;
        camera.transform.position = player.transform.position;
    }
}
