using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderPhaseable: MonoBehaviour
{
    private new Collider2D collider2D;
    public EnergyMeter energyMeter;

    // Start is called before the first frame update
    void Start()
    {
        collider2D = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        collider2D.enabled = !energyMeter.Phasing;
    }
}
