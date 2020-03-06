using UnityEngine;

public class PlayerMovement : CharacterMovement
{
    protected override float InputX => Input.GetAxisRaw("Horizontal");

    protected override bool IsJumpRequested(bool held)
    {
        return held ? Input.GetButton("Jump") : Input.GetButtonDown("Jump");
    }

    private EnergyMeter meter;

    public bool exit = false;

    protected override void Awake()
    {
        base.Awake();
        meter = GetComponent<EnergyMeter>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Battery"))
        {
            meter.Energy += 50;
            Destroy(coll.gameObject);
        }
        else if (coll.gameObject.CompareTag("Key"))
        {
            exit = true;
            Destroy(coll.gameObject);
        }
        else if (coll.gameObject.CompareTag("Takeable"))
        {
            Destroy(coll.gameObject);
        }
    }
}
