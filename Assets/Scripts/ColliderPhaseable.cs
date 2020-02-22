using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderPhaseable: MonoBehaviour
{
    public new Collider2D collider2D;
    public EnergyMeter energyMeter;

    // Update is called once per frame
    void Update()
    {
        collider2D.enabled = !energyMeter.Phasing;
    }
}
