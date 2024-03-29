﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthManager healthManager = collision.gameObject.GetComponent<HealthManager>();

            if (!healthManager.Invulnerable)
            {
                healthManager.Health--;
            }
        }
    }
}
